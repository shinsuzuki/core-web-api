using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace mvc_api.Models.Request
{
    [CustomValidation(typeof(User), "CheckNameAndPassBakaWord")]
    public class User
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} length cant'be more than {1}.")]
        [JsonPropertyName("login_name")]
        [Display(Name = "login_name")]
        [CustomValidation(typeof(User), "CheckNgWord")]
        public string? LoginName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} length cant'be more than {1}.")]
        [JsonPropertyName("password")]
        [Display(Name = "password")]
        public string? Password { get; set; }

        
       /// <summary>
       /// 独自検証
       /// </summary>
        public static ValidationResult CheckNgWord(string? name)
        {
            if (name?.ToLower().Contains("ngword") ?? false)
            {
                return new ValidationResult("NGワードあり");
            }

            return ValidationResult.Success!;
        }

        /// <summary>
        /// 複数プロパティ検証
        /// </summary>
        public static ValidationResult CheckNameAndPassBakaWord (User? user)
        {
            if ((user?.LoginName?.ToLower().Contains("bakaword") ?? false)
                    && (user.Password?.ToLower().Contains("bakaword") ?? false))
            {
                return new ValidationResult("NGワードあり",new List<string> { "loginName, Password"});
            }

            return ValidationResult.Success!;
        }
    }
}
