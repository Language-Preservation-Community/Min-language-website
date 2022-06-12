using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearnLanguagesDictionaryWebsite.Models
{
    // The members are arranged in the order they will appear
    // So english meaning will appear in first column, then example sentences, the vocabs
    public class VocabularyModel
    {
        // This is for the Key
        [Key]
        public int Key { get; set; }

        public string EnglishMeaning { get; set; }

        // Word category indicating whether if it is a verb, adjective, human part
        public List<CategoryModel> WordCategory { get; set; }
        
        // Example Sentences in the language not in English
        public string ExampleSentences { get; set; }

        // In the view, we will have a set of region values to search which will act as a key to the dictionary class
        // If the key contains regional pronunciation, it will be added to the view.
        // If it doesn't, it will leave it blank
        // We won't get any errors, this way
        public List<RegionalPronunciationModel> RegionalWords { get; set; }

        // Additional note if the user want to add something to note about for certain words
        // like sentence structure or grammer is different when certain words is used in Ph Hokkien
        public string AdditionalNote { get; set; }
    }
}