
using Newtonsoft.Json;

namespace WebApiExpenses.Model
{
    public class Expense
    {
        public int movimentYear { get; set; }

       
        public int movimentMonth { get; set; }

       
        public string movimentMonthDescription { get; set; }        

      
        public string categoryCode { get; set; }

        
        public string categoryName { get; set; }

     
        public string sourcePaymentCode { get; set; }

     
        public string sourcePaymentoName { get; set; }

      
        public double amountPaid { get; set; }
    }

    public class ExpenseAPI
    {
        [JsonProperty("ano_movimentacao")]
        public string movimentYear { get; set; }

        [JsonProperty("mes_movimentacao")]
        public string movimentMonth { get; set; }

        [JsonProperty("mes_descricao_movimentacao")]
        public string movimentMonthDescription { get; set; }        

        [JsonProperty("categoria_economica_codigo")]
        public string categoryCode { get; set; }

        [JsonProperty("categoria_economica_nome")]
        public string categoryName { get; set; }

        [JsonProperty("fonte_recurso_codigo")]
        public string sourcePaymentCode { get; set; }

        [JsonProperty("fonte_recurso_nome")]
        public string sourcePaymentoName { get; set; }

        [JsonProperty("valor_pago")]
        public string amountPaid { get; set; }
    }

}