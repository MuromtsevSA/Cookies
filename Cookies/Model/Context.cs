using Microsoft.EntityFrameworkCore;

namespace Cookies.Model;

public class Context : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<CookieModel> Cookies { get; set; }

    public Context()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"Server=.;Database=Cookie;Trusted_Connection=True;");
}