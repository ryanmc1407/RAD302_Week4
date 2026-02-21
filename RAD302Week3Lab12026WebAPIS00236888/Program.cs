
using ClassLibrary1;
using ClassLibrary1.Interfaces;
using ClassLibrary1.Migrations;
using ClassLibrary1.Respositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RAD302Week3Lab12026WebAPIS00236888.Datalayer;
using Tracker.WebAPIClient;
namespace RAD302Week3Lab12026WebAPIS00236888
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ActivityAPIClient.Track(StudentID: "S00236888", StudentName: "Ryan McClelland",
                activityName: "Rad302 2026 Week 4 Lab 1", 
                Task: "Implementing Authentication Context Model");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CustomerDbContext>();
            builder.Services.AddTransient<ICustomer<Customer>, CustomerRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //Register the context with the SQL Serverr
            builder.Services.AddDbContext<CustomerDbContext>(options =>
                options.UseSqlServer(connectionString));

            /// 1. Add Identity Services for the ApplicationUser
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            // 2. Register the Customer DB Context (from Week 3/4)
            builder.Services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // 3. Register the Application DB Context for Security
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // 4. Register the Repository and Seeder
            builder.Services.AddTransient<ICustomer<Customer>, CustomerRepository>();
            builder.Services.AddTransient<ApplicationDbSeeder>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
                context.Database.EnsureCreated();  // Creates the DB and tables automatically, stops sql translient failure
            }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
