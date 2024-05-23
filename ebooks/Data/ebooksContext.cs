using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebooks.Models;

namespace ebooks.Data
{
    //using Encapsulation
    public class ebooksContext : DbContext
    {
        public ebooksContext (DbContextOptions<ebooksContext> options)
            : base(options)
        {
        }

        public DbSet<ebooks.Models.Books> Books { get; set; } = default!;

        public DbSet<ebooks.Models.Customers>? Customers { get; set; }

        public DbSet<ebooks.Models.Feedback>? Feedback { get; set; }

        public DbSet<ebooks.Models.Sales>? Sales { get; set; }

        public DbSet<ebooks.Models.Users>? Users { get; set; }
    }
}
