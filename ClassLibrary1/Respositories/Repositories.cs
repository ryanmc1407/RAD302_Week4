using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.Interfaces;


namespace ClassLibrary1.Respositories
{
    public class CustomerRepository : ICustomer<Customer>
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.ID == id);
        }

        public bool CheckCustomerCredit(int customerId, decimal orderAmount)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.ID == customerId);

            if (customer == null)
                return false;

            return orderAmount <= (decimal) customer.CreditRating;
        }
    }
}
