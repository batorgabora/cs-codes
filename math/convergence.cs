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

class SequenceConvergence
{
    // This function checks whether a sequence converges based on the tolerance
    static (bool, double) CheckConvergence(Func<int, double> sequenceFunction, int terms, double tolerance)
    {
        double prevTerm = sequenceFunction(1);  // Get the first term of the sequence

        for (int n = 2; n <= terms; n++)
        {
            double currentTerm = sequenceFunction(n);  // Get the nth term of the sequence

            // If the difference between the current term and the previous term is small enough, the sequence has converged
            if (Math.Abs(currentTerm - prevTerm) < tolerance)
            {
                return (true, currentTerm);  // Sequence converged to currentTerm
            }

            prevTerm = currentTerm;  // Update prevTerm for the next iteration
        }

        return (false, prevTerm);  // If no convergence was found, return false
    }

    // Example sequence function 1 / n (converges to 0)
    static double SequenceFunction(int n)
    {
        return 1.0 / n;  // Sequence: 1/n, which converges to 0 as n -> ∞
    }

    // Main method to run the convergence check
    static void Main()
    {
        int terms = 100000;  // Number of terms to check for convergence
        double tolerance = 1e-6;  // Tolerance to determine convergence

        // Call the CheckConvergence method with the SequenceFunction
        var result = CheckConvergence(SequenceFunction, terms, tolerance);

        // Output the result
        if (result.Item1)
        {
            Console.WriteLine($"The sequence converges to approximately {result.Item2}.");
        }
        else
        {
            Console.WriteLine("The sequence diverges.");
        }
    }
}