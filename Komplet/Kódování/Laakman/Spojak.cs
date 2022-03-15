using System;
using System.Collections.Generic;
using System.Text;

namespace C_Sharp
{
    class Uzel_Spojaku
    {
        public Uzel_Spojaku(int hodnota)
        {
            this.hodnota = hodnota;
            this.dalsi = null;
        }
        public int hodnota;
        public Uzel_Spojaku dalsi;
    }
    class Spojak
    {
        public Uzel_Spojaku hlava;
        public Spojak() { }
        public Spojak(int hodnota)
        {
            hlava = new Uzel_Spojaku(hodnota);
        }
        public void pridej_na_konec(int hodnota)
        {
            if (hlava == null)
            {
                hlava = new Uzel_Spojaku(hodnota);
                return;
            }
            Uzel_Spojaku uzel = hlava;
            while (uzel.dalsi != null)
            {
                uzel = uzel.dalsi;
            }
            uzel.dalsi = new Uzel_Spojaku(hodnota);
        }
        public void smaz(int hodnota)
        {
            Uzel_Spojaku uzel = hlava;
            Uzel_Spojaku pomocny;
            while (uzel.dalsi != null && uzel.dalsi.hodnota != hodnota)
            {
                uzel = uzel.dalsi;
            }
            if (uzel.dalsi == null)
            {
                return;
            }
            uzel.dalsi = uzel.dalsi.dalsi;
        }
        public bool obsahuje_hodnotu(int hodnota)
        {
            Uzel_Spojaku uzel = hlava;
            while (uzel != null)
            {
                if (uzel.hodnota == hodnota)
                {
                    return true;
                }
                uzel = uzel.dalsi;
            }
            return false;
        }
        public void smaz(Uzel_Spojaku uzel)
        {
            if (hlava == null) { return; }
            if (uzel == hlava) { hlava = hlava.dalsi; return; }
            if (hlava.dalsi == null) { return; }
            Uzel_Spojaku aktualni = hlava;
            while (aktualni.dalsi != null && aktualni.dalsi != uzel)
            {
                aktualni = aktualni.dalsi;
            }
            if (aktualni.dalsi != null)
            {
                aktualni.dalsi = aktualni.dalsi.dalsi;
            }
        }
    }
}
