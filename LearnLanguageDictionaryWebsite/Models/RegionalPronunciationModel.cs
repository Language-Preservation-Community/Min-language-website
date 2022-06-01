using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearnLanguagesDictionaryWebsite.Models
{
    public class RegionalPronunciationModel
    {
        [Key]
        public int ID { get; set; }
        // The Chinese characters responsible for the meaning
        public string Hanji { get; set; }
        public string Pronunciation { get; set; }

        // The audio link will be in relative path format to the folder of the hosting website instead of
        // putting the audio directly in database
        public string AudioLink { get; set; }
    }
}