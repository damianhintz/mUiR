using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace muir.Model
{
    public class Projekt : Interfejs, IComparer
    {
        #region Implementacja konstruktorów

        public Projekt()
        {
            this.m_nazwa = "Nowy projekt - " + DateTime.Now.Year;
            this.m_opis = "Projekt na rok " + DateTime.Now.Year;
        }

        public Projekt(string nazwa)
        {
            this.m_nazwa = nazwa;
            this.m_opis = "Projekt na rok " + DateTime.Now.Year;
        }

        public Projekt(string nazwa, string opis)
        {
            this.m_nazwa = nazwa;
            this.m_opis = opis;
        }

        #endregion

        #region Implementacja atrybutów

        public string Typ
        {
            get { return "Projekt"; }
        }

        private string p_nazwa = "";
        private string m_nazwa = "";

        [Category(""), DisplayName("Nazwa projektu"), Description("Nazwa projektu"),
        ReadOnly(true)]
        public string Nazwa
        {
            get { return m_nazwa; }
            set { m_nazwa = value; }
        }

        protected string m_opis = "";

        [Category(""), DisplayName("Opis projektu"), Description("Kilka zdań na temat projektu"),
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

        [Category(""), DisplayName("Twórca projektu"), Description("Właściciel tego projektu"),
        ReadOnly(true)]
        public string Uzytkownik
        {
            get { return this.m_uzytkownik; }
            set { this.m_uzytkownik = value; }
        }

        private long p_dataUtworzenia = DateTime.Now.ToBinary();
        private long m_dataUtworzenia = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data utworzenia"), Description(""),
        ReadOnly(true)]
        public DateTime DataUtworzenia
        {
            get { return DateTime.FromBinary(m_dataUtworzenia); }
            set { m_dataUtworzenia = value.ToBinary(); }
        }

        private long p_dataAktualizacji = DateTime.Now.ToBinary();
        private long m_dataAktualizacji = DateTime.Now.ToBinary();

        [Category(""), DisplayName("Data aktualizacji"), Description(""),
        ReadOnly(true)]
        public DateTime DataAktualizacji
        {
            get { return DateTime.FromBinary(m_dataAktualizacji); }
            set { m_dataAktualizacji = value.ToBinary(); }
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
            this.m_dataUtworzenia = this.p_dataUtworzenia;
            this.m_nazwa = this.p_nazwa;
        }

        public void Zachowaj()
        {
            this.p_dataAktualizacji = this.m_dataAktualizacji;
            this.p_dataUtworzenia = this.m_dataUtworzenia;
            this.p_nazwa = this.m_nazwa;
        }

        public EdytorObiektu get_Edytor()
        {
            EdytorObiektu edytor = new EdytorObiektu();
            edytor.Text = "Edytor projektu";
            edytor.ObiektEdytora = this;
            return edytor;
        }

        public override string ToString()
        {
            return string.Format("Obiekt projekt: {0}", this.m_nazwa);
        }

        public string get_Info()
        {
            return "Nazwa: " + this.Nazwa + "\n" +
            "Opis: " + this.Opis + "\n" +
            "Autor: " + this.Uzytkownik + "\n" +
            "Data utworzenia: " + this.DataUtworzenia + "\n" +
            "Data aktualizacji: " + this.DataAktualizacji;
        }

        public int Compare(object a, object b)
        {
            Projekt oa = a as Projekt;
            Projekt ob = b as Projekt;

            return oa.Nazwa.CompareTo(ob.Nazwa);
        }

        public int CompareTo(object obj)
        {
            return this.Compare(this, obj);
        }

        #endregion
    }
}
