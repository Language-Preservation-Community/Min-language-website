using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnLanguagesWebsite.Models
{
    public class RegionalPronunciation
    {
        public int ID;
        // The Chinese characters responsible for the meaning
        public string Hanji;
        public string Pronunciation;


        public string AudioLink;
    }
}