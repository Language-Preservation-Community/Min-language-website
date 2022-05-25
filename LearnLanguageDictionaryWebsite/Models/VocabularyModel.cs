using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnLanguagesDictionaryWebsite.Models
{
    // The members are arranged in the order they will appear
    // So english meaning will appear in first column, then example sentences, the vocabs
    public class VocabularyModel
    {
        // This is for the Key
        public int ID;

        public string EnglishMeaning;
        public string ExampleSentences;

        // In the view, we will have a set of region values to search which will act as a key to the dictionary class
        // If the key contains regional pronunciation, it will be added to the view.
        // If it doesn't, it will leave it blank
        // We won't get any errors, this way
        public Dictionary<string, RegionalPronunciationModel> RegionalWords;

        // Additional note if the user want to add something to note about for certain words
        // like sentence structure or grammer is different when certain words is used in Ph Hokkien
        public string AdditionalNote;
    }
}