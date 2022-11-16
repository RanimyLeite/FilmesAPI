using FilmesAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesApi.Models
{
    public class Gerente
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
        //Define que um gerente pode ter varios cinemas vinculados a ele
        [JsonIgnore]
        public virtual List<Cinema> Cinemas { get; set;  }
    }
}
