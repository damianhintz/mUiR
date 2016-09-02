using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;

namespace muir.Model.Uprawnienia
{
    class Kierownik : Uzytkownik
    {
        /// <summary>
        /// kazdy kierownik posiada liste aktywnych osob z ktorymi w danym miesiacu pracuje
        /// lista taka jest przechowywana w pliku binarnym i dostep jest kontrolowany przez haslo
        /// w kazdym takim plikiem powiazane jest haslo ustalone przez kierownika
        /// admin, dyrektor maja dostep bez zadnej kontroli
        /// </summary>
        protected List<string> m_aktywneOsoby = new List<string>();

        public virtual bool insertAktywnaOsoba()
        {
            return true;
        }

        public override bool selectLokacja()
        {
            return true;
        }

        public override bool insertLokacja()
        {
            return false;
        }

        public override bool updateLokacja()
        {
            return true;
        }

        public override bool insertDyrektor()
        {
            return false;
        }

        public override bool deleteDyrektor()
        {
            return false;
        }

        public override bool insertKierownik()
        {
            return false;
        }

        public override bool deleteKierownik()
        {
            return false;
        }

        public override bool selectOsoba()
        {
            return true;
        }

        public override bool insertOsoba()
        {
            return false;
        }

        public override bool updateOsoba()
        {
            return false;
        }

        public override bool deleteOsoba()
        {
            return false;
        }

        public override bool selectUmowa()
        {
            return false;
        }

        public override bool insertUmowa()
        {
            return false;
        }

        public override bool updateUmowa()
        {
            return false;
        }

        public override bool deleteUmowa()
        {
            return false;
        }

        public override bool selectRachunek()
        {
            return false;
        }

        public override bool insertRachunek()
        {
            return false;
        }

        public override bool updateRachunek()
        {
            return false;
        }

        public override bool deleteRachunek()
        {
            return false;
        }
    }
}
