using System;
using System.Collections.Generic;
using TextP.Models;
using System.Linq;

namespace TextP.Logic
{
    public static class Requests
    {
        public static List<Word> TakeRequest(DBWords db,string partOrValue)
        {
            if (db!=null)
            {
                return db.Words
                .Where(x => x.Name.StartsWith(partOrValue))
                .OrderByDescending(w=>w.Frequency)
                .ThenBy(y=>y.Name)
                .Take(GlobalSettings.MAX_WORDS_QUERY_COUNT)
                .ToList();
            }
            return new List<Word>();
        }

        public static List<Word> GenerateUpdateRequestWithGlobalOptions(List<Word> source)
        {
            return source.Where(x => GlobalSettings.CheckPutSettings(x)).ToList();
        }

        public static void PrintRequest(List<Word> result)
        {
            for(var i = 0; i < result.Count; i++)
                Console.WriteLine(result[i].Name);
        }
    }
}
