using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using WebAPIMicrosservico.Features.User.Domain.Repository;
using WebAPIMicrosservico.Features.User.Domain.UseCases;
using WebAPIMicrosservico.Features.User.Infra.Repositories;
using WebAPIMicrosservico.Middleware;
using WebAPIMicrosservico.Services;

namespace WebAPIMicrosservico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(cfg =>
            {
                cfg.Filters.Add(typeof(ExceptionFilter));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Injeção de dependências
            builder.Services.AddScoped<ISubmitUserUseCase, SubmitUserUseCase>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IQueueService, QueueService>();
            builder.Services.AddScoped<AuthorizationFilter>();

            builder.Services.AddSwaggerGen(c =>
             {

                 // Configuração da autenticação do Swagger (JWT Bearer)
                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Description = "Informe o token de autenticação no cabeçalho no formato 'Bearer {token}'.",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.Http,
                     Scheme = "bearer",
                     BearerFormat = "JWT"
                 });

                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
             });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");

                    c.OAuthClientId("seu_client_id");
                    c.OAuthAppName("Minha API - Swagger");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}