using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using mvc_api.Extensions;

namespace mvc_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // service
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.ConfigureCors();
            builder.Services.ConfigureAuthentication();
            builder.Services.ConfigureAuthorization();
            builder.Services.ConfigureApiVersioning();
            builder.Services.ConfigureFilter();
            builder.Services.ConfigureSwagger();


            // app, Configure the HTTP request pipeline.
            var app = builder.Build();

            // todo �ݒ�t�@�C���ǂݍ��݂͕s�v�H �����œǂ܂�Ă���݂����H
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            //System.Diagnostics.Debug.WriteLine("=========> " + config["Logging:LogLevel:Default"]);
            //System.Diagnostics.Debug.WriteLine("=========> " + config.GetValue<int>("MyTest:Value"));



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();                   // Swagger�~�h���E�F�A��ǉ�
                app.UseSwaggerUI();                 // �ÓI�t�@�C���~�h���E�F�A��L����
                app.UseDeveloperExceptionPage();    // �J���җ�O�y�[�W��L����
            } 
            else
            {
                app.UseHsts();              // HTTP�̑����HTTPS���g�p����悤�w��
            }

            app.UseCors("CorsPolicy");      // CORS��L����
            app.UseStaticFiles();           // wwwroot�ɑ΂��ĐÓI�R���e���c�T�[�r�X��o�^
            app.UseHttpsRedirection();      // �����I�� HTTP �v���� HTTPS �փ��_�C���N�g���܂�
            app.UseAuthentication();        // �F�؂�L����
            app.UseAuthorization();         // �F��L����
            app.MapControllers();           // �������[�e�B���O �R���g���[���[���}�b�v����܂�
            app.Run();
        }
    }
}
