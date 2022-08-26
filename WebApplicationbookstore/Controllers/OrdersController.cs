using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplicationbookstore.Data;
using WebApplicationbookstore.Models;

namespace WebApplicationbookstore.Controllers
{
    public class OrdersController : Controller
    {

        private readonly WebApplicationbookstoreContext _context;

        public OrdersController(WebApplicationbookstoreContext context)
        {
            _context = context;
        }

        // GET: orders
        public async Task<IActionResult> Index()
        {
            var order = await _context.Orders!.ToListAsync();
            return View(order);
        }

        // GET: orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: orders/Create

        public async Task<IActionResult> Create(int? id)
        {
            var book = await _context.Book.FindAsync(id);
            return View(book);
        }


        // POST: orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId, int quantity)
        {
            Orders order = new Orders();
            order.BookId = bookId;
            order.Quantity = quantity;

            order.Userid = Convert.ToInt32(HttpContext.Session.GetString("Userid"));
            order.Orderdate = DateTime.Now;

            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Administrator\\Documents\\mynewdb4.mdf;Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "UPDATE book  SET Bookquantity  = Bookquantity   - '" + order.Quantity + "'  where (Id ='" + order.BookId + "' )";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();



            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Myorders));
        }
        //show order of customer
        public async Task<IActionResult> Myorders()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("Userid"));

            var orItems = await _context.Orders!.FromSqlRaw("select *  from orders where  Userid = '" + id + "'  ").ToListAsync();
            return View(orItems);
        }
        //show Customer Orders
        public async Task<IActionResult> CustomerOrders(int? id)
        {
            var orItems = await _context.Orders!.FromSqlRaw("select *  from orders where  Userid = '" + id + "'  ").ToListAsync();
            return View(orItems);

        }

        //show Customer report
        public async Task<IActionResult> Customerreport()
        {
            var orItems = await _context.Report!.FromSqlRaw("select Usersaccounts.id as Id, Name as customername, sum (Quantity * Price)  as total from book, orders,usersaccounts  where usersaccounts.id = orders.Userid  and Bookid= book.Id group by Name,Usersaccounts.id ").ToListAsync();
            return View(orItems);
        }



        // GET: orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,Userid,Quantity,Orderdate")] Orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ordersExists(orders.Id))
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
            return View(orders);
        }

        // GET: orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'WebApplicationbookstoreContext.orders'  is null.");
            }
            var orders = await _context.Orders.FindAsync(id);
            if (orders != null)
            {
                _context.Orders.Remove(orders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ordersExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
