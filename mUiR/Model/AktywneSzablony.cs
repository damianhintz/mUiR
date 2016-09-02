using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace muir.Model
{
    public class AktywneSzablony
    {
        #region Implementacja atrybutów

        protected string m_szablon_odzielo = "";

        [Category(""), DisplayName("Aktywny szablon do umowy o dzieło"), Description("")]
        public string SzablonDoUmowyODzielo
        {
            get { return this.m_szablon_odzielo; }
            set { this.m_szablon_odzielo = value; }
        }

        protected string m_szablon_nazlecenie = "";

        [Category(""), DisplayName("Aktywny szablon do umowy na zlecenie"), Description("")]
        public string SzablonDoUmowyNaZlecenie
        {
            get { return this.m_szablon_nazlecenie; }
            set { this.m_szablon_nazlecenie = value; }
        }

        protected string m_szablon_nazleceniestudent = "";

        [Category(""), DisplayName("Aktywny szablon do umowy na zlecenie - student"), Description("")]
        public string SzablonDoUmowyNaZlecenieStudent
        {
            get { return this.m_szablon_nazleceniestudent; }
            set { this.m_szablon_nazleceniestudent = value; }
        }

        protected Procenty m_szablonODzielo = new SzablonUmowyODzielo();
        protected Procenty m_szablonNaZlecenie = new SzablonUmowyNaZlecenie();
        protected Procenty m_szablonNaZlecenieStudent = new SzablonUmowyNaZlecenieStudent();

        public static string SzablonDzielo = "";
        public static string SzablonZlecenie = "";
        public static string SzablonStudent = "";

        public static Osoba AktywnaOsoba = null;
        public static Umowa AktywnaUmowa = null;
        public static Rachunek AktywnyRachunek = null;

        #endregion

        #region Implementacja interfejsu

        public Procenty get_Procenty(RodzajUmowy rodzaj)
        {
            switch (rodzaj)
            {
                case RodzajUmowy.NaZlecenie:
                    return m_szablonNaZlecenie;
                case RodzajUmowy.NaZlecenieStudent:
                    return m_szablonNaZlecenieStudent;
                case RodzajUmowy.ODzielo:
                    return m_szablonODzielo;
                default:
                    return m_szablonODzielo;
            }
        }

        public decimal get_KosztyUzyskania(RodzajUmowy rodzaj, decimal wynagrodzenie)
        {
            return get_Procenty(rodzaj).KosztyUzyskania * wynagrodzenie / 100.0m;
        }

        public decimal get_PodstawaOpodatkowania(RodzajUmowy rodzaj, decimal wynagrodzenie)
        {
            return wynagrodzenie - get_KosztyUzyskania(rodzaj, wynagrodzenie);
        }

        public decimal get_UbezpieczenieZdrowotne7(RodzajUmowy rodzaj, decimal wynagrodzenie)
        {
            return get_Procenty(rodzaj).UbezpieczenieZdrowotne_7 * wynagrodzenie / 100.0m;
        }

        public decimal get_UbezpieczenieZdrowotne9(RodzajUmowy rodzaj, decimal wynagrodzenie)
        {
            return get_Procenty(rodzaj).UbezpieczenieZdrowotne_9 * wynagrodzenie / 100.0m;
        }

        public decimal get_PodatekDochodowy(RodzajUmowy rodzaj, decimal wynagrodzenie)
        {
            //podatek dochodowy zaokraglic do calkowitych
            return decimal.Round(get_Procenty(rodzaj).PodatekDochodowy * get_PodstawaOpodatkowania(rodzaj, wynagrodzenie) / 100.0m -
                get_UbezpieczenieZdrowotne7(rodzaj, wynagrodzenie), 0, MidpointRounding.AwayFromZero);
        }

        public decimal get_DoWyplaty(RodzajUmowy rodzaj, decimal wynagrodzenie)
        {
            return wynagrodzenie - get_PodatekDochodowy(rodzaj, wynagrodzenie) - get_UbezpieczenieZdrowotne9(rodzaj, wynagrodzenie);
        }

        public string get_SlowniePLN(RodzajUmowy rodzaj, decimal wynagrodzenie)
        {
            return Slownie.innerTrim(Slownie.doubleSlownie(get_DoWyplaty(rodzaj, wynagrodzenie)));
        }

        public string get_Szablon(RodzajUmowy rodzaj)
        {
            StreamReader reader = null;
            switch (rodzaj)
            {
                case RodzajUmowy.ODzielo:
                    if (string.IsNullOrEmpty(SzablonDzielo))
                    {
                        reader = new StreamReader(Application.StartupPath +
                        @"\szablony\szablon do umowy o dzieło\szablon do umowy o dzieło.rtf");
                        SzablonDzielo = reader.ReadToEnd();
                        reader.Close();
                    }
                    return SzablonDzielo;
                case RodzajUmowy.NaZlecenieStudent:
                    if (string.IsNullOrEmpty(SzablonStudent))
                    {
                        reader = new StreamReader(Application.StartupPath +
                        @"\szablony\szablon do umowy na zlecenie - student\szablon do umowy na zlecenie - student.rtf");
                        SzablonStudent = reader.ReadToEnd();
                        reader.Close();
                    }
                    return SzablonStudent;
                case RodzajUmowy.NaZlecenie:
                    if (string.IsNullOrEmpty(SzablonZlecenie))
                    {
                        reader = new StreamReader(Application.StartupPath +
                        @"\szablony\szablon do umowy na zlecenie\szablon do umowy na zlecenie.rtf");
                        SzablonZlecenie  = reader.ReadToEnd();
                        reader.Close();
                    }
                    return SzablonZlecenie;
                default:
                    return SzablonDzielo;
            }
        }
        
        #endregion

    }
}
