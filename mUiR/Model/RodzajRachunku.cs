using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;

namespace muir.Model
{
    public class RodzajRachunku
    {
        #region Implementacja konstruktorów

        public RodzajRachunku()
        {
        }

        public RodzajRachunku(string nazwa)
        {
            this.m_nazwa = nazwa;
        }

        public RodzajRachunku(string nazwa, string opis)
        {
            this.m_nazwa = nazwa;
            this.m_opis = opis;
        }

        #endregion

        #region Implementacja atrybutów

        protected string m_nazwa = "";
        public string Nazwa
        {
            get { return this.m_nazwa; }
        }

        protected string m_opis = "";
        public string Opis
        {
            get { return this.m_opis; }
        }

        #endregion

        #region Implementacja interfejsu

        public override string ToString()
        {
            return this.m_nazwa;
        }
        #endregion

    }
}
