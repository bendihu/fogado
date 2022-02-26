namespace fogado;

public class Tanarok
{
    public string Tanar { get; set; }
    public string OOPP { get; set; }
        public int Ora { get; set; }
        public int Perc { get; set; }
    public string Datum { get; set; }
}
public class Program
{
    static List<Tanarok> lista = new List<Tanarok>();
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //1. feladat
        Beolvas();

        //2. feladat
        Feladat2();

        //3. feladat
        Feladat3();

        //4. feladat
        Feladat4();

        //5. feladat
        Feladat5();

        //6. feladat
        Feladat6();

        Console.ReadKey();
    }
    private static void Beolvas()
    {
        StreamReader sr = new StreamReader(@"fogado.txt");

        while (!sr.EndOfStream)
        {
            string[] line = sr.ReadLine().Split(' ');
            Tanarok uj = new Tanarok();

            uj.Tanar = $"{line[0]} {line[1]}";

            uj.OOPP = line[2];
            uj.Ora = int.Parse(line[2].Substring(0, 2));
            uj.Perc = int.Parse(line[2].Substring(3, 2));

            uj.Datum = line[3];

            lista.Add(uj);
        }

        sr.Close();
    }
    private static void Feladat2()
    {
        Console.WriteLine("2. feladat");
        Console.WriteLine($"Foglalások száma: {lista.Count}");
        Console.WriteLine();
    }
    private static void Feladat3()
    {
        Console.WriteLine("3. feladat");

        Console.Write("Adjon meg egy nevet: ");
        string bNev = Console.ReadLine();

        var szures = lista.Where(x => x.Tanar == bNev).ToList();

        if (szures.Count == 0) Console.WriteLine("A megadott néven nincs időpontfoglalás.");
        else Console.WriteLine($"{bNev} néven {szures.Count} időpontfoglalás van.");
        Console.WriteLine();
    }
    private static void Feladat4()
    {
        Console.WriteLine("4. feladat");

        Console.Write("Adjon meg egy érvényes időpontot (pl. 17:10): ");
        string bIdo = Console.ReadLine();

        string fajlNev = bIdo.Replace(":", null);
        StreamWriter sw = new StreamWriter(@$"{fajlNev}.txt");

        var szures = lista.Where(x => x.OOPP == bIdo).OrderBy(x => x.Tanar).ToList();

        foreach (var item in szures)
        {
            Console.WriteLine(item.Tanar);
            sw.WriteLine(item.Tanar);
        }

        Console.WriteLine();
        sw.Close();
    }
    private static void Feladat5()
    {
        Console.WriteLine("5. feladat");

        var elso = lista.FirstOrDefault();

        Console.WriteLine($"Tanár neve: {elso.Tanar}");
        Console.WriteLine($"Foglalt időpont: {elso.OOPP}");
        Console.WriteLine($"Foglalás ideje: {elso.Datum}");
        Console.WriteLine();
    }
    private static void Feladat6()
    {
        Console.WriteLine("5. feladat");

        var foglalt = lista.Where(x => x.Tanar == "Barna Eszter").OrderBy(x => x.OOPP).ToList();

        for (int i = 16; i < 18; i++)
        {
            var szabad = "";

            for (int j = 0; j < 60; j += 10)
            {
                szabad = $"{i:D2}:{j:D2}";

                if (!foglalt.Select(x => x.OOPP).Contains(szabad))
                {
                    Console.WriteLine(szabad);
                }
            }
        }

        var utolso = foglalt.LastOrDefault();
        int ora = 0, perc = 0;

        if (utolso.Perc + 10 < 60)
        {
            perc = utolso.Perc + 10;
            ora = utolso.Ora;
        }
        else if (utolso.Perc + 10 == 60)
        {
            perc = 0;
            ora = utolso.Ora + 1;
        }

        Console.WriteLine($"Barna Eszter legkorábban távozhat: {ora:D2}:{perc:D2}");
        Console.WriteLine();
    }
}