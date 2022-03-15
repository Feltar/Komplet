using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

/*Přečtěte ze systémového vstupního streamu Systém.in matici. Ke čtení použijte třídu Scanner. Konce řádek matice ukončete 
  středníkem a konec vstupu zapište jako „k“. Zjistěte maximální velikost  řádku a doplňte menší řádky nulami do maximálního 
  počtu. Matici uložte do souboru. Uložený soubor přečtěte a vytiskněte.*/
namespace UlohaMatice
{
    class UlohaMatice
    {
        public static List<List<int>> nacti_matici_a_dopln_nuly(String cesta_k_souboru)
        {

            String retezec = ""; //retezec do kteryho nacitam cislo
            List<int> radek = new List<int>(); //vzdy aktualni nacitany radek
            int max_delka_radku = 0;
            int delka_radku = 0;
            List<List<int>> matice = new List<List<int>>();
            bool nenarazeno_k = true;
            using (StreamReader rda = new StreamReader(cesta_k_souboru))
            {
                while (!rda.EndOfStream && nenarazeno_k)
                {
                    char znak = (char)rda.Read();
                    if ('0' <= znak && znak <= '9')
                        retezec += znak;

                    else
                    {
                        if (retezec != "")
                        {
                            radek.Add(int.Parse(retezec));
                            delka_radku++;
                            retezec = "";
                        }
                        switch (znak)
                        {
                            case ';':
                                matice.Add(radek);
                                radek = new List<int>();
                                if (delka_radku > max_delka_radku)
                                    max_delka_radku = delka_radku;
                                delka_radku = 0; break;
                            case 'k':
                                if (radek.Count() != 0)
                                {
                                    if (delka_radku > max_delka_radku)
                                        max_delka_radku = delka_radku;
                                    matice.Add(radek);
                                }
                                nenarazeno_k = false; break;
                            default:
                                break;
                        }
                    }
                }
            }
            //doplneni nul
            foreach (List<int> rad in matice)
            {
                int pocet_v_aktualnim_radku = rad.Count;
                for (int i = 0; i < max_delka_radku - pocet_v_aktualnim_radku; i++)
                {
                    rad.Add(0);
                }
            }
            return matice;
        }
        public static void vypis_matici_do_souboru(List<List<int>> matice, String jmeno_souboru)
        {
            using (StreamWriter vystupni_stream = File.CreateText(jmeno_souboru))
            {
                foreach (List<int> radek in matice)
                {
                    foreach (int prvek in radek)
                    {
                        vystupni_stream.Write(prvek); vystupni_stream.Write(' ');
                    }
                    vystupni_stream.WriteLine();
                }
                vystupni_stream.Close();
            }
        }
        public static void prepis_ze_souboru_na_konzoli(List<List<int>> matice, String jmeno_souboru)
        {
            List<String> retezce = new List<String>();
            StreamReader rda = new StreamReader(jmeno_souboru);
            while (!rda.EndOfStream)
            {
                String radek = rda.ReadLine();
                foreach (char prvek in radek)
                {
                    Console.Write(prvek);
                }
                Console.WriteLine();
            }
        }
        public static void main(String cesta_k_souboru)
        {
            List<List<int>> matice = nacti_matici_a_dopln_nuly(cesta_k_souboru);
            String cesta_k_souboru2 = "matice_ulozena.txt";//tohle je cesta k souboru cislo 2 read or write!!!
            vypis_matici_do_souboru(matice, cesta_k_souboru2);
            prepis_ze_souboru_na_konzoli(matice, cesta_k_souboru2);
        }
    }
    /*
     V projektu je soubor „cviceni4utf.txt“, který obsahuje text v češtině zapsaný v kódu UTF-8. Soubor zkopírujte do svého projektu 
     metodou java.nio.file.Files.copy(…) jako „data.utf“. Přepište tento soubor do znakového kódu WINDOWS-1250, tento soubor nechť se 
     jmenuje “data.txt“  Porovnejte délku obou souborů. Vytiskněte počet řádků, slov a čísel, které jsou v tomto souboru. 
     Ke čtení použijte StreamTokenizer. 
     */
    class Trida_Prekodovani
    {
        public static String readFileAsUtf8(string fileName)
        {
            Encoding encoding = Encoding.Default;
            String original = String.Empty;

            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                original = sr.ReadToEnd();
                encoding = sr.CurrentEncoding;
                sr.Close();
            }

            if (encoding == Encoding.UTF8)
                return original;

