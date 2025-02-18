using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSVH_Consultation_Poratl.Models;

namespace SSVH_Consultation_Poratl.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Roles.ToListAsync();
            ViewBag.RolesData = new SelectList(data, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login model)
        {
            if (ModelState.IsValid)
            {
                var verifyLoginDetails = await _context.Users.Where(x => x.UserName == model.UserName
                        && x.Password == model.Password && x.RoleId == model.Role && x.Status == true).FirstOrDefaultAsync();
                if (verifyLoginDetails != null) {
                    HttpContext.Session.SetString("FullName", verifyLoginDetails.FullName);
                    HttpContext.Session.SetString("UserName", verifyLoginDetails.UserName);
                    HttpContext.Session.SetInt32("RoleId", verifyLoginDetails.RoleId);
                    verifyLoginDetails.LastLogin = DateTime.Now;
                    _context.Update(verifyLoginDetails);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    var data1 = await _context.Roles.ToListAsync();
                    ViewBag.RolesData = new SelectList(data1, "Id", "Name");
                    return View(model);
                }
            }
            var data = await _context.Roles.ToListAsync();
            ViewBag.RolesData = new SelectList(data, "Id", "Name");
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("FullName", string.Empty);
            HttpContext.Session.SetString("UserName", string.Empty);
            HttpContext.Session.SetInt32("RoleId", 0);
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index), "Login");
        }
    }
}
