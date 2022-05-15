using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnLanguagesWebsite.Models
{
    // Programmer will only refer to here

    // So database will be looking like 
    // Teochew, Vocabs (likes, you, me) with regional pronunciations
    public class Dictionary
    {
        //public string LanguageName;

        // The name of the language will be the key
        // And each language will have list of vocabulary
        // When User selects Teochew, the function will try to grab Teochew and grab the List of Vocabularies
        public Dictionary<string, List<Vocabulary>> Dictionaries;
    }
}