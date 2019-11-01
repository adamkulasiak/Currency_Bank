using Database.Models;
using System.Threading.Tasks;

namespace CurrencyBank.BLL.Interfaces
{
    interface IAuth
    {
        Task<User> Login(string username, string password);
        Task<User> Register(User user, string password);
        Task<bool> UserExists(string username);
    }
}