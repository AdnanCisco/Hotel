using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class ServiceReservationsController : Controller
    {
        private readonly BdHotelContext _context;

        public ServiceReservationsController(BdHotelContext context)
        {
            _context = context;
        }

        // GET: ServiceReservations
        public async Task<IActionResult> Index()
        {
            var bdHotelContext = _context.ServiceReservations.Include(s => s.IdResNavigation).Include(s => s.IdServNavigation);
            return View(await bdHotelContext.ToListAsync());
        }

        // GET: ServiceReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceReservation = await _context.ServiceReservations
                .Include(s => s.IdResNavigation)
                .Include(s => s.IdServNavigation)
                .FirstOrDefaultAsync(m => m.IdServRes == id);
            if (serviceReservation == null)
            {
                return NotFound();
            }

            return View(serviceReservation);
        }

        // GET: ServiceReservations/Create
        public IActionResult Create()
        {
            ViewData["IdRes"] = new SelectList(_context.Reservations, "IdRes", "IdRes");
            ViewData["IdServ"] = new SelectList(_context.Services, "IdServ", "IdServ");
            return View();
        }

        // POST: ServiceReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServRes,Sessions,Date,Heure,Participants,IdRes,IdServ,Total")] ServiceReservation serviceReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRes"] = new SelectList(_context.Reservations, "IdRes", "IdRes", serviceReservation.IdRes);
            ViewData["IdServ"] = new SelectList(_context.Services, "IdServ", "IdServ", serviceReservation.IdServ);
            return View(serviceReservation);
        }

        // GET: ServiceReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceReservation = await _context.ServiceReservations.FindAsync(id);
            if (serviceReservation == null)
            {
                return NotFound();
            }
            ViewData["IdRes"] = new SelectList(_context.Reservations, "IdRes", "IdRes", serviceReservation.IdRes);
            ViewData["IdServ"] = new SelectList(_context.Services, "IdServ", "IdServ", serviceReservation.IdServ);
            return View(serviceReservation);
        }

        // POST: ServiceReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServRes,Sessions,Date,Heure,Participants,IdRes,IdServ,Total")] ServiceReservation serviceReservation)
        {
            if (id != serviceReservation.IdServRes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceReservationExists(serviceReservation.IdServRes))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRes"] = new SelectList(_context.Reservations, "IdRes", "IdRes", serviceReservation.IdRes);
            ViewData["IdServ"] = new SelectList(_context.Services, "IdServ", "IdServ", serviceReservation.IdServ);
            return View(serviceReservation);
        }

        // GET: ServiceReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceReservation = await _context.ServiceReservations
                .Include(s => s.IdResNavigation)
                .Include(s => s.IdServNavigation)
                .FirstOrDefaultAsync(m => m.IdServRes == id);
            if (serviceReservation == null)
            {
                return NotFound();
            }

            return View(serviceReservation);
        }

        // POST: ServiceReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceReservation = await _context.ServiceReservations.FindAsync(id);
            if (serviceReservation != null)
            {
                _context.ServiceReservations.Remove(serviceReservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceReservationExists(int id)
        {
            return _context.ServiceReservations.Any(e => e.IdServRes == id);
        }
    }
}
