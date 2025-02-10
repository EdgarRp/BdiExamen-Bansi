using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiexamen.Common {

    /// <summary>
    /// Constants Vars
    /// </summary>
    public class Constants {
        public const string ADD_EXAM_SP = "spAgregar";
        public const string UPDATE_EXAM_SP = "spActualizar";
        public const string DELETE_EXAM_SP = "spEliminar";
        public const string GET_EXAM_SP = "spConsultar";
        public const string GET_BY_ID_EXAM_SP = "spConsultarPorId";
        public const string APPLICATION_TYPE = "application/json";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Constants() {
            
        }
    }
}
