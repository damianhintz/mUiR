using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing.Design;

namespace muir
{
    public class Procenty
    {
        public Procenty()
        {
        }

        public Procenty(decimal a, decimal b, decimal c, decimal d)
        {
            this.m_kosztyUzyskania = a;
            this.m_podatekDochodowy = b;
            this.m_ubezpieczenieZdrowotne_7 = c;
            this.m_ubezpieczenieZdrowotne_9 = c;
        }

        protected decimal m_kosztyUzyskania = 20.0m;

        [Category(""), DisplayName("Koszty uzyskania [%]"), Description("Koszty uzyskania")]
        public decimal KosztyUzyskania
        {
            get { return m_kosztyUzyskania; }
            set { m_kosztyUzyskania = value; }
        }

        protected decimal m_ubezpieczenieZdrowotne_7 = 7.75m;

        [Category(""), DisplayName("Ubezpieczenie zdrowotne [%]"), Description("Ubezpieczenie zdrowotne")]
        public decimal UbezpieczenieZdrowotne_7
        {
            get { return m_ubezpieczenieZdrowotne_7; }
            set { m_ubezpieczenieZdrowotne_7 = value; }
        }

        protected decimal m_ubezpieczenieZdrowotne_9 = 9.0m;

        [Category(""), DisplayName("Ubezpieczenie zdrowotne [%]"), Description("Ubezpieczenie zdrowotne")]
        public decimal UbezpieczenieZdrowotne_9
        {
            get { return m_ubezpieczenieZdrowotne_9; }
            set { m_ubezpieczenieZdrowotne_9 = value; }
        }

        protected decimal m_podatekDochodowy = 18.0m;

        [Category(""), DisplayName("Podatek dochodowy [%]"), Description("Podatek dochodowy")]
        public decimal PodatekDochodowy
        {
            get { return m_podatekDochodowy; }
            set { m_podatekDochodowy = value; }
        }

    }
}
