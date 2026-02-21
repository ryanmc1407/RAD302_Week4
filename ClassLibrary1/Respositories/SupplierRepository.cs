using ClassLibrary1.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary1.Respositories
{
    public class SupplierRepository : ISupplierRepository
    {
        public IEnumerable<Supplier> GetAll()
        {
            // This calls the helper to read your embedded CSV
            return DbHelper.GetResource<Supplier, SupplierMap>("Supplier.csv");
        }
    }
}
