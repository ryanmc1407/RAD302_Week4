using System.Diagnostics;
using Tracker.WebAPIClient;
using ClassLibrary1;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace RAD302Week3Lab12026.S00236888

{
    public class Program
    {
        static void Main(string[] args)
        {
            ActivityAPIClient.Track(StudentID: "S00236888", StudentName: "Ryan McClelland", activityName: "Rad302 2026 Week 3 Lab 1", Task: "Testing Console Queries against the DB Model");



            using CustomerDbContext db = new CustomerDbContext();

            var allCustomers = db.Customers.ToList();

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