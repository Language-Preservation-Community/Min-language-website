﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearnLanguagesDictionaryWebsite.Models
{
    // Programmer will only refer to here
    // The frontend will only need access to Dictionary and access to the data within it
    // So database will be looking like 
    // Teochew, Vocabs (likes, you, me) with regional pronunciations
    public class DictionaryModel
    {
        [Key]
        public int Key { get; set; }

        // The language name of the dictionary
        // We can't use dictionary containers since we need to parse it to SQL
        public string LanguageName { get; set; }

        // The name of the language will be the key
        // And each language will have list of vocabulary
        // When User selects Teochew, the function will try to grab Teochew and grab the List of Vocabularies
        public List<VocabularyModel> VocabulariesList { get; set; }
    }
}