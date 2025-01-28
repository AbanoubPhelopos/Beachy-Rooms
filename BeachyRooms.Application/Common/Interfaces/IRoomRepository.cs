using BeachyRooms.Domain.Entities;

namespace BeachyRooms.Application.Common.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    Task<Room> GetRoomWithDetailsAsync(int id);
}