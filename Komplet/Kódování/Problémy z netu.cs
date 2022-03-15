using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
public class UlohyKodovani
{
    // vzato ze stranky https://www.udemy.com/course/50-problems/?utm_source=adwords&utm_medium=udemyads&utm_campaign=LongTail_la.EN_cc.ROWMTA-A&utm_content=deal4584&utm_term=_._ag_80979681514_._ad_533999950009_._kw__._de_c_._dm__._pl__._ti_dsa-1007766171312_._li_9062849_._pd__._&matchtype=&gclid=Cj0KCQiA2sqOBhCGARIsAPuPK0gtMCoqX8OEOufJA9F4piP0L7QO3YUk40CwORnw7pYb3cB7GL_Ypj0aAqHdEALw_wcB
    //........................Hashovaci tabulky...............................................................................
    /***************sum to n****************************************************************/
    public static int[] secteni_po_n(int suma, int[] vstup)
    {
        //int[] vstup = new int[] { 5, 4, 1, 2, 6 };
        //int suma = 10;
        Hashtable prvky_nalezeny = new Hashtable();
        for (int i = 0; i < vstup.Length; i++)
        {
            if (prvky_nalezeny[suma - vstup[i]] != null)
            {
                return new int[] { vstup[i], suma - vstup[i] };
            }
            else { prvky_nalezeny.Add(vstup[i], true); }
        }
        return null;
    }
    /***************sum to n*********************************************************************************/
    ////////////////first repeating character////////////////////////////////////////////////////////////////
    public static int prvni_opakujici_se_znak(int[] vstup)
    {
        //int[] vstup = new int[] { 5, 4, 1, 2, 6, 5 };
        Hashtable prvek_nalezen = new Hashtable();
        for (int i = 0; i < vstup.Length; i++)
        {
            if (prvek_nalezen[vstup[i]] != null)
            {
                return vstup[i]; //vrati prvni znak, ktery se opakuje v opacnem pripade da na vystup -1
            }
            else
            {
                prvek_nalezen.Add(vstup[i], true);
            }
        }
        return -1;
    }
    ////////////////first repeating character////////////////////////////////////////////////////////////////
    ////***************smaz duplicitu***********************************************************************/
    public static void smazani_diplicit_v_seznamu(List<int> vstup)
    {
        Hashtable prvek_nalezen = new Hashtable();
        for (int i = 0; i < vstup.Count; i++)
        {
            if (prvek_nalezen[vstup[i]] == null)
            {
                prvek_nalezen.Add(vstup[i], true);
            }
            else
            {
                vstup.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < vstup.Count; i++)
        {
            Console.Write(vstup[i] + " ");
        }
    }
    ////***************smaz duplicitu***********************************************************************/
    //........................Hashovaci tabulky...............................................................................
    //___________________________Grafy________________________________________________________________________________________
    class UzelBStromu<T>
    {
        public UzelBStromu<T> levy { get; set; }
        public UzelBStromu<T> pravy { get; set; }
        T hodnota;
        public UzelBStromu(T hodnota)
        {
            this.hodnota = hodnota;
        }
    }
    class BStrom<T>
    {
        UzelBStromu<T> koren;
        BStrom(T hodnota)
        {
            koren = new UzelBStromu<T>(hodnota);
        }
        public void inverze_binarniho_stromu()
        {
            rekurzivni_prohozeni(koren);
        }
        private void rekurzivni_prohozeni(UzelBStromu<T> uzel)
        {
            if (uzel.levy != null) { rekurzivni_prohozeni(uzel.levy); }
            if (uzel.pravy != null) { rekurzivni_prohozeni(uzel.pravy); }
            UzelBStromu<T> pom = uzel.levy;
            uzel.levy = uzel.pravy;
            uzel.pravy = pom;
        }
    }
    //___________________________Grafy________________________________________________________________________________________
    //...........................Dynamicke Programovani.....................................................................
    //////////////////////////Schodiste/////////////////////////////////////////////////////////////////////////////////
    static public int schody(int delka_schodiste)
    {
        ///There exists a staircase with n steps which you can climb up either 1 or 2 steps at  a time.Given n, write a 
        ///function that returns the number of unique ways you can climb the staircase.The order of the steps matters.
        int[] pamet = new int[delka_schodiste];
        int[] delka_kroku = new int[] { 1, 3, 5 };

        if (delka_kroku.Contains(1))
        {
            pamet[0] = 1;
        }
        for (int i = 1; i < delka_schodiste; i++)
        {
            int suma = 0;
            for (int k = 0; k < delka_kroku.Length; k++)
            {
                if (i - k >= 0)
                {
                    suma += delka_kroku[i - k];
                }
            }
            pamet[i] = suma;
        }
        return pamet[delka_schodiste];
    }
    //////////////////////////Schodiste/////////////////////////////////////////////////////////////////////////////////
    //*****************************************************************************************************************
    static public int kolik_moznych_kodovani(int[] kod)
    {
        ///Given the mapping a= 1, b = 2, ... , z = 26, and an encoded message, count the number of ways it can be decoded. 
        ///For example, the message "111" should be 3, since it could be decoded as "aaa", " ka", and "a k".
        ///You can assume that the messages are always decodable.For example, "001" is not allowed.
        int delka_retezce = kod.Length;
        int[] pamet = new int[delka_retezce];

        pamet[delka_retezce - 1] = 1;
        for (int i = delka_retezce - 1; i >= 0; i--)
        {
            if (kod[i + 1] + kod[i] * 10 <= 26)
            {
                pamet[i] = pamet[i + 1] + 1;
            }
            else
            {
                pamet[i] = pamet[i + 1];
            }
        }
        return pamet[0];
    }
    //*****************************************************************************************************************
    //...........................Dynamicke Programovani.....................................................................
}
