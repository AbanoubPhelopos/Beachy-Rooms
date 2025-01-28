using BeachyRooms.Application.Common.Interfaces;
using BeachyRooms.Infrastructure.Data;

namespace BeachyRooms.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IRoomRepository Rooms { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Rooms = new RoomRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}