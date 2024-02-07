using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iBay.Entities.Models;
using iBay.Entities.Repositories;
using iBay.WebAPI.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace iBay.WebAPI.Services
{
 public class UserService : IUserService
    {
        private readonly IBasicRepository<ApplicationUser> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;

        public UserService(IBasicRepository<ApplicationUser> userRepository, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , TokenService tokenService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<AuthResponse> AuthenticateAsync(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return null;
            }

            var token = _tokenService.CreateToken(user);

            return new AuthResponse
            {
                Token = token,
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
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

        public async Task<ApplicationUser> GetUserById(int id)
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

        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
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

        public async Task<bool> UpdateUser(ApplicationUser updatedUser)
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
