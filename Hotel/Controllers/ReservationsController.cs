﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel.Models;

namespace Hotel.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly BdHotelContext _context;

        public ReservationsController(BdHotelContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var bdHotelContext = _context.Reservations.Include(r => r.IdChambreNavigation).Include(r => r.IdClientNavigation);
            return View(await bdHotelContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.IdChambreNavigation)
                .Include(r => r.IdClientNavigation)
                .FirstOrDefaultAsync(m => m.IdRes == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["IdChambre"] = new SelectList(_context.Chambres, "IdChambre", "IdChambre");
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRes,DateDebut,DateFin,IdClient,IdChambre,Etat,Reduction,Total")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdChambre"] = new SelectList(_context.Chambres, "IdChambre", "IdChambre", reservation.IdChambre);
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", reservation.IdClient);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["IdChambre"] = new SelectList(_context.Chambres, "IdChambre", "IdChambre", reservation.IdChambre);
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", reservation.IdClient);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRes,DateDebut,DateFin,IdClient,IdChambre,Etat,Reduction,Total")] Reservation reservation)
        {
            if (id != reservation.IdRes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.IdRes))
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
            ViewData["IdChambre"] = new SelectList(_context.Chambres, "IdChambre", "IdChambre", reservation.IdChambre);
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", reservation.IdClient);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.IdChambreNavigation)
                .Include(r => r.IdClientNavigation)
                .FirstOrDefaultAsync(m => m.IdRes == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.IdRes == id);
        }
    }
}
