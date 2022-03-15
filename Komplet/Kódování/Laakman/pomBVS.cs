using System;
using System.Collections.Generic;
using System.Text;

class Uzel_BVS
{
    public int hodnota { get; set; }
    public Uzel_BVS levy_potomek { get; set; }
    public Uzel_BVS pravy_potomek { get; set; }
    public Uzel_BVS(int hodnota)
    {
        this.hodnota = hodnota;
    }
}
class pomBVS
{

    public Uzel_BVS koren { get; set; }

    public pomBVS()
    {
        Uzel_BVS koren = null;
    }
    public pomBVS(int hodnota)
    {
        koren = new Uzel_BVS(hodnota);
    }
    public pomBVS(Uzel_BVS uzel)
    {
        this.koren = uzel;
    }
    public void pridat_uzel(int hodnota)
    {
        Uzel_BVS aktualni_uzel = koren;
        Uzel_BVS dalsi_uzel;
        if (koren == null)
        {
            koren = new Uzel_BVS(hodnota);
            return;
        }
        if (hodnota <= aktualni_uzel.hodnota)
            dalsi_uzel = aktualni_uzel.levy_potomek;
        else
            dalsi_uzel = aktualni_uzel.pravy_potomek;
        while (dalsi_uzel != null)
        {
            aktualni_uzel = dalsi_uzel;
            if (hodnota <= dalsi_uzel.hodnota)
                dalsi_uzel = dalsi_uzel.levy_potomek;
            else
                dalsi_uzel = dalsi_uzel.pravy_potomek;
        }
        if (hodnota <= aktualni_uzel.hodnota)
            aktualni_uzel.levy_potomek = new Uzel_BVS(hodnota);
        else
            aktualni_uzel.pravy_potomek = new Uzel_BVS(hodnota);
    }
}
