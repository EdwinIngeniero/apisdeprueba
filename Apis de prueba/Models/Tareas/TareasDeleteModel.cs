using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apis_de_prueba.Models
{
    public class TareasDeleteModel
    {
        [Key]
        public int Codigotarea { get; set; }
        public string Mitarea { get; set; } = string.Empty;
        public DateTime Fechainicio { get; set; } = DateTime.Now;
        public string Estado { get; set; } = string.Empty;

        [NotMapped]
        public int DiasActivos
        {
            get
            {
                return (DateTime.Now - Fechainicio).Days;
            }
        }
    }
}
