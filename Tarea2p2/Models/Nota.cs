using System.ComponentModel.DataAnnotations;

namespace Tarea2p2.Models
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string Contenido { get; set; }
    }
}
