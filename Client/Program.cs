using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello client");
            WeatherFactory factory = new WeatherFactory();
            Location l = new Location("london", "uk");
            IweatherDataService adi = factory.WeatherService(l);
            adi.GetWeatherData(l,"4f3cbed25685e619c16d94f45e622782");

            
        }
    }
}
