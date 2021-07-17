using LocalMarket.Data;
using LocalMarket.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalMarket.Infrastructure
{
    public static class ApplicationBuilderExtencions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {

            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<LocalMarketDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

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
    }
}
