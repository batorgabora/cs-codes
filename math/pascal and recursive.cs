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
            /* int n = ints("n: ");
            int k = ints("k: ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t{binomial(n, k)}");
            Console.ResetColor();

            int rows = ints("\nrows: ");
            Console.Write($"{pascal(rows)}"); */


            int n = ints("n: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{pascal(n)}");
            Console.ResetColor();

            string str = strings("adj string: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{isPalindrome(str)}");
            Console.ResetColor();

        }

        public static bool isPalindrome(string text)
        {
            // Helper function for recursion
            bool CheckPalindrome(string s, int start, int end)
            {
                // Base cases
                if (start >= end) return true;
                if (s[start] != s[end]) return false;

                // Recursive step
                return CheckPalindrome(s, start + 1, end - 1);
            }

            // Clean up the text by removing spaces and converting to lowercase
            string cleanedText = text.Replace(" ", "").ToLower();
            return CheckPalindrome(cleanedText, 0, cleanedText.Length - 1);
        }
        static int ackermann(int m, int n)
        {
            if (m == 0)
            {
                return n + 1;
            }
            else if (n == 0 && m > 0)
            {
                return ackermann(m - 1, 1);
            }
            else
            {
                return ackermann(m - 1, ackermann(m, n - 1));
            }
        }
        static string pascal(int rows)
        {
            string pyramid = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    pyramid = pyramid + $"{binomial(i, j)} ";
                }
                pyramid = pyramid + "\n";
            }
            return pyramid;
        }
        static BigInteger fact(int n)
        {
            if (n == 0) return 1;
            return fact(n - 1) * n;
        }
        static BigInteger nCr(int n, int k)
        {
            if (k > n) return 0;
            if (k == 0 || k == n) return 1;
            BigInteger numerator = fact(n);
            BigInteger denominator = fact(k) * fact(n - k);
            return numerator / denominator;
        }
        static int binomial(int n, int k)
        {
            int binom = 0;
            if (k == 0 || n == k)
            {
                binom = 1;
            }
            else
            {
                binom = binomial(n - 1, k - 1) + binomial(n - 1, k);
            }
            return binom;
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
        static string strings(string prompt)
        {
            string? input; /* Declares input as a nullable string, allowing it to store null values */
            string pattern = @"^[a-zA-Z ]+$";

            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();

                if (input == null || !Regex.IsMatch(input, pattern))
                {
                    Console.WriteLine("nuh uh");
                }
            } while (input == null || !Regex.IsMatch(input, pattern));
            return input ?? "";  /* Uses the null-coalescing operator ?? to ensure a non-null value is returned. This step is mostly defensive since the loop ensures input won’t be null when returning, but it’s there to satisfy the compiler */
        }
        // Method to read a double input from the user
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
        static BigInteger bigints(string prompt)
        {
            BigInteger result;  // Variable to store the user's input
            Console.Write(prompt);  // Display the prompt message to the user

            // Keep looping until a valid BigInteger is entered
            while (!BigInteger.TryParse(Console.ReadLine(), out result))
            {
                // If the input is invalid, prompt the user again
                Console.Write("Invalid input. Please enter a valid integer: ");
            }
            // Return the valid BigInteger value
            return result;
        }
    }
}
