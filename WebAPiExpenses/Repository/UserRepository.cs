using System.Collections.Generic;
using System.Linq;
using WebAPiExpenses.Model;

namespace WebAPiExpenses.Repository
{
    public class UserRepository : IUserRepository
    {
            // In this solution, we dont have database, Static users was created to complete the authentication.
            public User Get(string username, string password)
            {
                var  users = new List<User>() ;

                users.Add( new User{ Id = 1, Username= "adriano@gmail.com", Password="adriano", Role="manager"});
               
                users.Add( new User{ Id = 1, Username= "recife@gmail.com", Password="recife", Role="employee"});

                return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password ).FirstOrDefault();
            }


    }
}