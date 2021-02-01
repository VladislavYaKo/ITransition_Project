using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITransitionProject.Models
{
    public class DBDataInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext appContext)
        {
            string login = "admin@mail.ru";
            string password = "_Aa12345";           
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(login) == null)
            {
                User admin = new User { Email = login, UserName = login, NotLoginName = "admin" };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            AdditionalFieldsNames afn = appContext.AdditionalFieldsNames.Find(Guid.Empty);
            if (afn == null)
                appContext.AdditionalFieldsNames.Add(new AdditionalFieldsNames { Id = Guid.Empty });
            AdditionalFieldsValues afv = appContext.AdditionalFieldsValues.Find(Guid.Empty);
            if (afv == null)
                appContext.AdditionalFieldsValues.Add(new AdditionalFieldsValues { Id = Guid.Empty });
            await appContext.SaveChangesAsync();
        }
    }
}
