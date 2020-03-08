using System.Collections.Generic;
using WebAPiExpenses.Model;

namespace WebAPiExpenses.Repository
{
    public interface IUserRepository
    {
           User Get(string username, string password);        
    }    

}