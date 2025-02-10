using apiexamen.Common;
using apiexamen.Data.Models;
using apiexamen.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace apiexamen.Data.Services {
    /// <summary>
    /// Definition Class
    /// </summary>
    public class WsApiExamenService : IWsApiExamenService{
        private readonly HttpClient _http;

       
        /// <summary>
        /// Constructor and Dependency Injection
        /// </summary>
        /// <param name="http"></param>
        public WsApiExamenService(HttpClient http) {
            _http = http;
        }


        /// <summary>
        /// Get execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Endpoint url</param>
        /// <returns>Api Response</returns>
        public async Task<ApiResponse<Exam>?> Get(string url ) {
            ApiResponse<Exam>? res = new();
            try {
                HttpResponseMessage response = await _http.GetAsync( url );
                if( response.IsSuccessStatusCode ) {
                    string json = await response.Content.ReadAsStringAsync();
                    res = JsonSerializer.Deserialize<ApiResponse<Exam>>( json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } );
                    
                }

                return res;
            } catch( Exception ex ) {
                return new() {
                    IsSuccess = false,
                    ResponseData = null,
                    Messages = [ new MessageResponse {
                        Message = ex.Message,
                        MessageCode = 500
                    } ]
                };
            }

        }


        /// <summary>
        /// Get execute list
        /// </summary>
        /// <param name="url">Endpoint url</param>
        /// <returns>Api Response</returns>
        public async Task<ApiResponse<List<Exam>>?> GetList( string url ) {
            ApiResponse<List<Exam>>? res = new();
            try {
                HttpResponseMessage response = await _http.GetAsync( url );
                if( response.IsSuccessStatusCode ) {
                    string json = await response.Content.ReadAsStringAsync();
                    res = JsonSerializer.Deserialize<ApiResponse<List<Exam>>>( json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } );

                }

                return res;
            } catch( Exception ex ) {
                return new() {
                    IsSuccess = false,
                    ResponseData = null,
                    Messages = [ new MessageResponse {
                        Message = ex.Message,
                        MessageCode = 500
                    } ]
                };
            }

        }


        /// <summary>
        /// Post Execute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Endpoint url</param>
        /// <param name="data">Body</param>
        /// <returns>ApiResponse</returns>
        public async Task<ApiResponse<Exam>?> Post(string url, object data ) {
            ApiResponse<Exam>? res = new();
            try {
                string json = JsonSerializer.Serialize( data );
                var content = new StringContent( json, Encoding.UTF8, Constants.APPLICATION_TYPE );
                HttpResponseMessage response = await _http.PostAsync( url, content );

                if( response.IsSuccessStatusCode ) {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    res = JsonSerializer.Deserialize<ApiResponse<Exam>>( jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } );
                }
                return res;

            } catch( Exception ex) {
                return new() {
                    IsSuccess = false,
                    ResponseData = null,
                    Messages = [ new MessageResponse {
                        Message = ex.Message,
                        MessageCode = 500
                    } ]
                };
            }
            
        }
    }
}
