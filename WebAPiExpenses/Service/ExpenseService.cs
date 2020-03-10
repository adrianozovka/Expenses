using System;
using System.Collections.Generic;
using System.Linq;
using WebApiExpenses.Model;
using WebApiExpenses.Service;
using WebAPiExpenses.Repository;
using WebAPiExpenses.Util;

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