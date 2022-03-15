using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
           Naprogramujte třídu Life  s následujícími vlastnostmi:
       -	Konstruktor zadá dimenzi hracího pole (předpokládejme čtverec)
       -	Metoda pro inicializaci init(), může být vyplněno náhodnými čísly, pozor zvolte vhodnou hustotu populace (viz pravidla)
       -	Metoda step() provede jeden krok hry
       -	Metoda show() vypíše stav hry,  nejlépe po řádcích aby to bylo dobře vidět.
       -	Metoda main(int m, int n) instancuje hru, provede m kroků a po každých n krocích zobrazí stav.    
        1.	Any live cell with fewer than two live neighbours dies, as if caused by under-population.
        2.	Any live cell with two or three live neighbours lives on to the next generation.
        3.	Any live cell with more than three live neighbours dies, as if by over-population.
        4.	Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.         
    */
namespace Cviko1
{
    class Life
    {
        private bool[,] naživu;
        public Life(int a)
        {
            if (a > 0)
                this.naživu = new bool[a, a];
        }
        /*////////////////////////////////////funkce step()////////////////////////////////////////////////////*/
        private void step()
        {
            bool[,] pom = new bool[naživu.GetLength(0), naživu.GetLength(1)];
            //uděláme zvlášť prostředek a zvlášť kraje
            for (int i = 0; i < naživu.GetLength(0); i++)
            {
                for (int j = 0; j < naživu.GetLength(1); j++)
                {
                    int pocet_zivych_sousedu = spocti_pocet_zivych_sousedu(i, j);
                    if (naživu[i, j])
                    {
                        if (pocet_zivych_sousedu == 2 | pocet_zivych_sousedu == 3) pom[i, j] = true;
                        else pom[i, j] = false;
                    }
                    else
                    {
                        if (pocet_zivych_sousedu == 3)
                            pom[i, j] = true;
                        else
                            pom[i, j] = false;
                    }
                }
            }
            this.naživu = pom;
        }
        private int spocti_pocet_zivych_sousedu(int i, int j)
        {
            //ošetření krajů
            int min_i, min_j, max_i, max_j;
            if (i == 0) min_i = 0;
            else min_i = i - 1;
            if (j == 0) min_j = 0;
            else min_j = j - 1;
            if (i == naživu.GetLength(0) - 1) max_i = naživu.GetLength(0) - 1;
            else max_i = i + 1;
            if (j == naživu.GetLength(1) - 1) max_j = naživu.GetLength(1) - 1;
            else max_j = j + 1;
            int pocet_zivych_sousedu = 0;
            //Následující dva cykly spočtou počet živých sousedů. 
            //Takhle tam ale bude započtena i ta buňka samotná (pokud je naživu) - což je chyba. Po projití cyklu ji odečteme. 
            //Šlo by to vyřešit i rozdělením na více cyklů, ale to by zbytečně komplikovalo kód.
            for (int a = min_i; a <= max_i; a++)
            {
                for (int b = min_j; b <= max_j; b++)
                {
                    if (naživu[a, b]) { pocet_zivych_sousedu++; }
                }
            }
            if (naživu[i, j])
                pocet_zivych_sousedu--;
            return pocet_zivych_sousedu;
        }
        /*////////////////////////////////////funkce step()////////////////////////////////////////////////////*/
        private void show()
        {
            for (int i = 0; i < naživu.GetLength(0); i++)
            {
                for (int j = 0; j < naživu.GetLength(1); j++)
                {
                    if (naživu[i, j])
                        Console.Write(1 + " ");
                    else
                        Console.Write(0 + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("////////////////////////vypis/////////////////////");
        }
        public void init()
        {
            var generátor = new Random();
            for (int i = 0; i < naživu.GetLength(0); i++)
            {
                for (int j = 0; j < naživu.GetLength(1); j++)
                {
                    if (generátor.Next(2) == 1)
                        naživu[i, j] = true;
                }
            }
        }
        public void main(int m, int n)
        {
            init();
            int kolikery_kroku_mod_n = m;
            for (int i = 0; i < m; i++)
            {
                if (kolikery_kroku_mod_n == 0)
                {
                    show();
                }
                kolikery_kroku_mod_n = (kolikery_kroku_mod_n + 1) % n;
                step();
            }
        }
    }
}