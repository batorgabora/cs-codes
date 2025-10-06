using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Linq;
using System.IO;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace taylorSeries
{
    class Program
    {
        static void Main(string[] args)
        {
            double degree = doubles("degree: ");
            double radian = radians(degree % 360);

            Console.WriteLine($"sine: {sine(radian)}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"real sine: {Math.Sin(radian)}");
            Console.ResetColor();
            Console.WriteLine($"cosine: {cosine(radian)}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"real cosine: {Math.Cos(radian)}");
            Console.ResetColor();

            double pow = doubles("e^x: ");
            Console.WriteLine($"e^{pow}: {ex(pow)}");

        }
        static double radians(double degree)
        {
            return degree * (Math.PI / 180);
        }

        static double sine(double x)
        {
            double result = x;
            for (int i = 3; i < 100; i += 2) //do checkStart instead of 12 --> optimize automatically
            {
                if (i % 4 == 3)
                {
                    result -= (expo(x, i) / fact(i));
                }
                else if (i % 4 == 1)
                {
                    result += (expo(x, i) / fact(i));
                }
            }
            return result;
        }
        static double cosine(double x)
        {
            double result = 1;
            for (int i = 2; i < 100; i += 2)
            {
                if (i % 4 == 2)
                {
                    result -= (expo(x, i) / fact(i));
                }
                else if (i % 4 == 0)
                {
                    result += (expo(x, i) / fact(i));
                }
            }
            return result;
        }
        static double ex(double x)
        {
            double result = 1;
            for (double i = 1; i < 12; i++)
            {
                result += (expo(x, i) / fact(i));
            }
            return result;
        }

        static double fact(double n)
        {
            double facts = 1;
            for (int i = 1; i <= n; i++)
            {
                facts *= i;
            }
            return facts;
        }
        static double expo(double x, double n)
        {
            return Math.Pow(x, n);
        }
        static int ints(string prompt)
        {
            int result;  // Variable to store the user's input
            Console.Write(prompt);  // Display the prompt message to the user
                                    // Keep looping until a valid integer is entered
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                // If the input is invalid, prompt the user again
                Console.Write("Invalid input. Please enter a valid integer: ");
            }
            // Return the valid integer value
            return result;
        }
        static double doubles(string prompt)
        {
            double result;  // Variable to store the user's input
            Console.Write(prompt);  // Display the prompt message to the user
                                    // Keep looping until a valid double is entered
            while (!double.TryParse(Console.ReadLine(), out result))
            {
                // If the input is invalid, prompt the user again
                Console.Write("Invalid input. Please enter a valid double: ");
            }
            // Return the valid double value
            return result;
        }
    }
}
