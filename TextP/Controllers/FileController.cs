using System;
using System.Collections.Generic;
using TextP.Models;

namespace TextP.Controllers
{
    public class FileController
    {
        public static List<string> ParseFile(string[] fileLines)
        {
            List<string> result = new List<string>();
            foreach(var e in fileLines)
            {
                result.AddRange(e.Split(GlobalSettings.SEPARATORS, StringSplitOptions.RemoveEmptyEntries));
            }
            return result;
        }

        public static List<Word> GenerateWordList(List<string> source)
        {
            var tmpResult = new Dictionary<string,int>();
            var result = new List<Word>();
            foreach (var e in source)
            {
                if (tmpResult.ContainsKey(e))
                    tmpResult[e]++;
                else
                    tmpResult.Add(e, 1);
            }
            foreach (var e in tmpResult)
                result.Add(new Word(e.Key, e.Value));
            return result;
        }
    }
}
