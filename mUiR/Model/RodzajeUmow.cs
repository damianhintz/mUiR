using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace muir.Model
{
    public class RodzajeUmow
    {
        #region Implementacja konstruktorów

        #endregion

        #region Implementacja atrybutów

        protected static string m_nazwa = @"rodzaje umów";
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

        public static List<string> get_SzablonyRachunkow(RodzajUmowy rodzaj)
        {
            string path = Application.StartupPath + @"\" + m_nazwa + @"\" + RodzajUmowyValue.get_Value(rodzaj);
            List<string> rodzaje = null;

            DirectoryInfo diRodzaje = new DirectoryInfo(path);
            if (!diRodzaje.Exists)
                return rodzaje;

            rodzaje = new List<string>();
            foreach (DirectoryInfo diRodzaj in diRodzaje.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                rodzaje.Add(diRodzaj.Name);
            }

            return rodzaje;
        }
        
        public static List<string> get_SzablonyUmow(RodzajUmowy rodzaj)
        {
            string path = Application.StartupPath + @"\szablony umów\" + RodzajUmowyValue.get_Value(rodzaj);
            List<string> rodzaje = null;
            //path = Application.StartupPath + @"\" + m_nazwa;
            DirectoryInfo diRodzaje = new DirectoryInfo(path);
            if (!diRodzaje.Exists)
                return rodzaje;
            //else throw new Exception(path);

            rodzaje = new List<string>();
            foreach (DirectoryInfo diRodzaj in diRodzaje.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                rodzaje.Add(diRodzaj.Name);
            }

            return rodzaje;
        }

        public static string get_Szablon(Umowa umowa, Rachunek rachunek)
        {
            StreamReader reader = null;
            
            string path = Application.StartupPath + @"\" + m_nazwa + @"\" + 
                RodzajUmowyValue.get_Value(umowa.Rodzaj) + @"\" +
                rachunek.NazwaSzablonu + @"\_szablon.rtf";

            //path = @"d:\var\lab\mUiR\1.0.0.13\bin\Release\rodzaje umów\Umowa o dzieło\Szablon rachunku do umowy o dzieło - domyślny\_szablon.rtf";

            string szablon = @"{\rtf1\ansi\ansicpg1250\ brak szablonu: " + path + @"}";

            try
            {
                if (File.Exists(path))
                {
                    reader = new StreamReader(path);
                    szablon = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
            }

            return szablon;
        }

        public static string get_SzablonUmowy(Umowa umowa)
        {
            StreamReader reader = null;

            string path = Application.StartupPath + @"\szablony umów\" +
                RodzajUmowyValue.get_Value(umowa.Rodzaj) + @"\" +
                umowa.NazwaSzablonu + @"\_szablon.rtf";

            string szablon = @"{\rtf1\ansi\ansicpg1250\ brak szablonu: " + path + @"}";

            try
            {
                if (File.Exists(path))
                {
                    reader = new StreamReader(path);
                    szablon = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
            }

            return szablon;
        }

        #endregion
    }
}
