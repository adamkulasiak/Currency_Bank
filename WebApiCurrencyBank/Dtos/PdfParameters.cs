using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyBank.API.Dtos
{
    public class PdfParameters
    {
        public string Filename { get; set; }
        public bool SaveToDefinedPath { get; set; }
    }
}
