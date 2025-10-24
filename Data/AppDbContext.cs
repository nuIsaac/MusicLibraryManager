using Microsoft.EntityFrameworkCore;
using MusicLib.Api.Models;

namespace MusicLib.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Song> Songs => Set<Song>();
}
