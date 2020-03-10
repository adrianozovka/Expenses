using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiExpenses.Model;
using WebAPiExpenses.Service;
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebAPiExpenses.Controllers
{
    [Route("api/[Controller]")]
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;


        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]    
        [Route("GetTotalExpensesPerMonth/")]  
        [Authorize]
        public ActionResult<List<Expense>> getTotalExpensesPerMonth()
        {          
            List<Expense> lst  = _expenseService.getTotalExpensesPerMonth();    

            if(lst == null)
            {
                return NotFound();
            }

            return Ok(lst);         

            
        }

        [HttpGet]
        [Route("GetTotalExpensesPerCategory/")]
        [Authorize]
        public ActionResult<List<Expense>> getTotalExpensesPerCategory()
        {
            List<Expense> lst  = _expenseService.getTotalExpensesPerCategory();    

            if(lst == null)
            {
                return NotFound();
            }

            return Ok(lst);         

        }

        [HttpGet]
        [Route("GetPaymentPerSource/")]
        [Authorize]
        public ActionResult<List<Expense>> getPaymentPerSource()
        {
            List<Expense> lst  = _expenseService.getPaymentPerSource();    

            if(lst == null)
            {
                return NotFound();
            }

            return Ok(lst);         

            
        }
    }
}