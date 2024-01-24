using System.Collections.Specialized;
using System.Net;
using System.Text.Json.Serialization;

namespace mvc_api.Models.Response
{
    public class BaseResponse
    {
        [JsonPropertyName("status")]
        [JsonPropertyOrder(1)]
        public int Status { get; set; }
    }
}
