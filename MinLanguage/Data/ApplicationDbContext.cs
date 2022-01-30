using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MinLanguage.Services;

namespace MinLanguage.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MinLanguage.Services.user> user { get; set; }
        public DbSet<MinLanguage.Services.Vocabs> Vocabs { get; set; }
    }
}
