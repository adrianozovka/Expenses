using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiExpenses.Model;
using WebAPiExpenses.Repository;
using Microsoft.AspNetCore.Authorization;
using System;

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
          public APIExpenseResult getTotalExpensesPerMonth()
        {
            APIExpenseResult result = new APIExpenseResult();

            try{
                List<Expense> lst  = _expenseRepositorio.getTotalExpensesPerMonth();
                result.lstExpense = lst;
                result.resultCode= "OK";                

            }catch(Exception ex)
            {
                  result.resultCode= "ERROR";    
                  result.resultDescription = ex.Message;
            }
        

            return result;
        }

        [HttpGet]
        [Route("GetTotalExpensesPerCategory/")]
        [Authorize]
        public APIExpenseResult getTotalExpensesPerCategory()
        {
            APIExpenseResult result = new APIExpenseResult();

            try{
                List<Expense> lst  = _expenseRepositorio.getTotalExpensesPerCategory();
                result.lstExpense = lst;
                result.resultCode= "OK";                

            }catch(Exception ex)
            {
                  result.resultCode= "ERROR";    
                  result.resultDescription = ex.Message;
            }
        

            return result;
        }

        [HttpGet]
        [Route("GetPaymentPerSource/")]
        [Authorize]
        public APIExpenseResult getPaymentPerSource()
        {
            APIExpenseResult result = new APIExpenseResult();

            try{
                
                List<Expense> lst  = _expenseRepositorio.getPaymentPerSource();
                result.lstExpense = lst;
                result.resultCode= "OK";                

            }catch(Exception ex)
            {
                  result.resultCode= "ERROR";    
                  result.resultDescription = ex.Message;
            }        

            return result;

            
        }
    }
}