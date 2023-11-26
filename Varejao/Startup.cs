using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Varejao.Data;
using Varejao.Repositorios.Interface;
using Varejao.Services;

namespace Varejao_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {

            Configuration = configuration;

        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITransacaoService, TransacaoService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Varejao_api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Sistema_varejaoIssuer",
                    ValidAudience = "Sistema_varejao",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chaveSuperSecreta123")),
                };
            });

            services.AddAuthorization();


            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure (WebApplication app, IWebHostEnvironment environment)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Varejao_api");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
        }
    }
}
