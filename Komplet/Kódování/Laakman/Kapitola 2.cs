using System;
using System.Collections.Generic;
using System.Text;

namespace C_Sharp
{
    static class Laakman_Kap2
    {
        /*//////////////////////////////////2.1////////////////////////////////////////////////////////////*/
        /*
        Napište kód, který odstraní duplikátý z nesetříděného seznamu.
        */
        static public void cviceni_1(Spojak spojak)
        {
            Uzel_Spojaku uzel = spojak.hlava;
            Spojak nalezene_hodnoty = new Spojak();
            while (uzel != null)
            {
                if (nalezene_hodnoty.obsahuje_hodnotu(uzel.hodnota))
                {
                    spojak.smaz(uzel);
                }
                else
                {
                    nalezene_hodnoty.pridej_na_konec(uzel.hodnota);
                }
                uzel = uzel.dalsi;
            }
            return;
        }
        /*//////////////////////////////////2.1////////////////////////////////////////////////////////////*/
        /*..................................2.2............................................................*/
        /*
        Implementujte algoritmus, který najde n-tý prvek od konce v lineárním seznamu.
        */
        static private Uzel_Spojaku hledany_uzel;
        static private int kolikaty_od_konce_hledam;
        static public Uzel_Spojaku najdi_n_teho_od_konce(Uzel_Spojaku koren, int n)
        {
            Uzel_Spojaku n_ty_od_konce = null;
            kolikaty_od_konce_hledam = n;
            int m = rekurzivni_vnoreni(koren);
            if (m >= n)
            {
                return hledany_uzel;
            }
            else return null;
        }
        static public int rekurzivni_vnoreni(Uzel_Spojaku aktualni)
        {
            if (aktualni == null)
                return 1;
            else
            {
                int kolikaty_od_konce = rekurzivni_vnoreni(aktualni.dalsi);
                if (kolikaty_od_konce == kolikaty_od_konce_hledam)
                {
                    hledany_uzel = aktualni;
                }
                return kolikaty_od_konce + 1;
            }
        }
        /*..................................2.2............................................................*/
        /*//////////////////////////////////2.3////////////////////////////////////////////////////////////*/
        /*
        Implementujte algoritmus, který smaže porstřední uzel spojového seznamu, máte-li přístup pouze k jednomu uzlu (v jeden čas)
        Příklad
        Vstup: uzel ‘c’ ze spojového seznamu a->b->c->d->e
        Výsledek: je spojový seznam tvaru a->b->d->e
        */
        public static void smaz_prostredni(Uzel_Spojaku uzel)
        {
            if (uzel == null) { return; }
            while (uzel.dalsi != null)
            {
                uzel.hodnota = uzel.dalsi.hodnota;
                uzel = uzel.dalsi;
            }
            uzel.dalsi = null;
        }
        /*//////////////////////////////////2.3////////////////////////////////////////////////////////////*/
        /*..................................2.4............................................................*/
        /*
            Máte dvě čísla reprezentované spojovým seznamem, kde každý uzel obsahuje jednu cifru. 
            Cifry jsou uložené v opačném pořadí, tak, že první cifra je hlavou seznamu. Napište funkci, která
            sečte tato dvě čísla.
            Příklad
            Vstup: (3 -> 1 -> 5) + (5 -> 9 -> 2)
            Výstup: 8 -> 0 -> 8
        */
        static private int drzim_si;
        public static Spojak secti_spojaky(Spojak horni, Spojak spodni)
        {
            Spojak soucet = new Spojak(); drzim_si = 0;
            Uzel_Spojaku aktualni_horni_uzel = horni.hlava; Uzel_Spojaku aktualni_spodni_uzel = spodni.hlava;
            while (aktualni_horni_uzel != null && aktualni_spodni_uzel != null)
            {
                int pom = aktualni_spodni_uzel.hodnota + aktualni_horni_uzel.hodnota + drzim_si;
                soucet.pridej_na_konec(pom % 10);
                drzim_si = pom / 10;
                aktualni_horni_uzel = aktualni_horni_uzel.dalsi;
                aktualni_spodni_uzel = aktualni_spodni_uzel.dalsi;
            }
            if (aktualni_horni_uzel == null)
            {
                Uzel_Spojaku pom = aktualni_horni_uzel;
                aktualni_horni_uzel = aktualni_spodni_uzel;
                aktualni_spodni_uzel = pom;
            }
            while (aktualni_horni_uzel != null)
            {
                int pom = aktualni_horni_uzel.hodnota + drzim_si;
                soucet.pridej_na_konec(pom % 10);
                drzim_si = pom / 10;
                aktualni_horni_uzel = aktualni_horni_uzel.dalsi;
            }
            return soucet;
        }
        /*..................................2.4............................................................*/
    }
}
