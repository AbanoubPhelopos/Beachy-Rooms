using System.ComponentModel.DataAnnotations;

namespace BeachyRooms.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string HotelName { get; set; }
    [Display(Name = "Room number")]
    public required int RoomNumber { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public string? Description { get; set; }
    [Display(Name = "Price per night")]
    [Range(100,10000)]
    public double Price { get; set; }
    public int Sqft { get; set; }
    [Range(1,5)]
    public int Occupancy { get; set; }
    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

}