namespace BeachyRooms.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IRoomRepository Rooms { get; }
    Task<int> CompleteAsync();
}