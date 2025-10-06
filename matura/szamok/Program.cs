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
            public string q, sub;
            public int a, point;
        }
        static List<valami> lista = new List<valami>();
        static StreamReader read = new StreamReader("felszam.txt");
        static void readin()
        {
            valami helper = new valami();
            while (!read.EndOfStream)
            {
                string firstLine = read.ReadLine();
                string[] secLine = read.ReadLine().Split(' ');
                helper.q = firstLine;
                helper.a = int.Parse(secLine[0]);
                helper.point = int.Parse(secLine[1]);
                helper.sub = secLine[2];
                lista.Add(helper);
            }
        }
        static void exerc()
        {
            Console.WriteLine($"\x1b[34m2. feladat\x1b[0m");
            System.Console.WriteLine(lista.Count());

            System.Console.WriteLine();
            Console.WriteLine($"\x1b[34m3. feladat\x1b[0m");
            var matek = lista.Where(x => x.sub == "matematika").ToList();
            Console.WriteLine($"{matek.Count()}db matematika feladat van");
            Console.WriteLine($" 1 pontos: {matek.Where(x => x.point == 1).Count()}");
            Console.WriteLine($" 2 pontos: {matek.Where(x => x.point == 2).Count()}");
            Console.WriteLine($" 3 pontos: {matek.Where(x => x.point == 3).Count()}");

            System.Console.WriteLine();
            Console.WriteLine($"\x1b[34m4. feladat\x1b[0m");
            var szamok = lista.OrderBy(x => x.a).Select(x => x.a).ToList();
            Console.WriteLine($"{szamok.First()}-től {szamok.Last()}-ig terjed a válaszok számértéke (terjedelem: {szamok.Last() - szamok.First()})");

            System.Console.WriteLine();
            Console.WriteLine($"\x1b[34m5. feladat\x1b[0m");
            var temakorok = lista.Select(x => x.sub).Distinct().ToList();
            foreach (var item in temakorok)
            {
                Console.Write($"{item}  ");
            }

            System.Console.WriteLine();
            Console.WriteLine($"\x1b[34m6. feladat\x1b[0m");
            Console.Write($"milyen témakörből szeretnél kérdést kapni: ");
            string tema = Console.ReadLine();
            var kerdesek = lista.Where(x => x.sub == tema).ToList();

            Random rnd = new Random();
            int lower = 0;
            int higher = kerdesek.Count() - 1;
            int random = rnd.Next(lower, higher + 1);

            Console.Write($"{kerdesek[random].q} ");
            int ans = int.Parse(Console.ReadLine());
            if (ans == kerdesek[random].a)
            {
                Console.WriteLine($"a válasz {kerdesek[random].point} pontot ér");
            }
            else
            {
                Console.WriteLine($"a válasz 0 pontot ér");
                Console.WriteLine($"a helyes válasz: {kerdesek[random].a}");
            }

            System.Console.WriteLine();
            Console.WriteLine($"\x1b[34m7. feladat\x1b[0m");
            StreamWriter write = new StreamWriter("tesztfel.txt");
            List<int> nums = new List<int>();
            int summa = 0;
            int downer = 0;
            int upper = lista.Count() - 1;
            for (int i = 0; i < 10; i++)
            {
                int randomer = rnd.Next(downer, upper + 1);
                while (nums.Contains(randomer))
                {
                    randomer = rnd.Next(downer, upper + 1);
                }
                Console.WriteLine($"{lista[randomer].point} {lista[randomer].a} {lista[randomer].q}");
                write.WriteLine($"{lista[randomer].point} {lista[randomer].a} {lista[randomer].q}");
                summa += lista[randomer].point;
                nums.Add(randomer);
            }
            Console.WriteLine($"a feladatsorra összesen {summa} pont adható");
            write.WriteLine($"a feladatsorra összesen {summa} pont adható");
            write.Close();
            //random szam generalas!!!

        }

        static void Main(string[] args)
        {
            readin();
            exerc();
            Console.ReadLine();
        }
    }
}
