using Microsoft.Extensions.DependencyInjection;
using PracticeInventory.Domain.Entities;
using PracticeInventory.Domain.Enums;

namespace PracticeInventory.Persistence.Seeds;

public class SeedCategories
{
    public static Task SeedData(IServiceProvider service)
    {
        var context = service.GetRequiredService<ApplicationDbContext>();

        var initCategories = new List<Categories>()
        {
          new Categories()
          {
              CategoryId = Guid.NewGuid(),
              CategoryName = DefaultCategories.CatFood
          },
          new Categories()
          {
              CategoryId = Guid.NewGuid(),
              CategoryName = DefaultCategories.DogFood
          }
        };

        foreach (var item in initCategories)
        {
            if(!context.Categories.Any(d => d.CategoryName == item.CategoryName))
            {
                context.Categories.Add(item);
            }
        }

        context.SaveChangesAsync();

        return Task.CompletedTask;
    }
}