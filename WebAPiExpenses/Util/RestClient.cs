using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

using System.Security.Cryptography.X509Certificates;
using System.Net;


namespace WebApiExpenses.Util
{
    public class RestClient
    {
        private readonly string _baseAddress;
        private string _bearerToken;
        private string _authorizationToken;
        private X509Certificate2 certificado;

        public string ArquivoLog { get; set; }

        public SecurityProtocolType SecurityProtocolType
        {
            set
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = value;
            }
        }



        public RestClient()
        {

        }

        public RestClient(X509Certificate2 certificado)
        {
            this.certificado = certificado;
        }



        public RestClient(string baseAddress) : this()
        {
            if (baseAddress != null && !baseAddress.ToLower().StartsWith("https"))
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            }
            this._baseAddress = baseAddress;
        }

        public RestClient(string baseAddress, string bearerToken) : this()
        {
            if (baseAddress != null && !baseAddress.ToLower().StartsWith("https"))
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            }
            this._baseAddress = baseAddress;
            this._bearerToken = bearerToken;
        }

        public RestClient(string baseAddress, string authorizationToken, string nada) : this()
        {
            if (baseAddress != null && !baseAddress.ToLower().StartsWith("https"))
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            }
            this._baseAddress = baseAddress;
            this._authorizationToken = authorizationToken;
        }

        public void AtualizarBearerToken(string bearerToken)
        {
            this._bearerToken = bearerToken;
        }

        public void AtualizarAuthorizationToken(string authorizationToken)
        {
            this._authorizationToken = authorizationToken;
        }



        /// <summary>
        /// Used to setup the base address, that we want json, and authentication headers for the request
        /// </summary>
        /// <param name="client">The HttpClient we are configuring</param>
        /// <param name="methodName">GET, POST, PUT or DELETE. Aim to prevent hacker changing the 
        /// method from say GET to DELETE</param>
        /// <param name="apiUrl">The end bit of the url we use to call the web api method</param>
        /// <param name="content">For posts and puts the object we are including</param>
        private HttpClient setupClient(Dictionary<string, string> headers = null)
        {

            var httpClientHandler = new HttpClientHandler();

            HttpClient client;

            client = new HttpClient(httpClientHandler);

            //client.BaseAddress = new Uri(_baseAddress);            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            int timeOutSegundos = 180;

            //client.Timeout = new TimeSpan(0, 0, 30);
            client.Timeout = TimeSpan.FromSeconds(timeOutSegundos);

            if (!string.IsNullOrWhiteSpace(this._bearerToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this._bearerToken);
            }

            if (!string.IsNullOrWhiteSpace(this._authorizationToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this._authorizationToken);
                //client.DefaultRequestHeaders.Add("Authorization", this._authorizationToken);
            }

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                }

            }

            return client;
        }

        /// <summary>
        /// For getting a single item from a web api uaing GET
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api get method, e.g. "products/1" to get a product with an id of 1</param>
        /// <returns>The item requested</returns>
        public T GetSingleItemRequest<T>(string apiUrl) where T : class
        {
            T result = null;
            if (!string.IsNullOrEmpty(_baseAddress))
            {
                apiUrl = _baseAddress.Trim() + apiUrl;
            }


            using (var client = setupClient())
            {

                var response = client.GetAsync(apiUrl).Result;

                HandleError(response);

                result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);

            }

            return result;
        }

        /// <summary>
        /// For getting multiple (or all) items from a web api using GET
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api get method, e.g. "products?page=1" to get page 1 of the products</param>
        /// <returns>The items requested</returns>
        public T[] GetMultipleItemsRequest<T>(string apiUrl) where T : class
        {
            T[] result = null;


            if (!string.IsNullOrEmpty(_baseAddress))
            {
                apiUrl = _baseAddress.Trim() + apiUrl;
            }

            using (var client = setupClient())
            {

                var response = client.GetAsync(apiUrl).Result;

                HandleError(response);

                result = JsonConvert.DeserializeObject<T[]>(response.Content.ReadAsStringAsync().Result);

            }

            return result;
        }

        /// <summary>
        /// For creating a new item over a web api using POST
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api post method, e.g. "products" to add products</param>
        /// <param name="postObject">The object to be created</param>
        /// <returns>The item created</returns>
        public T PostRequest<T>(string apiUrl, object postObject, Dictionary<string, string> headers = null) where T : class
        {
            string jsonRetornoOriginal;
            return PostRequest<T>(apiUrl, postObject, headers, out jsonRetornoOriginal);
        }

        public T PostRequest<T>(string apiUrl, object postObject, Dictionary<string, string> headers, out string jsonRetornoOriginal) where T : class
        {
            return PostJson<T>(apiUrl, Newtonsoft.Json.JsonConvert.SerializeObject(postObject), headers, out jsonRetornoOriginal);
        }

        public T PostJson<T>(string apiUrl, string json, Dictionary<string, string> headers = null) where T : class
        {
            string jsonRetornoOriginal;
            return PostJson<T>(apiUrl, json, headers, out jsonRetornoOriginal);
        }

        public T PostJson<T>(string apiUrl, string json, Dictionary<string, string> headers, out string jsonRetornoOriginal) where T : class
        {
            T result = null;
            jsonRetornoOriginal = "";
            if (!string.IsNullOrEmpty(_baseAddress))
            {
                apiUrl = _baseAddress.Trim() + apiUrl;
            }

            using (var client = setupClient(headers))
            {
                StringContent content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                System.Threading.Tasks.Task<HttpResponseMessage> callTask = System.Threading.Tasks.Task.Run(() => client.PostAsync(apiUrl, content));
                // Wait for it to finish
                callTask.Wait();

                var response = callTask.Result;

                //var response = client.PostAsync(apiUrl, content).Result;

                HandleError(response);

                System.Threading.Tasks.Task<string> callTask2 = System.Threading.Tasks.Task.Run(() => response.Content.ReadAsStringAsync());
                callTask2.Wait();

                try
                {
                    // Tenta fazer o parse e dpois converter identado, para ficar mais fácio de vizualizar.
                    jsonRetornoOriginal = Newtonsoft.Json.Linq.JValue.Parse(callTask2.Result).ToString(Formatting.Indented);
                }
                catch
                {
                    // se de algum erro pega o conteudo original mesmo
                    jsonRetornoOriginal = callTask2.Result;
                }

                result = JsonConvert.DeserializeObject<T>(callTask2.Result);

            }

            return result;
        }

        public T RequestToken<T>(string apiUrl, string usuario, string senha) where T : class
        {
            T result = null;

            if (!string.IsNullOrEmpty(_baseAddress))
            {
                apiUrl = _baseAddress.Trim() + apiUrl;
            }

            using (var client = setupClient())
            {
                var tokenModel = new Dictionary<string, string>
                    {
                        {"grant_type", "password"},
                        {"username", usuario},
                        {"password", senha},
                    };

                var response = client.PostAsync(apiUrl, new FormUrlEncodedContent(tokenModel)).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string erro = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Usuário ou senha Inválidos");
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }

                result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);              
            }

            return result;
        }

        /// <summary>
        /// For updating an existing item over a web api using PUT
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api put method, e.g. "products/3" to update product with id of 3</param>
        /// <param name="putObject">The object to be edited</param>
        public void PutRequest(string apiUrl, object putObject)
        {
            if (!string.IsNullOrEmpty(_baseAddress))
            {
                apiUrl = _baseAddress.Trim() + apiUrl;
            }

            using (var client = setupClient())
            {
                StringContent content = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(putObject), Encoding.UTF8, "application/json");

                var response = client.PutAsync(apiUrl, content).Result;

                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// For deleting an existing item over a web api using DELETE
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api delete method, e.g. "products/3" to delete product with id of 3</param>
        public void DeleteRequest(string apiUrl)
        {
            if (!string.IsNullOrEmpty(_baseAddress))
            {
                apiUrl = _baseAddress.Trim() + apiUrl;
            }

            using (var client = setupClient())
            {
                var response = client.DeleteAsync(apiUrl).Result;

                response.EnsureSuccessStatusCode();
            }
        }


        private void HandleError(HttpResponseMessage response)
        {           
            if (!response.IsSuccessStatusCode)
            {
                var resultContent = response.Content.ReadAsStringAsync().Result;
                var responseMessage = "Response status code does not indicate success: " + (int)response.StatusCode + " (" + response.StatusCode + " ). ";
                throw new HttpRequestException(responseMessage + Environment.NewLine + resultContent);
            }
        }
    }
}
