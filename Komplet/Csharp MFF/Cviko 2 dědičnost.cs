using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**************************************kód ze zadání**********************************************************/
namespace Cviko2
{
    public abstract class RovinneVlastnosti
    {
        int DIMENSION = 2;
        public abstract double delkaObvodu();
        public abstract double obsah();
        public abstract String jmeno();
    }
    class Obdelnik : RovinneVlastnosti
    {
        private double delka;
        private double vyska;
        public Obdelnik(double delka, double vyska)
        {
            this.delka = delka;
            this.vyska = vyska;
        }

        public override double delkaObvodu()
        {
            return 2 * (delka + vyska);
        }

        public override double obsah()
        {
            return delka * vyska;
        }

        public override String jmeno()
        {
            return "Obdélník";
        }
    }
    class Ctverec : RovinneVlastnosti
    {
        private double strana;
        public Ctverec(double strana)
        {
            this.strana = strana;
        }
        public override double delkaObvodu()
        {
            return 4 * strana;
        }
        public override double obsah()
        {
            return strana * strana;
        }
        public override String jmeno()
        {
            return "Čtverec";
        }
    }
    public class Testuj
    {
        public static void main()
        {
            RovinneVlastnosti[] tvary = new RovinneVlastnosti[10];
            var generátor = new Random();
            // generace objektů
            for (int i = 1; i < tvary.Length; i++)
            {
                double d = generátor.NextDouble();
                switch ((int)(1.0 + d * 2))
                {
                    case 1: tvary[i] = new Ctverec(i); break;
                    case 2: tvary[i] = new Obdelnik(i, 2 * i); break;
                }
            }
            // práce s objekty přes rozhraní RovinneVlastnosti
            for (int i = 1; i < tvary.Length; i++)
            {
                Console.WriteLine("//////////////////");
                Console.WriteLine(tvary[i].delkaObvodu());
                Console.WriteLine(tvary[i].obsah());
                Console.WriteLine(tvary[i].jmeno());
            }
        }
    }
    /**************************************kód ze zadání**********************************************************/
    /*
    Přidejte novou třídu Trojúhelník reprezentující trojúhelník zadaný třemi body:
    -	Vytvořte konstruktor Trojuhelnik(double[] x, double[] y)
    -	implementujte rozhraní RovinneVlastnosti
        ////Pozn: musel jsem zmenit na abstraktni tridu, protoze c# narozdil od javy nedovoluje mit v rozhrani promenne.
        ///Odmocnina(s(s-a)(s-b)(s-c)) pro s = (a+b+c)/2
    */
    class Trojuhelnik : RovinneVlastnosti
    {
        double[] x, y;
        public Trojuhelnik(double[] x, double[] y)
        {
            this.x = x; this.y = y;
        }
        public override double delkaObvodu()
        {
            double
                suma = Math.Sqrt((x[0] - x[1]) * (x[0] - x[1]) + (y[0] - y[1]) * (y[0] - y[1]));
            suma += Math.Sqrt((x[1] - x[2]) * (x[1] - x[2]) + (y[1] - y[2]) * (y[1] - y[2]));
            suma += Math.Sqrt((x[2] - x[0]) * (x[2] - x[0]) + (y[2] - y[0]) * (y[2] - y[0]));
            return suma;
        }
        public override double obsah()
        {
            double a = Math.Sqrt((x[0] - x[1]) * (x[0] - x[1]) + (y[0] - y[1]) * (y[0] - y[1]));
            double b = Math.Sqrt((x[1] - x[2]) * (x[1] - x[2]) + (y[1] - y[2]) * (y[1] - y[2]));
            double c = Math.Sqrt((x[2] - x[0]) * (x[2] - x[0]) + (y[2] - y[0]) * (y[2] - y[0]));
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }
        public override string jmeno()
        {
            return "Trojúhelník";

        }
    }
    /*
     Modifikujte Test1 tak, aby se nahodile vytvářely i instance trojúhelníků. 
     Hint: Modifikovat switch. Random vrací číslo v intervalu [0,1).
     Spočítejte obsah/obvod všech instancovaných tvarů. Výpočet musí být nezávislý na tom, zdali pracujete s obdélníky nebo čtverci.
     */
    public class Test1
    {
        public static void main()
        {
            RovinneVlastnosti[] tvary = new RovinneVlastnosti[10];
            var generátor = new Random();
            // generace objektů
            for (int i = 1; i < tvary.Length; i++)
            {
                double d = generátor.NextDouble();
                switch ((int)(1.0 + d * 3))
                {
                    case 1: tvary[i] = new Ctverec(i); break;
                    case 2: tvary[i] = new Obdelnik(i, 2 * i); break;
                    case 3: tvary[i] = new Trojuhelnik(new double[] { 0, i, 0 }, new double[] { 0, 0, i }); break;
                }
            }
            // práce s objekty přes rozhraní RovinneVlastnosti
            for (int i = 1; i < tvary.Length; i++)
            {
                Console.WriteLine("//////////////////");
                Console.WriteLine("Obvod: " + tvary[i].delkaObvodu());
                Console.WriteLine("Obsah: " + tvary[i].obsah());
                Console.WriteLine(tvary[i].jmeno());
            }
        }

    }
}