using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace muir.Model
{
    public enum TypRachunku
    {
        Jednorazowy,
        Częściowy,
        Ostateczny
    }

    public class TypRachunkuValue
    {
        public static string get_Value(TypRachunku typ)
        {
            switch (typ)
            {
                case TypRachunku.Jednorazowy:
                    return "Rachunek jednorazowy";
                case TypRachunku.Częściowy:
                    return "Rachunek częściowy";
                case TypRachunku.Ostateczny:
                    return "Rachunek ostateczny";
                default:
                    return "Rachunek normalny";
            }
        }
    }
    
    public class NazwaSzablonuConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(
                               ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> rodzaje = RodzajeUmow.get_SzablonyRachunkow(AktywneSzablony.AktywnaUmowa.Rodzaj);
            if (rodzaje == null)
                rodzaje = new List<string>();
            return new StandardValuesCollection((ICollection)rodzaje);
        }
    }

    public class RodzajRachunkuConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(
                               ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection
                     GetStandardValues(ITypeDescriptorContext context)
        {
            List<RodzajRachunku> rodzaje = RodzajeRachunkow.get_RodzajeRachunkow();
            if (rodzaje == null)
                rodzaje = new List<RodzajRachunku>();
            return new StandardValuesCollection((ICollection)rodzaje);
        }
    }

    //[DefaultProperty("Wynagrodzenie")]
    public class Rachunek : Interfejs, IComparer
    {
        #region Implementacja konstruktorów

        public Rachunek()
        {
            this.m_nazwa = "Nowy Rachunek";
            this.m_opis = "Nowy Rachunek";
        }

        public Rachunek(string nazwa)
        {
            this.m_nazwa = nazwa;
            this.m_opis = nazwa;
        }

        public Rachunek(string nazwa, string opis)
        {
            this.m_nazwa = nazwa;
            this.m_opis = opis;
        }

        #endregion

        #region Implementacja atrybutów

        public string Typ
        {
            get { return "Rachunek"; }
        }

        protected string m_nazwa = "";

        [Category(""), DisplayName("Nazwa"), Description(""), ReadOnly(true)]
        public string Nazwa
        {
            get { return m_nazwa; }
            set { m_nazwa = value; }
        }

        private decimal p_wynagrodzenie;
        private decimal m_wynagrodzenie;

        [Category(""), DisplayName("Wynagrodzenie"),
        Description("Atrybut ten jest mapowany w szablonie na zmienną %WYNAGRODZENIE%")]
        public decimal Wynagrodzenie
        {
            get { return m_wynagrodzenie; }
            set { m_wynagrodzenie = value; }
        }
        /*
        protected string m_rodzajRachunku = "";

        [Category(""), DisplayName("Rodzaj rachunku"), Description(""),
        TypeConverter(typeof(RodzajRachunkuConverter))]
        public string RodzajRachunku
        {
            get { return this.m_rodzajRachunku; }
            set { this.m_rodzajRachunku = value; }
        }
        */

        private TypRachunku p_typ = TypRachunku.Jednorazowy;
        private TypRachunku m_typ = TypRachunku.Jednorazowy;

        [Category(""), DisplayName("Typ rachunku"), Description("")]
        public TypRachunku Rodzaj
        {
            get { return m_typ; }
            set { m_typ = value; }
        }

        protected bool m_wKasie = false;

        [Category(""), DisplayName("Płatne w kasie"), Description("")]
        public bool WKasie
        {
            get { return m_wKasie; }
            set { m_wKasie = value; }
        }

        private long p_dataDnia = DateTime.Now.ToBinary();
        private long m_dataDnia = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data dnia"),
        Description("Atrybut ten jest mapowany w szablonie na zmienną %DATA_DNIA%")]
        public DateTime DataDnia
        {
            get { return DateTime.FromBinary(m_dataDnia); }
            set { m_dataDnia = value.ToBinary(); }
        }

        private long p_dataWykonania = DateTime.Now.ToBinary();
        private long m_dataWykonania = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data wykonania"), 
        Description("Atrybut ten jest mapowany w szablonie na zmienną %DATA_WYKONANIA%")]
        public DateTime DataWykonania
        {
            get { return DateTime.FromBinary(m_dataWykonania); }
            set { m_dataWykonania = value.ToBinary(); }
        }

        private string p_robota = "";
        private string m_robota = "";

        [Category(""), DisplayName("Numer roboty"), 
        Description("Atrybut ten jest mapowany w szablonie na zmienną %NUMER_ROBOTY%")]
        public string Robota
        {
            get { return m_robota; }
            set { m_robota = value; }
        }

        protected string m_nazwaSzablonu = "";

        [Category(""), DisplayName("Nazwa szablonu"), Description(""),
        TypeConverter(typeof(NazwaSzablonuConverter))]
        public string NazwaSzablonu
        {
            get { return this.m_nazwaSzablonu; }
            set { this.m_nazwaSzablonu = value; }
        }

        protected string m_opis = "";

        [Category(""), DisplayName("Opis"), Description("")]
        public string Opis
        {
            get { return m_opis; }
            set { m_opis = value; }
        }

        protected TakNie m_aktywny = TakNie.Tak;

        [Category(""), DisplayName("Aktywny"), Description("Aktywny rachunek oczekuje na zatwierdzenie przez dyrektora")]
        public TakNie Aktywny
        {
            get { return this.m_aktywny; }
            set { this.m_aktywny = value; }
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

        [Category(""), DisplayName("Twórca rachunku"), Description("Właściciel tego obiektu"),
        ReadOnly(true)]
        public string Uzytkownik
        {
            get { return this.m_uzytkownik; }
            set { this.m_uzytkownik = value; }
        }


        private long p_dataUtworzenia = DateTime.Now.ToBinary();
        private long m_dataUtworzenia = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data utworzenia"), Description("Data utworzenia obiektu rachunku"),
        ReadOnly(true)]
        public DateTime DataUtworzenia
        {
            get { return DateTime.FromBinary(m_dataUtworzenia); }
            set { m_dataUtworzenia = value.ToBinary(); }
        }

        private long p_dataAktualizacji = DateTime.Now.ToBinary();
        private long m_dataAktualizacji = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data aktualizacji"), Description("Data ostatniej aktualizacji obiektu rachunku"),
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

        public void Przywroc()
        {
            this.m_dataAktualizacji = this.p_dataAktualizacji;
            this.m_dataDnia = this.p_dataDnia;
            this.m_dataUtworzenia = this.p_dataUtworzenia;
            this.m_dataWykonania = this.p_dataWykonania;
            this.m_robota = this.p_robota;
            this.m_typ = this.p_typ;
            this.m_wynagrodzenie = this.p_wynagrodzenie;

        }

        public void Zachowaj()
        {
            this.p_dataAktualizacji = this.m_dataAktualizacji;
            this.p_dataDnia = this.m_dataDnia;
            this.p_dataUtworzenia = this.m_dataUtworzenia;
            this.p_dataWykonania = this.m_dataWykonania;
            this.p_robota = this.m_robota;
            this.p_typ = this.m_typ;
            this.p_wynagrodzenie = this.m_wynagrodzenie;
        }

        public EdytorObiektu get_Edytor()
        {
            EdytorObiektu edytor = new EdytorObiektu();
            edytor.Text = "Edytor rachunku";
            edytor.ObiektEdytora = this;
            return edytor;
        }

        public override string ToString()
        {
            return string.Format("Obiekt Rachunek: {0}", this.m_nazwa);
        }

        public string get_Info()
        {
            return "Nazwa: " + this.Nazwa + "\n" +
            "Opis: " + this.Opis + "\n" +
            "Autor: " + this.Uzytkownik + "\n" +
            "Data utworzenia: " + this.DataUtworzenia + "\n" +
            "Data aktualizacji: " + this.DataAktualizacji + "\n" +
            "Typ: " + TypRachunkuValue.get_Value(this.Rodzaj) + "\n" +
            "Wynagrodzenie: " + this.Wynagrodzenie + " zł\n" +
            "Numer roboty: " + this.Robota + "\n" +
            "Płatne w kasie: " + (this.WKasie ? "tak" : "nie");

        }

        public int Compare(object a, object b)
        {
            Rachunek ra = a as Rachunek;
            Rachunek rb = b as Rachunek;

            return ra.DataUtworzenia.CompareTo(rb.DataUtworzenia);
        }
        
        public int CompareTo(object obj)
        {
            return this.Compare(this, obj);
        }

        #endregion

    }
}
