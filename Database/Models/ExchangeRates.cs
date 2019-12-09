using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.Database.Models
{
    public class ExchangeRates
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Currency From { get; set; }
        public Currency To { get; set; }
        public decimal Rate { get; set; }
    }
}
