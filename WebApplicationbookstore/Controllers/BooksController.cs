using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationbookstore.Data;
using WebApplicationbookstore.Models;

namespace WebApplicationbookstore.Controllers
{
    public class BooksController : Controller
    {
        private readonly WebApplicationbookstoreContext _context;

        public BooksController(WebApplicationbookstoreContext context)
        {
            _context = context;
        }

        // GET: books
        public IActionResult Index(string SearchString, int CatgorySelectAdmin = 0)
        {
            ViewData["CurrentFilter"] = SearchString;
            ViewData["CurrentFilterCatgory"] = CatgorySelectAdmin;
            var books = from b in _context.Book select b;
            if (!String.IsNullOrEmpty(SearchString))
            {
                books = books.Where(b => b.Title!.Contains(SearchString));
            }
            else if (CatgorySelectAdmin != 0)
            {
                books = _context.Book.Where(c => c.Cataid == CatgorySelectAdmin);
            }
            return View(books);
        }

        //show catlog and serch and sort by catgory
        public IActionResult Catalogue(string SearchName = "", int CatgorySelect = 0)
        {

            var data = _context.Book?.Include(c => c.BookComments).AsQueryable();

            List<Book> books;
            if (SearchName != "" && SearchName != null)
            {
                books = data!.Where(b => b.Title!.Contains(SearchName)).ToList();
            }
            else if (CatgorySelect != 0)
            {
                books = data!.Where(c => c.Cataid == CatgorySelect).ToList();
            }
            else
            {
                books = data!.ToList();
            }
            return View(books);
        }
        //show the two most comment
        public IActionResult CatalogueMostTwo()
        {
            var data = _context.Book?.Include(c => c.BookComments).AsQueryable();

            return View(data!.OrderByDescending(a => a.BookComments!.Count).Take(2));
        }

        // GET: books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,Title,Info,Bookquantity,Price,Cataid,Author")] Book book)
        {
            if (file != null)
            {
                string filename = file.FileName;
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                book.Imgfile = filename;
            }
            try
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,Title,Info,Bookquantity,Price,Cataid,Author,Imgfile")] Book book)
        {

            if (file != null)
            {
                string filename = file.FileName;
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                book.Imgfile = filename;
            }
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'WebApplicationbookstoreContext.book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        private bool bookExists(int id)
        {
            return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
