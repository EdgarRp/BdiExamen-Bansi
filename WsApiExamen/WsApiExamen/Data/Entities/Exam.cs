using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsApiExamen.Data.Models {
    /// <summary>
    /// Examen class
    /// </summary>
    public class Exam {

        /// <summary>
        /// Id Exam
        /// </summary>
        [Key]
        public int IdExamen { get; set; }

        /// <summary>
        /// Exam Name
        /// </summary>
        [MaxLength( 255 )]
        [Required]
        public string? Nombre { get; set; }

        /// <summary>
        /// Exam Description
        /// </summary>
        [MaxLength( 255 )]
        [Required]
        public string? Descripcion { get; set; }
    }
}
