using System.Text.Json.Serialization;

namespace mvc_api.Models.Response
{
    public class ResponsePerson: BaseResponse
    {
        [JsonPropertyName("full_name")]
        [JsonPropertyOrder(10)]
        public string? fullName { get; set; }

        [JsonPropertyName("old")]
        [JsonPropertyOrder(20)]
        public int Old { get; set; }
    }

}
