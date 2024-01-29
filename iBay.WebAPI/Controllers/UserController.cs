using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using iBay.WebAPI.Services;
using iBay.Entities.Models;
//---- à créer ----//
using System;
//-----------------------//

namespace iBay.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET: api/User
        // Récupère la liste de tous les utilisateurs.
        [HttpGet]
        [Authorize(Roles = "Admin")] // Seul un utilisateur avec le rôle "Admin" peut récupérer tous les utilisateurs.
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la récupération des utilisateurs.");
            }
        }

        // GET: api/User/{id}
        // Récupère un utilisateur spécifique par son identifiant.
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la récupération de l'utilisateur.");
            }
        }

        // POST: api/User
        // Crée un nouvel utilisateur.
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                var createdUser = _userService.CreateUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la création de l'utilisateur.");
            }
        }

        // PUT: api/User/{id}
        // Met à jour un utilisateur existant.
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            try
            {
                if (id != updatedUser.Id)
                {
                    return BadRequest("L'identifiant de l'utilisateur ne correspond pas.");
                }

                var success = _userService.UpdateUser(updatedUser);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour de l'utilisateur.");
            }
        }

        // DELETE: api/User/{id}
        // Supprime un utilisateur.
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Seul un utilisateur avec le rôle "Admin" peut supprimer des utilisateurs.
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var success = _userService.DeleteUser(id);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la suppression de l'utilisateur.");
            }
        }
    }
}
