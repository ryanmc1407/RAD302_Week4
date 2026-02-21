using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.WebAPIClient;

namespace ClassLibrary1
{
    public class CustomerDbContext : DbContext
    {
        
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionstring = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = CustomerCoreDB-S00236888";
        //    optionsBuilder.UseSqlServer(connectionstring)
        //    .LogTo(Console.WriteLine,
        //           new[] { DbLoggerCategory.Database.Command.Name },
        //           LogLevel.Information);

        //  //  ActivityAPIClient.Track(StudentID: "S00236888", StudentName: "Ryan McClelland", activityName: "Rad302 2026 Week 3 Lab 1", Task: "Creating Customer DB Schema");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ActivityAPIClient.Track(StudentID: "S00236888", StudentName: "Ryan McClelland", activityName: "Rad302 2026 Week 3 Lab 1", Task: "Creating Customer DB Schema");

            modelBuilder.Entity<Customer>().HasData(
    new Customer { ID = 1, Name = "Patricia McKenna", Address = "8 Johnstown Road, Cork", CreditRating = 200.00f },
    new Customer { ID = 2, Name = "Helen Bennett", Address = "Garden House Crowther Way, Dublin", CreditRating = 400.00f },
    new Customer { ID = 3, Name = "Yoshi Tanami", Address = "1900 Oak St., Vancouver", CreditRating = 2000.00f },
    new Customer { ID = 4, Name = "John Steel", Address = "12 Orchestra Terrace, Dublin 20", CreditRating = 800.00f },
    new Customer { ID = 5, Name = "Catherine Dewey", Address = "Rue Joseph-Bens 532, Brussels", CreditRating = 600.00f }
         );



            base.OnModelCreating(modelBuilder);
        }
    }
}
