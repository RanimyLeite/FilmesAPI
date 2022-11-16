using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosAPI.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] //Especifica que esse campo será uma senha
        public string Password { get; set; }

        [Required]
        [Compare("Password")] //Faz uma comparação com o valor de Password
        public string RePassword { get; set; }
    }
}
