
using System.Collections.Generic;
using WebApiExpenses.Model;
using WebAPiExpenses.Repository;


namespace WebAPiExpenses.Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepo;


        public ExpenseService(IExpenseRepository expenseRepo)
        {
            this._expenseRepo = expenseRepo;
        }

        public List<Expense> getPaymentPerSource()
        {
            return _expenseRepo.getPaymentPerSource();
        }
               

        public List<Expense> getTotalExpensesPerCategory()
        {
             return _expenseRepo.getTotalExpensesPerCategory();
        }

        public List<Expense> getTotalExpensesPerMonth()
        {
              return _expenseRepo.getTotalExpensesPerMonth();             
        }
    }
}