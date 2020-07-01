using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using AriesWebApi.Validators;
using AriesWebApi.Entities.Accounts;

namespace AriesWebApi {
    public class Startup {

        public Startup (IConfiguration configuration) 
            => Configuration = configuration;
        
        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddControllers()
                .AddNewtonsoftJson(); 
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {

            app.UseStaticFiles ();
            app.UseRouting ();

            // app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute ("default", "{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute ("general", "company/{companyid}/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute ("entries", "company/{companyid}/accountingperiod/{accountingperiodid}/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute ("transactions", "bookentry/{bookentryid}/{controller=Home}/{action=Index}");
                
            });

        }
    }
}
// dotnet watch run