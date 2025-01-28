using BeachyRooms.Application.Common.Interfaces;
using BeachyRooms.Domain.Entities;
using BeachyRooms.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace BeachyRooms.Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IUnitOfWork unitOfWork, ILogger<RoomController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _unitOfWork.Rooms.GetAllAsync();
            return View(rooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
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
                    _unitOfWork.Rooms.Add(room);
                    await _unitOfWork.CompleteAsync();
                    TempData["success"] = "The Room created successfully.";
                    _logger.LogInformation("Create: Room with ID {RoomId} created successfully.", room.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = "The Room could not be created.";
                    _logger.LogError(ex, "Create: Error creating room.");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the room. Please try again.");
                }
            }

            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
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
                    _unitOfWork.Rooms.Update(room);
                    await _unitOfWork.CompleteAsync();
                    TempData["success"] = "The Room updated successfully.";
                    _logger.LogInformation("Edit: Room with ID {RoomId} updated successfully.", room.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = "The Room could not be updated.";
                    _logger.LogError(ex, "Edit: Error updating room with ID {RoomId}.", room.Id);
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the room. Please try again.");
                }
            }

            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
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
            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
            if (room == null)
            {
                _logger.LogWarning("DeleteConfirmed: Room with ID {RoomId} not found.", id);
                return NotFound();
            }

            try
            {
                _unitOfWork.Rooms.Remove(room);
                await _unitOfWork.CompleteAsync();
                TempData["success"] = "The Room deleted successfully.";
                _logger.LogInformation("DeleteConfirmed: Room with ID {RoomId} deleted successfully.", id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = "The Room could not be deleted.";
                _logger.LogError(ex, "DeleteConfirmed: Error deleting room with ID {RoomId}.", id);
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the room. Please try again.");
                return View(room);
            }
        }
    }
}