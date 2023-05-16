using Microsoft.AspNetCore.Mvc;
using Book.Models;
using Book.Date;
using Book.ViewModel;
using NToastNotify;
using Microsoft.EntityFrameworkCore;

namespace Book.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toast;
        public CategoriesController(ApplicationDbContext context , IToastNotification toast)
        {
            _context = context;
            _toast = toast;
        }


        public async Task<IActionResult> Index()
        {
            var category = await _context.Categories.ToListAsync();
            return View(category);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategotyViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var category = new Category { Name = model.Name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            _toast.AddSuccessToastMessage("Category Added Successfully");
            return RedirectToAction("Index", "Books");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
                return BadRequest();
            var category = await _context.Categories.SingleOrDefaultAsync(m => m.Id == id);

            _context.Remove(category);
            _context.SaveChanges();
            _toast.AddErrorToastMessage("Category Deleted Successfully");

            return RedirectToAction("Index", "Books");

        }

    }
}
