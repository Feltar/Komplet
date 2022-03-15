using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using Cviko7;

namespace Cviko7
{
    internal class ReseniXmlDocument
    {
        static readonly string cestaKeXmlSouboru = pristupKCeste.vratitCestuKeXMLSouboru();
        static readonly string cestaKeSchematu = pristupKCeste.vratitCestuKeSchematu();
        public static void main()
        {

            //*************************Nahodile generovani objektu, ktere ulozime do xmldokumentu
            List<Obrazek> seznam_obrazky = new List<Obrazek>();
            for (int i = 0; i < 10; i++)
            {
                seznam_obrazky.Add(new Kruh(i, i, 0));
                seznam_obrazky.Add(new Ctverec(i, i));
                seznam_obrazky.Add(new Ctverec(i, i));
            }

            //*************************Vytvoreni DOM xml dokumentu v pameti
            XmlDocument xml_obrazky = new XmlDocument();
            XmlElement koren = xml_obrazky.CreateElement("Obrazky");
            xml_obrazky.AppendChild(koren);

            foreach (Obrazek obrazek in seznam_obrazky)
            {
                XmlElement xml_obrazek = xml_obrazky.CreateElement(obrazek.GetType().ToString());

                xml_obrazek.SetAttribute("x", obrazek.getMinX().ToString());
                xml_obrazek.SetAttribute("y", obrazek.getMinY().ToString());
                xml_obrazek.SetAttribute("x1", obrazek.getMaxX().ToString());
                xml_obrazek.SetAttribute("y1", obrazek.getMaxY().ToString());


                xml_obrazky.FirstChild.AppendChild(xml_obrazek);

                /* Kdyby se v pripade souradnic "x" atp. nejednalo o atributy, ale o elementy, tak bychom to resili nasledovne
                        XmlElement xml_x = xml_obrazky.CreateElement("x");
                        xml_x.InnerText = obrazek.getMinX().ToString();
                        xml_obrazek.AppendChild(xml_x);
                */
            }
            //**************************Ulozeni do souboru
            xml_obrazky.Save(cestaKeXmlSouboru);

            //*************************Testovani proti schematu
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add("", cestaKeSchematu);

            xml_obrazky.Schemas.Add(schema);

            bool prosla_validace = true;
            xml_obrazky.Validate((e, d) =>
            {
                Console.WriteLine(d.Message);
                prosla_validace = false;
            }
                                 );
            if (prosla_validace)
            {
                Console.WriteLine("Dokument je naformatovan v souladu se schematem Schema.xsd");
            }
            else
            {
                Console.WriteLine("Dokument neni naformatovan v souladu se schematem Schema.xsd");
            }


            XmlDocument xml_nacteny_obrazky = new XmlDocument();
            xml_nacteny_obrazky.Load(cestaKeXmlSouboru);


            for (int i = 0; i < xml_nacteny_obrazky.SelectNodes("/Obrazky/Cviko7.Kruh").Count; i++)
            {
                Console.WriteLine("/////////////////////////////////////////////////////////////"
                                                + xml_nacteny_obrazky.SelectSingleNode(String.Format("/Obrazky/Cviko7.Kruh[{0} ]", i + 1)).Name);

                XmlElement element = (XmlElement)xml_nacteny_obrazky.SelectSingleNode(String.Format("/Obrazky/Cviko7.Kruh[{0} ]", i + 1));
                Console.Write("MinX " + element.GetAttribute("x"));
                Console.Write(" MinY " + element.GetAttribute("y"));
                Console.Write(" MinX " + element.GetAttribute("x1"));
                Console.WriteLine(" MinY " + element.GetAttribute("y1"));

                /* Kdyby se v pripade souradnic "x" atp. nejednalo o atributy, ale o elementy, tak bychom to resili nasledovne
                        XmlElement xx = (XmlElement)xml_nacteny_obrazky.SelectSingleNode(String.Format("/Obrazky/Kruh[{0} ]/x", i + 1));
                        Console.Write("MinX " + xx.InnerText);
                */

            }
        }
    }
}