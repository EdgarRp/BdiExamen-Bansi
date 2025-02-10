using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiexamen.Data.Models {
    /// <summary>
    /// Api Response Model
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
