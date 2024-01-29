using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iBay.Entities.Models;
using iBay.Entities.Repositories;
using iBay.WebAPI.Interfaces;

namespace iBay.WebAPI.Services
{
 public class UserService : IUserService
    {
        private readonly IBasicRepository<User> _userRepository;

        public UserService(IBasicRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la récupération des utilisateurs.", ex);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _userRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la récupération de l'utilisateur.", ex);
            }
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la création de l'utilisateur.", ex);
            }
        }

        public async Task<bool> UpdateUser(User updatedUser)
        {
            try
            {
                await _userRepository.UpdateAsync(updatedUser);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la mise à jour de l'utilisateur.", ex);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return false;
                }

                await _userRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la suppression de l'utilisateur.", ex);
            }
        }
    }
}
