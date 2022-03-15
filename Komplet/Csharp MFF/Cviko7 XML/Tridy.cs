using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Cviko7
{
    public interface Obrazek
    {
        double getMinX();
        double getMaxX();
        double getMinY();
        double getMaxY();
        XElement toXML();
        //void fromXML(Element elem);
    }
    public class Kruh : Obrazek
    {
        double XStred; double YStred;
        double polomer;
        public Kruh(double polomer, double Xstred, double YStred)
        {
            this.polomer = polomer;
            this.XStred = Xstred;
            this.YStred = YStred;
        }
        public double getMinX() { return XStred - polomer; }
        public double getMaxX() { return XStred + polomer; }
        public double getMinY() { return YStred - polomer; }
        public double getMaxY() { return YStred + polomer; }
        public XElement toXML()
        {
            return new XElement("Obrazek",
                    new XElement("xType", getMinX),
                    new XElement("xType", getMaxX),
                    new XElement("xType", getMinY),
                    new XElement("xType", getMaxY)
            );
        }
    }
    public class Ctverec : Obrazek
    {
        double XStred; double YStred;
        double XPolomer; double YPolomer;
        double polomer; double stred;
        public Ctverec(double polomer, double stred)
        {
            this.polomer = polomer;
            this.stred = stred;
        }
        public double getMinX() { return XStred - XPolomer; }
        public double getMaxX() { return XStred + XPolomer; }
        public double getMinY() { return YStred - YPolomer; }
        public double getMaxY() { return YStred + YPolomer; }
        public XElement toXML()
        {
            return new XElement("Obrazek",
                    new XElement("xType", getMinX),
                    new XElement("xType", getMaxX),
                    new XElement("xType", getMinY),
                    new XElement("xType", getMaxY)
            );
        }
    }
}