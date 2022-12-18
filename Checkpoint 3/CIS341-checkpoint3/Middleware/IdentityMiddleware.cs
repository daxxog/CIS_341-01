using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CIS341_checkpoint3.Areas.Identity.Data;
using CIS341_checkpoint3.Data;
using CIS341_checkpoint3.Data.Entities;
using CIS341_checkpoint3.Models;

namespace CIS341_checkpoint3.Middleware
{
    public class IdentityMiddleware : IMiddleware
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SqliteContext _context;

        public IdentityMiddleware(UserManager<ApplicationUser> userManager, SqliteContext context)
        {
            this._userManager = userManager;
            this._context = context;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(context.User);

            if (applicationUser == null)
            {
                context.Items.Add("AuthorizationStatus", new AuthorizationStatus
                {
                    UserId = -1,
                    IsContentManager = false,
                });
            }
            else
            {
                var id = applicationUser.UserId;
                User user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception($"IdentityMiddleware fatal exception :: User(Id={id}) does not exist!");
                }

                context.Items.Add("AuthorizationStatus", new AuthorizationStatus
                {
                    UserId = (int)applicationUser.UserId,
                    IsContentManager = user.ContentManager,
                });
            }

            await next.Invoke(context);
        }
    }
}