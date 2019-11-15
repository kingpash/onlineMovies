using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieBookingApp.Models;
using MovieBookingApp.Models.viewModels;

namespace MovieBookingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BookingTable> BookingTable { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<MovieDetails> MovieDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            base.OnModelCreating(ModelBuilder);
        }

        public DbSet<MovieBookingApp.Models.viewModels.MovieDetailViewmodel> MovieDetailViewmodel { get; set; }

        public DbSet<MovieBookingApp.Models.ApplicationUser> ApplicationUser { get; set; }

    }
}

