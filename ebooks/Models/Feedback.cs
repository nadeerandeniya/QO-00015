using System.ComponentModel.DataAnnotations;

namespace ebooks.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public string Feedbacks { get; set; }

        public string Fullname { get; set; }
        public string Bookname { get; set; }

        public string Bookcover { get; set; }
        public Feedback()
        {

        }
        
    }
}
