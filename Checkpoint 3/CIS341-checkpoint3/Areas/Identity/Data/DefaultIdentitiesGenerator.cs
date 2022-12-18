using Microsoft.AspNetCore.Identity;

namespace CIS341_checkpoint3.Areas.Identity.Data
{
    public class DefaultIdentitiesGenerator
    {
        public DefaultIdentitiesGenerator()
        {
        }

        public void generate(IdentityContext context)
        {
            Func<long, ApplicationUser> mktestuser = (userId) =>
            {
                String userEmail = $"user{userId}@example.com";
                return new ApplicationUser
                {
                    Id = userEmail,
                    UserId = userId,
                    UserName = userEmail,
                    NormalizedUserName = userEmail.ToUpper(),
                    Email = userEmail,
                    NormalizedEmail = userEmail.ToUpper(),
                    EmailConfirmed = true,
                    //
                    //- PASSWORD
                    // solarwinds123.1338CD66-706C-40D9-9503-C4AFC4DBE7C4
                    //- PASSWORD
                    //
                    // normally this hash would be different per user account
                    // also, hard-coding secrets is bad practice
                    //
                    PasswordHash =
                        "AQAAAAEAACcQAAAAECj3B3mJ4xxEOOT0bPZUz2bhDcJaQRd3jT4fAGxYuvyWrDy7fUZ0Ll03fOv+/6A3Jg==",
                    SecurityStamp = "BW4ZKJP66MQ3K5G4ORNLJARUI52REZ4B",
                    ConcurrencyStamp = "2e1da46b-551d-4007-9579-76269e62b76e",
                };
            };
            context.Add(mktestuser(1)); // user1
            context.Add(mktestuser(2)); // user2
            context.Add(mktestuser(3)); // user3
            context.SaveChanges();
        }
    }
}