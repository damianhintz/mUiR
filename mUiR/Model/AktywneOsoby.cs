using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;

namespace muir.Model
{
    /// <summary>
    /// aktywne osoby dla kierowników, kazdy kierownik bedzie mial wlasne osoby lokalnie
    /// </summary>
    public class AktywneOsoby
    {
        protected string m_kierownik = "";
        protected bool m_zatwierdzone = false;
        protected bool m_dlaDyrektora = false;

        protected List<string> m_osoby = new List<string>();
        public List<string> Osoby
        {
            get { return m_osoby; }
        }

        public void Przekaz()
        {
            this.m_dlaDyrektora = true;
        }

        public void Zatwierdz()
        {
            this.m_zatwierdzone = true;
        }
    }
}
