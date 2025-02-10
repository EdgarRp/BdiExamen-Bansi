using apiexamen.Common;
using apiexamen.Data.Models;
using apiexamen.Data.Repositories;
using apiexamen.Data.Repositories.Interfaces;
using apiexamen.Data.Services;
using apiexamen.Data.Services.Interfaces;
using Azure;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace apiexamen {
    public class ClsExamen
    {
        private readonly IStoredProcedureRepository _stored;
        private readonly IWsApiExamenService _service;
        private readonly HttpClient _http;
        private readonly bool _useAPI;
        private readonly string _wsService;
        private readonly List<string> _wsEndpoints;
        private readonly string _connection;

        /// <summary>
        /// By Simple Apps with App.Config files
        /// </summary>
        /// <param name="http"></param>
        public ClsExamen(HttpClient http) {
            _http = http;
            _wsService = ConfigurationManager.AppSettings["WsApiService"]!;
            _useAPI = bool.Parse( ConfigurationManager.AppSettings["UseAPI"] ?? "false" );
            _stored = new StoredProcedureRepository(); 
            _service = new WsApiExamenService(_http);
        }


        /// <summary>
        /// By WebApps with appSettins.json files
        /// </summary>
        /// <param name="http"></param>
        /// <param name="wsApiService"></param>
        /// <param name="connectionString"></param>
        /// <param name="UseAPI"></param>
        /// <param name="endpoints"></param>
        public ClsExamen( HttpClient http, string wsApiService, string connectionString, bool UseAPI, List<string> endpoints) {
            _wsService = wsApiService;
            _useAPI = UseAPI;
            _wsEndpoints = endpoints;
            _connection = connectionString;
            _http = http;
            _stored = new StoredProcedureRepository();
            _service = new WsApiExamenService( _http );
        }

        /// <summary>
        /// Add Exam from WsApiExam or StoredProcedure
        /// </summary>
        /// <param name="exam">Model</param>
        /// <returns></returns>
        public async Task<ApiResponse<Exam>?> AgregarExamen(Exam exam) {
            if( _useAPI ) {
                var endpoint = ConfigurationManager.AppSettings["ENDPOINT_ADD_EXAM"] ?? _wsEndpoints[0];
                return await _service.Post( _wsService + endpoint, exam );
            } else {
                ApiResponse<Exam> response = new ();
                var parameters = new Dictionary<string, object>{
                    { "@Id", exam.IdExamen },
                    { "@Nombre", exam.Nombre!},
                    { "@Descripcion", exam.Descripcion!}
                };
                var result = await _stored.ExecuteStoredProcedureAsync( Constants.ADD_EXAM_SP, parameters, _connection );
                if(result != null ) {
                    var parse = _stored.ParseToObject<StoredProcedureResponse>( result.FirstOrDefault() );
                    response.IsSuccess = parse.Code == 0;
                    response.Messages = [ new MessageResponse {
                        Message = parse.Message,
                        MessageCode = parse.Code
                    } ];
                   
                }
                return response;
            }
        }

        /// <summary>
        /// Update Exam from WsApiExam or StoredProcedure
        /// </summary>
        /// <param name="exam">Model</param>
        /// <returns></returns>
        public async Task<ApiResponse<Exam>?> ActualizarExamen( Exam exam ) {
            if( _useAPI ) {
                var endpoint = ConfigurationManager.AppSettings["ENDPOINT_UPDATE_EXAM"] ?? _wsEndpoints[1];
                return await _service.Post( _wsService + endpoint, exam );
            } else {
                ApiResponse<Exam> response = new();
                var parameters = new Dictionary<string, object>{
                    { "@Id", exam.IdExamen },
                    { "@Nombre", exam.Nombre!},
                    { "@Descripcion", exam.Descripcion!}
                };
                var result = await _stored.ExecuteStoredProcedureAsync( Constants.UPDATE_EXAM_SP, parameters, _connection );
                if( result != null ) {
                    var parse = _stored.ParseToObject<StoredProcedureResponse>( result.FirstOrDefault() );
                    response.IsSuccess = parse.Code == 0;
                    response.Messages = [ new MessageResponse {
                        Message = parse.Message,
                        MessageCode = parse.Code
                    } ];

                }
                return response;
            }
        }

        /// <summary>
        /// Delete Exam from WsApiExam or StoredProcedure
        /// </summary>
        /// <param name="id">Id Exam</param>
        /// <returns></returns>
        public async Task<ApiResponse<Exam>?> EliminarExamen( int id) {
            ApiResponse<Exam> response = new();
            try {
                if( _useAPI ) {
                    var endpoint = ConfigurationManager.AppSettings["ENDPOINT_DELETE_EXAM"] ?? _wsEndpoints[2];
                    return await _service.Get( $"{_wsService}{endpoint}?Id={id}" );
                } else {
                    
                    var parameters = new Dictionary<string, object>{
                    { "@Id", id }
                };
                    var result = await _stored.ExecuteStoredProcedureAsync( Constants.DELETE_EXAM_SP, parameters, _connection );
                    if( result != null ) {
                        var parse = _stored.ParseToObject<StoredProcedureResponse>( result.FirstOrDefault() );
                        response.IsSuccess = parse.Code == 0;
                        response.Messages = [ new MessageResponse {
                        Message = parse.Message,
                        MessageCode = parse.Code
                    } ];

                    }
                    return response;
                }
            } catch( Exception ex) {
                response.IsSuccess = false;
                response.Messages = [ new MessageResponse {
                    Message = ex.Message,
                    MessageCode = 500
                } ];

                return response;
            }
            
        }

        /// <summary>
        /// Get Exam from WsApiExam or StoredProcedure
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<Exam>?> ConsultarExamen( int id ) {
            ApiResponse<Exam> response = new();
            try {
                if( _useAPI ) {
                    var endpoint = ConfigurationManager.AppSettings["ENDPOINT_GET_EXAM"] ?? _wsEndpoints[3];
                    return await _service.Get( $"{_wsService}{endpoint}?Id={id}" );
                } else {
                    var parameters = new Dictionary<string, object>{
                    { "@Id", id }
                };
                    var result = await _stored.ExecuteStoredProcedureAsync( Constants.GET_BY_ID_EXAM_SP, parameters, _connection );
                    if( result != null ) {
                        var parse = _stored.ParseToObject<Exam>( result.FirstOrDefault() );
                        response.IsSuccess = true;
                        response.ResponseData = parse;
                        response.Messages = [ new MessageResponse {
                        MessageCode = 200
                    } ];

                    }
                    return response;
                }
            } catch( Exception ex) {
                response.IsSuccess = false;
                response.Messages = [ new MessageResponse {
                    Message = ex.Message,
                    MessageCode = 500
                } ];

                return response;
            }
            
        }

        /// <summary>
        /// Get Exam List from WsApiExam or StoredProcedure
        /// </summary>
        /// <param name="name">Nombre</param>
        /// <param name="description">Descripcion</param>
        /// <returns></returns>
        public async Task<ApiResponse<List<Exam>>?> ConsultarExamenes( string name, string description ) {
            ApiResponse<List<Exam>> response = new();
            try {
                if( _useAPI ) {
                    var endpoint = ConfigurationManager.AppSettings["ENDPOINT_GET_EXAMS"] ?? _wsEndpoints[4];
                    return await _service.GetList( $"{_wsService}{endpoint}?Name={name}&Description={description}" );
                } else {

                    var parameters = new Dictionary<string, object> {
                    { "@Nombre", name!},
                    { "@Descripcion", description!}
                };
                    var result = await _stored.ExecuteStoredProcedureAsync( Constants.GET_EXAM_SP, parameters, _connection );
                    if( result != null ) {
                        var parse = _stored.ParseToList<Exam>( result );
                        response.IsSuccess = true;
                        response.ResponseData = parse;
                        response.Messages = [ new MessageResponse {
                        MessageCode = 200
                    } ];

                    }
                    return response;
                }
            } catch( Exception ex) {
                response.IsSuccess = false;
                response.Messages = [ new MessageResponse {
                    Message = ex.Message,
                    MessageCode = 500
                } ];

                return response;
            }
            
        }


    }
}
