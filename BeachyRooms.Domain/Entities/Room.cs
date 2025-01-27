namespace BeachyRooms.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public required string HotelName { get; set; }
    public required int RoomNumber { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public int Sqft { get; set; }
    public int Occupancy { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

}