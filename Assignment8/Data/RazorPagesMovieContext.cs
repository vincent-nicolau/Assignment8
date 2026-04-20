using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment8.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Assignment8.Data
{
    public class RazorPagesMovieContext : DbContext
    {
        public RazorPagesMovieContext (DbContextOptions<RazorPagesMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment8.Models.Movie> Movie { get; set; } = default!;
    }
}
