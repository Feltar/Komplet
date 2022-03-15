using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
/*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
/*1.	Napište enum popisující soubor karet, vytvořte množinu zamíchaných karet a rozdejte z ní 4 balíčky po 4 kartách.    
    Můžete vytvořit např. 2 enum typy barva: (červená, zelená, žalud, kule)  a karta: (sedma, osma, devítka, desítka, spodek, svršek, král, eso). 
    Přiřaďte barvám hodnoty (7,8,9,10,1, 20, 2 11). Vypište rozdané karty a celkovou hodnotu rozdaného balíčku*/
namespace Cviko3
{
    enum Barva
    {
        piky, káry, srdce, kříze
    }
    enum Typ
    {
        sedma = 7, osma = 8, devítka = 9, desítka = 10, spodek = 1, svršek = 20, král = 2, eso = 11
    }
    class Karta
    {
        public Karta(Barva barva, Typ typ)
        {
            this.barva = barva;
            this.typ = typ;
        }
        public override string ToString()
        {
            string retezec = barva + " " + typ;
            return retezec;
        }
        Barva barva;
        Typ typ;
    }
    class Balicek : IEnumerable
    {
        private List<Karta> karty;

        public int pocet
        {
            get
            {
                return karty.Count();
            }
        }
        public void prohod(int i, int j)
        {
            Karta karta = karty[i];
            karty[i] = karty[j];
            karty[j] = karta;
        }
        public void zamichej()
        {
            Random rn = new Random();
            int kam = rn.Next();
            for (int i = pocet - 1; i > 0; i--)
            {
                prohod(i, rn.Next(i + 1));
            }
        }
        public Balicek()
        {
            this.karty = new List<Karta>();
        }
        public void pridej_kartu(Karta karta)
        {
            karty.Add(karta);
        }
        public bool odeber(Karta odebirana)
        {
            return karty.Remove(odebirana);
        }

