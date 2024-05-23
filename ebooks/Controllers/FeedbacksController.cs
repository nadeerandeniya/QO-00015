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

namespace ebooks.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly ebooksContext _context;
        List<Feedback> Feedbacks = new List<Feedback>();
        SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
        SqlCommand com = new SqlCommand();
        SqlDataReader r;

        public FeedbacksController(ebooksContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> feedbackview(int? id)
        {
            ViewBag.booktitle = null;
            ViewBag.Bookcover = null;

            string query = "SELECT Feedback.*,Customers.Fullname,Books.BookTitle,Books.CoverImage FROM Feedback LEFT JOIN Customers ON Feedback.CustomerId = Customers.CustomerId " +
                 "LEFT JOIN Books ON Feedback.BookId = Books.BookId WHERE Feedback.BookId='" + id+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            Feedback fb = new Feedback();
            r = cmd.ExecuteReader();
            while (r.Read())
            {
                Feedbacks.Add(new Feedback
                {
                    FeedbackId = (int)r["FeedbackId"],
                    BookId = (int)r["BookId"],
                    Fullname = r["Fullname"].ToString(),
                    Bookname = r["BookTitle"].ToString(),
                    Feedbacks = r["Feedbacks"].ToString(),
                    Bookcover= r["CoverImage"].ToString(),
                });

                ViewBag.booktitle = r["BookTitle"].ToString();
                ViewBag.Bookcover = r["CoverImage"].ToString();
            }
            ViewBag.Feedbacks = Feedbacks;
            con.Close();
            return View();
        }

        public async Task<IActionResult> Delete(int? id, int? bookid)
        {

            string query = "DELETE  Feedback WHERE FeedbackId='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i2 = cmd.ExecuteNonQuery();
            con.Close();
            var msges = "Item deleted successfully.";
            return RedirectToAction("feedbackview",new {id= bookid });
        }

    }
}
