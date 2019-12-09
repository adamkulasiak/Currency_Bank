using AutoMapper;
using CurrencyBank.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyBank.API.Dtos;

namespace CurrencyBank.API.AutoMapper
{
    public class Profiler: Profile
    {
        public Profiler()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<Account, AccountToCreateDto>().ReverseMap();
        }
        
    }
}
