using Microsoft.AspNetCore.Mvc;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data;

namespace TravelAgencyWebApp.Controllers
{
    public class TravelingWayController : BaseController
    {
        private readonly ApplicationDbContext _context; // Assume ApplicationDbContext is your EF Core DB context

        public TravelingWayController(ApplicationDbContext context, ILogger<TravelingWayController> logger) : base(logger)
        {
            _context = context;
        }

        // GET: TravelingWay
        public IActionResult Index()
        {
            var travelingWays = _context.TravelingWays.ToList(); // Retrieve all traveling ways
            return View(travelingWays); // Return to the index view
        }

        // GET: TravelingWay/Details/5
        public IActionResult Details(int id)
        {
            var travelingWay = _context.TravelingWays.Find(id); // Find the traveling way by ID
            if (travelingWay == null)
            {
                return NotFound(); // Return 404 if not found
            }
            return View(travelingWay); // Return the details view
        }

        // GET: TravelingWay/Create
        public IActionResult Create()
        {
            return View(); // Return the create view
        }

        // POST: TravelingWay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TravelingWay travelingWay)
        {
            if (!ModelState.IsValid)
            {
                return View(travelingWay); // Return view with validation errors
            }

            _context.TravelingWays.Add(travelingWay); // Add to the context
            _context.SaveChanges(); // Save changes to the database

            return RedirectToAction(nameof(Index)); // Redirect to the index action
        }

        // GET: TravelingWay/Edit/5
        public IActionResult Edit(int id)
        {
            var travelingWay = _context.TravelingWays.Find(id); // Find the traveling way by ID
            if (travelingWay == null)
            {
                return NotFound(); // Return 404 if not found
            }
            return View(travelingWay); // Return the edit view
        }

        // POST: TravelingWay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TravelingWay travelingWay)
        {
            if (id != travelingWay.Id)
            {
                return BadRequest(); // Return a bad request if the IDs don't match
            }

            if (!ModelState.IsValid)
            {
                return View(travelingWay); // Return view with validation errors
            }

            _context.TravelingWays.Update(travelingWay); // Update the entity
            _context.SaveChanges(); // Save changes to the database

            return RedirectToAction(nameof(Index)); // Redirect to the index action
        }

        // GET: TravelingWay/Delete/5
        public IActionResult Delete(int id)
        {
            var travelingWay = _context.TravelingWays.Find(id); // Find the traveling way by ID
            if (travelingWay == null)
            {
                return NotFound(); // Return 404 if not found
            }
            return View(travelingWay); // Return the delete confirmation view
        }

        // POST: TravelingWay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var travelingWay = _context.TravelingWays.Find(id); // Find the traveling way
            if (travelingWay != null)
            {
                _context.TravelingWays.Remove(travelingWay); // Remove the entity
                _context.SaveChanges(); // Save changes to the database
            }
            return RedirectToAction(nameof(Index)); // Redirect to the index action
        }
    }
}

