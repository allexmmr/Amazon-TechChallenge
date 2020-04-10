using System.Collections.Generic;
using System.Linq;

namespace Competitors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string> competitors = new List<string>
            {
                "newshop",
                "shopnow",
                "afshion",
                "fashionbeats"
            };

            List<string> reviews = new List<string>
            {
                "Bla bla newshop",
                "Bla bla newshop, newshop and newshop",
                "Bla newshop bla",
                "Bla fashionbeats bla",
                "Bla fashionbeats bla and bla",
                "Bla bla afshion",
                "Bla bla afshion bla and bla"
            };

            TopNCompetitors(6, 2, competitors, 6, reviews);
        }

        public static List<string> TopNCompetitors(int numCompetitors, int topNCompetitors, List<string> competitors, int numReviews, List<string> reviews)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            foreach (string competitor in competitors.Take(numCompetitors))
            {
                dictionary.Add(competitor, 0);
            }

            foreach (string review in reviews)
            {
                // Cleaning up a bit and then create an array of words
                string[] arr = review.Replace(",", "").Replace(".", "").Split(' ');

                foreach (string word in arr) // Let's loop over the words
                {
                    if (dictionary.ContainsKey(word))
                    {
                        // Increment the count if it's in the dictionary
                        dictionary[word] = dictionary[word] + 1;
                        // Rule one competitor per review
                        break;
                    }
                }
            }

            return dictionary
                .OrderByDescending(q => q.Value)
                .ThenBy(q => q.Key)
                .Take(topNCompetitors)
                .Select(q => q.Key)
                .ToList();
        }
    }
}
