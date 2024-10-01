using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AppMain
{
    class ApplicationDbContext: DbContext
{
    public DbSet<Anggota> Anggotas { get; set; }
    public DbSet<User> Users{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost;Username=postgres;Password=Sony@7777;Database=perpustakaandb");
        //=> optionsBuilder.UseNpgsql(@"Host=localhost;Username=postgres;Password=Sonyalpha@77;Database=perpustakaandb");
}
}
