using Api_CheckBehavior.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api_CheckBehavior.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("TestResponse")]
        public IActionResult TestResponse(string job)
        {
            // アロー演算子
            Debug.WriteLine(this.Hoge("tawasi"));

            // タプル  
            var (a1, a2) = this.Hoge2();
            Debug.WriteLine($"{a1}-{a2}");


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
                        Message = "Failed API"
                    }
                });
            }
        }


        private string Hoge(string a) => $"hoge {a}";

        private (string, string) Hoge2() => ("hoge2", "hoge3");
    }
}
