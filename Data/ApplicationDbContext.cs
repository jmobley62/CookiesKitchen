using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CookiesKitchen.Models;

namespace CookiesKitchen.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<CookiesKitchen.Models.Customer> Customer { get; set; } = default!;

public DbSet<CookiesKitchen.Models.Meal> Meal { get; set; } = default!;

public DbSet<CookiesKitchen.Models.Order> Order { get; set; } = default!;
}
