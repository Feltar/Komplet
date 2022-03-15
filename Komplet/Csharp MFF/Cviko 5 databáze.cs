using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


class CvikoJDBC
{
    /**********************************************************************************************************************************************************/
    /*Zjistěte počet řádek tabulky Objednavka*/
    /*public static int pocet_radku_v_databazi_objednavka()
    {
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true";
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            try
            {
                spojeni.Open();
                string query = "SELECT COUNT(*) FROM objednavka";
                SqlCommand prikaz = new SqlCommand(query, spojeni);
                return (int)prikaz.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return 0;
    }*/
    /**********************************************************************************************************************************************************/
    /*///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
    /*Vypište tabulku Obchodnik*/
    /*static private void vystup_na_konzoli(DataTable tabulka)
    {
        foreach (DataRow radek in tabulka.Rows)
        {
            foreach (var prvek in radek.ItemArray)
            {
                Console.Write("{0} ", prvek);
            }
            Console.WriteLine();
        }
    }
    static public void vypis_obchodniky()
    {
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true";
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            try
            {
                spojeni.Open();
                string query = "SELECT * FROM obchodnik";
                SqlCommand prikaz = new SqlCommand(query, spojeni);
                SqlDataAdapter adapter = new SqlDataAdapter(query, spojeni);

                DataSet mnozina = new DataSet();
                adapter.Fill(mnozina, "obchodnik");

                vystup_na_konzoli(mnozina.Tables["obchodnik"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }*/
    /*///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
    /**********************************************************************************************************************************************************/
    /*Vytvořte třídu Example3SelectKniha pro výpis tabulky kniha.  Budete potřebovat v dalších příkladech.*/
    /*static public void vypis_knihy()
    {
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true";
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            try
            {
                spojeni.Open();
                string query_nacti_hodnotu = "select * from zbozi; ";
                SqlCommand prikaz_nacteni = new SqlCommand(query_nacti_hodnotu, spojeni);
                SqlDataReader sr = prikaz_nacteni.ExecuteReader();
                while (sr.Read())
                {
                    int idZbozi = (int)sr["idZbozi"]; Console.Write("idZbozi: " + idZbozi + " | ");
                    string popisZbozi = (string)sr["popis"]; Console.Write("popisZbozi: " + popisZbozi + " | ");
                    double cenaKus = (double)sr["cenaKus"]; Console.WriteLine("cenaKus: " + cenaKus + " ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }*/
    /**********************************************************************************************************************************************************/
    /*///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
    /*Rozšiřte třídu Example6InsertZbozi s vložením dalšího zboží. Nezapomeňte, že primární klíč musí být  jednoznačný a nelze vkládat (spouštět) opakovaně 
     stejný insert. Ověřte zda-li došlo k vložení nové hodnoty.(TO DO) udělám tim, že vrací počet ovlivněnejch - nejdřív najdi maximum id.*/
    /*public static void vloz_zbozi(string popis_zbozi, int cenaKus)
    {
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true";
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            try
            {
                spojeni.Open();
                string query_najdi_maxid = "SELECT MAX(idZbozi) FROM zbozi";
                SqlCommand prikaz_najdi_max_id = new SqlCommand(query_najdi_maxid, spojeni);
                int max_id = (int)prikaz_najdi_max_id.ExecuteScalar();

                string query_vloz_radek = "INSERT INTO zbozi (idZbozi, popis, CenaKus) VALUES(@par_max_id, @par_popis, @par_CenaKus); ";
                SqlCommand prikaz_vloz = new SqlCommand(query_vloz_radek, spojeni);
                prikaz_vloz.Parameters.AddWithValue("par_max_id", max_id + 1);
                prikaz_vloz.Parameters.AddWithValue("par_popis", popis_zbozi);
                prikaz_vloz.Parameters.AddWithValue("par_CenaKus", cenaKus);

                if (prikaz_vloz.ExecuteNonQuery() > 0)
                    Console.WriteLine("Došlo ke vložení hodnoty");
                else
                    Console.WriteLine("Nedošlo ke vložení hodnoty");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }*/
    /*///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
    /**********************************************************************************************************************************************************/
    /*Vytvořte třídu ExampleUpdateZbozi a modifikujte cenu všeho zboží na dvojnásobek. Uvedenou operaci lze provést jediným SQL příkazem, ale z cvičných důvodů 
     proveďte průchod celou tabulkou zboží. Pomocí dotazu na tabulku zboží budete procházet jednotlivé řádky tabulky a pro každou řádku provedete její aktualizaci.
                                                                            -> udělám oboje*/
    /*static public void modifikace_po_jednom()
    {
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true; MultipleActiveResultSets=True";
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            try
            {
                spojeni.Open();
                string query_nacti_hodnotu = "select * from zbozi; "; //??
                SqlCommand prikaz_nacteni = new SqlCommand(query_nacti_hodnotu, spojeni);
                SqlDataReader sr = prikaz_nacteni.ExecuteReader();
                while (sr.Read())
                {
                    double nova_cena = 2 * (double)sr["cenaKus"];
                    int idZbozi = (int)sr["idZbozi"];
                    string query_update = "UPDATE zbozi SET cenaKus = @par_nova_cena WHERE idZbozi = @id; ";
                    SqlCommand prikaz_update = new SqlCommand(query_update, spojeni);
                    prikaz_update.Parameters.AddWithValue("par_nova_cena", nova_cena);
                    prikaz_update.Parameters.AddWithValue("id", idZbozi);
                    prikaz_update.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
    static public void modifikace_dohromady_v_jednom_sql_prikazu()
    {
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true";
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            spojeni.Open();
            string query = "UPDATE zbozi SET cenaKus = 2*cenaKus;"; //??
            SqlCommand prikaz = new SqlCommand(query, spojeni);
            prikaz.ExecuteNonQuery();
        }
    }*/
    /**********************************************************************************************************************************************************/
    /*///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
    /*Nastavit cenu u zbozi 5003 na 499*/
    /*void nastavit_cenu_knihy()
    {
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true";
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            spojeni.Open();
            string query_update = "UPDATE zbozi SET cenaKus = @par_nova_cena where idZbozi = 5003;";
            SqlCommand prikaz_update = new SqlCommand(query_update, spojeni);
            prikaz_update.Parameters.AddWithValue("par_nova_cena", 499);
            prikaz_update.ExecuteNonQuery();
        }
    }
    /*///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/

}

