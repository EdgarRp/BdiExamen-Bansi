using apiexamen.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiexamen.Data.Services.Interfaces {

    /// <summary>
    /// Interface Definition
    /// </summary>
    public interface IWsApiExamenService {

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Endpoint url</param>
        /// <returns>ApiResponse Exam</returns>
        public Task<ApiResponse<Exam>?> Get( string url );

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>ApiResponse Exam List</returns>
        public Task<ApiResponse<List<Exam>>?> GetList( string url );

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Endpoint url</param>
        /// <param name="data">Body</param>
        /// <returns>ApiResponse Exam</returns>
        public Task<ApiResponse<Exam>?> Post( string url, object data );
    }
}
