using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ebooks.Data;
using ebooks.Models;
using Microsoft.Data.SqlClient;
using static NuGet.Packaging.PackagingConstants;

namespace ebooks.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ebooksContext _context;
        
        //sesstion set variables
        public const string SessionKeyName = "_Name";
        public const string SessionKeyCart = "_Cart";
        public const string SessionKeyCartId = "_CartId";
        public const string SessionKeyUserId = "_UserId";

        //call database connection class
        SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
        SqlCommand com = new SqlCommand();
        SqlDataReader r;

        public CustomersController(ebooksContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
              return _context.Customers != null ? 
                          View(await _context.Customers.ToListAsync()) :
                          Problem("Entity set 'ebooksContext.Customers'  is null.");
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FullName,Phone,Email,Address,UserName,Password")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Loginview));
            }
            return View(customers);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Phone,Email,Address,UserName,Password")] Customers customers)
        {
            if (id != customers.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.CustomerId))
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
            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'ebooksContext.Customers'  is null.");
            }
            var customers = await _context.Customers.FindAsync(id);
            if (customers != null)
            {
                _context.Customers.Remove(customers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(int id)
        {
          return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> Login(String UserName, String Password)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set  is null.");
            }
            Customers cus = new Customers();
            cus.UserName = UserName;
            cus.Password = Password;

            string query = "SELECT * FROM Customers" +
                    " where UserName='" + cus.UserName + "' AND Password='" + cus.Password + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            r = cmd.ExecuteReader();
            while (r.Read())
            {
                string displayval = r["UserName"].ToString();
                HttpContext.Session.SetString(SessionKeyName, displayval);

                string cusid = r["CustomerId"].ToString();
                HttpContext.Session.SetString(SessionKeyUserId, cusid);


            }




            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {

                ViewBag.Result = "Username or Password is incorrect. Please try again.";
                return View("LoginView");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        public IActionResult Loginview()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.Result = "Successfully Log Out";
            return View("LoginView");
        }

        public IActionResult Fogetpass()
        {
            return View();
        }

        public IActionResult PasswordReset()
        {
            ViewBag.msg = "Please check your email..! We have send password reset link to your email";
            return View("LoginView");
        }
    }
}
