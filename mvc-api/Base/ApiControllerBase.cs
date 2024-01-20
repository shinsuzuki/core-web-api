using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mvc_api.Base
{
    public class ApiControllerBase : ControllerBase
    {
        public IActionResult ToResult<T>(T res)
        {
            return new JsonResult(
                res,
                new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
# if DEBUG
                    WriteIndented = true,
# endif
                });

        }


    }
}
