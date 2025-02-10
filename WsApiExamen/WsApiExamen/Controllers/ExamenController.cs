using Azure;
using Microsoft.AspNetCore.Mvc;
using WsApiExamen.Data.Models;
using WsApiExamen.Data.Models.Response;
using WsApiExamen.Data.Repositories.Interface;

namespace WsApiExamen.Controllers {

    /// <summary>
    /// Controller class definition
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("[controller]")]
    public class ExamenController : ControllerBase {

        private readonly IExamManager _repository;

        /// <summary>
        /// Contructor and Dependency Injection
        /// </summary>
        /// <param name="repository">ExamenRepository</param>
        public ExamenController(IExamManager repository) {
            _repository = repository;
        }

        /// <summary>
        /// Add Exam
        /// </summary>
        /// <returns>Api Response Exam</returns>
        [HttpPost("AgregarExamen", Name = "AgregarExamen")]
        [ProducesResponseType( typeof (ApiResponse<Exam>), StatusCodes.Status200OK )]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<Exam>> Add(Exam model) {
            ApiResponse<Exam> response = new ();

            try {
                var result = await _repository.AddExam( model );
                
                response.IsSuccess = true;
                response.Messages = [new MessageResponse { Message = "Success", MessageCode = StatusCodes.Status200OK }];
                
                response.ResponseData = result;
            } catch( Exception ex) {
                response.IsSuccess = false;
                response.Messages = [ new MessageResponse {
                    Message = ex.Message,
                    MessageCode = StatusCodes.Status500InternalServerError
                } ];
            }
            return response;
        }

        /// <summary>
        /// Update Exam
        /// </summary>
        /// <returns>Api Response</returns>
        [HttpPost( "ActualizarExamen", Name = "ActualizarExamen" )]
        [ProducesResponseType( typeof( ApiResponse<Exam> ), StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<ApiResponse<Exam>> Update(Exam model) {
            ApiResponse<Exam> response = new();

            try {
                response.IsSuccess = await _repository.UpdateExam( model );
                response.Messages = [new MessageResponse { Message = "Success", MessageCode = StatusCodes.Status200OK }];
            } catch( Exception ex ) {
                response.IsSuccess = false;
                response.Messages = [ new MessageResponse {
                    Message = ex.Message,
                    MessageCode = StatusCodes.Status500InternalServerError
                } ];
            }
            return response;
        }

        /// <summary>
        /// Delete Exam
        /// </summary>
        /// <param name="Id">Id Entity</param>
        /// <returns>Api Response</returns>
        [HttpGet( "EliminarExamen", Name = "EliminarExamen" )]
        [ProducesResponseType( typeof(ApiResponse<Exam> ), StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<ApiResponse<Exam>> Delete(int Id) {
            ApiResponse<Exam> response = new();

            try {
                response.IsSuccess = await _repository.DeleteExam( Id );
                response.Messages = [new MessageResponse { Message = "Success", MessageCode = StatusCodes.Status200OK }];
            } catch( Exception ex ) {
                response.IsSuccess = false;
                response.Messages = [ new MessageResponse {
                    Message = ex.Message,
                    MessageCode = StatusCodes.Status500InternalServerError
                } ];
            }
            return response;
        }

        [HttpGet("ConsultarExamen", Name = "ConsultarExamen")]
        [ProducesResponseType( typeof(ApiResponse<Exam> ), StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<ApiResponse<Exam>> GetById(int Id) {
            ApiResponse<Exam> response = new();

            try {
                response.ResponseData = await _repository.GetById(Id);

                response.IsSuccess = true;
                response.Messages = [new MessageResponse { Message = "Success", MessageCode = StatusCodes.Status200OK }];
            } catch( Exception ex ) {
                response.IsSuccess = false;
                response.Messages = [ new MessageResponse {
                    Message = ex.Message,
                    MessageCode = StatusCodes.Status500InternalServerError
                } ];
            }
            return response;
        }

        [HttpGet( "ConsultarExamenes", Name = "ConsultarExamenes" )]
        [ProducesResponseType( typeof( ApiResponse<List<Exam>> ), StatusCodes.Status200OK )]
        [ProducesResponseType( StatusCodes.Status500InternalServerError )]
        [ProducesResponseType( StatusCodes.Status400BadRequest )]
        public async Task<ApiResponse<List<Exam>>> GetExams(string Name, string Description) {
            ApiResponse<List<Exam>> response = new();

            try {
                var result = await _repository.Get( Name, Description );

                response.ResponseData = result.ToList();
                response.IsSuccess = true;
                response.Messages = [new MessageResponse { Message = "Success", MessageCode = StatusCodes.Status200OK }];
            } catch( Exception ex ) {
                response.IsSuccess = false;
                response.Messages = [ new MessageResponse {
                    Message = ex.Message,
                    MessageCode = StatusCodes.Status500InternalServerError
                } ];
            }
            return response;
        }

    }
}
