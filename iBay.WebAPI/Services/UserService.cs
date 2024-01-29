using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iBay.Entities.Models;
using iBay.Entities.Repositories;
using iBay.WebAPI.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace iBay.WebAPI.Services
{
 public class UserService : IUserService
    {
        private readonly IBasicRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IBasicRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Issuer").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration.GetSection("Jwt:Issuer").Value));

            var token = new JwtSecurityToken(
                _configuration.GetSection("Jwt:Issuer").Value,
                _configuration.GetSection("Jwt:Issuer").Value,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            try
            {
                // Logique d'authentification ici, par exemple, vérifier les informations d'identification dans la base de données
                var user = await _userRepository.GetSingleAsync(u => u.Email == email && u.Password == password);

                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de l'authentification de l'utilisateur.", ex);
            }
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
