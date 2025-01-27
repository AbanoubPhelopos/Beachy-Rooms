using BeachyRooms.Domain.Entities;
using BeachyRooms.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace BeachyRooms.Web.Controllers;

public class RoomController(ApplicationDbContext context) : Controller
{
    private readonly ApplicationDbContext _context = context;

    public IActionResult Index()
    {
        var rooms = _context.Rooms.ToList();
        return View(rooms);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Room room)
    {
        if (room.HotelName == room.Description)
        {
            ModelState.AddModelError("HotelName"," the hotel name and description  cannot be the same ");
        }
        if(!ModelState.IsValid)
            return View(room);
        _context.Rooms.Add(room);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}