using BeachyRooms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeachyRooms.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Room> Rooms { get; set; }
}