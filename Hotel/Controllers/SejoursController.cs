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
    public class SejoursController : Controller
    {
        private readonly BdHotelContext _context;

        public SejoursController(BdHotelContext context)
        {
            _context = context;
        }

        // GET: Sejours
        public async Task<IActionResult> Index()
        {
            var bdHotelContext = _context.Sejours.Include(s => s.IdChambreNavigation).Include(s => s.IdClientNavigation);
            return View(await bdHotelContext.ToListAsync());
        }

        // GET: Sejours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sejour = await _context.Sejours
                .Include(s => s.IdChambreNavigation)
                .Include(s => s.IdClientNavigation)
                .FirstOrDefaultAsync(m => m.IdSejour == id);
            if (sejour == null)
            {
                return NotFound();
            }

            return View(sejour);
        }

        // GET: Sejours/Create
        public IActionResult Create()
        {
            ViewData["IdChambre"] = new SelectList(_context.Chambres, "IdChambre", "IdChambre");
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient");
            return View();
        }

        // POST: Sejours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSejour,DateDebut,DateFin,IdChambre,IdClient")] Sejour sejour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sejour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdChambre"] = new SelectList(_context.Chambres, "IdChambre", "IdChambre", sejour.IdChambre);
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", sejour.IdClient);
            return View(sejour);
        }

        // GET: Sejours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sejour = await _context.Sejours.FindAsync(id);
            if (sejour == null)
            {
                return NotFound();
            }
            ViewData["IdChambre"] = new SelectList(_context.Chambres, "IdChambre", "IdChambre", sejour.IdChambre);
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", sejour.IdClient);
            return View(sejour);
        }

        // POST: Sejours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSejour,DateDebut,DateFin,IdChambre,IdClient")] Sejour sejour)
        {
            if (id != sejour.IdSejour)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sejour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SejourExists(sejour.IdSejour))
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
            ViewData["IdChambre"] = new SelectList(_context.Chambres, "IdChambre", "IdChambre", sejour.IdChambre);
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", sejour.IdClient);
            return View(sejour);
        }

        // GET: Sejours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sejour = await _context.Sejours
                .Include(s => s.IdChambreNavigation)
                .Include(s => s.IdClientNavigation)
                .FirstOrDefaultAsync(m => m.IdSejour == id);
            if (sejour == null)
            {
                return NotFound();
            }

            return View(sejour);
        }

        // POST: Sejours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sejour = await _context.Sejours.FindAsync(id);
            if (sejour != null)
            {
                _context.Sejours.Remove(sejour);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SejourExists(int id)
        {
            return _context.Sejours.Any(e => e.IdSejour == id);
        }
    }
}
