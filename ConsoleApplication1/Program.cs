using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using System.Web;
using Newtonsoft.Json.Linq;


namespace ConsoleApplication1
{

    public class Weather
    {
        public string description { set; get; }
        
        

    }
    public class Location
    {
        public String city;
        public String country;

        public Location(String City, String Country)
        {
            this.city = City;
            this.country = Country;
        }
    }

    public class WeatherDataServiceException : Exception
    {

    }


    public class Data
    {

          public String name { get; set; }
          public String id { get; set; }
          public Weather wed {  get; set; }
          
        

     
              
        public Data( )
        {
            
        }
    }
    public interface IweatherDataService
    {
        Data GetWeatherData(Location loc, String key);
    }
    public class serviceFactory
    {

    }

    class MyUploads : IweatherDataService
    {
        public Data data;
        public String URL;

        private static MyUploads _instance = new MyUploads();

        private MyUploads()
        {
            this.URL = "http://api.openweathermap.org/data/2.5/weather";
        }

        public static MyUploads getInstance()
        {
            return _instance;
        }

        Data IweatherDataService.GetWeatherData(Location loc, String key)
        {
            String urlParameters;
            urlParameters = "?q="+ loc.city + "," + loc.country + "&APPID=" + key;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for Json format.
            client.DefaultRequestHeaders.Accept.Add(
           new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response);

                // Parse the response body. Blocking!
                try
                {
                    
                    data = response.Content.ReadAsAsync<Data>().Result;


                    Console.WriteLine("city:" + data.name + ", city ID:" + data.id);
                    Console.WriteLine(data.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            return data;
        }


    }
    public class WeatherFactory
    {
        //use getYouTubeData method to get object that implements IyouTubeDataService interface
        public IweatherDataService WeatherService(Location loc)
        {
            if (loc == null)
            {
                return null;
            }

            if (string.Equals( loc.city, "london", StringComparison.OrdinalIgnoreCase))
            {
                IweatherDataService obj = MyUploads.getInstance();
                return obj;
            }

            if (!string.Equals(loc.city, "london", StringComparison.OrdinalIgnoreCase))
            {
                throw new WeatherDataServiceException();
            }

            return null;
        }

 

    }
    class Program
    {

        static string Key = "4f3cbed25685e619c16d94f45e622782";
        static void Main(string[] args)
        {
            WeatherFactory factory = new WeatherFactory();
            Location loc = new Location("london", "uk");
            IweatherDataService func1 = factory.WeatherService(loc);
            func1.GetWeatherData(loc, Key);
            Console.ReadLine();
            
        }


    }
}