using BeachyRooms.Application.Common.Interfaces;
using BeachyRooms.Domain.Entities;
using BeachyRooms.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeachyRooms.Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomRepository roomRepository, ILogger<RoomController> logger)
        {
            _roomRepository = roomRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return View(rooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                _logger.LogWarning("Details: Room with ID {RoomId} not found.", id);
                return NotFound();
            }

            return View(room);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelName,RoomNumber,Country,City,Description,Price,Sqft,Occupancy,ImageUrl")] Room room)
        {
            if (room.HotelName == room.Description)
            {
                ModelState.AddModelError("HotelName", "The hotel name and description cannot be the same.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roomRepository.AddAsync(room);
                    TempData["success"] = "The Room created successfully.";
                    _logger.LogInformation("Create: Room with ID {RoomId} created successfully.", room.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["error"] = "The Room could not be created.";
                    _logger.LogError(ex, "Create: Error creating room with ID {RoomId}.", room.Id);
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the room. Please try again.");
                }
            }

            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                _logger.LogWarning("Edit: Room with ID {RoomId} not found.", id);
                return NotFound();
            }

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HotelName,RoomNumber,Country,City,Description,Price,Sqft,Occupancy,ImageUrl")] Room room)
        {
            if (id != room.Id)
            {
                _logger.LogWarning("Edit: Room ID mismatch. URL ID {UrlId}, Model ID {ModelId}.", id, room.Id);
                return BadRequest();
            }

            if (room.HotelName == room.Description)
            {
                ModelState.AddModelError("HotelName", "The hotel name and description cannot be the same.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roomRepository.UpdateAsync(room);
                    TempData["success"] = "The Room updated successfully.";
                    _logger.LogInformation("Edit: Room with ID {RoomId} updated successfully.", room.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["error"] = "The Room could not be updated.";
                    if (!await _roomRepository.ExistsAsync(room.Id))
                    {
                        _logger.LogWarning("Edit: Room with ID {RoomId} not found during update.", room.Id);
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError("Edit: Concurrency error when updating room with ID {RoomId}.", room.Id);
                        throw;
                    }
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Edit: Error updating room with ID {RoomId}.", room.Id);
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the room. Please try again.");
                }
            }

            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                _logger.LogWarning("Delete: Room with ID {RoomId} not found.", id);
                return NotFound();
            }

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                _logger.LogWarning("DeleteConfirmed: Room with ID {RoomId} not found.", id);
                return NotFound();
            }

            try
            {
                await _roomRepository.DeleteAsync(room);
                TempData["success"] = "The Room deleted successfully.";
                _logger.LogInformation("DeleteConfirmed: Room with ID {RoomId} deleted successfully.", id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                TempData["error"] = "The Room could not be deleted.";
                _logger.LogError(ex, "DeleteConfirmed: Error deleting room with ID {RoomId}.", id);
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the room. Please try again.");
                return View(room);
            }
        }
    }
}