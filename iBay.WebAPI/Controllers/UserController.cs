using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iBay.Entities.Models;
using iBay.WebAPI.Interfaces;
namespace iBay.WebAPI.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des utilisateurs.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>Récupère la liste de tous les utilisateurs.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// GET /api/User
        ///
        /// </remarks>
        /// <returns>Une liste de tous les utilisateurs.</returns>
        /// <response code="200">Retourne la liste des utilisateurs.</response>
        /// <response code="500">Si une erreur survient lors de la récupération des utilisateurs.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la récupération des utilisateurs.");
                return StatusCode(500, "Une erreur s'est produite lors de la récupération des utilisateurs.");
            }
        }
        /// <summary>Récupère un utilisateur spécifique par son identifiant.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// GET /api/User/{id}
        ///
        /// </remarks>
        /// <param name="id">L'identifiant de l'utilisateur.</param>
        /// <returns>Les détails de l'utilisateur.</returns>
        /// <response code="200">Retourne les détails de l'utilisateur.</response>
        /// <response code="404">Si l'utilisateur n'est pas trouvé.</response>
        /// <response code="500">Si une erreur survient lors de la récupération de l'utilisateur.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la récupération de l'utilisateur.");
                return StatusCode(500, "Une erreur s'est produite lors de la récupération de l'utilisateur.");
            }
        }

        /// <summary>Crée un nouvel utilisateur.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// POST /api/User
        /// {
        ///   "userId": 1,
        ///   "userName": "Sam Sam",
        ///   "email": "sam@supinfo.com",
        ///   "password": "123456",
        ///   "role": "User"
        /// }
        ///
        /// </remarks>
        /// <param name="user">Les détails du nouvel utilisateur.</param>
        /// <returns>Les détails du nouvel utilisateur créé.</returns>
        /// <response code="201">Retourne les détails du nouvel utilisateur créé.</response>
        /// <response code="400">Si les données de l'utilisateur sont invalides.</response>
        /// <response code="500">Si une erreur survient lors de la création de l'utilisateur.</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                var createdUser = await _userService.CreateUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la création de l'utilisateur.");
                return StatusCode(500, "Une erreur s'est produite lors de la création de l'utilisateur.");
            }
        }

        /// <summary>Met à jour un utilisateur existant.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// PUT /api/User/{id}
        /// {
        ///   "userId": 1,
        ///   "userName": "Sam Updated",
        ///   "email": "sam@supinfo.com",
        ///   "password": "sam123456",
        ///   "role": "User"
        /// }
        ///
        /// </remarks>
        /// <param name="id">L'identifiant de l'utilisateur à mettre à jour.</param>
        /// <param name="updatedUser">Les détails mis à jour de l'utilisateur.</param>
        /// <returns>Code de statut HTTP indiquant le résultat de la mise à jour.</returns>
        /// <response code="204">Aucun contenu, indique que la mise à jour a réussi.</response>
        /// <response code="400">Si les données de l'utilisateur sont invalides ou l'identifiant ne correspond pas.</response>
        /// <response code="403">Si l'utilisateur n'est pas autorisé à effectuer la mise à jour.</response>
        /// <response code="404">Si l'utilisateur à mettre à jour n'est pas trouvé.</response>
        /// <response code="500">Si une erreur survient lors de la mise à jour de l'utilisateur.</response>
        [HttpPut("{id}")]
        [Authorize]        
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            try
            {
                // Vérifier si l'utilisateur est autorisé à mettre à jour
                if (!User.HasClaim(c => c.Type == "UserId" && c.Value == id.ToString()))
                {
                    return Forbid();
                }

                if (id != updatedUser.UserId)
                {
                    return BadRequest("L'identifiant de l'utilisateur ne correspond pas.");
                }

                var success = await _userService.UpdateUser(updatedUser);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la mise à jour de l'utilisateur.");
                return StatusCode(500, "Une erreur s'est produite lors de la mise à jour de l'utilisateur.");
            }
        }

        /// <summary>Supprime un utilisateur.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// DELETE /api/User/{id}
        ///
        /// </remarks>
        /// <param name="id">L'identifiant de l'utilisateur à supprimer.</param>
        /// <returns>Code de statut HTTP indiquant le résultat de la suppression.</returns>
        /// <response code="204">Aucun contenu, indique que la suppression a réussi.</response>
        /// <response code="403">Si l'utilisateur n'est pas autorisé à effectuer la suppression.</response>
        /// <response code="404">Si l'utilisateur à supprimer n'est pas trouvé.</response>
        /// <response code="500">Si une erreur survient lors de la suppression de l'utilisateur.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                // Vérifier si l'utilisateur est autorisé à supprimer
                if (!User.HasClaim(c => c.Type == "UserId" && c.Value == id.ToString()))
                {
                    return Forbid();
                }

                var success = await _userService.DeleteUser(id);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la suppression de l'utilisateur.");
                return StatusCode(500, "Une erreur s'est produite lors de la suppression de l'utilisateur.");
            }
        }
    }
}
