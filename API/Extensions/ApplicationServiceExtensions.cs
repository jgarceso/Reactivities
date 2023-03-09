﻿
using Application.Activities;
using Application.Core;
using Persistence;
using Microsoft.EntityFrameworkCore;
namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<ReactivitiesDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("ReactivitiesDbContext"));
            });

            //services.AddDbContext<DataContext>(opt => {

            //    opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            //});

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000", "https://*.azurestaticapps.net");
                });
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}
