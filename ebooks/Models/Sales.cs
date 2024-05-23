using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using static NuGet.Packaging.PackagingConstants;

namespace ebooks.Models
{
    public class Sales
    {
        [Key]
        public int SalesId { get; set; }

        public string SalesDate { get; set; }

        public int CusId { get; set; }
        public string PaymentMethod { get; set; }
        public string TotalPrice { get; set; }
        public string Status { get; set; }

        public string Fullname { get; set; }

        public virtual ICollection<SalesItems> SalesItems { get; set; }

        List<Sales> cartsales = new List<Sales>();
        List<SalesItems> Itemlist = new List<SalesItems>();
        public Sales()
        {

        }

        public List<Sales> mycart(int id)
        {
            SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
            SqlCommand com = new SqlCommand();
            SqlDataReader r;

            string query = "SELECT * FROM Sales" +
                    " where SalesId='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            r = cmd.ExecuteReader();
            while (r.Read())
            {
                SalesItems itemdata = new SalesItems();
                Itemlist = itemdata.GetmysalesItems(id);
                cartsales.Add(new Sales()
                {
                    SalesId = id,
                    SalesDate = Convert.ToDateTime(r["SalesDate"]).ToString("yyyy/MM/dd"),
                    PaymentMethod = r["PaymentMethod"].ToString(),
                    SalesItems = Itemlist.ToList(),

                });

            }
            con.Close();
            return cartsales;
        }

        
            public List<Sales> Getitems(int id)
        {
            SqlConnection con = new SqlConnection(MainConnectionClass.ConString());
            SqlCommand com = new SqlCommand();
            SqlDataReader r;

            string query = "SELECT * FROM Sales" +
                    " where SalesId='" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            r = cmd.ExecuteReader();
            while (r.Read())
            {
                SalesItems itemdata = new SalesItems();
                Itemlist = itemdata.GetmysalesItems(id);
                cartsales.Add(new Sales()
                {
                    SalesId = id,
                    SalesDate = Convert.ToDateTime(r["SalesDate"]).ToString("yyyy/MM/dd"),
                    PaymentMethod = r["PaymentMethod"].ToString(),
                    SalesItems = Itemlist.ToList(),

                });

            }
            con.Close();
            return cartsales;
        }
    }
}
