using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplicationbookstore.Data;
using WebApplicationbookstore.Models;

namespace WebApplicationbookstore.Controllers
{
    public class UsersaccountsController : Controller
    {
        private readonly WebApplicationbookstoreContext _context;

        public UsersaccountsController(WebApplicationbookstoreContext context)
        {
            _context = context;
        }

        // GET: usersaccounts
        public async Task<IActionResult> Index()
        {
            return _context.Usersaccounts != null ?
                        View(await _context.Usersaccounts.ToListAsync()) :
                        Problem("Entity set 'WebApplicationbookstoreContext.usersaccounts'  is null.");
        }

        // GET: usersaccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usersaccounts == null)
            {
                return NotFound();
            }

            var usersaccounts = await _context.Usersaccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersaccounts == null)
            {
                return NotFound();
            }

            return View(usersaccounts);
        }

        //show login
        public IActionResult Login()
        {
            return View();
        }
        //check login
        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string na, string pa)
        {
            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Administrator\\Documents\\mynewdb4.mdf;Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "SELECT * FROM usersaccounts where Name ='" + na + "' and  Pass ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string role = (string)reader["Role"];
                string id = Convert.ToString((int)reader["Id"]);

                HttpContext.Session.SetString("Name", na);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("Userid", id);
                reader.Close();
                conn1.Close();
                if (role == "customer")
                    return RedirectToAction("CatalogueMostTwo", "Books");

                else
                    return RedirectToAction("Index", "Books");

            }
            else
            {
                ViewData["Message"] = "wrong user name and password";
                return View();
            }
        }


        // GET: usersaccounts/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: usersaccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Pass,Email")] Usersaccounts usersaccounts)
        {
            if (ModelState.IsValid)
            {
                usersaccounts.Role = "customer";
                _context.Add(usersaccounts);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Login));
        }

        // GET: usersaccounts/Edit/5
        public async Task<IActionResult> Edit()
        {
         
            int id = Convert.ToInt32(HttpContext.Session.GetString("Userid"));
            var usersaccounts = await _context.Usersaccounts!.FindAsync(id);
           
            return View(usersaccounts);
        }

        // POST: usersaccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Pass,Role,Email")] Usersaccounts usersaccounts)
        {
            _context.Update(usersaccounts);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Login));
        }

        private bool usersaccountsExists(int id)
        {
            return (_context.Usersaccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
