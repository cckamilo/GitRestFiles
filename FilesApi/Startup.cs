using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using FilesApi.Business.Services;
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
using FilesApi.Business.Interface;
using FilesApi.DataAccess.Interfaces;
using FilesApi.DataAccess.Implementaion;
using Azure.Storage.Blobs;
using FilesApi.DataAccess.Interfaces.MongoDb;
using FilesApi.DataAccess.Implementaion.MongoDb;

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
            services.AddTransient<ServiceResponse>();
            services.AddTransient<SftpResponse>();
            services.AddTransient<ProductsDb>();
            services.AddTransient<IProductsBll, ProductsBll>();
            services.AddTransient<IFiles, Files>();
            services.AddSingleton<IBlobService, BlobService>();
            services.AddSingleton<IUserBll, UserBll>();
            services.AddSingleton<IUserRepository, UserDb>();
            //MongoDb
            services.Configure<StoreDataBaseSettings>(
                Configuration.GetSection(nameof(StoreDataBaseSettings)));

            services.AddSingleton<IStoreDataBaseSettings>(sp => 
            sp.GetRequiredService<IOptions<StoreDataBaseSettings>>().Value);
            services.AddControllers().AddNewtonsoftJson();
            //Azure
            services.AddSingleton(x =>
             new BlobServiceClient(connectionString:Configuration.GetValue<string>(key: "AzureBlobStorageConnectionsString")));

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
