using System;
using System.Linq;

namespace Day3
{
    class Pesel
    {
        public string Value { get;}

        public Pesel(string pesel)
        {
            if(Utilities.IsNull(pesel))
            {
                throw new ArgumentException(nameof(pesel));
            }

            if (pesel.Length != 11 || !pesel.All(char.IsDigit))     // sprawdza nie tylko numeryczne znaki ASCII
            {
                throw new ArgumentException($"Invalid argument: {nameof(pesel)}.");
            }
            
            Value = pesel;
        }
    }
}
