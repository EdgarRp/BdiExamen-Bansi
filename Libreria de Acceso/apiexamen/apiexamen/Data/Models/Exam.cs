using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiexamen.Data.Models {
    /// <summary>
    /// Examen class
    /// </summary>
    public class Exam {

        /// <summary>
        /// Id Exam
        /// </summary>
        public int IdExamen { get; set; }

        /// <summary>
        /// Exam Name
        /// </summary>
        public string? Nombre { get; set; }

        /// <summary>
        /// Exam Description
        /// </summary>
        public string? Descripcion { get; set; }
    }
}
