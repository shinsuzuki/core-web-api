using System.Text.Json.Serialization;

namespace mvc_api.Models.Response
{
    public class Person
    {
        [JsonPropertyName("full_name")]
        [JsonPropertyOrder(1)]
        public string? fullName { get; set; }

        [JsonPropertyName("old")]
        [JsonPropertyOrder(2)]
        public int Old { get; set; }
    }

}
