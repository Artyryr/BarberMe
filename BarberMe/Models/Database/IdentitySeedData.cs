//using BarberMe.Models.Classes;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BarberMe.Models.Database
//{
//    public class IdentitySeedData
//    {



//        public static async void EnsurePopulated(IApplicationBuilder app)
//        {
//            UserManager<BarbershopUser> userManager = app.ApplicationServices
//               .GetRequiredService<UserManager<BarbershopUser>>();

//            BarbershopUser user = await userManager.FindByEmailAsync(artur);
//            if (user == null)
//            {
//                user = new BarbershopUser
//                {
//                    Email = artur,
//                    UserName = artur,
//                };
//                await userManager.CreateAsync(user, user1Password);
//            }

//            BarbershopUser test = await userManager.FindByEmailAsync(testUser);

//            if (test == null)
//            {
//                test = new BarbershopUser
//                {
//                    Email = testUser,
//                    UserName = testUser,
//                };
//                await userManager.CreateAsync(test, testPassword);
//            }
//            SeedData.EnsurePopulatedAsync(app);
//        }
//    }
//}
