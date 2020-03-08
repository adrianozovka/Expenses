using System.Collections.Generic;
using WebApiExpenses.Model;

namespace WebAPiExpenses.Repository
{
    public interface IExpenseRepository
    {
        List<Expense> getTotalExpensesPerMonth();

        List<Expense> getTotalExpensesPerCategory();

        List<Expense> getPaymentPerSource();
        
    }    

}