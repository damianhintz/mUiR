using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.ComponentModel;

namespace muir.Model
{
    //[DefaultProperty("")]
    public class Konfiguracja
    {
        #region Implementacja konstruktorów
        #endregion

        #region Implementacja atrybutów
        
        private List<string> m_ostatnieProjekty = new List<string>();

        [Category(""), DisplayName("Ostatnie projekty"), Description("Lista ostatnich projektów"),
        Browsable(false)]
        public List<string> OstatnieProjekty
        {
            get { return m_ostatnieProjekty; }
        }

        protected AktywneOsoby m_aktywneOsoby = new AktywneOsoby();

        [Category(""), DisplayName("Aktywne osoby"), Description("Lista osób na dany miesiąc"),
        Browsable(false)]
        public AktywneOsoby Osoby
        {
            get { return m_aktywneOsoby; }
        }

        //nazwa katalogu na rodzaje umow
        //nazwa katalogu na rodzaje rachunkow
        //private string m_szablonRachunkuDoUmowyNaZlecenieStudent = "";
        //private string m_szablonRachunkuDoUmowyNaZlecenie = "";
        //private string m_szablonRachunkuDoUmowyODzielo = "";

        #endregion

        #region Implementacja interfejsu

        public EdytorObiektu get_Edytor()
        {
            EdytorObiektu edytor = new EdytorObiektu();
            edytor.Text = "Edytor konfiguracji";
            edytor.ObiektEdytora = this;
            return edytor;
        }

        public bool jestPoprawny()
        {
            return true;
        }

        #endregion

    }
}
