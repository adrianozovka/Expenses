
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApiExpenses.Model
{
    public class APIExpenseResult
    {       
        public string resultCode { get; set; }
       
        public string resultDescription { get; set; }
              
        public List<Expense> lstExpense {get;set;}
    }

}