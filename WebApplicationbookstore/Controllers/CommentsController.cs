using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationbookstore.Data;
using WebApplicationbookstore.Models;

namespace WebApplicationbookstore.Controllers
{
    public class CommentsController : Controller
    {
        private readonly WebApplicationbookstoreContext _context;

        public CommentsController(WebApplicationbookstoreContext context)
        {
            _context = context;
        }

        // GET: Comments
        //show Comment
        public IActionResult Index(Comment comment)
        {
            var blogComment = _context.Comment?
                       .Where(c => c.BookId == comment.Id)
                       .Include(m => m.BookComment);

            return View(blogComment);
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,CommentText")] Comment comment)
        {
            if (ModelState.IsValid && comment != null)
            {
                try
                {
                    _context.Comment?.Add(comment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Comments", new { id = comment.BookId });
                }
                catch
                {
                    return NotFound();
                }
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,bookId,CommentText")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'WebApplicationbookstoreContext.Comment'  is null.");
            }
            var comment = await _context.Comment.FindAsync(id);
            if (comment != null)
            {
                _context.Comment.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Comments", new { id = comment?.BookId });
        }

        private bool CommentExists(int id)
        {
            return (_context.Comment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
