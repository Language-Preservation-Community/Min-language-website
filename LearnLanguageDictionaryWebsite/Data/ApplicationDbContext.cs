using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LearnLanguagesDictionaryWebsite.Models;

namespace LearnLanguageDictionaryWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LearnLanguagesDictionaryWebsite.Models.DictionaryModel> DictionaryModel { get; set; }
    }
}
