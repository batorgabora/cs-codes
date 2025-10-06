using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Linq;
using System.IO;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel; //for BigIntiger

class Program
{
    static string[,] matrix = new string[360, 640];
    static void readIn()
    {
        StreamReader read = new StreamReader("kep.txt");
        string oneLine = "";
        for (int i = 0; i < 360; i++)
        {
            oneLine = read.ReadLine();
            string[] átmenet = oneLine.Split(' ');
            for (int j = 0; j < 640 * 3; j += 3)
            {
                matrix[i, j / 3] = átmenet[j] + " " + átmenet[j + 1] + " " + átmenet[j + 2];
            }
        }
        /* for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                Console.Write($"{matrix[i, j]}");
            }
            Console.WriteLine();
        } */
    }
    static void f2()
    {
        Console.Write($"sor: ");
        int sor = int.Parse(Console.ReadLine()) - 1;
        Console.Write($"oszlop: ");
        int oszlop = int.Parse(Console.ReadLine()) - 1;
        string[] valami = matrix[sor, oszlop].Split();
        Console.WriteLine($"pixelek: ({valami[0]},{valami[1]},{valami[2]})");
    }
    static void f3()
    {
        int sum = 0;
        int count = 0;
        for (int i = 0; i < 360; i++)
        {
            for (int j = 0; j < 640; j++)
            {
                string[] valami = matrix[i, j].Split();
                sum = int.Parse(valami[0]) + int.Parse(valami[1]) + int.Parse(valami[2]);
                if (sum > 600)
                {
                    count++;
                }
            }
        }
        Console.WriteLine($"{count}db");
    }
    static void f4()
    {
        int sum = 0;
        int min = 1000;
        for (int i = 0; i < 360; i++)
        {
            for (int j = 0; j < 640; j++)
            {
                string[] valami = matrix[i, j].Split();
                sum = int.Parse(valami[0]) + int.Parse(valami[1]) + int.Parse(valami[2]);
                if (sum < min)
                {
                    min = sum;
                }
            }
        }
        Console.WriteLine($"legsötétebb: {min}");
        for (int i = 0; i < 360; i++)
        {
            for (int j = 0; j < 640; j++)
            {
                string[] valami = matrix[i, j].Split();
                sum = int.Parse(valami[0]) + int.Parse(valami[1]) + int.Parse(valami[2]);
                if (sum == min)
                {
                    Console.WriteLine($"{valami[0]},{valami[1]},{valami[2]}");
                }
            }
        }
    }
    static bool hatar(int sor, int diff)
    {
        int blue = 0;
        int bluee = 0;

        bool valami2 = false;
        for (int i = 0; i < 640 - 1; i++)
        {
            string[] valami = matrix[sor, i].Split();
            blue = int.Parse(valami[2]);
            string[] valami1 = matrix[sor, i + 1].Split();
            bluee = int.Parse(valami1[2]);
            if (Math.Abs((bluee - blue)) > diff)
            {
                valami2 = true;
            }
        }
        return valami2;
    }
    static void f6()
    {
        int sorutso = 0;
        int sorelso = 0;
        for (int i = 0; i < 360; i++)
        {
            for (int j = 0; j < 640; j++)
            {
                if (hatar(i, 10))
                {
                    sorutso = i + 1;
                }
            }
        }
        for (int i = 359; i >= 0; i--)
        {
            for (int j = 0; j < 640; j++)
            {
                if (hatar(i, 10))
                {
                    sorelso = i + 1;
                }
            }
        }
        Console.WriteLine($"{sorelso},{sorutso}");
    }
    static void Main()
    {
        readIn();
        f2();
        f3();
        f4();
        f6();
    }
}