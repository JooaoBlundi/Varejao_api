
using Microsoft.EntityFrameworkCore;
using Varejao.Data;
using Varejao.Repositorios.Interface;
using Varejao.Services;
using Varejao_api;

namespace Varejao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new Startup(builder.Configuration);

            startup.ConfigureServices(builder.Services);

            string mySqlConnection =
                builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContextPool<SistemaVarejaoDbContext>(
                options => options.UseMySql(mySqlConnection,
                ServerVersion.AutoDetect(mySqlConnection)));

            var app = builder.Build();

            startup.Configure(app, app.Environment);

            app.Run();
        }
    }
}