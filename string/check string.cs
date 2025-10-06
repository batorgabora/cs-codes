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

namespace projectThree
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = strings("utasítás: ");
            int e = 0;
            int n = 0;
            int d = 0;
            int k = 0;
            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == 'e')
                {
                    e++;
                }
                else if (command[i] == 'd')
                {
                    d++;
                }
                else if (command[i] == 'n')
                {
                    n++;
                }
                else if (command[i] == 'k')
                {
                    k++;
                }
            }
            Console.Write($"e: {e}\nd: {d}\nn: {n}\nk: {k}");
        }
        static string strings(string prompt)
        {
            string? input; // Declares input as a nullable string, allowing it to store null values
            string pattern = @"^\p{L}+$"; // Matches only letters, including those with accents

            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, pattern))
                {
                    Console.WriteLine("Please enter letters only (including accented characters).");
                }
            } while (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, pattern));

            return input; // Safe because the loop ensures input is not null or invalid
        }
    }
}
