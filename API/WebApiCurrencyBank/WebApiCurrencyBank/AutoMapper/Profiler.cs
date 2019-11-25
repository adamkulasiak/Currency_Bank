using AutoMapper;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCurrencyBank.Dtos;

namespace WebApiCurrencyBank.AutoMapper
{
    public class Profiler: Profile
    {
        public Profiler()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
        
    }
}
