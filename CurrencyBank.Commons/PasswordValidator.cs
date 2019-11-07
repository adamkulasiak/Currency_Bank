using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.Commons
{
    public static class PasswordValidator
    {
        /// <summary>
        /// Sprawdza czy podane hasła są identyczne
        /// </summary>
        /// <param name="pass1">Pierwsze hasło</param>
        /// <param name="pass2">Drugie hasło do porównania z pierwszym</param>
        /// <returns>true jeżeli hasła są takie same, false w przeciwnym wypadku</returns>
        public static bool ArePasswordsEquals(string pass1, string pass2)
        {
            if (pass1 == pass2)
                return true;
            else return false;
        }
    }
}
