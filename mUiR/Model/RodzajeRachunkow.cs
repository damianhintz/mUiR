using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace muir.Model
{
    public class RodzajeRachunkow
    {
        #region Implementacja konstruktorów

        #endregion
        
        #region Implementacja atrybutów

        protected static string m_nazwa = @"rodzaje rachunków";
        public string Nazwa
        {
            get { return m_nazwa; }
        }

        #endregion

        #region Implementacja interfejsu

        public static string get_Rodzaj(string nazwa)
        {
            return nazwa;
        }

        public static List<RodzajRachunku> get_RodzajeRachunkow()
        {
            string path = Application.StartupPath + @"\" + m_nazwa;
            List<RodzajRachunku> rodzaje = null;

            DirectoryInfo diRodzaje = new DirectoryInfo(path);
            if (!diRodzaje.Exists)
                return rodzaje;

            rodzaje = new List<RodzajRachunku>();
            foreach (DirectoryInfo diRodzaj in diRodzaje.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                RodzajRachunku rodzaj = new RodzajRachunku(diRodzaj.Name);
                rodzaje.Add(rodzaj);
            }

            return rodzaje;
        }

        #endregion

    }
}
