using System.Collections.Generic;
using WebApiExpenses.Model;
using WebApiExpenses.Service;

namespace WebAPiExpenses.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        public List<Expense> getPaymentPerSource()
        {
            return APIClientService.getPaymentPerSource();
        }
               

        public List<Expense> getTotalExpensesPerCategory()
        {
             return APIClientService.getTotalExpensesPerCategory();
        }

        public List<Expense> getTotalExpensesPerMonth()
        {
             return APIClientService.getTotalExpensesPerMonth();
        }
    }
}