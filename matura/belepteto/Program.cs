using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;

namespace belepteto
{
    class Program
    { 
        struct rek2
        {
            public string nev;
            public int hanyszor;
        }
        struct valami
        {
            public string szemazon;
            public string ido;
            public TimeSpan ora_perc;
            public int kod;
            public int ora, perc;
        }
        static List<valami> lista = new List<valami>();
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("bedat.txt");
            string sor;
            while (!sr.EndOfStream)
            {
                valami helper = new valami();
                sor = sr.ReadLine();
                string[] atmenet = sor.Split(' ');
                helper.szemazon = atmenet[0];
                helper.ido = atmenet[1];
                helper.ora_perc = TimeSpan.Parse(atmenet[1]);
                helper.kod = int.Parse(atmenet[2]);
                string[] oraba = atmenet[1].Split(':');
                helper.ora = int.Parse(oraba[0]);
                helper.perc = int.Parse(oraba[1]);
                lista.Add(helper);
            }
            Console.WriteLine("2. feladat");
            Console.WriteLine("Az első tanuló {0}-kor lépett be a főkapun",lista[0].ido);
            Console.WriteLine("Az utolsó tanuló {0}-kor lépett ki a főkapun", lista[lista.Count-1].ido);
            //3. feladat
            StreamWriter sw = new StreamWriter("kesok.txt");
            var fel3 = lista.Where(x => x.ora_perc > TimeSpan.Parse("07:50") && x.ora_perc <= TimeSpan.Parse("08:15") && x.kod==1).ToList();
            for (int i = 0; i < fel3.Count(); i++)
            {
                sw.WriteLine("{0} {1}",fel3[i].ido,fel3[i].szemazon);
            }
            sw.Close();
            Console.WriteLine("4. feladat");
            var fel4 = lista.Where(x => x.kod == 3).Count();
            Console.WriteLine("A menzán aznap {0} tanuló ebédelt.",fel4);
            Console.WriteLine("5. feladat");
            var fel5 = lista.Where(x => x.kod == 4).GroupBy(x => x.szemazon).ToList();
            Console.WriteLine("Aznap {0} tanuló kölcsönzött  a könyvtárban.",fel5.Count());
            if (fel4>fel5.Count())
            {
                Console.WriteLine("Nem voltak többen, mint a menzán");
            }
            else
            {
                Console.WriteLine("Többen voltak, mint a menzán");
            }
            Console.WriteLine("6. feladat");
            Console.WriteLine("Az érintett tanulók:");
            var bejonnek = lista.Where(x =>  x.kod == 1 ).GroupBy(x=>x.szemazon).ToList();
            var kimennek = lista.Where(x => x.kod == 2).GroupBy(x => x.szemazon).ToList();

            List<rek2> be = new List<rek2>();
            List<rek2> ki = new List<rek2>();
            foreach (var item in bejonnek)
            {
                rek2 helper = new rek2();
                helper.nev = item.Key;
                helper.hanyszor = item.Count();
                be.Add(helper);
            }
            foreach (var item in kimennek)
            {
                rek2 helper = new rek2();
                helper.nev = item.Key;
                helper.hanyszor = item.Count();
                ki.Add(helper);
            }
            for (int i = 0; i < ki.Count(); i++)
            {
                for (int j = 0; j < be.Count(); j++)
                {
                    if (be[i].nev==ki[j].nev && be[i].hanyszor>ki[j].hanyszor)
                    {
                        Console.Write($"{be[i].nev}\t");
                    }
                }
            }
            Console.WriteLine();

            /* Console.WriteLine("7. feladat");
            Console.Write("Egy tanuló azonosítója=");
            string aktszemazon = Console.ReadLine();
            var adott_tanulo_be = lista.Where(x => x.szemazon == aktszemazon && x.kod==1).OrderBy(x=>x.ido).ToList();
            var adott_tanulo_ki = lista.Where(x => x.szemazon == aktszemazon && x.kod == 2).OrderBy(x => x.ido).ToList();
           TimeSpan elteltido = (adott_tanulo_ki[adott_tanulo_ki.Length - 1].ora_perc - adott_tanulo_be[0].ora_perc);
            Console.WriteLine(elteltido);
            Console.WriteLine(   elteltido.Hours)
            Console.WriteLine("A tanuló érkezése és távozása között {0} óra és {1} perc telt el",(-adott_tanulo_be[0].ora_perc+adott_tanulo_ki[adott_tanulo_ki.Count()-1].ora_perc).Hours, (-adott_tanulo_be[0].ora_perc +adott_tanulo_ki[adott_tanulo_ki.Count() - 1].ora_perc).Minutes);
            Console.ReadLine(); */

            //6. alternativa
            
            var elott1045 = lista.Where(x => x.ora < 10 || (x.ora==10 && x.perc <= 45)).Where(x => x.kod == 1).Select(x => x.szemazon).ToHashSet();
            var utan1050 = lista.Where(x => (x.ora == 10 && x.perc >= 50) || (x.ora == 11 && x.perc == 0)).Where(x => x.kod == 1).Select(x => x.szemazon).ToHashSet();
            var kozott4550 = lista.Where(x => x.ora == 10 && (x.perc <=50 && x.perc >= 45)).Where(x => x.kod == 2).Select(x => x.szemazon).ToHashSet();

            //System.Console.WriteLine(utan1050.Count());
            utan1050.IntersectWith(elott1045);
            //System.Console.WriteLine(utan1050.Count());
            utan1050.ExceptWith(kozott4550);
            //System.Console.WriteLine(utan1050.Count());


            foreach (var item in utan1050){
                System.Console.Write($"{item}\t");
            }
            Console.ReadLine();
            
        }
    }
}
