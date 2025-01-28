using BeachyRooms.Application.Common.Interfaces;
using BeachyRooms.Domain.Entities;
using BeachyRooms.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BeachyRooms.Infrastructure.Repositories;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Example of a room-specific method
    public async Task<Room> GetRoomWithDetailsAsync(int id)
    {
        // Include related entities as needed
        return await _entities.FirstOrDefaultAsync(r => r.Id == id);
    }
}
