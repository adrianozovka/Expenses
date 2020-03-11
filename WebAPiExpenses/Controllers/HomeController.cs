using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPiExpenses.Model;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using WebAPiExpenses.Service;
using WebAPiExpenses.Repository;
using Microsoft.Extensions.Configuration;

namespace WebAPiExpenses.Controllers
{

    

    [Route("api/[Controller]")]
    public class HomeController : Controller
    {

        IConfiguration _configuration;

        private readonly IUserRepository _userRepo;
        public HomeController(IConfiguration configuration, IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {

            string key = _configuration.GetSection("SecretTokenKey").Value;
        // Recupera o usuário
                var user = _userRepo.Get(model.Username, model.Password);

                // Verifica se o usuário existe
                if (user == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                // Gera o Token
                var token = TokenService.GenerateToken(user, key);

                // Oculta a senha
                user.Password = "";
                
                // Retorna os dados
                return new
                {
                    user = user,
                    token = token
                };
            
        }

      

    }
}