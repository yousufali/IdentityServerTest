using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityServer4Core
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDeveloperIdentityServer()
                    .AddInMemoryScopes(Config.GetScopes())
                    .AddInMemoryClients(Config.GetClients())
                    .Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            //var builder = services.AddDeveloperIdentityServer();
            //builder.AddInMemoryClients(Config.GetClients());
            //builder.AddInMemoryScopes(Config.GetScopes());
            //builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();
        }
    }

    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var response = new Dictionary<string, object>

                        {
                                { "string_value", "some_string" },
               { "int_value", 42 }
                           };

            if (context.UserName == context.Password)
            {
                context.Result = new GrantValidationResult(context.UserName, "password", customResponse: response);
            }
            else
            {
                context.Result = new GrantValidationResult(TokenErrors.InvalidGrant, "invalid_credential", response);
            }

            return Task.CompletedTask;
        }
    }
}
