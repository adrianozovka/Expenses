using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPiExpenses.Service;
using WebAPiExpenses.Repository;

namespace WebAPiExpenses
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	      	services.AddCors(c =>  
            {  
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());  
            });
             
            // Injection was created to use in services snd controllers
            services.AddTransient<IExpenseService,ExpenseService>();
            services.AddTransient<IExpenseRepository,ExpenseRepository>();
            services.AddTransient<IUserRepository,UserRepository>();

			services.AddControllers();

            // Swagger to documentation
            services.AddSwaggerGen(c =>
            {
               c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPiExpenses", Version = "v1" });
               c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            // Bearer token was implemented to authenticate
            var key = Encoding.ASCII.GetBytes(Setting.Secret);
            services.AddAuthentication(
                    x => {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
            )
             .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


           
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

             app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // middleware from swagger, could be set in env.IsDevelopment().
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPiExpenses");
            });

             
            // middleware from authentication 
            app.UseAuthentication();

            app.UseAuthorization();      

         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
