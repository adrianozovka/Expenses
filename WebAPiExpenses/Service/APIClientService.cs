
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

namespace WebApiExpenses.Service
{
    public static class APIClientService
    {
        private static string _urlBase =  Setting.HostQuery;
        static RestClient client = new RestClient(_urlBase);

         static List<Expense> lstTotalExpensesPerMonth;
         static List<Expense> lstTotalExpensesPerCategory;
         static List<Expense> lstPaymentPerSource;

      
        internal static List<Expense> getTotalExpensesPerMonth()
        {
            if(lstTotalExpensesPerMonth == null)
            {                 
                string caminhoUrl = $"api/3/action/datastore_search_sql?sql=SELECT%20ano_movimentacao,%20mes_movimentacao,%20valor_pago%20from%20%22d4d8a7f0-d4be-4397-b950-f0c991438111%22";
                RootAPI root;
                List<ExpenseAPI> lst = new List<ExpenseAPI>();
                lstTotalExpensesPerMonth= new List<Expense>();
                try
                {
                    root = client.GetSingleItemRequest<RootAPI>(caminhoUrl);
                    if(root != null && root.result != null && root.result.records != null)
                    {
                        lst = new List<ExpenseAPI>(root.result.records);

                        var lstGrouped = lst.GroupBy(
                            x => new { x.movimentYear, x.movimentMonth }, (key, group) => new
                            {
                                year = key.movimentYear,
                                month = key.movimentMonth,
                                listExpenseGrouped = group.ToList()
                            }
                            );


                        double amountPaid;
                        double value;
                        int year = 0;
                        int month= 0;
                        foreach (var group in lstGrouped)
                        {
                            amountPaid = 0;
                            foreach (var expense in group.listExpenseGrouped)
                            {
                                if(Double.TryParse(expense.amountPaid, out value))
                                {
                                    amountPaid += value;
                                }
                            }
                            if(group.year != null && group.month != null && Functions.IsNumeric(group.year, out year) &&  Functions.IsNumeric(group.month, out month))
                            {
                                lstTotalExpensesPerMonth.Add(new Expense() { movimentYear = year, movimentMonth =month, movimentMonthDescription = $"{month.ToString().PadLeft(2,'0')} - {(CultureInfo.CurrentCulture).DateTimeFormat.GetMonthName(Convert.ToInt32(group.month)).ToUpper()} " , amountPaid = Math.Round(amountPaid, 2) });
                            }
                        }
                    }
                    lst = null;
                }
                catch (Exception ex1)
                {
                    throw ex1;
                }
            }
            return lstTotalExpensesPerMonth;
        }


        internal static List<Expense> getTotalExpensesPerCategory()
        {   
            if(lstTotalExpensesPerCategory == null)
            {
                string caminhoUrl = $"api/3/action/datastore_search_sql?sql=SELECT%20categoria_economica_codigo,%20categoria_economica_nome,%20valor_pago%20from%20%22d4d8a7f0-d4be-4397-b950-f0c991438111%22";
                RootAPI root;
                List<ExpenseAPI> lst = new List<ExpenseAPI>();

                lstTotalExpensesPerCategory = new List<Expense>();
                try
                {
                    root = client.GetSingleItemRequest<RootAPI>(caminhoUrl);
                    if(root != null && root.result != null && root.result.records != null)
                    {
                        lst = new List<ExpenseAPI>(root.result.records);

                        var lstGrouped = lst.GroupBy(
                            x => new { x.categoryCode, x.categoryName }, (key, group) => new
                            {
                                categoryCode = key.categoryCode,
                                categoryName = key.categoryName,
                                listExpenseGrouped = group.ToList()
                            }
                            );

                        double amountPaid;
                        double value;
                        foreach (var group in lstGrouped)
                        {
                            amountPaid = 0;
                            foreach (var expense in group.listExpenseGrouped)
                            {
                                if(Double.TryParse(expense.amountPaid, out value))
                                {
                                    amountPaid += value;
                                }
                            }
                            if(group.categoryCode != null && group.categoryName != null)
                            {
                                lstTotalExpensesPerCategory.Add(new Expense() { categoryCode = group.categoryCode, categoryName = group.categoryName, amountPaid = Math.Round(amountPaid, 2) });
                            }
                        }
                    }
                    lst = null;
                }
                catch (Exception ex1)
                {
                    throw ex1;
                }
            }
            return lstTotalExpensesPerCategory;
        }

        internal static List<Expense> getPaymentPerSource()
        {
            if(lstPaymentPerSource == null)
            {
                string caminhoUrl = $"api/3/action/datastore_search_sql?sql=SELECT%20fonte_recurso_codigo,%20fonte_recurso_nome,%20valor_pago%20from%20%22d4d8a7f0-d4be-4397-b950-f0c991438111%22";
                RootAPI root;
                List<ExpenseAPI> lst = new List<ExpenseAPI>();
                lstPaymentPerSource =  new List<Expense>();
                try
                {
                    root = client.GetSingleItemRequest<RootAPI>(caminhoUrl);
                    lst = new List<ExpenseAPI>(root.result.records);

                    var lstGrouped = lst.GroupBy(
                        x => new { x.sourcePaymentCode, x.sourcePaymentoName }, (key, group) => new
                        {
                            sourcePaymentCode = key.sourcePaymentCode,
                            sourcePaymentoName = key.sourcePaymentoName,
                            listExpenseGrouped = group.ToList()
                        }
                        );


                    double amountPaid;
                    double value;
                    foreach (var group in lstGrouped)
                    {
                        amountPaid = 0;
                        foreach (var expense in group.listExpenseGrouped)
                        {
                            if(Double.TryParse(expense.amountPaid, out value))
                                {
                                    amountPaid += value;
                                }
                        }

                        if(group.sourcePaymentCode != null && group.sourcePaymentCode != null)
                        {
                            lstPaymentPerSource.Add(new Expense() { sourcePaymentCode = group.sourcePaymentCode, sourcePaymentoName = group.sourcePaymentoName, amountPaid = Math.Round(amountPaid, 2) });
                        }
                    }
                    lst = null;
                }
                catch (Exception ex1)
                {
                    throw ex1;
                }
            }
            return lstPaymentPerSource;
        }



    }
}
