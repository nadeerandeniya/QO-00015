using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ebooks.Models
{
    public class SalesItems
    {
        [Key] public int SalesItemId { get; set; }


        public int SalesId { get; set; }
        public int BookId { get; set; }
        public int Units { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }

        public string BookTitle { get; set; }

        List<SalesItems> Saleitemlist = new List<SalesItems>();
        public SalesItems()
        {

        }
        public List<SalesItems> GetmysalesItems(int id)
        {
            SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
            SqlCommand com = new SqlCommand();
            SqlDataReader r;

            string query = "SELECT * FROM SalesItems" +
                    " where SalesId='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            r = cmd.ExecuteReader();
            while (r.Read())
            {
                Books bk = new Books();
                BookTitle = bk.Getbookname((int)r["BookId"]);
                Saleitemlist.Add(new SalesItems()
                {

                    SalesItemId = (int)r["SalesItemId"],
                    SalesId = id,
                    BookId = (int)r["BookId"],
                    Units = (int)r["Units"],
                    UnitPrice = (int)decimal.Parse(r["UnitPrice"].ToString()),
                    TotalPrice = (int)decimal.Parse(r["TotalPrice"].ToString()),
                    BookTitle = BookTitle,

                });

            }
            con.Close();
            return Saleitemlist;
        }

    }
}
