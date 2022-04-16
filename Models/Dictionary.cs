using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MinLanguage.Models
{
    public class RegionalPronunciation
    {
        // Name of the region
        [Key]
        public int Key { get; set; }
        public string Name { get; set; }
        public string Pronunciation { get; set; }
        public string Hanji { get; set; }
    }

    public class RegionalSuggest
    {
        public int RegionalKey { get; set; }
        public string Name { get; set; }
        public string Pronunciation { get; set; }
        public string Hanji { get; set; }
        [Key]
        public int Key { get; set; }
        public RegionalPronunciation GetRegionalPronunciation() => new RegionalPronunciation()
        {
            Key = RegionalKey,
            Name = Name,
            Pronunciation = Pronunciation,
            Hanji = Hanji
        };
        public static RegionalSuggest FromRegionalPronunciation(RegionalPronunciation regionalPronunciation) => new RegionalSuggest()
        {
            RegionalKey = regionalPronunciation.Key,
            Name = regionalPronunciation.Name,
            Pronunciation = regionalPronunciation.Pronunciation,
            Hanji = regionalPronunciation.Hanji
        };
    }

    public class Vocabs
    {
       [Key]
        public int Key { get; set; }
        public string EnglishTranslation { get; set; }
        public string RegionUsed { get; set; }
        public string ExampleSentences { get; set; }
        public string WordClass { get; set; }
        public string Category { get; set; }
        public List<RegionalPronunciation> RegionalPronunciations { get; set; }
    }

    public class VocabsSuggest
    {
        public int? VocabsKey { get; set; }
        public string EnglishTranslation { get; set; }
        public string RegionUsed { get; set; }
        public string ExampleSentences { get; set; }
        public string WordClass { get; set; }
        public string Category { get; set; }
        public List<RegionalSuggest> RegionalPronunciations { get; set; }
        [Key]
        public int Key { get; set; }
        public string UserId { get; set; }
        public Vocabs GetVocabs() => new Vocabs()
        {
            Key = VocabsKey ?? 0,
            EnglishTranslation = EnglishTranslation,
            RegionUsed = RegionUsed,
            ExampleSentences = ExampleSentences,
            WordClass = WordClass,
            Category = Category,
            RegionalPronunciations = RegionalPronunciations.ConvertAll(r => r.GetRegionalPronunciation())
        };
        public static VocabsSuggest FromVocabs(Vocabs vocabs) => new VocabsSuggest()
        {
            VocabsKey = vocabs.Key,
            EnglishTranslation = vocabs.EnglishTranslation,
            RegionUsed = vocabs.RegionUsed,
            ExampleSentences = vocabs.ExampleSentences,
            WordClass = vocabs.WordClass,
            Category = vocabs.Category,
            RegionalPronunciations = vocabs.RegionalPronunciations.ConvertAll(r => RegionalSuggest.FromRegionalPronunciation(r))
        };
    }
}
