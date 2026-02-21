using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using Tracker.WebAPIClient;

namespace RAD302Week3Lab12026.S00236888

{
    public class Program
    {
        static void Main(string[] args)
        {
            ActivityAPIClient.Track(StudentID: "S00236888", StudentName: "Ryan McClelland", activityName: "Rad302 2026 Week 3 Lab 1", Task: "Testing Console Queries against the DB Model");


            // 1. Set up the options to point to my Week 4 database
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=week4-db-2026-S00236888;Integrated Security=true;MultipleActiveResultSets=true;");

            // 2. Pass those options into the constructor
            using CustomerDbContext db = new CustomerDbContext(optionsBuilder.Options);

            // Your existing logic remains the same
            var allCustomers = db.Customers.ToList();// 1.Set up the options to point to my Week 4 database
            

    

            foreach (var c in allCustomers)
            {
                Console.WriteLine(
                    $"ID: {c.ID}, Name: {c.Name}, Address: {c.Address}, Credit Rating: {c.CreditRating}");
            }

            var highCreditCustomers = db.Customers.Where(c => c.CreditRating > 400).ToList();

            Console.WriteLine("\nCustomers with Credit Rating > 400:");

            foreach (var c in highCreditCustomers)
            {
                Console.WriteLine(
                    $"ID: {c.ID}, Name: {c.Name}, Address: {c.Address}, Credit Rating: {c.CreditRating}");
            }

            int maxId = db.Customers.Max(c => c.ID);

            Customer newCustomer = new Customer
            {
                ID = maxId + 1,
                Name = "New Customer",
                Address = "123 New St, New City",
                CreditRating = 500.00f
            };

            db.Customers.Add(newCustomer);
            db.SaveChanges();

            Console.WriteLine("\n new customer added:");
        }
    }
}