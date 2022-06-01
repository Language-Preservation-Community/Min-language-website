using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLanguagesDictionaryWebsite.Models
{
    // A word can have multiple categories
    // Like it belongs to Human part, adjectives etc
    public class CategoryModel
    {
        [Key]
        public int Key { get; set; }

        public string Category { get; set; }
    }
}
