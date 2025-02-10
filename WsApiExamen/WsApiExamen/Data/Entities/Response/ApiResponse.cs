namespace WsApiExamen.Data.Models.Response {

    /// <summary>
    /// Api Response Model
    /// Comentarios:
    /// Con esto cumplimos la mayoria de las respuestas de salida del examen
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class ApiResponse<TObject> {
        public bool IsSuccess { get; set; }
        public List<MessageResponse> Messages { get; set; }
        public TObject? ResponseData { get; set; }

        public ApiResponse() {
            Messages = new();
        }
    }

    /// <summary>
    /// Message Class definition
    /// </summary>
    public class MessageResponse {
        public string? Message { get; set; }
        public int MessageCode { get; set; }
        public EnumTypeMessage MessageType { get; set; }
    }

    /// <summary>
    /// Enum of Message Types
    /// </summary>
    public enum EnumTypeMessage {
        Warning = 1,
        Error
    }
}
