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
    static int[,] matrix = new int[9, 9];
    static string tip1, tip2, tip3, tip4;
    static int sor = 0;
    static int oszlop = 0;
    static void readIn()
    {
        Console.Write($"milyen fajl: ");
        string file = Console.ReadLine();
        Console.Write($"sor:");
        sor = int.Parse(Console.ReadLine()) - 1;
        Console.Write($"oszlop:");
        oszlop = int.Parse(Console.ReadLine()) - 1;
        StreamReader read = new StreamReader($"{file}.txt");
        string oneLine;
        for (int i = 0; i < 9; i++)
        {
            oneLine = read.ReadLine();
            string[] atmenet = oneLine.Split(' ');
            for (int j = 0; j < 9; j++)
            {
                matrix[i, j] = int.Parse(atmenet[j]);
            }
        }
        tip1 = read.ReadLine();
        tip2 = read.ReadLine();
        tip3 = read.ReadLine();
        tip4 = read.ReadLine();
    }
    static void f3()
    {
        if (matrix[sor, oszlop] == 0)
        {
            Console.Write($"nono");
        }
        else
        {
            Console.WriteLine($"ertek: {matrix[sor, oszlop]}");
        }

        Console.WriteLine($"ternegyed: {odaTer(sor, oszlop)}");
    }
    static void f4()
    {
        int counter = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (matrix[i, j] == 0)
                {
                    counter++;
                }
            }
        }
        Console.WriteLine($"{(double)counter / 81 * 100}%");
    }
    static string tippek(string tippo)
    {
        string valasz = "";
        string[] dolog = tippo.Split(' ');
        int num = int.Parse(dolog[0]);
        int sorr = int.Parse(dolog[1]) - 1;
        int oszlopp = int.Parse(dolog[2]) - 1;
        System.Console.WriteLine($"kiválasztott sor: {sorr+1} oszlop: {oszlopp+1} szam: {num}");
        if (matrix[sorr, oszlopp] == 0) // [] nem () !!!!!!!!!!!!!
        {
            int count = 0;
            int count2 = 0;
            for (int i = 0; i < 9; i++)
            {
                if (matrix[sorr, i] == num)
                {
                    count++;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (matrix[i, oszlopp] == num)
                {
                    count2++;
                }
            }
            if (count != 0)
            {
                valasz ="van mar sorban";
            }
            else if (count2 != 0)
            {
                valasz = "van mar oszlopban";
            }
            else if (ter(odaTer(sorr, oszlopp), num)==false)
            {
                valasz = "mar van szam resztablaban";
            }
            else if (count==0 && count2 == 0 && ter(odaTer(sorr, oszlopp), num))
            {
                valasz = "jo lesz!";
            }
        }
        else
        {
            valasz = "hely foglalt";
        }
        return valasz;
    }
    static int odaTer(int sork, int oszlopk)
    {
        int ternegyed = 0;
        if ((double)sork / 3 < 1)     //most az egyszer lecci double
        {
            if ((double)oszlopk / 3 < 1)
            {
                ternegyed = 1;
            }
            else if ((double)oszlopk / 3 >= 1 && (double)oszlopk / 3 < 2)
            {
                ternegyed = 2;
            }
            else
            {
                ternegyed = 3;
            }
        }
        else if ((double)sork / 3 >= 1 && (double)sork / 3 < 2)
        {
            if ((double)oszlopk / 3 < 1)
            {
                ternegyed = 4;
            }
            else if ((double)oszlopk / 3 >= 1 && (double)oszlopk / 3 < 2)
            {
                ternegyed = 5;
            }
            else
            {
                ternegyed = 6;
            }
        }
        else
        {
            if ((double)oszlopk / 3 < 1)
            {
                ternegyed = 7;
            }
            else if ((double)oszlopk / 3 >= 1 && (double)oszlopk / 3 < 2)
            {
                ternegyed = 8;
            }
            else
            {
                ternegyed = 9;
            }
        }
        return ternegyed;
    }
    static bool ter(int ternegyed, int num)
    {
        bool result = true;
        if (ternegyed==1)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (num==matrix[i,j])
                    {
                        result= false;
                    }
                }
            }
        }
        else if (ternegyed==2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    if (num==matrix[i,j])
                    {
                        result= false;
                    }
                }
            }
        }
        else if (ternegyed==3)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 6; j < 9; j++)
                {
                    if (num==matrix[i,j])
                    {
                        result= false;
                    }
                }
            }
        }
        if(ternegyed==4)
        {
            for (int i = 3; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (num==matrix[i,j])
                    {
                        result= false;
                    }
                }
            }
        }
        else if (ternegyed==5)
        {
            for (int i = 3; i < 6; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    if (num==matrix[i,j])
                    {
                        result= false;
                    }
                }
            }
        }
        else if (ternegyed==6)
        {
            for (int i = 3; i < 6; i++)
            {
                for (int j = 6; j < 9; j++)
                {
                    if (num==matrix[i,j])
                    {
                        result= false;
                    }
                }
            }
        }
        else if(ternegyed==7)
        {
            for (int i = 7; i < 9; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (num==matrix[i,j])
                    {
                        result= false;
                    }
                }
            }
        }
        else if (ternegyed==8)
        {
            for (int i = 7; i < 9; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    if (num==matrix[i,j])
                    {
                        result= false;
                    }
                }
            }
        }
        else if (ternegyed==9)
        {
            for (int i = 7; i < 9; i++)
            {
                for (int j = 6; j < 9; j++)
                {
                    if (num==matrix[i,j])
                    {
                        result= false;
                    }
                }
            }
        }
        return result;
    }
    static void Main()
    {
        readIn();
        f3();
        f4();
        System.Console.WriteLine(tippek(tip1));
        System.Console.WriteLine(tippek(tip2));
        System.Console.WriteLine(tippek(tip3));
        System.Console.WriteLine(tippek(tip4));
        Console.ReadLine();
    }
}