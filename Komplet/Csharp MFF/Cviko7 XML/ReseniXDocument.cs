using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Cviko7
{
    internal class ReseniXDocument
    {
        static readonly string cestaKeXmlSouboru = pristupKCeste.vratitCestuKeXMLSouboru();
        static readonly string cestaKeSchematu = pristupKCeste.vratitCestuKeSchematu();
        public static void main()
        {

            //*************************Nahodile generovani objektu, ktere pozdeji ulozime do xmldokumentu
            List<Obrazek> seznam_obrazky = new List<Obrazek>();
            for (int i = 0; i < 10; i++)
            {
                seznam_obrazky.Add(new Kruh(i, i, 0));
                seznam_obrazky.Add(new Ctverec(i, i));
                seznam_obrazky.Add(new Ctverec(i, i));
            }

            //*************************Vytvoreni DOM xml dokumentu v pameti
            XDocument x_obrazky = new XDocument(new XElement("Obrazky"));
            foreach (Obrazek obrazek in seznam_obrazky)
            {
                /* Kdyby se nejednalo o atributy ale o elementy
                XElement x_obrazek = new XElement(obrazek.GetType().ToString(),
                        new XElement("x", obrazek.getMinX()), 
                        new XElement("x1", obrazek.getMaxX()),
                        new XElement("y", obrazek.getMinY()),
                        new XElement("y1", obrazek.getMaxY())
                );
                */

                XElement x_obrazek = new XElement(obrazek.GetType().ToString());
                x_obrazek.SetAttributeValue("x", obrazek.getMinX());
                x_obrazek.SetAttributeValue("x1", obrazek.getMaxX());
                x_obrazek.SetAttributeValue("y", obrazek.getMinY());
                x_obrazek.SetAttributeValue("y1", obrazek.getMaxY());
                x_obrazky.Root.Add(x_obrazek);
            }
            //**************************Ulozeni do souboru
            x_obrazky.Save(cestaKeXmlSouboru);

            //*************************Testovani proti schematu
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add("", cestaKeSchematu);

            x_obrazky.Validate(schema, (e, d) => Console.WriteLine(d.Message));

            //*************************Nacteme ze souboru - V zasade analogicky, co se da udelat pomoci x path se da udelat i pomoci LinQ
            XDocument x_nacteny_obrazky = XDocument.Load(cestaKeXmlSouboru);

            /* Kdyby se jednalo o elementy
            foreach (XElement x_nacteny_obrazek in x_nacteny_obrazky.Elements("Kruh"))
            {
                Console.Write(
                    "MinX" + x_nacteny_obrazek.Element("x").Value +
                    "MaxX" + x_nacteny_obrazek.Element("x1").Value +
                    "MinY" + x_nacteny_obrazek.Element("y").Value +
                    "MaxY" + x_nacteny_obrazek.Element("y1").Value
                );

            } */
            //syntax misto XPATH retezim .Element(...)
            foreach (XElement x_nacteny_obrazek in x_nacteny_obrazky.Element("Obrazky").Elements("Cviko7.Kruh"))
            {
                Console.WriteLine(
                    "MinX " + x_nacteny_obrazek.Attribute("x").Value +
                    " MaxX " + x_nacteny_obrazek.Attribute("x1").Value +
                    " MinY " + x_nacteny_obrazek.Attribute("y").Value +
                    " MaxY " + x_nacteny_obrazek.Attribute("y1").Value
                );

            }

            /*  Dalsi moznost s Linq dotazem
            IEnumerable<string> retezce =
                    from XElement kruh
                    in x_nacteny_obrazky.Elements("Kruh")
                    select "MinX " + kruh.Attribute("x") + " MinY " + kruh.Attribute("y") + " MinX " + kruh.Attribute("x1") + " MinY " + kruh.Attribute("y1");
            foreach (string retezec in retezce) Console.WriteLine(retezec);
            */
        }
    }

}