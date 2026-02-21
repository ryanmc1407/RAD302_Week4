using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;

namespace ClassLibrary1.Helpers
{
    public static class DbHelper
    {
        public static List<T> GetResource<T, S>(string resourceName) where S : CsvHelper.Configuration.ClassMap
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourcePath = assembly.GetManifestResourceNames()
                .FirstOrDefault(str => str.EndsWith(resourceName));

            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<S>();
                return csv.GetRecords<T>().ToList();
            }
        }
    }
}