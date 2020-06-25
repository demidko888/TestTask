using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TextP.Models
{
    public class Word
    {
        public Word(string name)
        {
            Name = name.ToLower(CultureInfo.InvariantCulture);
            Frequency = 1; 
        }

        public Word(string name, int value)
        {
            Name = name.ToLower(CultureInfo.InvariantCulture);
            Frequency = value;
        }

        [Key]
        [StringLength(GlobalSettings.MAX_WORD_LENGTH,MinimumLength =GlobalSettings.MIN_WORD_LEGTH)]
        public  string Name { get; private set; }

        public  int Frequency { get; set; } 

        public void Write()
        {
            Console.WriteLine("{0} -- {1}", this.Name, this.Frequency);
        }
    }
}
