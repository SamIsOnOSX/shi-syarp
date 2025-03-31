using System;

namespace TemperatureConversion
{
    public class Program
    {
        static void Main(string[]args)
        {
            Console.Write("Please enter the temperature in Celsius:");

            string userInput = Console.ReadLine();
            float celsiusTemperature;

            if (float.TryParse(userInput, out celsiusTemperature))
            {
                float fahrenheitTemperature = (celsiusTemperature * 9 / 5) + 32;
                Console.WriteLine($"The temperature in Fahrenheit is: {fahrenheitTemperature} degrees.");
            }
            else
            {
                Console.WriteLine("Oops, sayop nga input boss.");
            }
        }
    }
}
