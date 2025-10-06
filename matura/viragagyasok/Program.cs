using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Linq;
using System.IO;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Runtime.InteropServices; //for BigIntiger

class Virágágyás
{
    struct valami 
    {
        public int kezdo, vegzo, sorszam, szam;
        public string szin;
    }
    static List<valami> lista = new List<valami> {};
    static int num = 0;
    static void readIn()
    {
        StreamReader read = new StreamReader("felajanlas.txt");
        valami helper = new valami();
        num = int.Parse(read.ReadLine());
        while (!read.EndOfStream){
            string[] oneLine = read.ReadLine().Split(' ');
            helper.kezdo = int.Parse(oneLine[0]);
            helper.vegzo = int.Parse(oneLine[1]);
            helper.szin = oneLine[2];
            helper.sorszam++;
            helper.szam = helper.kezdo < helper.vegzo ? helper.vegzo-helper.kezdo : num-helper.kezdo+helper.vegzo;
            lista.Add(helper);
        }
    }
    static void feladatok(){
        System.Console.WriteLine("2. feladat");
        System.Console.WriteLine($"össz: {lista.ToList().Count}");

        System.Console.WriteLine("3. feladat");
        var baljobb = lista.Where(x => x. kezdo >= x.vegzo).ToList();
        System.Console.Write($"mindkét oldalra:");
        foreach (var item in baljobb){
            System.Console.Write($" {item.sorszam}");
        }
        System.Console.WriteLine();

        System.Console.WriteLine("4. feladat");
        System.Console.Write("ágyás sorszáma: ");
        int agyas = int.Parse(Console.ReadLine());
        System.Console.WriteLine($"felajánlások száma: {lista.Where(x => x.kezdo <= agyas && x.vegzo >= agyas).Count()}");
        System.Console.WriteLine($" {agyas}. ágyás színe ha csak első ültet: {szin(agyas)}");
        System.Console.WriteLine($" {agyas}. ágyás színei: {szinossz(agyas)}");

        System.Console.WriteLine("5.feladat");
        System.Console.WriteLine(ultetesek());

        StreamWriter write = new StreamWriter("szinek.txt");
        for (int i = 1; i < num; i++)
        {
            write.WriteLine(vegso(i));
            //System.Console.WriteLine(vegso(i));
        }
        write.Close();

    }
    static string szin(int agyas)
    {
        if (lista.Where(x => x.kezdo <= agyas && x.vegzo >= agyas).Count() == 0)
        {
            return "ezt az ágyást nem ültetik be";
        }
        var ultetveny = lista.Where(x => x.kezdo <= agyas && x.vegzo >= agyas).Select(x => x.szin).ToList();
        return ultetveny[0];
    }
    static string szinossz(int agyas)
    {
        string result = "";
        if (lista.Where(x => x.kezdo <= agyas && x.vegzo >= agyas).Count() == 0)
        {
            return "ezt az ágyást nem ültetik be";
        }
        var ultetveny = lista.Where(x => x.kezdo <= agyas && x.vegzo >= agyas).Select(x => x.szin).Distinct().ToList();
        foreach (var item in ultetveny){
            result += item + " ";
        }
        result.Remove(result.Length-1);
        return result;
    }
    static string ultetesek(){
        var ajanlasok = lista.Select(x => x.szam).Sum();

        int sum =0;
        for (int i = 0; i < num-1; i++)
        {
            var virag = lista.Where(x => x.kezdo <= i && x.vegzo >= i).Count();
            if (virag > 0)
            {
                sum++;
            }
        }

        if (sum >= num)
        {
            return "minden ágyás beültetésére van jelentkező";
        }
        else if (ajanlasok>=num)
        {
            return "átszervezéssel megoldható a beültetés";
        }
        return "a beültetés nem oldható meg";
    }
    static string vegso(int agyas)
    {
        
        if (lista.Where(x => x.kezdo <= agyas && x.vegzo >= agyas).Count() == 0)
        {
            return "# 0";
        }
        var ultetveny = lista.Where(x => x.kezdo <= agyas && x.vegzo >= agyas).OrderBy(x => x.sorszam).ToList();
        return ultetveny[0].szin + " " + ultetveny[0].sorszam;
    }
    static void Main()
    {
        readIn();
        feladatok();
    } 
}