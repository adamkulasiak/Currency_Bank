using AutoMapper;
using CurrencyBank.API.Interfaces;
using CurrencyBank.API.Repositories;
using CurrencyBank.API.Utilities;
using CurrencyBank.Database.Data;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;

namespace CurrencyBank.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CurrencyBankContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
            var wkHtmlToPdfPath = Path.Combine(_env.ContentRootPath, $"wkhtmltox\\v0.12.4\\{architectureFolder}\\libwkhtmltox");
            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddAutoMapper();
            services.AddTransient<Seed>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IExchangeRate, ExchangeRateRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                                ValidateIssuer = false,
                                ValidateAudience = false
                            };
                        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seeder)
        {
            CreateDatabaseDirectory();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseMvc();
            
            seeder.SeedData();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }

        private void CreateDatabaseDirectory()
        {
            string path = @"C:\Database";

            if (Directory.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Database\lang.txt"))
                {
                    sw.WriteLine("pl-PL");
                }
                return;
            }
                
            try
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Directory created succesfully");
                using (StreamWriter sw = new StreamWriter(@"C:\Database\lang.txt"))
                {
                    sw.WriteLine("pl-PL");
                }
            }
            catch (Exception e) { Console.WriteLine("EEEEEEEEEE" + e.ToString()); }
        }
    }
}
