using ClassLibrary1;
using ClassLibrary1.Interfaces;
using ClassLibrary1.Respositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RAD302Week3Lab12026WebAPIS00236888.Datalayer;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Tracker.WebAPIClient;
namespace RAD302Week3Lab12026WebAPIS00236888
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ActivityAPIClient.Track(StudentID: "S00236888", StudentName: "Ryan McClelland",
                activityName: "Rad302 2026 Week 4 Lab 1",
                Task: "testing Authorisation");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<ICustomer<Customer>, CustomerRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //Register the context with the SQL Serverr
            builder.Services.AddDbContext<CustomerDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerConnection")));

            /// 1. Add Identity Services for the ApplicationUser
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            // 2. Register the Customer DB Context (from Week 3/4)
            builder.Services.AddDbContext<CustomerDbContext>();

            builder.Services.AddTransient<ICustomer<Customer>, CustomerRepository>();

            // 3. Register the Application DB Context for Security
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie().AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Tokens:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Tokens:Key"]))

                };

            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RAD302Week3WebAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization Header using the Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            builder.Services.AddControllers();



            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<CustomerDbContext>().Database.EnsureCreated();


                var _ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                _ctx.Database.Migrate(); // Apply any pending migrations for Azure and local deployemnt if not already done
                                         //Create a new instance of the ApplicationDbSeeder class and call the Seed method for users and roles
                ApplicationDbSeeder dbSeeder = new ApplicationDbSeeder(_ctx, userManager, roleManager);
                dbSeeder.Seed().Wait(); // Seed method is in the dbseeder class
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.MapControllers();

            app.Run();
        }
    
    }
}
           
