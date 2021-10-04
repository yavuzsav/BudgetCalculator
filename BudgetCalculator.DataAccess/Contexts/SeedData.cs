using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using BudgetCalculator.Entities.Concrete;
using Microsoft.Extensions.Logging;

namespace BudgetCalculator.DataAccess.Contexts
{
    public class SeedData
    {
        public static void Seed(DataContext context, ILoggerFactory loggerFactory)
        {
            var faker = new Faker();
            List<Category> categories = new List<Category>();
            List<Budget> budgets = new List<Budget>();
            try
            {
                if (!context.Categories.Any())
                {
                    categories.Add(new Category() { Id = new Guid(), Name = "", Description = "" });
                    categories.Add(new Category() { Id = new Guid(), Name = "", Description = "" });
                    categories.Add(new Category() { Id = new Guid(), Name = "", Description = "" });
                    categories.Add(new Category() { Id = new Guid(), Name = "", Description = "" });
                    categories.Add(new Category() { Id = new Guid(), Name = "", Description = "" });

                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                }

                if (!context.Budgets.Any())
                {
                    budgets.Add(new Budget()
                    {
                        Id = new Guid(),
                        Name = "My Budget",
                        Year = 2021,
                        Period = 10,
                        Target = 7500,
                        CategoryId = faker.Random.ListItem(context.Categories.Select(x => x.Id).ToList()),
                    });
                    context.Budgets.AddRange(budgets);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<SeedData>();
                logger.LogError(e.Message);
            }
        }
    }
}