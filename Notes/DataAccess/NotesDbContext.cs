using Microsoft.EntityFrameworkCore;

namespace Notes.DataAccess;

public class NotesDbContext: DbContext
{
    private readonly IConfiguration _configuration;

    public NotesDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
    }
    
    // public NotesDbContext(DbContextOptions<NotesDbContext> options)
    //     : base(options)
    // {
    // }
    //
    // public DbSet<NotesModel> Notes { get; set; } = null!;
}