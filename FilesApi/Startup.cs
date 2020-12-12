using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesApi.Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FilesApi.Utilities.Response;
using Microsoft.Extensions.Options;
using FilesApi.DataAccess.Data;
using FilesApi.DataAccess.Data.Configuration;
using FilesApi.Business.Implementation;

namespace FilesApi
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
            services.AddControllers();
            services.AddTransient<FilesSftp>();
            services.AddTransient<ServiceResponse>();
            services.AddTransient<SftpResponse>();
           

            //MongoDb
            services.Configure<StoreDataBaseSettings>(
                Configuration.GetSection(nameof(StoreDataBaseSettings)));

            services.AddSingleton<IStoreDataBaseSettings>(sp => 
            sp.GetRequiredService<IOptions<StoreDataBaseSettings>>().Value);
            services.AddSingleton<ProductsDb>();
            services.AddSingleton<ProductsBll>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
