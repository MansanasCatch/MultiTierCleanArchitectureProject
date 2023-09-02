using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracticeInventory.Domain.Entities;

namespace PracticeInventory.Persistence;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
    }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Items> Items { get; set; }
    public DbSet<Inventories> Inventories { get; set; }
    public DbSet<IdentityUser> Accounts { get; set; }
}