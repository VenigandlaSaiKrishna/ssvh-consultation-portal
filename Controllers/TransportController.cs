using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSVH_Consultation_Poratl.ActionFilters;
using SSVH_Consultation_Poratl.Models;

namespace SSVH_Consultation_Poratl.Controllers
{
    [RoleFilter(1)]
    public class TransportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransportController(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Transport.ToListAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transport model)
        {
            if (ModelState.IsValid)
            {
                _context.Transport.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var transportEntity = await _context.Transport.FindAsync(id);

            if (transportEntity == null)
                return NotFound();
            return View(transportEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transport transportEntity)
        {
            if (id != transportEntity.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transportEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportExists(transportEntity.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transportEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var transportEntity = await _context.Transport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transportEntity == null)
                return NotFound();

            return View(transportEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transportEntity = await _context.Transport.FindAsync(id);
            if (transportEntity != null)
            {
                _context.Transport.Remove(transportEntity);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TransportExists(int id)
        {
            return _context.Transport.Any(e => e.Id == id);
        }
    }
}
