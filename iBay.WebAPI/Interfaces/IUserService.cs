using System.Collections.Generic;
using System.Threading.Tasks;
using iBay.Entities.Models;

namespace iBay.WebAPI.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(ApplicationUser user, string password);
        Task<AuthResponse> AuthenticateAsync(AuthRequest request);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetUserById(int id);
        Task<bool> UpdateUser(ApplicationUser updatedUser);
        Task<bool> DeleteUser(int id);
    }
}
