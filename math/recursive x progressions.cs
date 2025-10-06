using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Linq;
using System.IO;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel; //for BigIntiger
using System.Diagnostics;    //for Stopwatch
/* option + shift + a --> comments the selected section */

class Program
{
    static void Main()
    {
        //factorial
        int fac = ints("factorial: ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"\t{fac}! = {fact(fac)}");
        Console.ResetColor();

        //n choose k
        int n = ints("n choose k (nCr)\nn : ");
        int k = ints("k : ");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"\tnCr(n, k) = {nCr(n, k)}");
        Console.WriteLine($"\tnCr(n, k) iterative = {nCrIterative(n, k)}");
        Console.ResetColor();

        //fibonacci
        int num = ints("fibonacci num: ");

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"\tFibonacci({num}) = {fibo(num)}");

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"\tFibonacci({num}) memoization = {Fibonacci(num)}");
        Console.ResetColor();

        //exponential
        int expobase = ints("base: ");
        int pow = ints("power: ");

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"\t{expobase} to the power of {pow} = {expo(expobase, pow)}");
        Console.ResetColor();

        //progressions!
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"progressions from now on");
        Console.ResetColor();
        //arithmetic progression
        double arith = doubles($"(start of the arithmetic progression) a1: ");    // Difference for arithmetic progression
        double diff = doubles("difference: ");
        //geometric progression
        double geo = doubles($"(start of the geometric progression) b1: ");       // Starting value of geometric progression
        double ratio = doubles("quotiens: ");
        //x for both                           
        int x = ints("(what x position) - ax or bx: ");                           //a1 values
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"a2: {arith + diff}");
        Console.WriteLine($"b2: {geo * ratio}");
        Console.ResetColor();


        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"\na{x}:    {arithProg(x, arith, diff)}");
        Console.WriteLine($"b{x}:    {geoProg(x, geo, ratio)}");
        Console.WriteLine($"b{x} iterative:    {geoProg(x, geo, ratio)}");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"\nSa^{x}:     {arithSum(x, arith, diff)}");
        Console.WriteLine($"Sb^{x}:     {geoSumFormula(x, geo, ratio)}");
        if (Math.Abs(ratio) < 1)
        {
            Console.WriteLine($"S∞={limitSum(geo, ratio)}");
        }
        Console.ResetColor();

        //with tuple
        // Get both sums
        var (arithmeticSum, geometricSum) = combinedSum(x, arith, diff, geo, ratio);        //TUPLE DECONSTRUCTION
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"\ndifferently Sa^{x}:       {arithmeticSum}");
        Console.WriteLine($"differently Sb^{x}:       {geometricSum}");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"\n(a1*a2*a3*...*ax):    {arithMulti(x, arith, diff)}");
        Console.WriteLine($"(a1*a2*a3*...*ax) iteratively:    {arithMulti2(x, arith, diff)}");
        Console.ResetColor();
    }

    // Method to read an integer input from the user
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
    static double arithMulti(int x, double start, double difference) // arithmetic progression's segments' product until (including) xth value
    {
        if (x == 1) return start;
        return (start + (x - 1) * difference) * arithMulti(x - 1, start, difference);
    }
    static double arithMulti2(int x, double start, double difference) // arithmetic progression's segments' product until (including) xth value
    {
        double product = 1;
        for (int i = 1; i <= x; i++)
        {
            product = product * arithProg(i, start, difference);
        }
        return product;
    }
    static double arithProg(int x, double start, double difference)
    {
        if (x == 1) return start;
        return arithProg(x - 1, start, difference) + difference;
    }
    static double geoProg(int x, double start, double ratio) //geometric progression's xth value
    {
        if (x == 1) return start;
        return geoProg(x - 1, start, ratio) * ratio;
    }
    static double geoProgIterative(int x, double start, double ratio)
    {
        double result = start; // Initialize result with the starting value
        for (int i = 1; i <= x; i++) // Iterate from 1 to x
        {
            result *= ratio; // Multiply by the ratio at each step
        }
        return result; // Return the final result
    }
    static double arithSum(int x, double start, double difference) // arithmetic progression's sum until (including) xth value
    {
        if (x == 1) return start;
        return (start + (x - 1) * difference) + arithSum(x - 1, start, difference);
    }
    static double geoSum(int x, double start, double ratio) // geometric progression's sum until (including) xth value
    {
        if (x == 1) return start;
        return (start * Math.Pow(ratio, x - 1)) + geoSum(x - 1, start, ratio);
    }
    static double geoSumFormula(int x, double start, double ratio) // geometric progression's sum until (including) xth value using the formula
    {                                                               //            (1-r∧(n+1))
        if (x == 0) return start;                                   //Sn = a *  --------------
        return start * (1 - Math.Pow(ratio, x)) / (1 - ratio);      //              (1-r)
    }
    static long fibo(int n)
    {
        if (n <= 1) return n;
        return fibo(n - 1) + fibo(n - 2);
    }
    static double expo(int x, int n)
    {
        if (n == 0) return 1;
        else if (n > 0) return x * expo(x, n - 1);
        // If n is negative, return the reciprocal
        return 1.0 / expo(x, -n);
    }
    static (double arithmeticSum, double geometricSum) combinedSum(int x, double startArith, double diff, double startGeo, double ratio)
    {
        double totalGeoSum = 0;
        double totalArithSum = 0;
        for (int i = 1; i <= x; i++) // Starting from 1 to avoid confusion with 'a0' and 'b0'
        {
            double arithValue = arithProg(i, startArith, diff); // Get the ith term once
            double geoValue = geoProg(i, startGeo, ratio);      // Get the ith term once
            totalArithSum += arithValue; // Accumulate the arithmetic progression term
            totalGeoSum += geoValue;     // Accumulate the geometric progression term
        }
        return (totalArithSum, totalGeoSum);
    }

    static double limitSum(double start, double quotiens)
    {
        return start / (1 - quotiens);
    }

    //TAIL RECURSION
    static double Factorial(int n, double accumulator = 1)
    {
        if (n == 0) return accumulator;
        return Factorial(n - 1, n * accumulator);
    }
    static BigInteger fact(int n)
    {
        if (n == 0) return 1;
        return fact(n - 1) * n;
    }

    //MEMOIZATION for making recursion more efficient
    static Dictionary<int, double> memo = new Dictionary<int, double>();
    static double Fibonacci(int n)
    {
        if (n <= 1) return n;
        if (memo.ContainsKey(n)) return memo[n];
        // Recursive calculation
        double result = Fibonacci(n - 1) + Fibonacci(n - 2);
        memo[n] = result; // Store the result in the dictionary
        return result;
    }
    static BigInteger nCr(int n, int k)
    {
        if (k > n) return 0;
        if (k == 0 || k == n) return 1;
        BigInteger numerator = fact(n);
        BigInteger denominator = fact(k) * fact(n - k);
        return numerator / denominator;
    }
    static BigInteger nCrIterative(int n, int k)
    {
        if (k > n) return 0; // There are no valid combinations if k > n
        if (k == 0 || k == n) return 1; // nC0 = 1 and nCn = 1
        k = Math.Min(k, n - k); // Optimization: Use the smaller of k and n - k
        BigInteger result = 1;
        for (int i = 0; i < k; i++)
        {
            result *= (n - i); // Multiply by decreasing values from n
            result /= (i + 1); // Divide by increasing values from 1 to k
        }
        return result;
    }
}