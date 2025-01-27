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
}