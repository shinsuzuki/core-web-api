using Api_CheckBehavior.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;


namespace Api_CheckBehavior.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet("secondtimeout/{waitMillSeconds:int}")]
        [RequestTimeout(2000)]
        public async Task<IActionResult> secondtimeout ([FromRoute] int waitMillSeconds, CancellationToken cancellationToken)
        {
            // Delayの待ち時間よりリクエストタイムアウトが長い場合は200レスポンス
            // Delayの待ち時間よりリクエストタイムアウトが短い場合は504を返す
            await Task.Delay(TimeSpan.FromMilliseconds(waitMillSeconds), cancellationToken);

            return Ok(new CommonResponse<MyResponseData>
            {
                IsSuccess = true,
                Data = new MyResponseData
                {
                    ID = 1,
                    Message = "secondtimeout API"
                }
            });
        }

        private void Myloop()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    Console.WriteLine($"loop_{j}");
                }
                Thread.Sleep(10);
            }
        }

        [HttpGet("secondtimeout2")]
        [RequestTimeout(2000)]
        public async Task<IActionResult> secondtimeout2(CancellationToken cancellationToken)
        {
            // ループ中にタイムアウトになっても処理が進んでしまう
            await Task.Run(Myloop);

            return Ok(new CommonResponse<MyResponseData>
            {
                IsSuccess = true,
                Data = new MyResponseData
                {
                    ID = 1,
                    Message = "secondtimeout2 API"
                }
            });
        }


        [HttpGet("Check_NormalTimeout")]
        public async Task<IActionResult> Check_NormalTimeout()
        {
            Console.WriteLine(DateTime.Now.ToString());
            await Task.Delay(1000 * 180); // 180秒待つ→タイムアウトが発生しない
            Console.WriteLine(DateTime.Now.ToString());

            return Ok(new CommonResponse<MyResponseData>
            {
                IsSuccess = true,
                Data = new MyResponseData
                {
                    ID = 1,
                    Message = "normal timeout API"
                }
            });
        }

        [HttpGet("Check_CancellationToken")]
        [RequestTimeout(5000)]
        public async Task<IActionResult> Check_CancellationToken(CancellationToken ct)
        {
            //for (int i = 0; i < 100000; i++)
            //{
            //    try
            //    {
            //        Console.WriteLine($"loop_{i}_" + ct.IsCancellationRequested.ToString());
            //        if (ct.IsCancellationRequested)
            //        {
            //            // タイムアウトでフラグが変化する
            //            Console.WriteLine($"loop_{i}_" + ct.IsCancellationRequested.ToString());
            //            // レスポンスが出力されない？
            //            return Ok(new CommonResponse<MyResponseData>
            //            {
            //                IsSuccess = false,
            //                Data = new MyResponseData
            //                {
            //                    ID = 1,
            //                    Message = "キャンセルトークンの値を判別 true  API"
            //                }
            //            });
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.ToString());
            //    }
            //}

            //await Task.Delay(1000 * 3, ct); // 3秒待つ→504エラー発生

            await Task.Delay(1000 * 1, ct); 
            await Task.Delay(1000 * 1, ct); 
            await Task.Delay(1000 * 1, ct); 
            await Task.Delay(1000 * 1, ct); 
            await Task.Delay(1000 * 1, ct); 
            await Task.Delay(1000 * 1, ct); // 6秒待つ→504エラー発生

            // レスポンスが出力されない？
            return Ok(new CommonResponse<MyResponseData>
            {
                IsSuccess = true,
                Data = new MyResponseData
                {
                    ID = 2,
                    Message = $"キャンセルトークンの値を判別 API"
                }
            });
        }



        [HttpGet("Check_IsCancellationRequested")]
        [RequestTimeout(10)]
        public async Task<IActionResult> Check_IsCancellationRequested(CancellationToken cancellationToken)
        {
           // RequestTimeoutを過ぎたら、CancellationTokenがキャンセルされているかどうかを確認
           var isExist = false;
            for (int i = 0; i < 100000; i++)
            {
                Console.WriteLine($"loop_{i}_" + cancellationToken.IsCancellationRequested.ToString());
                if (cancellationToken.IsCancellationRequested)
                {
                    // タイムアウトでフラグが変化する
                    Console.WriteLine($"loop_{i}_" + cancellationToken.IsCancellationRequested.ToString());

                    // レスポンスが出力されない？
                    return Ok(new CommonResponse<MyResponseData>
                    {
                        IsSuccess = false,
                        Data = new MyResponseData
                        {
                            ID = 1,
                            Message = "キャンセルトークンの値を判別 true  API"
                        }
                    });
                }
            }

            // レスポンスが出力されない？
            return Ok(new CommonResponse<MyResponseData>
            {
                IsSuccess = true,
                Data = new MyResponseData
                {
                    ID = 2,
                    Message = $"キャンセルトークンの値を判別 API"
                }
            });
        }


        // ASP.NET Core の要求タイムアウト ミドルウェア
        // https://learn.microsoft.com/ja-jp/aspnet/core/performance/timeouts?view=aspnetcore-9.0
        // ミドルウェアでタイムアウトを設定しない規定の場合はタイムアウトは発生しない
        [HttpGet("GetResponse_DisableRequestTimeoutAsync")]
        [DisableRequestTimeout]// ここでRequestTimeoutを無効にする
        public async Task<IActionResult> GetResponse_DisableRequestTimeoutAsync(CancellationToken cancellationToken)
        {
            // タイムアウトを無視
            //for (int i = 0; i < 1000; i++)
            //{
            //    Console.WriteLine($"loop_{i}");
            //}
            Console.WriteLine(DateTime.Now.ToString());
            await Task.Delay(1000 * 300);
            Console.WriteLine(DateTime.Now.ToString());


            return Ok(new CommonResponse<MyResponseData>
            {
                IsSuccess = true,
                Data = new MyResponseData
                {
                    ID = 1,
                    Message = "タイムアウトを無視した API"
                }
            });
        }

        [HttpGet("TestResponse")]
        [RequestTimeout(milliseconds: 500)]
        public async Task<IActionResult> TestResponse(string job)
        {
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
