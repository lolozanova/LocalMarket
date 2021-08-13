using LocalMarket.Data;
using LocalMarket.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Infrastructure
{
    public static class ApplicationBuilderExtencions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {

            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var data = serviceProvider.GetRequiredService<LocalMarketDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            SeedUnits(data);

            SeedTowns(data);

            SeedAdmin(serviceProvider);

            return app;
        }


        private static void SeedCategories(LocalMarketDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }
            data.Categories.AddRange(new[]
              {

            new Category{Name = "Fruit" },
            new Category{Name = "Vegetable" },
            new Category{Name = "Meet" },
             new Category{Name = "Eggs" },
            new Category{Name = "Milk" },
             new Category{Name = "Nuts" }
        });

            data.SaveChanges();
        }

        private static void SeedUnits(LocalMarketDbContext data)
        {
            if (data.Units.Any())
            {
                return;
            }
            data.Units.AddRange(new[]
              {

            new Unit {Name = "kg" },
            new Unit{Name = "piece" },
            new Unit{Name = "ml" },

        });

            data.SaveChanges();
        }

        private static void SeedTowns(LocalMarketDbContext data)
        {
            if (data.Towns.Any())
            {
                return;
            }
            data.Towns.AddRange(new[]
              {

            new Town {Name = "Sofia" },
            new Town{Name = "Plovdiv" },
            new Town{Name = "Varna" },
            new Town{Name = "Burgas" },
            new Town{Name = "Gabrovo" },
            new Town{Name = "Pleven" },
            new Town{Name = "Ruse" },
            new Town{Name = "Stara Zagora" },
            new Town{Name = "Dobrich" },
            new Town{Name = "Razgrad" },
            new Town{Name = "Pernik" },
            new Town{Name = "Vidin" },
            new Town{Name = "Shumen" },
            new Town{Name = "Montana" },
            new Town{Name = "Vraca" },
            new Town{Name = "Lovech" },
            new Town{Name = "Veliko Tarnovo" },
            new Town{Name = "Sliven" },
            new Town{Name = "Haskovo" },
            new Town{Name = "Kardzali" },
            new Town{Name = "Pazarddzik" },
            new Town{Name = "Kiustendil" },
            new Town{Name = "Silistra" },
            new Town{Name = "Smolyan" },

        });

            data.SaveChanges();
        }

        private static void SeedAdmin(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(WebConstants.adminRole))
                {
                    return;
                }

                var role = new IdentityRole { Name = WebConstants.adminRole };

                await roleManager.CreateAsync(role);

                var user = new User
                {
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                await userManager.CreateAsync(user, "admin123");

                await userManager.AddToRoleAsync(user, role.Name);
            })
                .GetAwaiter()
                .GetResult();

        }
    }
}
