using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cviko7
{
    static class pristupKCeste
    {
        public static string vratitCestuKeSchematu() { 
            // Vrati slozka projektu ...\bin\Debug 
            string cestaKPrcovniSlozce = Environment.CurrentDirectory;
            // Timto dostaneme projektovou slozku
            string cestaKProjektoveSlozce = Directory.GetParent(cestaKPrcovniSlozce).Parent.Parent.FullName;

            string cesta = Path.Combine(cestaKProjektoveSlozce, @"Csharp MFF\Cviko7 XML\Schema.xsd");
            return cesta;
        }

        public static string vratitCestuKeXMLSouboru()
        {
            // Vrati slozka projektu ...\bin\Debug 
            string cestaKPrcovniSlozce = Environment.CurrentDirectory;
            // Timto dostaneme projektovou slozku
            string cestaKProjektoveSlozce = Directory.GetParent(cestaKPrcovniSlozce).Parent.Parent.FullName;

            string cesta = Path.Combine(cestaKProjektoveSlozce, @"Csharp MFF\Cviko7 XML\Lambada.xml");
            return cesta;
        }

    }
}
