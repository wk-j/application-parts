using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyLibrary.Controllers;

namespace MyWeb {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            var asm = typeof(HelloController).Assembly;
            var asmParts = new AssemblyPart(asm);
            services.AddMvc()
                .ConfigureApplicationPartManager(apm => {
                    apm.ApplicationParts.Add(asmParts);
                    var library = apm.ApplicationParts.FirstOrDefault(part => part.Name == "MyLibrary");
                    if (library != null) {
                        // apm.ApplicationParts.Remove(library);
                    }
                })
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            // app.UseHttpsRedirection();

            app.UseRouting(routes => {
                routes.MapControllers();
            });

            app.UseAuthorization();
        }
    }
}
