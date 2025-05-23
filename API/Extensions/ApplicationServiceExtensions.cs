using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        //AddTransient<IPhotoService, PhotoService>() → Nuova istanza ogni volta che viene richiesta.
        //AddScoped<IPhotoService, PhotoService>() → Nuova istanza per ogni richiesta HTTP.
        //AddSingleton<IPhotoService, PhotoService>() → Un'unica istanza per tutta la durata dell'applicazione.
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
            //AddSingleton AddTransient AddScoped - ep 38
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();//ep 86 (end)
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<LogUserActivity>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            return services;
        }
    }
}
