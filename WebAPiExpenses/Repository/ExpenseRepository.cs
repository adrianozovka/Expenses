using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebApiExpenses.ApiClient;
using WebApiExpenses.Model;
using WebAPiExpenses.Util;

namespace WebAPiExpenses.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {


         static List<Expense> lstTotalExpensesPerMonth;
         static List<Expense> lstTotalExpensesPerCategory;
         static List<Expense> lstPaymentPerSource;

        public List<Expense> getPaymentPerSource()
        {
        

            if(lstPaymentPerSource == null)
            {      
                List<ExpenseAPI> lst = new List<ExpenseAPI>();

                lst =  APIClient.getPaymentPerSource();

                lstPaymentPerSource =  new List<Expense>();
                try
                {      
                    if(lst != null)             
                    {
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
                }
                catch (Exception ex1)
                {
                    throw ex1;
                }
            }
            return lstPaymentPerSource;
        }
               

        public List<Expense> getTotalExpensesPerCategory()
        {
            if(lstTotalExpensesPerCategory == null)
            {   
                List<ExpenseAPI> lst = new List<ExpenseAPI>();

                lst =  APIClient.getTotalExpensesPerCategory();         

                lstTotalExpensesPerCategory = new List<Expense>();
                try
                {
                     if(lst != null)
                    {                        
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

        public List<Expense> getTotalExpensesPerMonth()
        {
            if(lstTotalExpensesPerMonth == null)
            {      
                List<ExpenseAPI> lst = new List<ExpenseAPI>();

                lst =  APIClient.getTotalExpensesPerMonth();

                lstTotalExpensesPerMonth= new List<Expense>();
                try
                {
                    if(lst != null)
                    {
                        
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
    }
}