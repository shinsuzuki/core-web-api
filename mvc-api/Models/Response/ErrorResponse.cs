using mvc_api.Models.Response;

namespace mvc_api.Models.Response
{
    public class ErrorResponse: BaseResponse
    {
        public ErrorResponse()
        {
            this.ErrorList = new List<object>();
        }
    }
}
