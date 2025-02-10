using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiexamen.Data.Models {

    /// <summary>
    /// Definition Class
    /// </summary>
    public class StoredProcedureResponse {

        /// <summary>
        /// Get Code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Get Message
        /// </summary>
        public string? Message { get; set; }
    }
}
