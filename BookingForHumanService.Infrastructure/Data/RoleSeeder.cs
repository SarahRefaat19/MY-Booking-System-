using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingForHumanService.Infrastructure.Data
{
    public static class RoleSeeder
    {
        public  async static Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
        {

            string[] roles = { "Admin", "Customer", "Provider" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                   await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
        }
    }
}
