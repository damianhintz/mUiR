using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace muir.Model.Uprawnienia
{
    /// <summary>
    /// kazdy typ uzytkownikow ma prawa do roznych klas obiektow
    /// </summary>
    public enum TypUzytkownika
    {
        Administrator,
        Dyrektor,
        Kierownik
    }

    /// <summary>
    /// uprawnienia sa stosowane na podstawie klasy obiektu, a nie pojedynczych obiektów
    /// </summary>
    public enum TypUprawnienia
    {
        Select,
        Insert,
        Update,
        Delete
    }

    class Uzytkownik
    {
        protected string m_nazwa = "";
        protected string m_haslo = "";

        public bool Login(string haslo)
        {
            return this.m_haslo == haslo;
        }

        public virtual bool selectLokacja()
        {
            return true;
        }

        public virtual bool insertLokacja()
        {
            return false;
        }

        public virtual bool updateLokacja()
        {
            return false;
        }

        public virtual bool insertDyrektor()
        {
            return false;
        }

        public virtual bool deleteDyrektor()
        {
            return false;
        }

        public virtual bool insertKierownik()
        {
            return false;
        }

        public virtual bool deleteKierownik()
        {
            return false;
        }

        public virtual bool selectOsoba()
        {
            return true;
        }

        public virtual bool insertOsoba()
        {
            return false;
        }

        public virtual bool updateOsoba()
        {
            return false;
        }

        public virtual bool deleteOsoba()
        {
            return false;
        }

        public virtual bool selectUmowa()
        {
            return false;
        }

        public virtual bool insertUmowa()
        {
            return false;
        }

        public virtual bool updateUmowa()
        {
            return false;
        }

        public virtual bool deleteUmowa()
        {
            return false;
        }

        public virtual bool selectRachunek()
        {
            return false;
        }

        public virtual bool insertRachunek()
        {
            return false;
        }

        public virtual bool updateRachunek()
        {
            return false;
        }

        public virtual bool deleteRachunek()
        {
            return false;
        }
    }
}
