using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationTest.Api;
using WebApplicationTest.Domain.Interfaces.Repositories;
using WebApplicationTest.Domain.Interfaces.Services;
using WebApplicationTest.Infrastructure;
using WebApplicationTest.Service;

namespace WebApplicationTest
{
    public static class ApiDependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mapper => mapper.AddProfile(new AutoMapperApiConfiguration()));
            var mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
