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
using System.Data.Common;

class zeneiAdok
    {
        struct valami
        {
            public int id, ado, hossz, ido1, ido2, ido3, extra1;
            public string banda, zene;
        }
        static List<valami> lista = new List<valami>();
        static int length = 0;
        static void readIn()
        {
            StreamReader reader = new StreamReader("musor.txt");
            length = int.Parse(reader.ReadLine());
            valami helper = new valami();
            string oneLine;
            while(!reader.EndOfStream)
            {
                oneLine = reader.ReadLine();
                string[] split = oneLine.Split(':');
                string[] splitagain = split[0].Split(' ');

                helper.ado = int.Parse(splitagain[0]);
                helper.hossz = int.Parse(splitagain[1])*60 + int.Parse(splitagain[2]);
                helper.zene = split[1];

                if(splitagain.Length == 6)
                {
                    helper.banda = splitagain[3] + " " + splitagain[4] + " " + splitagain[5];
                }
                else if(splitagain.Length == 5)
                {
                    helper.banda = splitagain[3] + " " + splitagain[4];
                }
                else
                {
                    helper.banda = splitagain[3];
                }

                helper.id++;
                
                if(helper.ado == 1)
                {
                    helper.ido1+=helper.hossz;
                    helper.extra1 += helper.hossz+60;
                }
                else if(helper.ado == 2)
                {
                    helper.ido2+=helper.hossz;
                }
                else
                {
                    helper.ido3+=helper.hossz;
                }

                lista.Add(helper);
            }
        }
        static void exercises()
        {
            System.Console.WriteLine("2. feladat");
            var one = lista.Where(x=>x.ado==1).Count();
            var two = lista.Where(x=>x.ado==2).Count();
            var three = lista.Where(x=>x.ado==3).Count();
            System.Console.WriteLine($"1.ado: {one}\t2.ado: {two}\t3.ado:{three}");


            System.Console.WriteLine("3. feladat");
            var clapton = lista.Where(x=>x.ado == 1 && x.banda=="Eric Clapton").ToList();
            var elsoke = clapton.First().id;
            var utolsoka = clapton.Last().id;
            //System.Console.WriteLine($"{elsoke} {utolsoka}");
            var interval = lista.Where(x=>x.ado == 1 && x.id>=elsoke && x.id<=utolsoka).Select(x=>x.hossz).Sum();
            System.Console.WriteLine($"eltelt idő: {format(interval)} - {interval}s");


            System.Console.WriteLine("4.feladat");
            var omegaado = lista.Where(x=>x.banda == "Omega" && x.zene == "Legenda").ToList().First().ado;
            var omegaid = lista.Where(x=>x.banda == "Omega" && x.zene == "Legenda").ToList().First().id;
            System.Console.WriteLine($"({omegaid}.) Omega:Legenda");
            if (omegaado==1)
            {
                var omegaido = lista.Where(x=>x.banda == "Omega" && x.zene == "Legenda").ToList().First().ido1;

                var masikon = lista.Where(x=>x.ado == 2 && x.ido2 <= omegaido).ToArray().Last();
                System.Console.WriteLine($"{masikon.ado}.ado - {masikon.banda}:{masikon.zene}");
                var masikonn = lista.Where(x=>x.ado == 3 && x.ido3 <= omegaido).ToArray().Last();
                System.Console.WriteLine($"{masikonn.ado}.ado - {masikonn.banda}:{masikonn.zene}");
            }
            else if (omegaado==2)
            {
                var omegaido = lista.Where(x=>x.banda == "Omega" && x.zene == "Legenda").ToList().First().ido2;

                var masikon = lista.Where(x=>x.ado == 1 && x.ido1 <= omegaido).ToArray().Last();
                System.Console.WriteLine($"{masikon.ado}.ado - {masikon.banda}:{masikon.zene}");
                var masikonn = lista.Where(x=>x.ado == 3 && x.ido3 <= omegaido).ToArray().Last();
                System.Console.WriteLine($"{masikonn.ado}.ado - {masikonn.banda}:{masikonn.zene}");
            }
            else
            {
                var omegaido = lista.Where(x=>x.banda == "Omega" && x.zene == "Legenda").ToList().First().ido3;

                var masikon = lista.Where(x=>x.ado == 1 && x.ido1 <= omegaido).ToList().Last();
                System.Console.WriteLine($"{masikon.ado}.ado - {masikon.banda}:{masikon.zene} ({masikon.id}.)");
                var masikonn = lista.Where(x=>x.ado == 2 && x.ido2 <= omegaido).ToList().Last();
                System.Console.WriteLine($"{masikonn.ado}.ado - {masikonn.banda}:{masikonn.zene} ({masikonn.id}.)");
            }   


            System.Console.WriteLine("5.feladat");
            List<string> muzsika = new List<string>();
            foreach (var item in lista)
            {
                muzsika.Add(item.banda + ": " + item.zene);
            }
            var simaliba = muzsika.Distinct().ToList();

            System.Console.Write("suta tipped:");
            string sutatipp = Console.ReadLine().ToLower();
            List<string> vegso = new List<string>();
            foreach (var szo in simaliba)
            {
                if(rekurzio(sutatipp, szo))
                {
                    vegso.Add(szo);
                }
            }

            StreamWriter writer = new StreamWriter("keres.txt");
            writer.WriteLine(sutatipp);
            foreach (var item in vegso)
            {
                writer.WriteLine($"{item}");
                System.Console.WriteLine(item);
            }
            writer.Close();

            System.Console.WriteLine("6.feladat");
            var radio1 = lista.Where(x => x.ado == 1).Select(x => x.extra1).ToList();
            var radiosum = radio1.Last();
            System.Console.WriteLine(radiosum);
            int extraora=180;
            int extraszam = 60;
            int kitoltendo  = 0;
            int summa = 0;
            for (int i = 0; i < radio1.Count-1; i++)
            {
                summa = 180 + radio1[i];
                for (int j = 1; j <= radiosum/3600; j++)
                {
                    if (summa <= j*3600 && radio1[i+1]+180 >=j*3600)
                    {
                        kitoltendo = 3*60 + ((j*3600)-radio1[i]);
                        summa += kitoltendo;
                        System.Console.WriteLine(summa);
                        Console.ReadLine();
                    }
                }
            }
            System.Console.WriteLine($"vege: {summa} -- {format(summa)}");
        }

        static bool rekurzio(string sutatipp, string csekk)
        {
            csekk = csekk.ToLower();
            if (sutatipp.Length < 1)
            {
                return true;
            }
            else if (csekk.Contains(sutatipp[0]))
            { 
                int index = csekk.IndexOf(sutatipp[0]);
                csekk = csekk.Remove(0, index+1); //remove all characters up to and including the index
                sutatipp = sutatipp.Remove(0, 1);
                //System.Console.WriteLine($"{sutatipp} - {csekk}");
                //Console.ReadLine();
                
                return rekurzio(sutatipp, csekk);  
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            readIn();
            exercises();
            Console.ReadLine();
        }
        
        static string format(int sec)
        {
            int hour = sec/3600;
            int minute = (sec%3600)/60;
            int second = (sec%3600)%60;
            return $"{hour}:{minute}:{second}";
        }
    }