using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;

namespace muir.Model.Uprawnienia
{
    class Dyrektor : Kierownik
    {
        public override bool insertAktywnaOsoba()
        {
            return true;
        }

        public override bool selectLokacja()
        {
            return true;
        }

        public override bool insertLokacja()
        {
            return true;
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
            return true;
        }

        public override bool deleteKierownik()
        {
            return true;
        }

        public override bool selectOsoba()
        {
            return true;
        }

        public override bool insertOsoba()
        {
            return true;
        }

        public override bool updateOsoba()
        {
            return true;
        }

        public override bool deleteOsoba()
        {
            return true;
        }

        public override bool selectUmowa()
        {
            return true;
        }

        public override bool insertUmowa()
        {
            return true;
        }

        public override bool updateUmowa()
        {
            return true;
        }

        public override bool deleteUmowa()
        {
            return true;
        }

        public override bool selectRachunek()
        {
            return true;
        }

        public override bool insertRachunek()
        {
            return true;
        }

        public override bool updateRachunek()
        {
            return true;
        }

        public override bool deleteRachunek()
        {
            return true;
        }
    }
}
