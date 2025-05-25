using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Api_CheckBehavior
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            builder.Services.AddControllers();
            
            // add swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SampleApi",
                    Description = "サンプルAPI",
                });
            });

            // add timeout 
            builder.Services.AddRequestTimeouts();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();        // 開発者例外ページを有効化
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            // add timeouts
            app.UseRequestTimeouts();

            app.MapControllers();

            app.Run();
        }
    }
}
