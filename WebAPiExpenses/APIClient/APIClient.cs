
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiExpenses.Model;
using WebApiExpenses.Util;
using WebAPiExpenses.Util;
using WebAPiExpenses;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace WebApiExpenses.ApiClient
{
    public static class APIClient
    {
        static string _urlBase = "";

          static RestClient client = new RestClient(_urlBase);

        internal  static void SettingUrlBase(string urlBase){
                _urlBase = urlBase;
                client = new RestClient(_urlBase);
        }

      

      
        internal static List<ExpenseAPI> getTotalExpensesPerMonth()
        {
            string caminhoUrl = $"api/3/action/datastore_search_sql?sql=SELECT%20ano_movimentacao,%20mes_movimentacao,%20valor_pago%20from%20%22d4d8a7f0-d4be-4397-b950-f0c991438111%22";
            RootAPI root;    
            List<ExpenseAPI> lst = new List<ExpenseAPI>();            
            try
            {
                root = client.GetSingleItemRequest<RootAPI>(caminhoUrl);
                if(root != null && root.result != null && root.result.records != null)
                {
                    lst = new List<ExpenseAPI>(root.result.records);
                }                    
            }
            catch (Exception ex1)
            {
                throw ex1;
            }            
            return lst;
        }


        internal static List<ExpenseAPI> getTotalExpensesPerCategory()
        {   

            string caminhoUrl = $"api/3/action/datastore_search_sql?sql=SELECT%20categoria_economica_codigo,%20categoria_economica_nome,%20valor_pago%20from%20%22d4d8a7f0-d4be-4397-b950-f0c991438111%22";
            RootAPI root;    
            List<ExpenseAPI> lst = new List<ExpenseAPI>();            
            try
            {
                root = client.GetSingleItemRequest<RootAPI>(caminhoUrl);
                if(root != null && root.result != null && root.result.records != null)
                {
                    lst = new List<ExpenseAPI>(root.result.records);
                }                    
            }
            catch (Exception ex1)
            {
                throw ex1;
            }            
            return lst;

           
        }

        internal static List<ExpenseAPI> getPaymentPerSource()
        {
            string caminhoUrl = $"api/3/action/datastore_search_sql?sql=SELECT%20fonte_recurso_codigo,%20fonte_recurso_nome,%20valor_pago%20from%20%22d4d8a7f0-d4be-4397-b950-f0c991438111%22";
            RootAPI root;    
            List<ExpenseAPI> lst = new List<ExpenseAPI>();            
            try
            {
                root = client.GetSingleItemRequest<RootAPI>(caminhoUrl);
                if(root != null && root.result != null && root.result.records != null)
                {
                    lst = new List<ExpenseAPI>(root.result.records);
                }                    
            }
            catch (Exception ex1)
            {
                throw ex1;
            }            
            return lst;

            
        }



    }
}
