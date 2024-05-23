using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ebooks.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string BookTitle { get; set; }
        public string BookYear { get; set; }
        public string BookType { get; set; }
        public string BookPrice { get; set; }
        public string BookDescription { get; set; }
        public string Status { get; set; }
        public int Availability { get; set; }
        public string CoverImage { get; set; }

        public Books()
        {

        }
        public String Getbookname(int Id)
        {
            SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
            SqlCommand com = new SqlCommand();
            SqlDataReader r;

            string query = "SELECT * FROM Books" +
                    " where BookId='" + Id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            r = cmd.ExecuteReader();
            while (r.Read())
            {
                BookTitle = r["BookTitle"].ToString();
            }



            return BookTitle;
        }
        public bool stockupdate(int id, int avalabity)
        {
            SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
            SqlCommand com = new SqlCommand();
            SqlDataReader r;

            string query = "UPDATE Books SET Availability='" + avalabity + "' WHERE BookId='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i2 = cmd.ExecuteNonQuery();
            con.Close();
            if (i2 > 0)
            {


                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
