using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.Commons
{
    public static class PeselValidator
    {
        /// <summary>
        /// Sprawdza czy podany pesel jest poprawny
        /// </summary>
        /// <param name="pesel">pesel do sprawdzenia</param>
        /// <returns>true jeżeli jest poprawny, w przeciwnym wypadku false</returns>
        public static bool IsPeselValid(string pesel)
        {
            int.TryParse(pesel, out int parsedPesel);
            if (parsedPesel == 0)
                return false;

            if (pesel.Length != 11)
                return false;

            return true;
        }
    }
}
