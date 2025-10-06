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
using System.Runtime.InteropServices;
using System.Collections.Immutable;
using System.Security.Cryptography.X509Certificates; //for BigIntiger
using System.Threading;

class reklám
{
    struct valami
    {
        public int day, num;
        public string city;
    }
    static List<valami> lista = new List<valami>();
    static StreamReader read = new StreamReader("rendel.txt");
    static void readIn(){
        valami helper = new valami();
        while(!read.EndOfStream) {
            string[] oneLine = read.ReadLine().Split(' ');
            helper.day = int.Parse(oneLine[0]);
            helper.city = oneLine[1];
            helper.num = int.Parse(oneLine[2]);
            lista.Add(helper);
        }
    }
    static void exerc()
    {
        System.Console.WriteLine(lista.Count());

        System.Console.Write("nap: ");
        int nap = int.Parse(Console.ReadLine());
        System.Console.WriteLine($"adott napon a rendelések száma: {lista.Where(x => x.day == nap).Count()}");

        var volt = lista.Where(x => x.city == "NR").Select(x => x.day).Distinct().Count();
        if(30 - volt > 0){
            System.Console.WriteLine($"{30-volt} nap nem volt rendelés NR városban");
        }
        else{
            System.Console.WriteLine("Minden nap volt rendelés a reklámban nem érintett városból");
        }

        var max = lista.Select(x => x.num).Distinct().ToList().Max();
        var maxos = lista.Where(x => x.num == max).ToList().First();
        System.Console.WriteLine($"max: {max} - napja: {maxos.day}");

        System.Console.WriteLine($"num a 21. napon - PL:{összes("PL", 21)}  TV:{összes("TV", 21)}  NR:{összes("NR", 21)}");


        int pl1 = 0;
        int tv1 = 0;
        int nr1 = 0;
        int pl2 = 0;
        int tv2 = 0;
        int nr2 = 0;
        int pl3 = 0;
        int tv3 = 0;
        int nr3 = 0;
        for (int i = 1; i <= 10; i++)
        {
            pl1 += összes2("PL", i);
            tv1 += összes2("TV", i);
            nr1 += összes2("NR", i);
        }
        for (int i = 11; i <= 20; i++)
        {
            pl2 += összes2("PL", i);
            tv2 += összes2("TV", i);
            nr2 += összes2("NR", i);
        }
        for (int i = 21; i <= 30; i++)
        {
            pl3 += összes2("PL", i);
            tv3 += összes2("TV", i);
            nr3 += összes2("NR", i);
        }
        System.Console.WriteLine("Napok\t1..10\t1..20\t21..30");
        System.Console.WriteLine($"PL\t{pl1}\t{pl2}\t{pl3}");
        System.Console.WriteLine($"TV\t{tv1}\t{tv2}\t{tv3}");
        System.Console.WriteLine($"NR\t{nr1}\t{nr2}\t{nr3}");

    }
    static int összes(string varos, int nap){
        var ilyen = lista.Where(x => x.city == varos && x.day == nap).Select(x => x.num).ToList().Sum();
        return ilyen;
    }
    static int összes2(string varos, int nap){
        var ilyen = lista.Where(x => x.city == varos && x.day == nap).Select(x => x.num).ToList().Count();
        return ilyen;
    }
    static void Main()
    {
        readIn();
        exerc();
    } 
}