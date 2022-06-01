using LearnLanguageDictionaryWebsite.Data;
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
        private readonly ApplicationDbContext _context;

        // Constructor
        public DictionaryController(ApplicationDbContext context)
        {
            // var dictionary = new DictionaryModel();
            _context = context;
        }

        // We show the list of dictionaries we have
        public IActionResult Index()
        {
            return View(_context.DictionaryModel.ToList());
        }

        // Just a test on putting an own made model to make sure it display
        public IActionResult Random()
        {
            // the code below is just for testing
            var dictionary = new DictionaryModel() { 
                LanguageName = AllAvailableLanguages.Hokkien.ToString()
            };

            var allRegions = new List<RegionModel>() { 
                new RegionModel() { RegionName="RegionA" }, 
                new RegionModel() {RegionName= "RegionB" }, 
                new RegionModel() {RegionName="RegionC" } };

            var regionalPronunciationA = new List<RegionalPronunciationModel>();
            var regionalPronunciationB = new List<RegionalPronunciationModel>();

            var wordCategoryA = new List<CategoryModel>() { new CategoryModel() { Category = "verb" } };
            var wordCategoryB = new List<CategoryModel>() { new CategoryModel() { Category = "adjective" } };

            // Regional Pronunciation for vocab A
            for (int i=0; i< allRegions.Count; i++ )
            {
                regionalPronunciationA.Add(new RegionalPronunciationModel() { Hanji = "哭", Pronunciation="khau"+i });
                regionalPronunciationB.Add(new RegionalPronunciationModel() { Hanji = "哭", Pronunciation = "hau" + i*2 });
            }

            var vocabularyA = new VocabularyModel() 
            { AdditionalNote = "", EnglishMeaning = "VocabA", ExampleSentences = "哭父，恁父母無共汝是毋是?", AllRegion = allRegions, WordCategory = wordCategoryA, RegionalWords = regionalPronunciationA};
            var vocabularyB = new VocabularyModel() 
            { AdditionalNote = "", EnglishMeaning ="VocabB", ExampleSentences= "伊个頭毛足歹看", AllRegion=allRegions, WordCategory = wordCategoryB, RegionalWords= regionalPronunciationB};

            var vocabularyList = new List<VocabularyModel>();

            vocabularyList.Add(vocabularyA);
            vocabularyList.Add(vocabularyB);

            dictionary.VocabulariesList = vocabularyList;

            return View("ViewDictionary" , dictionary);
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