/*Vytvoření tříd Zbozí, Objednavka reprezentující objekty Zbozi a Objednavka v databázi.  Součástí Zbozi je i pole objednávek tohoto zbozí*/


/*Ve třídě Objednavka.java je třeba vytvořit getry a setry pro jednotlivé sloupce tabulky. Implementovat funkci load() na základě nastavené idObjednavka.*/
/*class Objednavka
{
    public int idObjednavka { get; }//udelej jako takovou tu almost constantu!!
    public int idZakaznik;
    public DateTime datum;
    public int idZbozi;
    public int mnozstviKus;
    public int idObchodnik;
    public double cenaKus;
    public string poznamka;
    public Objednavka(int id) { this.idObjednavka = id; }
    public Objednavka(DataRow radek)
    {
        this.idObjednavka = (int)radek["idObjednavka"];
        this.idZakaznik = (int)radek["idZakaznik"];
        this.datum = (DateTime)radek["datum"];
        this.idObjednavka = (int)radek["idZbozi"];
        this.mnozstviKus = (int)radek["mnozstviKus"];
        this.idObchodnik = (int)radek["idObchodnik"];
        this.cenaKus = (double)radek["cenaKus"];
        this.poznamka = (string)radek["poznamka"];
    }
    public static Objednavka load(int idObjednavka)
    {
        Objednavka vystup = new Objednavka(idObjednavka);
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true";//oddratuj!!!!!!!!
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            string select_query = "SELECT * FROM objednavka where (@idObjednavka = idObjednavka)";
            SqlCommand prikaz = new SqlCommand(select_query, spojeni);
            prikaz.Parameters.AddWithValue("idObjednavka", idObjednavka);
            try
            {
                spojeni.Open();
                SqlDataReader ctecka = prikaz.ExecuteReader();
                if (ctecka.Read())
                {
                    vystup.idZakaznik = (int)ctecka["idZakaznik"];
                    vystup.datum = (DateTime)ctecka["datum"];
                    vystup.mnozstviKus = (int)ctecka["mnozstviKus"];
                    vystup.idObchodnik = (int)ctecka["idObchodnik"];
                    vystup.cenaKus = (double)ctecka["cenaKus"];
                    vystup.poznamka = (string)ctecka["poznamka"];
                }
                else { return null; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        return vystup;
    }
}*/
/*Implementovat funkci load() na základě nastavené idZbozi. Součástí load() je načtení řádku z tabulky idZbozi a načtení seznamu objednávek se zadaným 
* idZbozi a vložení těchto objednávek do pole objednavek.*/
/*class Zbozi
{
    public Zbozi(int id)
    {
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true";
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            try
            {
                spojeni.Open();
                string query_nacti = "SELECT * FROM zbozi WHERE idZbozi = @id;";
                //string query_nacti = "SELECT * FROM zbozi WHERE idZbozi = " + id + ';';
                SqlCommand prikaz = new SqlCommand(query_nacti, spojeni);
                prikaz.Parameters.AddWithValue("id", id);

                SqlDataReader ctecka = prikaz.ExecuteReader();
                this.cenaKus = (double)ctecka["cenaKus"];
                this.popis = (string)ctecka["popis"];
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        this.idZbozi = id;
        this.objednavky_s_timto_zbozim = new List<Objednavka>();
        load();
    }
    void load()
    {
        string cesta = "server =MSI\\SQLEXPRESS; initial catalog = dbap;integrated security = true";
        using (SqlConnection spojeni = new SqlConnection(cesta))
        {
            try
            {
                spojeni.Open();
                //string query_nacti = "SELECT * FROM objednavka WHERE (idZbozi = @id);";
                string query_nacti = "SELECT * FROM objednavka WHERE idZbozi = " + idZbozi + ';';
                SqlCommand prikaz = new SqlCommand(query_nacti, spojeni);
                //prikaz.Parameters.AddWithValue("id", this.idZbozi);

                SqlDataAdapter adapter = new SqlDataAdapter(query_nacti, spojeni);
                DataSet tabulka_objednavek = new DataSet();
                adapter.Fill(tabulka_objednavek, "objednavka");

                foreach (DataRow radek in tabulka_objednavek.Tables["objednavka"].Rows)
                {
                    Objednavka o = new Objednavka(radek);
                    this.objednavky_s_timto_zbozim.Add(o);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

    }
    int idZbozi;
    double cenaKus;
    string popis;
    public List<Objednavka> objednavky_s_timto_zbozim;


}*/
/*Vytvořte v třídě DbAP funkci, která spočítá celkovou výši všech objednávek zadaného zboží. Výpočet neprovádějte na základě specifického SQL, ale načtením objektu
* Zbozi a v něm příslušných Objednávek. Volejte tuto funkci s idZbozi= 5003.*/
/*class DbAP
{
    static public double celkova_vyse_zbozi(int id)
    {
        Zbozi z = new Zbozi(id);
        double vysledna_cena = 0;
        foreach (Objednavka o in z.objednavky_s_timto_zbozim)
        {
            vysledna_cena += o.mnozstviKus * o.cenaKus;
        }
        return vysledna_cena;
    }
}*/
