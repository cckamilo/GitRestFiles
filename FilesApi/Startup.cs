using Azure.Storage.Blobs;
using FilesApi.Business.Implementation;
using FilesApi.Business.Interface;
using FilesApi.DataAccess.Azure;
using FilesApi.DataAccess.MongoDb.Configuration;
using FilesApi.DataAccess.MongoDb.Interfaces;
using FilesApi.DataAccess.MongoDb.Repository;
using FilesApi.DataAccess.Other;
using FilesApi.Utilities.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            //Response
            services.AddTransient<UserResponse>();
            services.AddTransient<SftpResponse>();
            services.AddTransient<ProductsDb>();
            services.AddTransient<IProductsBll, ProductsBll>();
            services.AddTransient<IFiles, Files>();
            services.AddSingleton<IBlobService, BlobService>();
            services.AddSingleton<IUserBll, UserBll>();
            services.AddSingleton<IUserRepository, UserRepository>();
            //Authentication Jwt
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Authentication:key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };


            });

            services.AddSingleton<IAuthenticationManager, AuthenticationManager>();
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
