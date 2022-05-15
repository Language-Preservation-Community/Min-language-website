using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnLanguagesWebsite.Models
{
    public class Dictionary
    {
        //public string LanguageName;

        // The name of the language will be the key
        // And each language will have list of vocabulary
        // When User selects Teochew, the function will try to grab Teochew and grab the List of Vocabularies
        public Dictionary<string, List<Vocabulary>> Dictionaries;
    }
}