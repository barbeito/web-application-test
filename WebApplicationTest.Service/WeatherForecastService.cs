using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTest.Domain.Interfaces.Repositories;
using WebApplicationTest.Domain.Interfaces.Services;

namespace WebApplicationTest.Service
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public void Execute()
        {
            _weatherForecastRepository.GetData();
        }
    }
}
