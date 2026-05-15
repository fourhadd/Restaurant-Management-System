using FoodieRestaurant.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodieRestaurant.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


        await context.Database.MigrateAsync();


        var roles = new[] { "Admin", "Customer" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        const string adminEmail = "admin@foodie.local";
        const string adminPassword = "Admin123!";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "Foodie Admin"
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {

                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Admin istifadəçisi yaradıla bilmədi! Səbəblər: {errors}");
            }
        }


        if (!context.Categories.Any())
        {
            var categories = new[]
            {
                new Category { Name = "Pizza", Description = "Wood-fired classics and signatures." },
                new Category { Name = "Burgers", Description = "Juicy burgers and house sauces." },
                new Category { Name = "Desserts", Description = "Sweet bites after the meal." }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }


        if (!context.Foods.Any())
        {
            var pizzaId = await context.Categories.Where(c => c.Name == "Pizza").Select(c => c.Id).FirstAsync();
            var burgerId = await context.Categories.Where(c => c.Name == "Burgers").Select(c => c.Id).FirstAsync();
            var dessertId = await context.Categories.Where(c => c.Name == "Desserts").Select(c => c.Id).FirstAsync();

            var foods = new[]
            {
                new Food { Name = "Truffle Margherita", Description = "Fresh basil, mozzarella, black truffle drizzle.", Price = 14.99m, CategoryId = pizzaId, Rating = 4.7, ImagePath = "/images/foods/margherita.jpg" },
                new Food { Name = "Smoky House Burger", Description = "Angus patty, cheddar, smoky aioli and caramelized onion.", Price = 12.50m, CategoryId = burgerId, Rating = 4.6, ImagePath = "/images/foods/burger.jpg" },
                new Food { Name = "Molten Lava Cake", Description = "Warm dark chocolate center with vanilla cream.", Price = 7.25m, CategoryId = dessertId, Rating = 4.9, ImagePath = "/images/foods/lava-cake.jpg" }
            };

            await context.Foods.AddRangeAsync(foods);
            await context.SaveChangesAsync();
        }
    }
}