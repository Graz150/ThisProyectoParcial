using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ProyectoParcial3.Models
{
 

    [Table("Competencia")]
    public class Competencia
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Competencia")]
        public string Nombre { get; set; }
    }
}