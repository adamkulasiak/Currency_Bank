using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCurrencyBank.Dtos
{
    public class AccountToCreateDto
    {
        [Required]
        public Currency Currency { get; set; }
    }
}
