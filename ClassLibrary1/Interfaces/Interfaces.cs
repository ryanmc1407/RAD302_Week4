using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interfaces
{
    public interface ICustomer<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool CheckCustomerCredit(int customerId, decimal orderAmount);
    }
   
}
