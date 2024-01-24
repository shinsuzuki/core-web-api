using mvc_api.Models.Response;
using System.Net;
using System.Text.Json.Serialization;

namespace mvc_api.Models.Response
{
    public class ErrorResponse: BaseResponse
    {
        [JsonPropertyName("errors")]
        [JsonPropertyOrder(2)]
        public List<object>? ErrorList { get; set; }

        public ErrorResponse(HttpStatusCode statusCode)
        {
            this.Status = (int)statusCode;
            this.ErrorList = new List<object>();
        }

        public void AddError(string errorCode, string errorMsg)
        {
            this.ErrorList?.Add(new { code = errorCode, message = errorMsg });
        }
    }
}
