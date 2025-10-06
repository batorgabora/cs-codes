using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Linq;
using System.IO;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel; //for BigIntiger

class ötszáz
{
    struct valami 
    {
        public int sorszam, summa;
        public string termek;
    }
    static List<valami> lista = new List<valami> {};
    static void Main()
    {
        StreamReader reader = new StreamReader("penztar.txt");
        string oneLine;
        int szamlalo = 1;
        valami helpp = new valami();    //NAGYON FONTOS
        while(!reader.EndOfStream)
        {
            oneLine = reader.ReadLine() ?? "";
            if(oneLine == "F")
            { 
                szamlalo++;
            }
            else
            {
                helpp.termek = oneLine ?? "";
                helpp.sorszam = szamlalo;
                lista.Add(helpp);
            } 
        }
        //1.
        //System.Console.WriteLine(lista[lista.Count-1].sorszam);
        //System.Console.WriteLine(lista.Max(x => x.sorszam));
        System.Console.WriteLine(lista.GroupBy(x => x.sorszam).Count()); //össz vásárló
        System.Console.WriteLine(lista.Where(x => x.sorszam == 1).Count()); // első vásárló termékszám

        //2.
        System.Console.Write("sorszám: ");
        int sorszamo = int.Parse(Console.ReadLine() ?? "");
        System.Console.Write("cikk neve: ");
        string cikko = Console.ReadLine() ?? "";
        System.Console.Write("darabszám: ");
        int dbo = int.Parse(Console.ReadLine() ?? "");

        var termekes = lista.Where(x => x.termek == cikko).ToList();
        System.Console.WriteLine($"elso: {termekes[0].sorszam} \nutolso: {termekes[termekes.Count() - 1].sorszam}"); 
        //hány darab

        //where majd groupby
        var naponta = lista.Where(x => x.termek == cikko).GroupBy(x => x.sorszam).ToList();
        System.Console.WriteLine($"db: {naponta.Count()}");

        //csel
        var napontaa = lista.GroupBy(x => x.sorszam).ToList();
        int counted = 0;
        foreach (var item in napontaa)
        {
            var csalafintaság = item.Select(x => x).ToList();

            var termekenkent = csalafintaság.GroupBy(x => x.termek).ToList();
            foreach (var itemm in termekenkent)
            {
                if (itemm.Key == cikko)
                counted++;
            }
        }
        System.Console.WriteLine($"csel: {counted}");

        //halmaz
        HashSet<int> napok = new HashSet<int>();
        foreach (var item in lista)
        {
            napok.Add(item.sorszam);        //összes napot belerakjuk
        }
        HashSet<int> vegso = new HashSet<int>();
        foreach(var item in napok)
        {
            foreach (var itemm in lista)
            {
                if(itemm.sorszam==item && itemm.termek==cikko)
                vegso.Add(item);
            }
        }
        System.Console.WriteLine($"hashset: {vegso.Count}");

        var sorszamosok = lista.Where(x => x.sorszam == sorszamo).GroupBy(x => x.termek).ToList();
        foreach(var item in sorszamosok){
            System.Console.WriteLine($"{item.Count()} {item.Key} ");
        }

        //groupby val
        StreamWriter writer = new StreamWriter("osszeg.txt");
        int sum = 0;
        foreach(var item in napontaa)
        {
            var csalafintaság = item.Select(x => x).ToList();

            var termekenkent = csalafintaság.GroupBy(x => x.termek).ToList();
            foreach (var itemm in termekenkent)
            {
                sum += ertekreq(itemm.Count());
            }
            writer.WriteLine($"{item.Key}: {sum}");
            sum = 0;
        }
        writer.Close();

        //halmazzal
        /* StreamWriter writer2 = new StreamWriter("osszeg2.txt");
        foreach(var item in napok)
        {
            foreach (var itemm in lista)
            {
                if(itemm.sorszam==item && )
                vegso.Add(item);
            }
        }
        System.Console.WriteLine($"hashset: {vegso.Count}"); */
        //tul bonyolult

        //rekordbővítés no bueno

        Console.ReadLine();
    }
    static int ertekreq(int db)
    {
        if(db == 1)
        {
            return 500;
        }
        else if(db==2)
        {
            return 950;
        }
        return ertekreq(db-1)+400;
    }
    /* static int ertek(int db) // rekurziv
    {
        int sum = 0;
        if(db == 1)
        {
            sum = 500;
        }
        else if (db==2)
        {
            sum = 950;
        }
        else
        {
            sum = 950 + (db-2)*400;
        }
        return sum;
    } */
}