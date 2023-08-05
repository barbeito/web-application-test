using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTest.Domain.Interfaces.Repositories;

namespace WebApplicationTest.Infrastructure
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        public void GetData()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
