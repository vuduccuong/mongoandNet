using App.API.Hubs;
using App.Domain.Base;
using App.Domain.IRepositories;
using App.Domain.IServices;
using App.Infratructure.Repositories;
using App.Service.ImplementService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace App.API
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
            //Add config
            services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<IMongoDBSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value);
            //End config

            //services.AddRepositories();
            //services.AddServices();

            //db
            services.AddSingleton<IMongoClient, MongoClient>(config =>
            {
                var uri = config.GetRequiredService<IConfiguration>()["MongoDbSettings:ConnectionString"];
                return new MongoClient(uri);
            });
            //end db
            services.AddScoped<IShipwreckService, ShipwreckService>();
            services.AddScoped<IFileManagerService, FileManagerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFileManagerRepository, FileManagerRepository>();
            services.AddScoped<IShipwreckRepository, ShipwreckRepository>();
            //services.AddScoped(typeof(IMongoRepositoryBase<>), typeof(MongoRepositoryBase<>));

            //JWT
            //services.AddAuthentication(options=>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            // .AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new()
            //    {
            //        ValidateIssuerSigningKey = true,
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidIssuer = Configuration["JWt:Key"],
            //        ValidAudience = Configuration["JWt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWt:Key"]))
            //    };
            //});
            //End JWT

            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "App.API",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Email = "github.com/vuduccuong",
                        Name = "CuongGit"
                    }
                });
            });
            //cache
            services.AddMemoryCache();
            //end cache
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.API v1"));
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors("ClientPermission");
            app.UseRouting();
            app.UseAuthorization();
            //app.UseMiddleware<JwtMiddleWare>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CountDownHub>("/hubs/countdown");
            });
        }
    }
}