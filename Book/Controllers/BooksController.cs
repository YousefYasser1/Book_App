using Microsoft.AspNetCore.Mvc;
using Book.Date;
using Book.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore;
using Book.ViewModel;
using NToastNotify;

namespace Book.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        public BooksController(ApplicationDbContext context , IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }


       public async Task<IActionResult> Index()
        {
            var book = await _context.BooKs.Include(m=> m.Category).ToListAsync();

            
            return View(book);
        }

        public async Task<IActionResult> Create()
        {
            var viewmodel = new BookViewModel
            {
                Categories = await _context.Categories.OrderBy(m => m.Name).ToListAsync()
            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            var book = new BooK
            {
                Author = model.Author,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Rate = model.Rate,
            };

            _context.BooKs.Add(book);

            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Book created successfully");

            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
          
            var book = await _context.BooKs.FindAsync(id);

            _context.BooKs.Remove(book);
            _context.SaveChanges();
            _toastNotification.AddErrorToastMessage("Book Deleted successfully");

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
                return BadRequest();
            var book = await _context.BooKs.FindAsync(id);
            if(book == null)
                return NotFound();

            var viewmodel = new BookViewModel
            {
                Id = book.Id,
                Author = book.Author,
                CategoryId = book.CategoryId,
                Description = book.Description,
                Rate = book.Rate,
                Categories = await _context.Categories.ToListAsync()
            };

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookViewModel model)
        {
            var book = await _context.BooKs.FindAsync(model.Id);
            if(book == null)
                return NotFound();

            book.Author = model.Author;
            book.CategoryId = model.CategoryId;
            book.Description = model.Description;
            book.Rate = model.Rate;
            _context.SaveChanges();
            _toastNotification.AddAlertToastMessage("Book Edited successfully");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            var book = await _context.BooKs.Include(m => m.Category).SingleOrDefaultAsync(m=> m.Id == id);
            if(book == null)
                return NotFound();

            return View(book);
        }



        [HttpPost]
        public async Task<IActionResult> Search(string term)
        {
            var book = await _context.BooKs.Include(m => m.Category).Where(m => m.Author.Contains(term)).SingleOrDefaultAsync();
            return View(book);
        }
    }
}
