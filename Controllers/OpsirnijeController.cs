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
    public class OpsirnijeController : Controller
    {
        private readonly KontekstBaze _context;

        public OpsirnijeController(KontekstBaze context)
        {
            _context = context;
        }

        // GET: Opsirnije
        public async Task<IActionResult> Index()
        {
            return View(await _context.Opsirnije.ToListAsync());
        }

        // GET: Opsirnije/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opsirnije = await _context.Opsirnije
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opsirnije == null)
            {
                return NotFound();
            }

            return View(opsirnije);
        }

        // GET: Opsirnije/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Opsirnije/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pozicija,Visina,Noga,Vrijednost")] Opsirnije opsirnije)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opsirnije);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opsirnije);
        }

        // GET: Opsirnije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opsirnije = await _context.Opsirnije.FindAsync(id);
            if (opsirnije == null)
            {
                return NotFound();
            }
            return View(opsirnije);
        }

        // POST: Opsirnije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pozicija,Visina,Noga,Vrijednost")] Opsirnije opsirnije)
        {
            if (id != opsirnije.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opsirnije);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpsirnijeExists(opsirnije.Id))
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
            return View(opsirnije);
        }

        // GET: Opsirnije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opsirnije = await _context.Opsirnije
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opsirnije == null)
            {
                return NotFound();
            }

            return View(opsirnije);
        }

        // POST: Opsirnije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opsirnije = await _context.Opsirnije.FindAsync(id);
            _context.Opsirnije.Remove(opsirnije);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpsirnijeExists(int id)
        {
            return _context.Opsirnije.Any(e => e.Id == id);
        }
    }
}
