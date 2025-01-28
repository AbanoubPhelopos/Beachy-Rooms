using BeachyRooms.Domain.Entities;

namespace BeachyRooms.Application.Common.Interfaces;

public interface IRoomRepository
{
    Task<List<Room>> GetAllAsync();
    Task<Room> GetByIdAsync(int id);
    Task AddAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(Room room);
    Task<bool> ExistsAsync(int id);
}