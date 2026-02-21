using CsvHelper.Configuration;
using ClassLibrary1;

namespace ClassLibrary1.Helpers
{
    public sealed class SupplierMap : ClassMap<Supplier>
    {
        public SupplierMap()
        {
            Map(m => m.SID).Name("SID");
            Map(m => m.Name).Name("Name");
            Map(m => m.Address).Name("Address");
        }
    }
}