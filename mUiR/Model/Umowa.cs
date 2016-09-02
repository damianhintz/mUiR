using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace muir.Model
{
    public enum TakNie
    {
        Tak,
        Nie
    }

    public class RodzajUmowyConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(
                               ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(
                new string[]{
                    "Umowa o dzieło", 
                    "Umowa na zlecenie", 
                    "Umowa na zlecenie - student"
                }
            );
        }
    }

    public class RodzajPlatnosciConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(
                               ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(
                new string[]{
                    "przelewem", 
                    "w siedzibie firmy"
                }
            );
        }
    }

    public class NazwaSzablonuUmowyConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(
                               ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> rodzaje = RodzajeUmow.get_SzablonyUmow(AktywneSzablony.AktywnaUmowa.Rodzaj);
            if (rodzaje == null)
                rodzaje = new List<string>();
            return new StandardValuesCollection((ICollection)rodzaje);
        }
    }

    public enum RodzajUmowy
    {
        ODzielo,
        NaZlecenie,
        NaZlecenieStudent
    }

    public class RodzajUmowyValue
    {
        public static string get_Value(RodzajUmowy rodzaj)
        {
            switch (rodzaj)
            {
                case RodzajUmowy.ODzielo:
                    return "Umowa o dzieło";
                case RodzajUmowy.NaZlecenie:
                    return "Umowa na zlecenie";
                case RodzajUmowy.NaZlecenieStudent:
                    return "Umowa na zlecenie - student";
                default:
                    return "Umowa inna";
            }
        }
    }

    //[DefaultProperty("Numer umowy")]
    public class Umowa : Interfejs, IComparer
    {
        #region Implementacja konstruktorów

        public Umowa()
        {
            this.m_nazwa = "Nowa Umowa";
            this.m_opis = "Nowa Umowa";
        }

        public Umowa(string nazwa)
        {
            this.m_nazwa = nazwa;
            this.m_opis = nazwa;
        }

        public Umowa(string nazwa, string opis)
        {
            this.m_nazwa = nazwa;
            this.m_opis = opis;
        }

        public Umowa(string nazwa, RodzajUmowy rodzaj)
        {
            this.m_nazwa = nazwa;
            this.m_rodzaj = rodzaj;
        }

        #endregion

        #region Implementacja atrybutów

        public string Typ
        {
            get { return "Umowa"; }
        }

        protected string m_nazwa = "";

        [Category(""), DisplayName("Nazwa"), Description(""), ReadOnly(true)]
        public string Nazwa
        {
            get { return m_nazwa; }
            set { m_nazwa = value; }
        }

        private string p_numer = "";
        private string m_numer = "";

        [Category(""), DisplayName("Numer umowy"), Description("Podaj numer umowy (%NUMER_UMOWY%)")]
        public string Numer
        {
            get { return m_numer; }
            set { m_numer = value; }
        }

        private RodzajUmowy p_rodzaj = RodzajUmowy.ODzielo;
        private RodzajUmowy m_rodzaj = RodzajUmowy.ODzielo;

        [Category(""), DisplayName("Rodzaj umowy"), Description("Wybierz rodzaj umowy")]
        public RodzajUmowy Rodzaj
        {
            get { return m_rodzaj; }
            set { m_rodzaj = value; }
        }

        protected bool m_zdrowotne = false;
        
        [Category(""), DisplayName("Ubezpieczenie zdrowotne"), Description(""),
        Browsable(false)]
        public bool Zdrowotne
        {
            get { return this.m_zdrowotne; }
            set { this.m_zdrowotne = value; }
        }

        private string m_robota = "";

        [Category(""), DisplayName("Numer roboty"),
        Description("Atrybut ten jest mapowany w szablonie na zmienną %NUMER_ROBOTY%")]
        public string Robota
        {
            get { return m_robota; }
            set { m_robota = value; }
        }

        protected decimal m_kwota = 0.0m;

        [Category(""), DisplayName("Wynagrodzenie"), Description("Podaj na jaką kwotę jest umowa (%WYNAGRODZENIE%)")]
        public decimal Kwota
        {
            get { return this.m_kwota; }
            set { this.m_kwota = value; }
        }

        protected string m_rodzajPlatnosci = "przelewem";

        [Category(""), DisplayName("Rodzaj płatności"), Description("(%RODZAJ_PLATNOSCI%)"),
        TypeConverter(typeof(RodzajPlatnosciConverter))]
        public string RodzajPlatnosci
        {
            get { return this.m_rodzajPlatnosci; }
            set { this.m_rodzajPlatnosci = value; }
        }

        private long m_dataDnia = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data dnia"), Description("(%DATA_DNIA%)"),
        ReadOnly(false)]
        public DateTime DataDnia
        {
            get { return DateTime.FromBinary(m_dataDnia); }
            set { m_dataDnia = value.ToBinary(); }
        }

        private long m_dataOd = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data od"), Description("%DATA_OD%"),
        ReadOnly(false)]
        public DateTime DataOd
        {
            get { return DateTime.FromBinary(m_dataOd); }
            set { m_dataOd = value.ToBinary(); }
        }

        private long m_dataDo = DateTime.Now.AddMonths(1).ToBinary();

        [Category(""), DisplayName("Data do"), Description("%DATA_DO%"),
        ReadOnly(false)]
        public DateTime DataDo
        {
            get { return DateTime.FromBinary(m_dataDo); }
            set { m_dataDo = value.ToBinary(); }
        }

        private long m_dataOdbioru = DateTime.Now.AddMonths(1).ToBinary();

        [Category(""), DisplayName("Data odbioru"), Description("%DATA_ODBIORU%"),
        ReadOnly(false)]
        public DateTime DataOdbioru
        {
            get { return DateTime.FromBinary(m_dataOdbioru); }
            set { m_dataOdbioru = value.ToBinary(); }
        }

        private long m_dataUkonczenia = DateTime.Now.AddMonths(1).ToBinary();

        [Category(""), DisplayName("Data ukończenia"), Description("%DATA_UKONCZENIA%"),
        ReadOnly(false)]
        public DateTime DataUkonczenia
        {
            get { return DateTime.FromBinary(m_dataUkonczenia); }
            set { m_dataUkonczenia = value.ToBinary(); }
        }

        protected string m_postanowieniaDodatkowe = "";

        [Category(""), DisplayName("Postanowienia dodatkowe"), Description("%POSTANOWIENIA_DODATKOWE%"),
        ReadOnly(false)]
        public string PostanowieniaDodatkowe
        {
            get { return this.m_postanowieniaDodatkowe; }
            set { this.m_postanowieniaDodatkowe = value; }
        }

        protected string m_nazwaSzablonu = "";

        [Category(""), DisplayName("Nazwa szablonu"), Description(""),
        TypeConverter(typeof(NazwaSzablonuUmowyConverter))]
        public string NazwaSzablonu
        {
            get { return this.m_nazwaSzablonu; }
            set { this.m_nazwaSzablonu = value; }
        }

        protected string m_opis = "";

        [Category(""), DisplayName("Opis umowy"), Description("%OPIS_UMOWY%"),
        Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Opis
        {
            get { return m_opis; }
            set { m_opis = value; }
        }

        protected bool m_publiczny = false;

        [Category(""), DisplayName("Publiczny"), Description("Określ czy obiekt może być modyfikowany przez innych użytkowników"),
        ReadOnly(true), Browsable(false)]
        public bool Publiczny
        {
            get { return this.m_publiczny; }
            set { this.m_publiczny = value; }
        }

        protected string m_uzytkownik = "";

        [Category(""), DisplayName("Twórca umowy"), Description("Właściciel tego obiektu"),
        ReadOnly(true)]
        public string Uzytkownik
        {
            get { return this.m_uzytkownik; }
            set { this.m_uzytkownik = value; }
        }

        private long p_dataUtworzenia = DateTime.Now.ToBinary();
        private long m_dataUtworzenia = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data utworzenia"), Description("Data utworzenia obiektu umowy"),
        ReadOnly(true)]
        public DateTime DataUtworzenia
        {
            get { return DateTime.FromBinary(m_dataUtworzenia); }
            set { m_dataUtworzenia = value.ToBinary(); }
        }

        private long p_dataAktualizacji = DateTime.Now.ToBinary();
        private long m_dataAktualizacji = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data aktualizacji"), Description("Data ostatniej aktualizacji obiektu umowy"),
        ReadOnly(true)]
        public DateTime DataAktualizacji
        {
            get { return DateTime.FromBinary(m_dataAktualizacji); }
            set { m_dataAktualizacji = value.ToBinary(); }
        }

        protected long m_dataZapisu = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data zapisu"), Description("Data zapisu pliku na dysk"),
        ReadOnly(true), Browsable(false)]
        public DateTime DataZapisu
        {
            get { return DateTime.FromBinary(m_dataZapisu); }
            set { m_dataZapisu = value.ToBinary(); }
        }

        #endregion

        #region Implementacja interfejsu

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Zachowaj()
        {
            this.p_dataAktualizacji = this.m_dataAktualizacji;
            this.p_dataUtworzenia = this.m_dataUtworzenia;
            this.p_numer = this.m_numer;
            this.p_rodzaj = this.m_rodzaj;
        }

        public void Przywroc()
        {
            this.m_dataAktualizacji = this.p_dataAktualizacji;
            this.m_dataUtworzenia = this.p_dataUtworzenia;
            this.m_numer = this.p_numer;
            this.m_rodzaj = this.p_rodzaj;
        }

        public EdytorObiektu get_Edytor()
        {
            EdytorObiektu edytor = new EdytorObiektu();
            edytor.Text = "Edytor umowy";
            edytor.ObiektEdytora = this;
            return edytor;
        }

        public override string ToString()
        {
            return string.Format("Obiekt Umowa: {0}", this.m_nazwa);
        }

        public string get_Info()
        {
            return "Numer: " + this.Numer + "\n" +
            "Opis: " + this.Opis + "\n" +
            "Autor: " + this.Uzytkownik + "\n" +
            "Data utworzenia: " + this.DataUtworzenia + "\n" +
            "Data aktualizacji: " + this.DataAktualizacji + "\n" +
            "Rodzaj: " + RodzajUmowyValue.get_Value(this.Rodzaj) + "\n" +
            "Kwota: " + this.Kwota + " zł";
        }

        public int Compare(object a, object b)
        {
            Umowa ra = a as Umowa;
            Umowa rb = b as Umowa;

            return ra.DataUtworzenia.CompareTo(rb.DataUtworzenia);
        }

        public int CompareTo(object obj)
        {
            return this.Compare(this, obj);
        }

        #endregion

    }
}
