using System.Collections.Generic;
using WebApiExpenses.Model;

namespace WebAPiExpenses.Service
{
    public interface IExpenseService
    {
        List<Expense> getTotalExpensesPerMonth();

        List<Expense> getTotalExpensesPerCategory();

        List<Expense> getPaymentPerSource();
        
    }    

}