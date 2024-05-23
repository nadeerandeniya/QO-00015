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
using System.Xml.Linq;

namespace ebooks.Controllers
{
    public class SalesController : Controller
    {
        private readonly ebooksContext _context;

        //call connection class 
        SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
        SqlCommand com = new SqlCommand();
        SqlDataReader r;

        List<Sales> salesdata= new List<Sales>();
        List<SalesItems> items= new List<SalesItems>();
        public SalesController(ebooksContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            string query = "SELECT Sales.*,Customers.Fullname FROM Sales LEFT JOIN Customers ON Sales.CusId = Customers.CustomerId ";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            Sales sl = new Sales();
            r = cmd.ExecuteReader();
            while (r.Read())
            {
                salesdata.Add(new Sales
                {
                    SalesId = (int)r["SalesId"],
                    SalesDate = Convert.ToDateTime(r["SalesDate"]).ToString("yyyy/MM/dd"),
                    CusId = (int)r["CusId"],
                    PaymentMethod = r["PaymentMethod"].ToString(),
                    TotalPrice = r["TotalPrice"].ToString(),
                    Status = r["Status"].ToString(),
                    Fullname = r["Fullname"].ToString(),
                });


            }
            ViewBag.salesdata = salesdata;
            con.Close();
            return View();
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales.Select(p => new Sales
            {
                SalesId = p.SalesId,
                SalesDate = p.SalesDate,
                CusId = p.CusId,
                PaymentMethod = p.PaymentMethod,
                TotalPrice = p.TotalPrice,
                Status = p.Status
            }).FirstOrDefaultAsync(m => m.SalesId == id);
            if (sales == null)
            {
                return NotFound();
            }

            Sales sl = new Sales();
            ViewBag.cartdata = sl.Getitems(sales.SalesId);

            return View(sales);
        }

       
        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .FirstOrDefaultAsync(m => m.SalesId == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'ebooksContext.Sales'  is null.");
            }
            var sales = await _context.Sales.FindAsync(id);
            if (sales != null)
            {
                _context.Sales.Remove(sales);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesExists(int id)
        {
          return (_context.Sales?.Any(e => e.SalesId == id)).GetValueOrDefault();
        }
    }
}
