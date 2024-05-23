using System.ComponentModel.DataAnnotations;

namespace ebooks.Models
{
    public class Users
    {
        [Key] public int UserId { get; set; }


        public string UserName { get; set; }
        public string Password { get; set; }
        public string Userrole { get; set; }

        public Users()
        {

        }

    }
}
