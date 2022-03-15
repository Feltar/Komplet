using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peter_Setoft
{
    static class Reseni
    {
        /******************************************************************************************************************************************************/
        /*Na vstupu dostanete pole integerů. Na vraťte nové pole, aby každý jeho element nového pole na indexu i byl produktem všech prvků původního pole kromě prvku na i tém indexu */
        public static int[] reseni1_1(int[] vstup)
        {
            int[] vystup = new int[vstup.Length];
            int produkt = vstup[0];
            for (int i = 1; i < vstup.Length; i++)
            {
                produkt *= vstup[i];
            }
            for (int i = 0; i < vystup.Length; i++)
            {
                vystup[i] = produkt / vstup[i];
            }
            return vystup;
        }
        /*Co pokud nemůžeme používat dělení?*/
        public static int[] reseni1_1b(int[] vstup)
        {
            int[] soucin_prvnich_i = new int[vstup.Length];
            int[] soucin_poslednich_od_i = new int[vstup.Length];
            int[] vystup = new int[vstup.Length];

            soucin_poslednich_od_i[vstup.Length - 1] = vstup[vstup.Length - 1];
            soucin_prvnich_i[0] = vstup[0];
            for (int i = 1; i <= vstup.Length - 2; i++)
            {
                soucin_prvnich_i[i] = soucin_prvnich_i[i - 1] * vstup[i];
                soucin_poslednich_od_i[vstup.Length - 1 - i] = soucin_poslednich_od_i[vstup.Length - i] * vstup[vstup.Length - i - 1];
            }

            vystup[0] = soucin_poslednich_od_i[1];
            vystup[vstup.Length - 1] = soucin_prvnich_i[vstup.Length - 2];
            for (int i = 1; i <= vstup.Length - 2; i++)
            {
                vystup[i] = soucin_prvnich_i[i - 1] * soucin_poslednich_od_i[i + 1];
            }
            return vystup;
        }
        /******************************************************************************************************************************************************/
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Na vstupu dostanate nesetříděné pole integerů. Zjistěte hranice nejmenšího okna, které musí být setříděno, aby bylo setříděno celé pole. Například pro [ 3 , 7 , 5 , 6 , 9] , 
         * by na výstupu mělo být ( 1 , 3 ) .*/
        public static int[] reseni1_2(int[] vstup)
        {
            int index_aktualni_maximum = 0, index_prvni_nesrovnalosti = -1, nosrovnalost_pokud = 0;
            for (int i = 1; i < vstup.Length; i++)
            {
                if (vstup[index_aktualni_maximum] > vstup[i])
                {
                    if (index_prvni_nesrovnalosti == -1)
                    {
                        index_prvni_nesrovnalosti = index_aktualni_maximum;
                    }
                    nosrovnalost_pokud = i;
                }
                else
                {
                    index_aktualni_maximum = i;
                }
            }
            return new int[] { index_prvni_nesrovnalosti, nosrovnalost_pokud };
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /******************************************************************************************************************************************************/
        /* Na vstupu obdržíte pole čísel, najděte maximální sumu přes všechny dílčí (pod)řetězce. Napříkla pro vstup  [34, -50, 42, 14, -5,  86], by maximální suma byĺa  137, 
         * Neboť bychom mohlu vzít prvky 42, 14, -5, a 86. Pokud bychom obdrželi vstup [ -5,  -1,  -8,  -9], maximální suma by byla 0, neboť bychom nevzali žádný z daných prvků.*/
        public static int reseni1_3(int[] vstup)
        {
            int aktualni_max = vstup[0];
            int aktualni_soucet = vstup[0];
            for (int i = 1; i < vstup.Length; i++)
            {
                //osetreni nasledujiciho prvku
                if (vstup[i] < 0)
                {
                    if (aktualni_soucet + vstup[i] > 0)
                        aktualni_soucet += vstup[i];
                    else
                        aktualni_soucet = vstup[i];
                }
                else
                {
                    if (aktualni_soucet >= 0)
                        aktualni_soucet += vstup[i];
                    else
                        aktualni_soucet = vstup[i];
                }
                //osetreni maxima
                if (aktualni_soucet > aktualni_max)
                    aktualni_max = aktualni_soucet;
            }
            return aktualni_max;
        }
        /******************************************************************************************************************************************************/
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static int[] reseni1_4(int[] vstup)
        {
            int[] vystup = new int[vstup.Length];
            List<int> setridene = new List<int>();
            setridene.Add(vstup[vstup.Length - 1]);
            for (int i = vstup.Length - 2; i >= 0; i--)
            {
                int index_vlozeni = vrat_index_vlozeni(setridene, vstup[i]);
                setridene.Insert(index_vlozeni, vstup[i]);
                vystup[i] = index_vlozeni;
            }
            //................................................................................................................................
            return vystup;
        }
        public static int vrat_index_vlozeni(List<int> list, int prvek)
        {
            int index_prvni_mozny = 0; int index_posledni_mozny = list.Count - 1;
            int index_median = index_prvni_mozny + (index_posledni_mozny - index_prvni_mozny) / 2;
            while (index_prvni_mozny < index_posledni_mozny - 1)
            {
                if (list[index_median] < prvek)
                {
                    index_prvni_mozny = index_median + 1;
                }
                else
                {
                    index_posledni_mozny = index_median - 1;
                }
            }
            if (list[index_posledni_mozny] >= prvek)
                return index_prvni_mozny;
            else
                if (index_posledni_mozny <= list.Count)
            {
                return index_posledni_mozny + 1;
            }
            else
            {
                return index_posledni_mozny;

            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
