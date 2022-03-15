using System;
using System.Collections.Generic;
using System.Text;

namespace C_Sharp
{
    class Laakman_Kap4
    {
        /*//////////////////////////////////4.1////////////////////////////////////////////////////////////
        /*
        Implementujte funkci, která zjistí, jestli je strom vyvážený.Vyvážený strom definujeme tak, že žádné dva listy se neliší ve vzdálenosti od kořene o více jak o jedna.
        */
        static private int hodnota;
        static private bool nenalezen_rozdil; static private int hloubka_stromu;
        public static bool je_strom_vyvazeny(pomBVS strom)
        {
            Uzel_BVS koren = strom.koren;
            hloubka_stromu = 0; nenalezen_rozdil = true;
            //Pokud se jedná o spoják, tak je daný strom dle definice vyvážený.
            //Pokud má alespoň jeden uzel U dva potomky, pak hloubku stromu s kořenem
            //v U lze zjistit rekurentně zjištěním hloubky jeho levého a pravého podstromu ( posléze přičtením jedničky).
            //Dojdi na konec spojáku
            bool pravda = false;
            do
            {
                if (koren == null) return true;
                if (koren.levy_potomek == null && koren.pravy_potomek != null)
                {
                    pravda = true;
                    koren = koren.pravy_potomek;
                }
                else if (koren.pravy_potomek == null && koren.levy_potomek != null)
                {
                    pravda = true;
                    koren = koren.levy_potomek;
                }
                else { pravda = false; }
            } while (pravda);
            //Dojdi na konec spojáku
            zjisti_hloubku_v_podstromu(koren, 0);
            return nenalezen_rozdil;
        }
        private static void zjisti_hloubku_v_podstromu(Uzel_BVS aktualni, int hloubka_rodice)
        {
            if (nenalezen_rozdil && aktualni != null)
            {
                zjisti_hloubku_v_podstromu(aktualni.levy_potomek, hloubka_rodice + 1);
                zjisti_hloubku_v_podstromu(aktualni.pravy_potomek, hloubka_rodice + 1);
            }
            else
            {
                if (!nenalezen_rozdil) return;
                if (hloubka_stromu == 0)
                {
                    hloubka_stromu = hloubka_rodice;
                    return;
                }
                if (hloubka_rodice != hloubka_stromu)
                {
                    nenalezen_rozdil = false;
                }
            }
        }
        /*//////////////////////////////////4.1////////////////////////////////////////////////////////////*/
        /*..................................4.3............................................................*/
        /* Máte-li (vzestupně) setříděné pole, napište algoritmus, který vytvoří strom s minimální hloubkou.
        */
        public static Uzel_BVS vytvor_podstrom(int[] pole, int pocatek_podstromu_v_poli, int konec_podstromu_v_poli)
        {
            if (pocatek_podstromu_v_poli > konec_podstromu_v_poli) return null;
            int median = (konec_podstromu_v_poli + pocatek_podstromu_v_poli) / 2;
            Uzel_BVS koren_podstromu = new Uzel_BVS(pole[median]);
            koren_podstromu.levy_potomek = vytvor_podstrom(pole, pocatek_podstromu_v_poli, median - 1);
            koren_podstromu.pravy_potomek = vytvor_podstrom(pole, median + 1, konec_podstromu_v_poli);
            return koren_podstromu;
        }
        public static pomBVS vytvor_strom(int[] pole)
        {
            int konec = pole.Length - 1;
            int pocatek = 0;
            int median = (konec - pocatek) / 2;
            Uzel_BVS koren = new Uzel_BVS(pole[median]);
            pomBVS strom = new pomBVS(koren);
            koren.levy_potomek = vytvor_podstrom(pole, pocatek, median - 1);
            koren.pravy_potomek = vytvor_podstrom(pole, median + 1, konec);
            return strom;
        }

    }
}
