using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Igracapp.Data;
using Igracapp.Models;

namespace Igracapp.Controllers
{
    public class IgracController : Controller
    {
        private readonly KontekstBaze _context;

        public IgracController(KontekstBaze context)
        {
            _context = context;
        }

        // GET: Igrac
        public async Task<IActionResult> Index()
        {
            var kontekstBaze = _context.Igrac.Include(i => i.Opsirnije);
            return View(await kontekstBaze.ToListAsync());
        }

        // GET: Igrac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igrac = await _context.Igrac
                .Include(i => i.Opsirnije)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (igrac == null)
            {
                return NotFound();
            }

            return View(igrac);
        }

        // GET: Igrac/Create
        public IActionResult Create()
        {
            ViewData["OpsirnijeId"] = new SelectList(_context.Opsirnije, "Id", "Pozicija");
            return View();
        }

        // POST: Igrac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImePrezime,Klub,DatRod,Nacionalnost,OpsirnijeId")] Igrac igrac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(igrac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OpsirnijeId"] = new SelectList(_context.Opsirnije, "Id", "Pozicija", igrac.OpsirnijeId);
            return View(igrac);
        }

        // GET: Igrac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igrac = await _context.Igrac.FindAsync(id);
            if (igrac == null)
            {
                return NotFound();
            }
            ViewData["OpsirnijeId"] = new SelectList(_context.Opsirnije, "Id", "Pozicija", igrac.OpsirnijeId);
            return View(igrac);
        }

        // POST: Igrac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImePrezime,Klub,DatRod,Nacionalnost,OpsirnijeId")] Igrac igrac)
        {
            if (id != igrac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(igrac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IgracExists(igrac.Id))
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
            ViewData["OpsirnijeId"] = new SelectList(_context.Opsirnije, "Id", "Pozicija", igrac.OpsirnijeId);
            return View(igrac);
        }

        // GET: Igrac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var igrac = await _context.Igrac
                .Include(i => i.Opsirnije)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (igrac == null)
            {
                return NotFound();
            }

            return View(igrac);
        }

        // POST: Igrac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var igrac = await _context.Igrac.FindAsync(id);
            _context.Igrac.Remove(igrac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IgracExists(int id)
        {
            return _context.Igrac.Any(e => e.Id == id);
        }
    }
}
