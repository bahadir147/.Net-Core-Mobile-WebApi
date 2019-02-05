using Afrodit.Repositories.DTOs;
using Afrodit.Entities.Concrete;
using System.Threading.Tasks;

namespace Afrodit.Business.Abstract
{
    public interface IUserService
    {
        Task<UserLoginDTO> Login(string username, string password);
        Task<UserLoginDTO> Register(UserRegisterDTO registerParam);
        Task<User> GetUserById(int userId);
        Task<UserHeadersDTO> GetUserHeader(int userId);
        Task<bool> UpdateUser(UserUpdateDTO userUpdateDTO);
    }
}
