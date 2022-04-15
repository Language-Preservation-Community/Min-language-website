using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinLanguage.Models;

namespace MinLanguage.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MinLanguage.Services.user> user { get; set; }
        public DbSet<Vocabs> Vocabs { get; set; }
        public DbSet<RegionalPronunciation> RegionalPronunciation { get; set; }
        public DbSet<VocabsSuggest> VocabsSuggest { get; set; }
        public DbSet<RegionalSuggest> RegionalSuggest { get; set; }
    }
}
