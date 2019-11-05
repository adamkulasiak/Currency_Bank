using CurrencyBank.BLL.Dtos;
using Database.Models;
using System.Threading.Tasks;

namespace CurrencyBank.BLL.Interfaces
{
    interface IAuth
    {
        Task<User> Login(string username, string password);
        Task<UserRegisterDto> Register(UserRegisterDto user, string password);
        Task<bool> UserExists(string username);
    }
}