            byte[] encBytes = encoding.GetBytes(original);
            byte[] utf8Bytes = Encoding.Convert(encoding, Encoding.UTF8, encBytes);
            return Encoding.UTF8.GetString(utf8Bytes);
        }
        static void prekoduj(String jmeno_souboru)
        {
            FileInfo fi = new FileInfo(jmeno_souboru);
            fi.CopyTo("data.utf", true);
            readFileAsUtf8("data.utf");
        }
        static public void main(String cesta_k_souboru)
        {
            FileInfo soubor = new FileInfo(cesta_k_souboru);
            String cesta_k_data_utf8 = "data.utf";
            soubor.CopyTo(cesta_k_data_utf8, true);

            using (StreamReader čtečka = new StreamReader(cesta_k_data_utf8, Encoding.GetEncoding("utf-8"), false))
            {
                using (StreamWriter zapisovačka = new StreamWriter("data.txt", false, Encoding.GetEncoding("windows-1250")))
                {
                    while (!čtečka.EndOfStream)
                    {
                        zapisovačka.Write(čtečka.Read());
                    }
                    zapisovačka.Close();
                }
            }
            //*******************************************porovnani delek
            FileInfo info_o_utf = new FileInfo(cesta_k_data_utf8);
            FileInfo info_o_windows = new FileInfo("data.txt");
            long delka_utf = info_o_utf.Length;
            long delka_windows = info_o_utf.Length;
            if (delka_utf == delka_windows) Console.WriteLine("Soubour data.utf kódovaný utf8 je stejně dlouhý jako, když ho překodujeme do windows-1250.");
            else if (delka_utf > delka_windows) Console.WriteLine("Soubour data.utf kódovaný utf8 je delší než když ho překodujeme do windows-1250.");
            else Console.WriteLine("Soubour data.utf kódovaný utf8 je kratší než když ho překodujeme do windows-1250.");
        }
    }
    /*
     Napiště třídy, která popisuje různé geometrické útvary ve dvojrozměrném prostoru: Ctryuhelnik Trojuhelnik, Elipsa. Ctyřúhelník 
     nechť je definován čtyřmi vrcholy, trojúhelník třemi body a elipsa krajními body hlavní osy a délkou vedlejší poloosy. Tyto třídy 
     nechť mají implementován interface Utvary, který má metody „obsah“ a „obvod“ a je rozšířením interface java.io.Serializable, což
     nám umožní tyto objekty zapsat do souboru pomocí ObjectWriteru. Doporučujeme rovněž navrhnout třídu Bod  a Usecka, což usnadní 
     celkově návrh – obě tyto třídy rovněž musí mít implementován interface Seriazable.
     Pomocí konstruktorů vytvořte několik útvarů umístěných v rovině a zapište je do souboru „utvary.dat“.
     Přečtěte zapsané útvary a vytiskněte informace o útvaru (jmeno, sourcadnice, obvod a obsah každého z nich).   
        -> v C# se zapis objektu do souboru dela pomoci dekorace atributem  [Serializable] -> to rekne kompilatoru, ze dany soubor muzeme chtit za behu vytisknout
        do souboru a pak pomoci BineryStreamu, coz je pomocna trida, kterou zapisujeme sekvencni vstup.
     */
    interface Utvar
    {
        double obvod();
        double obsah();
    }
    [Serializable]
    class Bod
    {
        public double x { get; }
        public double y { get; }
        public Bod(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
    [Serializable]
    class Usecka
    {
        public Bod b1 { get; }
        public Bod b2 { get; }
        public Usecka(int x1, int y1, int x2, int y2)
        {
            this.b1 = new Bod(x1, y1);
            this.b2 = new Bod(x2, y2);
        }
        public Usecka(Bod b1, Bod b2)
        {
            this.b1 = b1;
            this.b2 = b2;
        }
        public double delka()
        {
            return Math.Sqrt((b1.x - b2.x) * (b1.x - b2.x) + (b1.y - b2.y) * (b1.y - b2.y));
        }
    }

    [Serializable]
    class Trojuhelnik : Utvar
    {
        public Bod b1 { get; }
        public Bod b2 { get; }
        public Bod b3 { get; }
        private Usecka _u12;
        private Usecka _u13;
        private Usecka _u23;
        public Trojuhelnik(Bod b1, Bod b2, Bod b3)
        {
            this.b1 = b1; this.b2 = b2;
            this.b3 = b3;
            this._u12 = new Usecka(b1, b2);
            this._u13 = new Usecka(b1, b3);
            this._u23 = new Usecka(b2, b3);
        }
        public double obvod()
        {
            return _u12.delka() + _u13.delka() + _u23.delka();
        }
        public double obsah()
        {
            double s = _u12.delka() + _u13.delka() + _u23.delka();
            return s * (s - _u12.delka()) * (s - _u13.delka()) * (s - _u12.delka());
        }
    }
    [Serializable]
    class Ctyruhelnik : Utvar
    {
        public Bod b1 { get; }
        public Bod b2 { get; }
        public Bod b3 { get; }
        public Bod b4 { get; }
        private Usecka _u12; private Usecka _u14;
        private Usecka _u23; private Usecka _u34;
        public Ctyruhelnik(Bod b1, Bod b2, Bod b3, Bod b4)
        {
            this.b1 = b1; this.b2 = b2;
            this.b3 = b3; this.b4 = b4;
            this._u12 = new Usecka(b1, b2);
            this._u14 = new Usecka(b1, b4);
            this._u23 = new Usecka(b2, b3);
            this._u34 = new Usecka(b3, b4);
        }
        public double obsah()
        {
            return (new Trojuhelnik(b1, b2, b3).obsah()) + (new Trojuhelnik(b2, b3, b4).obsah());
        }
        public double obvod()
        {
            return _u12.delka() + _u14.delka() + _u23.delka() + _u34.delka();
        }
    }
    [Serializable]
    class Elipsa : Utvar
    {
        public Bod b1; public Bod b2;
        public Usecka _hlavni_poloosa { get; }
        public double delka_vedlejsi_poloosy { get; }
        public double obvod()
        {
            return Math.PI * Math.Sqrt(2 * (_hlavni_poloosa.delka() * _hlavni_poloosa.delka() / 2 + delka_vedlejsi_poloosy * delka_vedlejsi_poloosy));
        }
        public double obsah()
        {
            return Math.PI * _hlavni_poloosa.delka() / 2 * delka_vedlejsi_poloosy;
        }
        public Elipsa(Bod b1, Bod b2, double delka_vedlejsi_poloosy)
        {
            this.b1 = b1;
            this.b2 = b2;
            this.delka_vedlejsi_poloosy = delka_vedlejsi_poloosy;
            this._hlavni_poloosa = new Usecka(b1, b2);
        }
    }



    class Test
    {
        static public void main()
        {
            Random generátor = new Random();
            Utvar[] utvary = new Utvar[10];
            for (int i = 0; i < 10; i++)
            {
                double d = generátor.NextDouble();
                int a = ((int)(1.0 + d * 3));
                switch (a)
                {
                    case 1:
                        {
                            Bod b1 = new Bod(generátor.NextDouble() * 5, generátor.NextDouble() * 5);
                            Bod b2 = new Bod(generátor.NextDouble() * 5, generátor.NextDouble() * 5);
                            Bod b3 = new Bod(generátor.NextDouble() * 5, generátor.NextDouble() * 5);
                            utvary[i] = new Trojuhelnik(b1, b2, b3);
                            break;
                        }
                    case 2:
                        {
                            Bod b1 = new Bod(generátor.NextDouble() * 5, generátor.NextDouble() * 5);
                            Bod b2 = new Bod(generátor.NextDouble() * 5, generátor.NextDouble() * 5);
                            double delka_poloosy = generátor.NextDouble() * 2;
                            utvary[i] = new Elipsa(b1, b2, delka_poloosy);
                            break;
                        }
                    case 3:
                        {
                            Bod b1 = new Bod(generátor.NextDouble() * 5, generátor.NextDouble() * 5);
                            Bod b2 = new Bod(generátor.NextDouble() * 5, generátor.NextDouble() * 5);
                            Bod b3 = new Bod(generátor.NextDouble() * 5, generátor.NextDouble() * 5);
                            Bod b4 = new Bod(generátor.NextDouble() * 5, generátor.NextDouble() * 5);
                            utvary[i] = new Ctyruhelnik(b1, b2, b3, b4);
                            break;
                        }
                    default:
                        break;
                }
            }

            string jmeno_souboru = "dokument.dat";
            FileInfo data_soubor = new FileInfo(jmeno_souboru);
            BinaryFormatter binarni_formater = new BinaryFormatter();
            //Formatovani se da udelat i rucne bez BinaryFormatteru. V takovem pripade dane tridy musi implementovat ISerializable rozhrani - v nem GetObjectData() metodu
            //a konstruktor s digitalnim podpisem (SerializableInfo info, StartingContext context). V GetObjectData clovek musi pro kazdou promennou nadefinovat par promenna a
            //nazev (string) podle kterych bude ulozeno v souboru. TO se pridava do info.AddValue("nazev", promenna). 
            //V jiz zminovanem konstruktoru pri nacitani pak nacitam prislusna data na zaklade toho nazvu (info.GetValue("nazev")) a prirazuji do
            //poli toho nacitaneho objektu. - Zpravidla se jedna o protected konstruktor, neni urcen k ledabylemu volani.
            for (int i = 0; i < 10; i++)
            {
                using (Stream zapisovaci_stream_objektu = new FileStream("dokument.dat", FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    binarni_formater.Serialize(zapisovaci_stream_objektu, utvary[i]);
                }
            }
            Utvar[] utvary_nacteny = new Utvar[10];
            using (Stream vypisovaci_stream_objektu = File.OpenRead(jmeno_souboru))
            {
                for (int i = 0; i < 10; i++)
                {
                    utvary_nacteny[i] = (Utvar)binarni_formater.Deserialize(vypisovaci_stream_objektu);
                }
            }
            foreach (Utvar utvar in utvary_nacteny)
            {
                if (utvar is Elipsa)
                {
                    Console.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine("Jedná se o elipsu definovanou body hlavní osy:");

                    /* Console.Write("Bod b1 = [" + ((Elipsa)utvar).b1.x + "; " + ((Elipsa)utvar).b1.y + "]");
                    Console.WriteLine(" a bod b2 = [" + ((Elipsa)utvar).b2.x + "; " + ((Elipsa)utvar).b2.y + "]");
                    Console.Write("a délkou vedlejší poloosy: ");
                    Console.WriteLine(((Elipsa)utvar).delka_vedlejsi_poloosy);*/
                    Console.Write("Bod b1 = [" + Math.Round(((Elipsa)utvar).b1.x, 2) + "; " + Math.Round(((Elipsa)utvar).b1.y, 2) + "]");
                    Console.WriteLine(" a b2 = [" + Math.Round(((Elipsa)utvar).b2.x, 2) + "; " + Math.Round(((Elipsa)utvar).b2.y, 2) + "]");
                    Console.Write("a délkou vedlejší poloosy: ");
                    Console.WriteLine(Math.Round(((Elipsa)utvar).delka_vedlejsi_poloosy, 2));



                    Console.Write("Její obsah je " + Math.Round(utvar.obsah(), 2) + ' ');
                    Console.WriteLine("a její obvod je " + Math.Round(utvar.obvod(), 2) + ' ');
                    Console.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                }
                if (utvar is Ctyruhelnik)
                {
                    Console.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine("Jedná se o čtyřúhelník definovaný následujícími čtyřmi vrcholy");
                    Console.Write("Bod b1 = [" + Math.Round(((Ctyruhelnik)utvar).b1.x, 2) + "; " + Math.Round(((Ctyruhelnik)utvar).b1.y, 2) + "]");
                    Console.WriteLine(", b2 = [" + Math.Round(((Ctyruhelnik)utvar).b2.x, 2) + "; " + Math.Round(((Ctyruhelnik)utvar).b2.y, 2) + "]");
                    Console.Write(", b3 = [" + Math.Round(((Ctyruhelnik)utvar).b3.x, 2) + "; " + Math.Round(((Ctyruhelnik)utvar).b3.y, 2) + "]");
                    Console.WriteLine(" a b4 = [" + Math.Round(((Ctyruhelnik)utvar).b4.x, 2) + "; " + Math.Round(((Ctyruhelnik)utvar).b4.y, 2) + "]");


                    Console.Write("Jeho obsah je " + Math.Round(utvar.obsah(), 2) + ' ');
                    Console.WriteLine("a obvod  " + Math.Round(utvar.obvod(), 2) + ' ');
                    Console.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                }
                if (utvar is Trojuhelnik)
                {
                    Console.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine("Jedná se o trojúhelník definovaný následujícími třemi vrcholy");
                    Console.Write("Bod b1 = [" + Math.Round(((Trojuhelnik)utvar).b1.x, 2) + "; " + Math.Round(((Trojuhelnik)utvar).b1.y, 2) + "]");
                    Console.WriteLine(", b2 = [" + Math.Round(((Trojuhelnik)utvar).b2.x, 2) + "; " + Math.Round(((Trojuhelnik)utvar).b2.y, 2) + "]");
                    Console.WriteLine(" a b3 = [" + Math.Round(((Trojuhelnik)utvar).b3.x, 2) + "; " + Math.Round(((Trojuhelnik)utvar).b3.y, 2) + "]");


                    Console.Write("Jeho obsah je " + Math.Round(utvar.obsah(), 2) + ' ');
                    Console.WriteLine("a obvod  " + Math.Round(utvar.obvod(), 2) + ' ');
                    Console.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                }
            }
        }
    }
}
