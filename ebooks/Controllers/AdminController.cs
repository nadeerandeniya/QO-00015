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
using NuGet.Protocol.Plugins;

namespace ebooks.Controllers
{
    public class AdminController : Controller
    {
        private readonly ebooksContext _context;

        SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
        SqlCommand com = new SqlCommand();
        SqlDataReader r;

        //create sesstion varibles
        public const string SessionKeyName = "_Name";
        public const string SessionKeyUserId = "_UserId";

        //create data lists
        List<Customers> cus = new List<Customers>();
        List<Books> book = new List<Books>();
        List<Sales> sales = new List<Sales>();

        public AdminController(ebooksContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Login()
        {

            Users lg = new Users();
            lg.UserName = HttpContext.Request.Form["UserName"];
            lg.Password = HttpContext.Request.Form["Password"];

            //check login
            string query = "SELECT * FROM Users WHERE UserName='" + lg.UserName + "' AND Password='" + lg.Password + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            r = cmd.ExecuteReader();
            while (r.Read())
            {
                string displayval = r["UserName"].ToString();
                HttpContext.Session.SetString(SessionKeyName, displayval);

                string cusid = r["UserId"].ToString();
                HttpContext.Session.SetString(SessionKeyUserId, cusid);


            }




            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                //pass error massage
                ViewBag.Result = "Username or Password is incorrect. Please try again.";
                return View("Index");
            }
            else
            {
                return RedirectToAction("Dashboard");
            }

        }

        public IActionResult SignOut()
        {
            //clear session
            HttpContext.Session.Clear();
            ViewBag.Result = "Successfully Log Out";
            return View("Index");
        }

        public IActionResult Dashboard()
        {

            //create dashboard data
            ViewBag.todaysale1 = 4;
            ViewBag.todaysale2 = 5;
            ViewBag.todaysale3 = 3;
            ViewBag.todaysale4 = 6;

            ///book list
            Books books = new Books();
            string query1 = "SELECT TOP 5 * FROM Books ORDER BY BookId DESC";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            con.Open();
            r = cmd1.ExecuteReader();
            while (r.Read())
            {
                book.Add(new Books
                {
                    BookTitle = r["BookTitle"].ToString(),
                });


            }
            ViewBag.book = book;
            con.Close();

            return View();
        }
    }
}