using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class ExchangeRates
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Currency Currency { get; set; }
        public decimal Mid { get; set; }
    }
}
