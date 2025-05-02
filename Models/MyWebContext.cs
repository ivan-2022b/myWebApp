using Microsoft.EntityFrameworkCore;

namespace Visual.myWebAPI.Models;

public class MyWebContext : DbContext
{
    public MyWebContext(DbContextOptions<MyWebContext> options)
        : base(options)
    {
    }

    public DbSet<MyWebItem> MyWebItems { get; set; } = null!;
}
