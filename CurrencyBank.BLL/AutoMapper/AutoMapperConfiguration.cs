using AutoMapper;
using CurrencyBank.BLL.Dtos;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBank.BLL.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public AutoMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserRegisterDto>();
            });
        }
         
    }
}
