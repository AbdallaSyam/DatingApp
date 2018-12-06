using System.Threading.Tasks;
using DatingApp.API.Model;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register (User User ,string password)  ;
        Task<User> Login (string UserName ,string password)  ;
        Task<bool> UserExist (string UserName)  ;
    }
}