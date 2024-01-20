using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace mvc_api.Models.Request
{
    public class User
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} length cant'be more than {1}.")]
        [JsonPropertyName("login_name")]
        [Display(Name = "login_name")]
        public string? LoginName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} length cant'be more than {1}.")]
        [JsonPropertyName("password")]
        [Display(Name = "password")]
        public string? Password { get; set; }
    }
}
