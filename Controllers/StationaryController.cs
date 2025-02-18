using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSVH_Consultation_Poratl.ActionFilters;
using SSVH_Consultation_Poratl.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SSVH_Consultation_Poratl.Controllers
{
    [RoleFilter(1)]
    public class StationaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StationaryController(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _context.Stationary.ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var data = await _context.ClassNames.ToListAsync();
            ViewBag.ClassNamesListData = new SelectList(data, "Id", "ClassName");
            ViewBag.ClassNamesData = data;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Stationary model)
        {
            if (ModelState.IsValid)
            {
                _context.Stationary.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var data = await _context.ClassNames.ToListAsync();
            ViewBag.ClassNamesListData = new SelectList(data, "Id", "ClassName");
            ViewBag.ClassNamesData = data;
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stationaryEntity = await _context.Stationary.FindAsync(id);
            var data = await _context.ClassNames.ToListAsync();
            ViewBag.ClassNamesListData = new SelectList(data, "Id", "ClassName");
            ViewBag.ClassNamesData = data;

            if (stationaryEntity == null)
                return NotFound();
            return View(stationaryEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Stationary stationaryEntity)
        {
            if (id != stationaryEntity.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stationaryEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationaryExists(stationaryEntity.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var data = await _context.ClassNames.ToListAsync();
            ViewBag.ClassNamesListData = new SelectList(data, "Id", "ClassName");
            ViewBag.ClassNamesData = data;
            return View(stationaryEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stationaryEntity = await _context.Stationary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stationaryEntity == null)
            {
                return NotFound();
            }

            return View(stationaryEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stationaryEntity = await _context.Stationary.FindAsync(id);
            if (stationaryEntity != null)
            {
                _context.Stationary.Remove(stationaryEntity);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StationaryExists(int id)
        {
            return _context.Stationary.Any(e => e.Id == id);
        }
    }
}
