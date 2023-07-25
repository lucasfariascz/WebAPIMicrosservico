using WebAPIMicrosservico.Features.User.Domain.Repository;
using WebAPIMicrosservico.Features.User.Domain.UseCases;
using WebAPIMicrosservico.Features.User.Infra.Repositories;
using WebAPIMicrosservico.Services;

namespace WebAPIMicrosservico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DI container
            builder.Services.AddScoped<ISubmitUserUseCase, SubmitUserUseCase>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IQueueService, QueueService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}