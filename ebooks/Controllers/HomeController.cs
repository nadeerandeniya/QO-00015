using ebooks.Data;
using ebooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static NuGet.Packaging.PackagingConstants;

namespace ebooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ebooksContext _context;
        private readonly ILogger<HomeController> _logger;
        List<Books> novel = new List<Books>();
        List<Books> kids = new List<Books>();
        List<Feedback> Feedback = new List<Feedback>();
        List<Sales> cartdata = new List<Sales>();

        public const string SessionKeyName = "_Name";
        public const string SessionKeyCart = "_Cart";
        public const string SessionKeyCartId = "_CartId";

       
        public int CartId { get; set; }

        //call connection class 
        SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
        SqlCommand com = new SqlCommand();
        SqlDataReader r;

        public HomeController(ILogger<HomeController> logger,ebooksContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //LINQ search algorithm
            //select by type
            var type = "Novel";
            ViewBag.novel = await _context.Books.Where(x=>x.BookType== type).OrderByDescending(x => x.BookId).Take(6).ToListAsync();
           

            //LINQ search algorithm
            //select by type
            var query2 = "Kids";
            
            ViewBag.kids = await _context.Books.Where(x => x.BookType == query2).OrderByDescending(x => x.BookId).Take(6).ToListAsync();
            con.Close();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> AllBooks()
        {
            return _context.Books != null ?
                          View(await _context.Books.OrderByDescending(x => x.BookId).ToListAsync()) :
                          Problem("Entity set 'Sydenham.Books'  is null.");
        }

        public async Task<IActionResult> Details(int? id , string? msg)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (books == null)
            {
                return NotFound();
            }
            string query = "SELECT Feedback.*,Customers.FullName FROM Feedback LEFT JOIN Customers ON" +
               " Feedback.CustomerId = Customers.CustomerId" +
                   " where BookId='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            Feedback fb = new Feedback();
            r = cmd.ExecuteReader();
            while (r.Read())
            {
                Feedback.Add(new Feedback
                {
                    Feedbacks = r["FeedBacks"].ToString(),
                    Fullname = r["FullName"].ToString(),
                });


            }
            ViewBag.Feedback = Feedback;
            ViewBag.msg = msg;
            return View(books);
        }

        public async Task<IActionResult> SearchResults()
        {
            Books bk = new Books();
            var search = HttpContext.Request.Form["search"];
            List<Books> books = new List<Books>();

            //LINQ search algorithm
            //select by type
            string query = "SELECT * FROM Books " +
            " where BookTitle LIKE '%" + search + "%' OR BookDescription LIKE '%" + search + "%' OR BookType LIKE '%" + search + "%' OR Author LIKE '%" + search + "%'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            r = cmd.ExecuteReader();
            while (r.Read())
            {
                books.Add(new Books()
                {
                    BookId = (int)r["BookId"],
                    BookTitle = r["BookTitle"].ToString(),
                    CoverImage = r["CoverImage"].ToString(),
                });
            }
            return View(books);
        }

        public async Task<IActionResult> AddCart()
        {
            Sales sl = new Sales();
            SalesItems slitem = new SalesItems();

            slitem.BookId = Convert.ToInt32(HttpContext.Request.Form["BookId"]);
            slitem.Units = Convert.ToInt32(HttpContext.Request.Form["Units"]);
            if (slitem.BookId == null || _context.Books == null)
            {
                return NotFound();
            }
            var books = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == slitem.BookId);

            var cusid = Convert.ToInt32(HttpContext.Session.GetString("_UserId"));
            int cartcount = Convert.ToInt32(HttpContext.Session.GetString("_cart"));
            var cartname = HttpContext.Session.GetString("_CartId");
            int totprice = 0;

            slitem.UnitPrice = (int)decimal.Parse(books.BookPrice);
            totprice = (int)decimal.Parse(books.BookPrice) * slitem.Units;
            slitem.TotalPrice = (int)decimal.Parse(totprice.ToString());

            DateTime today = DateTime.Today;
            if (cartname == null)
            {


                sl.SalesDate = today.ToString();
                sl.CusId = cusid;
                sl.PaymentMethod = "Cash";
                sl.Status = "Pending";

                

                string query = "INSERT INTO Sales (SalesDate,CusId,PaymentMethod,Status,TotalPrice)" +
                    "output INSERTED.SalesId VALUES ('" + sl.SalesDate + "','" + sl.CusId + "','" + sl.PaymentMethod + "','" + sl.Status + "','" + slitem.TotalPrice + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                //int i = cmd.ExecuteNonQuery();
                CartId = (int)cmd.ExecuteScalar();
                con.Close();

                HttpContext.Session.SetString(SessionKeyCartId, CartId.ToString());
            }
            else
            {
                CartId = Convert.ToInt32(HttpContext.Session.GetString("_CartId"));
            }

            slitem.SalesId = CartId;
            


            string query2 = "INSERT INTO SalesItems (SalesId,BookId,Units,UnitPrice,TotalPrice)" +
                    " VALUES ('" + slitem.SalesId + "','" + slitem.BookId + "','" + slitem.Units + "','" + slitem.UnitPrice + "','" + slitem.TotalPrice + "')";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            con.Open();
            int i2 = cmd2.ExecuteNonQuery();
            con.Close();
            if (i2 > 0)
            {
                string query3 = "SELECT SUM(Units) AS count FROM SalesItems" +
                    " where SalesId='" + slitem.SalesId + "'";
                SqlCommand cmd3 = new SqlCommand(query3, con);
                con.Open();

                r = cmd3.ExecuteReader();
                while (r.Read())
                {
                    string count = r["count"].ToString();
                    HttpContext.Session.SetString(SessionKeyCart, count);


                }
                con.Close();
            }

            var msges = "Your items successfuly added to the cart successfuly. Pleace Click Cart icon in menubar to see your cart";
            return RedirectToAction("ViewCart", new { msg = msges });
        }

        public IActionResult ViewCart(string? msg)
        {
            var cusid = Convert.ToInt32(HttpContext.Session.GetString("_UserId"));
            var cartid = Convert.ToInt32(HttpContext.Session.GetString("_CartId"));
            try
            {
                Sales sl = new Sales();
                cartdata = sl.mycart(cartid);


            }
            catch (Exception ex)
            {

                throw ex;
            }
            ViewBag.msg = msg;
            return View(cartdata);
        }

        public async Task<IActionResult> AddSale()
        {
            Sales sl = new Sales();
            Books bk = new Books();
            var cartid = Convert.ToInt32(HttpContext.Session.GetString("_CartId"));
            sl.SalesId = cartid;
            sl.TotalPrice = HttpContext.Request.Form["paytot"];
            sl.Status = "Completed";

            string query = "UPDATE Sales SET TotalPrice='" + sl.TotalPrice + "' ,Status='" + sl.Status + "' WHERE SalesId='" + sl.SalesId + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i2 = cmd.ExecuteNonQuery();
            if (i2 > 0)
            {

                cartdata = sl.mycart(cartid);
                foreach (var item in cartdata)
                {
                    foreach (var item2 in item.SalesItems)
                    {
                        var books = await _context.Books.FirstOrDefaultAsync(m => m.BookId == item2.BookId);
                        int ava = books.Availability;
                        int unit = item2.Units;
                        int nowava = ava - unit;
                        books.stockupdate(item2.BookId, nowava);
                    }

                }
            }
            con.Close();

            ViewBag.msg = "Thank you. Your order will deliver in next 3 to 5 days.";

            HttpContext.Session.Remove("_Cart");
            HttpContext.Session.Remove("_CartId");
            return View("Message");
        }

      

        public IActionResult SaveFeedback()
        {
            Feedback fb = new Feedback();
            var cusid = Convert.ToInt32(HttpContext.Session.GetString("_UserId"));
            fb.BookId = Convert.ToInt32(HttpContext.Request.Form["BookId"]);
            fb.Feedbacks = HttpContext.Request.Form["FeedBack"];
            if (fb.BookId == null || _context.Books == null)
            {
                return NotFound();
            }

            string query = "INSERT INTO Feedback (BookId,CustomerId,Feedbacks)" +
                    " VALUES ('" + fb.BookId + "','" + cusid + "','" + fb.Feedbacks + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i2 = cmd.ExecuteNonQuery();
            con.Close();
            var msges = "Feedback added successfully.";
            return RedirectToAction("Details", new { id = fb.BookId , msg= msges });
        }

        public async Task<IActionResult> ItemDelete(int? id)
        {

            string query = "DELETE  SalesItems WHERE SalesItemId='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i2 = cmd.ExecuteNonQuery();
            con.Close();
            var msges = "Item deleted successfully.";
            return RedirectToAction("ViewCart", new {msg=msges});
        }
        public IActionResult Message(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }

    }
}