using ebooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static NuGet.Packaging.PackagingConstants;

namespace ebooks.Controllers
{
    public class ReportController : Controller
    {
        SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
        SqlCommand com = new SqlCommand();
        SqlDataReader r;

        List<Customers> cus = new List<Customers>();
        List<Books> book = new List<Books>();
        List<Sales> sales = new List<Sales>();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewReport()
        {
            var fromdate = HttpContext.Request.Form["sdate"];
            var todate = HttpContext.Request.Form["edate"];
            var report = HttpContext.Request.Form["report"];
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;
            ViewBag.report = report;
            if (report == "Customer List")
            {
                Customers customers = new Customers();
                string query = "SELECT Customers.* FROM Customers ORDER BY Customers.CustomerId DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    cus.Add(new Customers
                    {
                        FullName = r["FullName"].ToString(),
                        Phone = r["Phone"].ToString(),
                        Email = r["Email"].ToString(),
                        Address = r["Address"].ToString(),
                    });

                }
                //cus.Add(customers);
                ViewBag.cus = cus;
                con.Close();
            }
            else if (report == "Book List")
            {
                Books books = new Books();
                string query = "SELECT * FROM Books ORDER BY BookId DESC";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    book.Add(new Books
                    {
                        BookTitle = r["BookTitle"].ToString(),
                        Author = r["Author"].ToString(),
                        Publisher = r["Publisher"].ToString(),
                        BookPrice = r["BookPrice"].ToString(),
                        Availability = (int)r["Availability"],
                    });


                }
                ViewBag.book = book;
                con.Close();
            }
            else if (report == "Sales List")
            {
                Sales sale = new Sales();
                string query = "SELECT Sales.*,Customers.Fullname FROM Sales LEFT JOIN Customers ON Sales.CusId = Customers.CustomerId";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                r = cmd.ExecuteReader();
                while (r.Read())
                {
                    sales.Add(new Sales
                    {
                        SalesId = (int)r["SalesId"],
                        SalesDate = r["SalesDate"].ToString(),
                        TotalPrice = r["TotalPrice"].ToString(),
                        Fullname = r["Fullname"].ToString(),
                        PaymentMethod = r["PaymentMethod"].ToString(),
                    });


                }
                ViewBag.sales = sales;
                con.Close();
            }
            return View();
        }

    }
}
