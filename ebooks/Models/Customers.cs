﻿using System.ComponentModel.DataAnnotations;

namespace ebooks.Models
{
    public class Customers
    {

        [Key]
    public int CustomerId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string FullName { get; set; }

        public Customers()
        {

        }
    }
}
