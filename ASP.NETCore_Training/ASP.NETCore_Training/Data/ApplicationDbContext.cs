using System;
using System.Collections.Generic;
using System.Text;
using ASP.NETCore_Training.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCore_Training.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductTypes> ProductTypes { get; set; }
    }
}
