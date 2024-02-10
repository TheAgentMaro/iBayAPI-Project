using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBay.Entities.Models
{
    public class RegistrationRequestModel
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        public string Username { get; set; }

        [Required(ErrorMessage = "L'adresse e-mail est requise")]
        [EmailAddress(ErrorMessage = "L'adresse e-mail n'est pas valide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit comporter au moins 6 caractères")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le rôle est requis")]
        public string Role { get; set; }
    }
}
