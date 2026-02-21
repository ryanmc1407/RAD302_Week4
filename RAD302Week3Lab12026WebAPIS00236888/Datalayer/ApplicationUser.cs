using Microsoft.AspNetCore.Identity;

namespace RAD302Week3Lab12026WebAPIS00236888.Datalayer
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
}
