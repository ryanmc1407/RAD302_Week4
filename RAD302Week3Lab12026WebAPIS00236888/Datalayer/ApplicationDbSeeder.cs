using Microsoft.AspNetCore.Identity;

namespace RAD302Week3Lab12026WebAPIS00236888.Datalayer
{
    public class ApplicationDbSeeder
    {
        private readonly ApplicationDbContext _ctx;

        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbSeeder(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        public async void Seed()
        {
            // Seed the Main User
            ApplicationUser user = await _userManager.FindByEmailAsync("jbloggs@itsligo.ie");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    SecondName = "Bloggs",
                    FirstName = "Joe",
                    Email = "jbloggs@itsligo.ie",
                    UserName = "jbloggs@itsligo.ie",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await _userManager.CreateAsync(user, "Rad302$1");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in Seeding");
                }

                _ctx.SaveChanges();
            }
        }
    }
}
