using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace ClassLibrary1
{
    public class Customer
    {
        [Key]
        [Display(Name = "Customer Number")]
        public int ID { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Display(Name = "Credit Rating")]
        public float CreditRating { get; set; }

    }
    // Create the view model
    public class CustomerCreditRequest
    {
        public int CustomerID { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
