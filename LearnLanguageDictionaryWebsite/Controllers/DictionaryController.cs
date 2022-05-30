using LearnLanguagesDictionaryWebsite.Models;
using LearnLanguagesDictionaryWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLanguagesDictionaryWebsite.Controllers
{
    public class DictionaryController : Controller
    {
        // Constructor
        public DictionaryController()
        {
            var dictionary = new DictionaryModel();
        }

        // We show the list of dictionaries we have
        public IActionResult Index()
        {
            // the code below is just for testing
            var dictionary = new DictionaryModel();


            var vocabularyA = new VocabularyModel() { EnglishMeaning = "VocabA"};
            var vocabularyB = new VocabularyModel();

            var vocabularyList = new List<VocabularyModel>();

            dictionary.Dictionaries.Add(AllAvailableLanguages.Hokkien.ToString(), vocabularyList);

            return View();
        }

        // Viewing all the dictionary
        // We might not need this
        public ActionResult ViewDictionary(string language)
        {
            return View();
        }

        // Viewing the vocabs
        // This will lead to each individual vocabs
        // We only need the key to reach the ID
        public ActionResult ViewDictionary(string language, int id)
        {
            return View();
        }

        // Searching the Dictionary
        // Dictionary/language?=Hokkien+englishMeaning?=Default+hanji?=Default+?pronunciation=?ah1
        public ActionResult SearchDictionary(string language, string englishMeaning, string hanji, string pronunciation)
        {
            if (!String.IsNullOrEmpty(englishMeaning))
            {

            }

            if (!String.IsNullOrEmpty(hanji))
            {

            }

            if (!String.IsNullOrEmpty(pronunciation))
            {

            }

            // We will return a result view here
            return View();
        }

        [HttpPost]
        public ActionResult SuggestVocabulary(VocabularyModel vocabulary)
        {


            return View();
        }

    }
}
