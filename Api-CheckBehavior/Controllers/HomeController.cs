using Api_CheckBehavior.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Api_CheckBehavior.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet("twosecondtimeout/{waitMillSeconds:int}")]
        [RequestTimeout(2000)]
        public async Task<IActionResult> GetCustomerWithTwoSecondTimeoutAsync([FromRoute] int waitMillSeconds, CancellationToken cancellationToken)
        {
            // 待ち時間を過ぎたらキャンセル
            await Task.Delay(TimeSpan.FromMilliseconds(waitMillSeconds), cancellationToken);

            return Ok(new CommonResponse<MyResponseData>
            {
                IsSuccess = true,
                Data = new MyResponseData
                {
                    ID = 1,
                    Message = "twosecondtimeout API"
                }
            });

        }


        [HttpGet("TestResponse")]
        [RequestTimeout(milliseconds: 500)]
        public async Task<IActionResult> TestResponse(string job)
        {
            //Thread.Sleep(5000);
            //await Task.Delay(5000);


            for (int i = 0; i < 100000000; i++)
            {
                Console.WriteLine($"test_{i}");
            }


            // アロー演算子
            //Debug.WriteLine(this.Hoge("tawasi"));

            // タプル  
            //var (a1, a2) = this.Hoge2();
            //Debug.WriteLine($"{a1}-{a2}");


            // レスポンスを簡単に書く
            if (job == "success")
            {
                return Ok(new CommonResponse<MyResponseData>
                {
                    IsSuccess = true,
                    Data = new MyResponseData
                    {
                        ID = 1,
                        Message = "Success API"
                    }
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommonResponse<MyResponseData>
                {
                    IsSuccess = false,
                    Data = new MyResponseData
                    {
                        ID = 1,
                        Message = "My Failed API"
                    }
                });
            }
        }


        //[ApiExplorerSettings(IgnoreApi = true)]
        //private string Hoge(string a) => $"hoge {a}";

        //[ApiExplorerSettings(IgnoreApi = true)]
        //private (string, string) Hoge2() => ("hoge2", "hoge3");
    }
}
