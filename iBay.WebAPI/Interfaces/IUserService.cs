using System.Collections.Generic;
using System.Threading.Tasks;
using iBay.Entities.Models;

namespace iBay.WebAPI.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> CreateUser(User user);
        Task<bool> UpdateUser(User updatedUser);
        Task<bool> DeleteUser(int id);
    }
}
