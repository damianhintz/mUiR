using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace muir.Model
{
    //[DefaultProperty("Imię")]
    public class Osoba : Interfejs, IComparer
    {
        #region Implementacja konstruktorów

        public Osoba()
        {
            this.m_nazwa = "Nowa osoba";
            this.m_opis = "Nowa osoba";
        }

        public Osoba(string nazwa)
        {
            this.m_nazwa = nazwa;
            this.m_opis = nazwa;
        }

        public Osoba(string nazwa, string opis)
        {
            this.m_nazwa = nazwa;
            this.m_opis = opis;
        }

        #endregion

        #region Implementacja atrybutów

        [Category("Identyfikacja"), DisplayName("Typ"), Description("")]
        public string Typ
        {
            get { return "Osoba"; }
        }

        protected string m_nazwa = "";

        [Category("Identyfikacja"), DisplayName("Nazwa"), Description(""),
        ReadOnly(true)]
        public string Nazwa
        {
            get { return m_nazwa; }
            set { m_nazwa = value; }
        }

        protected string m_imie = "";

        [Category("Dane osobowe"), DisplayName("Imię"), Description("%IMIE%"),
        ReadOnly(false)]
        public string Imie
        {
            get { return this.m_imie; }
            set { this.m_imie = value; }
        }

        protected string m_nazwisko = "";

        [Category("Dane osobowe"), DisplayName("Nazwisko"), Description("%NAZWISKO%"),
        ReadOnly(false)]
        public string Nazwisko
        {
            get { return this.m_nazwisko; }
            set { this.m_nazwisko = value; }
        }

        private long m_dataUrodzenia = DateTime.Now.ToBinary();

        [Category("Dane osobowe"), DisplayName("Data urodzenia"), Description("%DATA_URODZENIA%"),
        ReadOnly(false)]
        public DateTime DataUrodzenia
        {
            get { return DateTime.FromBinary(m_dataUrodzenia); }
            set { m_dataUrodzenia = value.ToBinary(); }
        }

        private string m_adres = "";

        [Category("Dane osobowe"), DisplayName("Adres zamieszkania"), Description("%ADRES%"),
        ReadOnly(false)]
        public string Adres
        {
            get { return this.m_adres; }
            set { this.m_adres = value; }
        }

        protected string m_powiat = "";

        [Category(""), DisplayName("Powiat"), Description("%POWIAT%"),
        ReadOnly(false)]
        public string Powiat
        {
            get { return this.m_powiat; }
            set { this.m_powiat = value; }
        }
        
        protected string m_wojewodztwo = "";

        [Category(""), DisplayName("Województwo"), Description("%WOJEWODZTWO%"),
        ReadOnly(false)]
        public string Wojewodztwo
        {
            get { return this.m_wojewodztwo; }
            set { this.m_wojewodztwo = value; }
        }

        protected string m_nip = "";
        
        [Category(""), DisplayName("Numer NIP"), Description("%NIP%"),
        ReadOnly(false)]
        public string Nip
        {
            get { return this.m_nip; }
            set { this.m_nip = value; }
        }

        protected string m_pesel = "";

        [Category(""), DisplayName("Numer PESEL"), Description("%PESEL%"),
        ReadOnly(false)]
        public string Pesel
        {
            get { return this.m_pesel; }
            set { this.m_pesel = value; }
        }

        protected string m_bank = "";

        [Category(""), DisplayName("Nazwa banku"), Description("%BANK%")]
        public string Bank
        {
            get { return m_bank; }
            set { m_bank = value; }
        }

        protected string m_konto = "";

        [Category(""), DisplayName("Numer konta w banku"), Description("%KONTO%"),
        ReadOnly(false)]
        public string Konto
        {
            get { return this.m_konto; }
            set { this.m_konto = value; }
        }

        protected string m_urzadSkarbowy = "";

        [Category(""), DisplayName("Urząd skarbowy"), Description("%URZAD_SKARBOWY%")]
        public string UrzadSkarbowy
        {
            get { return this.m_urzadSkarbowy; }
            set { this.m_urzadSkarbowy = value; }
        }

        protected string m_opis = "";

        [Category(""), DisplayName("Opis"), Description("%OPIS_OSOBY%"),
        ReadOnly(false)]
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

        [Category("Atrybuty obiektu"), DisplayName("Twórca osoby"), Description("Właściciel tego obiektu"),
        ReadOnly(true)]
        public string Uzytkownik
        {
            get { return this.m_uzytkownik; }
            set { this.m_uzytkownik = value; }
        }

        private long p_dataUtworzenia = DateTime.Now.ToBinary();
        private long m_dataUtworzenia = DateTime.Now.ToBinary();

        [Category("Atrybuty obiektu"), DisplayName("Data utworzenia"), Description(""),
        ReadOnly(true)]
        public DateTime DataUtworzenia
        {
            get { return DateTime.FromBinary(m_dataUtworzenia); }
            set { m_dataUtworzenia = value.ToBinary(); }
        }

        private long p_dataAktualizacji = DateTime.Now.ToBinary();
        private long m_dataAktualizacji = DateTime.Now.ToBinary();

        [Category("Atrybuty obiektu"), DisplayName("Data aktualizacji"), Description(""),
        ReadOnly(true)]
        public DateTime DataAktualizacji
        {
            get { return DateTime.FromBinary(m_dataAktualizacji); }
            set { m_dataAktualizacji = value.ToBinary(); }
        }

        #endregion

        #region Implementacja interfesju

        public override string ToString()
        {
            return string.Format("Obiekt osoba: {0}", this.m_nazwa);
        }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Zachowaj()
        {
        }

        public void Przywroc()
        {
        }

        public EdytorObiektu get_Edytor()
        {
            EdytorObiektu edytor = new EdytorObiektu();
            edytor.Text = "Edytor osoby";
            edytor.ObiektEdytora = this;
            return edytor;
        }

        public string get_Info()
        {
            return "Imię i nazwisko: " + this.Imie + " " + this.Nazwisko + "\n" +
            "Opis: " + this.Opis + "\n" +
            "Autor: " + this.Uzytkownik + "\n" +
            "Data utworzenia: " + this.DataUtworzenia + "\n" +
            "Data aktualizacji: " + this.DataAktualizacji;
        }
        
        public int Compare(object a, object b)
        {
            Osoba oa = a as Osoba;
            Osoba ob = b as Osoba;

            return oa.Nazwa.CompareTo(ob.Nazwa);
        }

        public int CompareTo(object obj)
        {
            return this.Compare(this, obj);
        }

        #endregion

    }
}
