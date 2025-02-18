using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSVH_Consultation_Poratl.ActionFilters;
using SSVH_Consultation_Poratl.Models;

namespace SSVH_Consultation_Poratl.Controllers
{
    [RoleFilter(1)]
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassController(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Classes.ToListAsync();
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
        public async Task<IActionResult> Create(Class model)
        {
            if (ModelState.IsValid)
            {
                _context.Classes.Add(model);
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

            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }

            var data = await _context.ClassNames.ToListAsync();
            ViewBag.ClassNamesListData = new SelectList(data, "Id", "ClassName");
            ViewBag.ClassNamesData = data;

            return View(classEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Class classEntity)
        {
            if (id != classEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(classEntity.Id))
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
            return View(classEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classEntity = await _context.Classes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classEntity == null)
            {
                return NotFound();
            }

            return View(classEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity != null)
            {
                _context.Classes.Remove(classEntity);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
