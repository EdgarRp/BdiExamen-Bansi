using apiexamen.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiexamen.Data.Repositories.Interfaces {
    /// <summary>
    /// Interface Definition
    /// </summary>
    public interface IStoredProcedureRepository  {

        /// <summary>
        /// Execute Store Procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName">Store Procedure Name</param>
        /// <param name="parameters">PArams</param>
        /// <returns>ApiResponse</returns>
        public Task<List<Dictionary<string, object>>?> ExecuteStoredProcedureAsync( string storedProcedureName, Dictionary<string, object> parameters, string connetion );


        /// <summary>
        /// Parse Dictionary to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">Data row</param>
        /// <returns></returns>
        public T ParseToObject<T>( Dictionary<string, object>? row ) where T : new();

        /// <summary>
        /// Parse Dictionary List to List Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dynamicList"></param>
        /// <returns></returns>
        public List<T> ParseToList<T>( List<Dictionary<string, object>> dynamicList ) where T : new();

    }
}
