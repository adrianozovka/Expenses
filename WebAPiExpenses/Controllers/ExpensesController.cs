using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiExpenses.Model;
using WebAPiExpenses.Repository;
using Microsoft.AspNetCore.Authorization;

namespace WebAPiExpenses.Controllers
{
    [Route("api/[Controller]")]
    public class ExpensesController : Controller
    {
        private readonly IExpenseRepository _expenseRepositorio;


        public ExpensesController(IExpenseRepository expenseRepo)
        {
            _expenseRepositorio = expenseRepo;
        }

        [HttpGet]    
        [Route("GetTotalExpensesPerMonth/")]  
        [Authorize]
          public List<Expense> getTotalExpensesPerMonth()
        {
            return _expenseRepositorio.getTotalExpensesPerMonth();
        }

        [HttpGet]
        [Route("GetTotalExpensesPerCategory/")]
        [Authorize]
        public List<Expense> getTotalExpensesPerCategory()
        {
            return _expenseRepositorio.getTotalExpensesPerCategory();
        }

        [HttpGet]
        [Route("GetPaymentPerSource/")]
        [Authorize]
          public List<Expense> getPaymentPerSource()
        {
            return _expenseRepositorio.getPaymentPerSource();
        }
    }
}