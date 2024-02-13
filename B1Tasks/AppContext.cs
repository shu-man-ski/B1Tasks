using Microsoft.EntityFrameworkCore;

class AppContext : DbContext
{
    private readonly string ConnectionString = @"Data Source=DIMA-PC\SQLEXPRESS; Initial Catalog=B1Tasks; Persist Security Info=True; MultipleActiveResultSets=True; Pooling = true; Max Pool Size = 100;Connection Timeout=3600;";

    public DbSet<ImportTableModel> ImportTable { get; set; }

    public AppContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}
