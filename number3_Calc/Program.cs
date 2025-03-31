using System;

namespace CalculatorApp

{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Calculator");

            Console.WriteLine("Enter the first number:");
            double firstNumber = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter a sign or unsa imong ganahan (+, -, *, /):");
            string operatorInput = Console.ReadLine();

            Console.WriteLine("Enter the second number:");
            double secondNumber = Convert.ToDouble(Console.ReadLine());

            double result = 0;
            switch (operatorInput)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    if (secondNumber != 0)
                    {
                        result = firstNumber / secondNumber;
                    }
                    else
                    {
                        Console.WriteLine("Error: bawal ang division by zero boss.");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Error: Invalid operator boss.");
                    return;
            }

            Console.WriteLine($"Ang answer kay: {result}");
        }
    }
}