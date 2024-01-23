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

        [JsonPropertyName("errors")]
        [JsonPropertyOrder(2)]
        public List<object>? ErrorList { get; set; }

        public BaseResponse()
        {
            this.ErrorList = null;
        }

        /// <summary>
        /// エラー設定
        /// </summary>
        public void AddErrorList(HttpStatusCode status, string errorCode, string errorMsg)
        {
            this.Status = (int)status;
            this.ErrorList?.Add(new { code = errorCode, message = errorMsg });
        }

        /// <summary>
        /// エラー設定
        /// </summary>
        public void AddErrorList(int status, string errorCode, string errorMsg)
        {
            this.Status = status;
            this.ErrorList?.Add(new { code = errorCode, message = errorMsg });
        }
    }
}
