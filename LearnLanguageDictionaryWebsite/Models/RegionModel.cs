using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLanguagesDictionaryWebsite.Models
{
    // Because SQL can't accept List<string> this is what we gotta do
    public class RegionModel
    {
        [Key]
        public int Key { get; set; }

        public string RegionName { get; set; }
    }
}