        public IEnumerator GetEnumerator()
        {
            return karty.GetEnumerator();
        }
        public Karta this[int i]
        {
            get
            {
                return karty[i];
            }
            set
            {
                karty[i] = value;
            }
        }
    }
    class Cviko3_Hra
    {
        Balicek rozdavaci_balicek;
        public Cviko3_Hra()
        {
            //vygeneruji rozdavaci balicek
            rozdavaci_balicek = new Balicek();
            foreach (Barva barva in Enum.GetValues(typeof(Barva)))
            {
                foreach (Typ typ in Enum.GetValues(typeof(Typ)))
                {
                    rozdavaci_balicek.pridej_kartu(new Karta(barva, typ));
                }
            }
            //zamicham a rozdam
            rozdavaci_balicek.zamichej();
            Balicek balicek1 = new Balicek(); Balicek balicek2 = new Balicek();
            Balicek balicek3 = new Balicek(); Balicek balicek4 = new Balicek();
            odeber_ctyri_karty(balicek1); odeber_ctyri_karty(balicek2);
            odeber_ctyri_karty(balicek3); odeber_ctyri_karty(balicek4);

            vypis_balicek(balicek1); vypis_balicek(balicek2); vypis_balicek(balicek3); vypis_balicek(balicek4);
        }
        private void odeber_ctyri_karty(Balicek balicek)
        {
            for (int i = 0; i < 4; i++)
            {
                Karta karta = rozdavaci_balicek[0];
                rozdavaci_balicek.odeber(karta);
                balicek.pridej_kartu(karta);
            }
        }
        private void vypis_balicek(Balicek balicek)
        {
            Console.WriteLine("/////////////////////////////////////////");
            foreach (var karta in balicek)
            {
                Console.WriteLine(karta);
            }


        }
    }
    /*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
    /*Vytvořte třídu obsahující jméno a příjmení. Implementujte interface Comparable  pro tento objekt. Vytvořte množinu SortedSet 
        se studenty na tomto cvičení (použijte třídu TreeSet). Vypište setříděný seznam studentů podle příjmeních a jmen.
     Vytvořte mapu (HashMap). Ke každému jménu  přiřadíte heslo.
        -	Napište program, kde zadáte jméno a zkontrolujete správnost hesla. Při chybném heslu nebo nesprávném jménu vyhoďte výjimku.*/
    class Zak : IComparable
    {
        public string jmeno { get; }
        public string prijmeni { get; }
        public Zak(string jmeno, string prijmeni)
        {
            this.jmeno = jmeno;
            this.prijmeni = prijmeni;
        }
        public int CompareTo(object obj)
        {
            Zak zak = obj as Zak;
            if (zak != null)
            {
                switch (zak.prijmeni.CompareTo(this.prijmeni))
                {
                    case 1: return 1;
                    case -1: return -1;
                    default: break;
                }
                switch (zak.jmeno.CompareTo(this.jmeno))
                {
                    case 1: return 1;
                    case -1: return -1;
                    case 0: return 0;
                }
            }
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "prijmeni: " + prijmeni + ", jmeno: " + jmeno;
        }
    }
    class Ucet
    {
        public Ucet(Zak zak, string heslo)
        {
            this.zak = zak;
            this.heslo = heslo;
        }
        private string spocti_hash(string text, string sul)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }
            //Sha256 je typu IDisposable, using je elegantnější způsob, kdy nemusim vypisovat try/catch atp...
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // SHA256
                byte[] prekodovano_na_pole_bajtu = System.Text.Encoding.UTF8.GetBytes(text + sul);
                byte[] hash_byty = sha.ComputeHash(prekodovano_na_pole_bajtu);
                //metoda BitConverter.ToString(...) dava ve vyslednem stringu oddeliuje jednotlive hexadecimalni kody znakem "-". Ty musime smazat - napr funkci string.Replace(...). 
                string hash = BitConverter.ToString(hash_byty).Replace("-", String.Empty);
                return hash;
            }
        }
        public bool verifikuj(string verifikovane_heslo)
        {
            return spocti_hash(verifikovane_heslo, sul) == _heslo;
        }
        Zak zak;
        public string getJmeno() { return zak.jmeno; }
        public string getPrijmeni() { return zak.prijmeni; }
        private string _heslo;
        private string sul;
        public string heslo
        {
            set
            {
                Random rn = new Random();
                String sul_prac = "";
                String znaky = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                for (int i = 0; i < Math.Max(rn.Next(256), 10); i++)
                {
                    sul_prac += znaky[rn.Next(znaky.Length)];
                }

                _heslo = spocti_hash(value, sul_prac);
                sul = sul_prac;
            }
        }
    }
    class Ucty : IEnumerable
    {
        public void pridej(Ucet ucet)
        {
            ucty.Add(ucet);
        }
        List<Ucet> ucty = new List<Ucet>(); //= new List<Ucet>(); 
        public IEnumerator GetEnumerator()
        {
            return ucty.GetEnumerator();
            throw new NotImplementedException();
        }
        public Ucet vrat_ucet(string jmeno, string prijmeni)
        {
            foreach (Ucet ucet in ucty)
            {
                if (ucet.getJmeno() == jmeno && ucet.getPrijmeni() == prijmeni)
                {
                    return ucet;
                }
            }
            return null;
        }
    }
    class Cviko3_Hlavni_Program
    {
        public static void main()
        {
            /*trideni a vypisovani*/
            List<Zak> zaci = new List<Zak>();
            zaci.Add(new Zak("Antonín", "Švehla"));
            zaci.Add(new Zak("Alois", "Rašín"));
            zaci.Sort();

            Console.WriteLine("***********************************************************************");
            Console.WriteLine("Vypis Zaku:");
            foreach (var Zak in zaci)
            {
                Console.WriteLine(Zak);
            }
            /*Hesla a Hash*/
            Ucty ucty = new Ucty();
            foreach (var zak in zaci)
            {
                ucty.pridej(new Ucet(zak, "heslo"));
            }

            //*****************************************************************************************************
            bool nezadane_jmeno = true;
            string jmeno;
            string prijmeni;
            Ucet zadany_ucet = null;
            while (nezadane_jmeno)
            {
                Console.WriteLine("Zadejte jmeno");
                jmeno = Console.ReadLine();
                Console.WriteLine("Zadejte prijmeni");
                prijmeni = Console.ReadLine();
                zadany_ucet = ucty.vrat_ucet(jmeno, prijmeni);
                if (zadany_ucet == null) Console.WriteLine("Takový žák neexistuje.");
                else nezadane_jmeno = false;

            }
            bool nezadane_heslo = true;
            while (nezadane_heslo)
            {
                Console.WriteLine("Zadejte 'heslo'");
                string zadane_heslo = Console.ReadLine();
                if (!zadany_ucet.verifikuj(zadane_heslo))
                {
                    Console.WriteLine("Spatne heslo"); //vyhod vyjimku
                }
                else
                {
                    Console.WriteLine("Dobra prace");
                    nezadane_heslo = false;
                }

            }
        }
    }
}