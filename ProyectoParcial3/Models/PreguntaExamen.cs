using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoParcial3.Models
{
    [Table("PreguntaExamen")]
    public class PreguntaExamen
    {
        public int ID { get; set; }
        
 
        [ForeignKey("Examen")]
        public int? ExamenID { get; set; }
        public Examen Examen { get; set; }

      
        [ForeignKey("Pregunta")]
        public int? PreguntaID { get; set; }
        public Pregunta Pregunta { get; set; }
    }
}