using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MinLanguage.Services
{
    public class regionalPronunciation
    {
        // Name of the region
        [Key]
        public int key { get; set; }

        public string name { get; set; }
        public string pronunciation { get; set; }
    }

    public class Vocabs
    {
       [Key]
        public int key { get; set; }
        public string hanji { get; set; }
        public string englishTranslation { get; set; }

        public string regionUsed { get; set; }
        public string exampleSentences { get; set; }
        public string wordClass { get; set; }
        public string category { get; set; }

        public List <regionalPronunciation> regionalPronunciations  {get; set;}

        public Vocabs()
        {
            regionalPronunciations = new List<regionalPronunciation>();
        }
        
    }


}
