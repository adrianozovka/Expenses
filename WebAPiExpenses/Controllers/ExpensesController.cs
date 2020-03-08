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

        [HttpGet(Name="GetTotalExpensesPerMonth")]    
        [Route("GetTotalExpensesPerMonth/")]  
        [Authorize]
          public List<Expense> getTotalExpensesPerMonth()
        {
            return _expenseRepositorio.getTotalExpensesPerMonth();
        }

        [HttpGet(Name="GetTotalExpensesPerCategory")]
        [Route("GetTotalExpensesPerCategory/")]
        [Authorize]
        public List<Expense> getTotalExpensesPerCategory()
        {
            return _expenseRepositorio.getTotalExpensesPerCategory();
        }

        [HttpGet(Name="GetPaymentPerSource")]
        [Route("GetPaymentPerSource/")]
        [Authorize]
          public List<Expense> getPaymentPerSource()
        {
            return _expenseRepositorio.getPaymentPerSource();
        }
    }
}