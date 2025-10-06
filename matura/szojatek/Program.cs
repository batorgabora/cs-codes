using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace szamok
{
    class Program
    {
        struct valami
        {
            public string szo;
            public int len, magan, massal;
        }
        static List<valami> lista = new List<valami>();
        static StreamReader read = new StreamReader("szoveg.txt");
        static void readin()
        {
            valami helper = new valami();
            while (!read.EndOfStream)
            {
                helper.szo = read.ReadLine();
                helper.len = helper.szo.Length;
                helper.magan = maganhangzo(helper.szo);
                helper.massal = massalhangzo(helper.szo);
                lista.Add(helper);


            }
        }
        static void exerc()
        {
            System.Console.Write("1. feladat Adjon meg egy szót: ");
            string szo = Console.ReadLine();
            szo = szo.ToLower();
            if (szo.Contains('a') || szo.Contains('á') || szo.Contains('e') || szo.Contains('é') || szo.Contains('o') || szo.Contains('ó') || szo.Contains('u') || szo.Contains('ú') || szo.Contains('ü') || szo.Contains('ű') || szo.Contains('i') || szo.Contains('í') || szo.Contains('ö') || szo.Contains('ő'))
            {
                System.Console.WriteLine("van benne magánhangzó");
            }
            else
            {
                System.Console.WriteLine("nincs benne magánhangzó");
            }

            System.Console.WriteLine("2.feladat");
            var hosszu = lista.Select(x => x.len).Max();
            foreach (var item in lista)
            {
                if (item.len == hosszu)
                {
                    Console.Write($"{item.szo}\t");
                }
            }

            System.Console.WriteLine("3. feladat");
            var kevesebb = lista.Where(x => x.magan > x.massal).Select(x => x.szo).ToList();
            foreach (var item in kevesebb)
            {
                Console.Write($"{item} ");
            }
            Console.Write($"");
            Console.WriteLine($"{kevesebb.Count()}/{lista.Count()} : {(double)kevesebb.Count() / (double)lista.Count() * 100:0.00}%");

            System.Console.WriteLine("4. feladat");
            var otos = lista.Where(x => x.len == 5).ToList();
            Console.Write($"adj szoreszlet: ");
            string reszlet = Console.ReadLine();
            var jok = otos.Where(x => x.szo.Substring(1, 3) == reszlet).ToList();
            foreach (var item in jok)
            {
                Console.Write($"{item.szo} ");
            }

            System.Console.WriteLine("5.feladat");
            var kozepek = otos.Select(x => x.szo.Substring(1, 3)).Distinct().ToList();
            foreach (var item in kozepek)
            {
                var jokk = otos.Where(x => x.szo.Substring(1, 3) == item).ToList();
                if (jokk.Count > 1)
                {
                    foreach (var itemm in jokk)
                    {
                        Console.WriteLine($"{itemm.szo}");
                    }
                    System.Console.WriteLine();
                }

            }
        }

        static int maganhangzo(string szo)
        {
            int value = 0;
            szo = szo.ToLower();
            for (int i = 0; i < szo.Length; i++)
            {
                if (szo[i] == 'a' || szo[i] == 'e' || szo[i] == 'i' || szo[i] == 'o' || szo[i] == 'u')
                {
                    value++;
                }
            }
            return value;
        }
        static int massalhangzo(string szo)
        {
            return szo.Length - maganhangzo(szo);
        }

        static void Main(string[] args)
        {
            readin();
            exerc();
            Console.ReadLine();
        }
    }
}
