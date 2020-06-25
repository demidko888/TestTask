namespace TextP.Models
{
    public class GlobalSettings
    {
        public const int MAX_ARGS_COUNT = 1;

        public const int MIN_WORD_LEGTH = 3;

        public const int MAX_WORD_LENGTH = 15;

        public const int MIN_WORD_FREQUENCY = 3;

        public const int MAX_WORDS_QUERY_COUNT = 5;

        public static readonly char[] SEPARATORS = new char[] { ' ','\"', '.', ',', ':', ';', '!', '?', '\'', '/', '\\', '{','}','[',']','(',')','|','"','^','<','>','+','-','_','=', '«', '»','\t','\n','\r' };
        public static bool CheckPutSettings(Word word)
        {
            return word.Name.Length >= MIN_WORD_LEGTH
                && word.Name.Length <= MAX_WORD_LENGTH
                && word.Frequency >= MIN_WORD_FREQUENCY;
        }
    }
}
