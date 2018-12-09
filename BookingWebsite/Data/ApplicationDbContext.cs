using System;
using System.Collections.Generic;
using System.Text;
using BookingWebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //add model to database - passing ProductTypes to DBSet object 
        public DbSet<ProductTypes> ProductTypes { get; set; }

        // Add model for tags to database - passing tags to DBSet object

    }
}
