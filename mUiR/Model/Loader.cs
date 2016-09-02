using System;
using System.Collections.Generic;
////using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

using muir.Model;

namespace muir
{
    public class Loader
    {
        public static object Load(Type typ, string file)
        {
            string fileLock = file + ".lock";
            if(File.Exists(fileLock))
            {
                Thread.Sleep(300);
                if (File.Exists(fileLock))
                    return null;
            }

            StreamReader reader = null;
            object obiekt = null;
            try
            {
                XmlSerializer xmlSer = new XmlSerializer(typ);
                reader = new StreamReader(file);
                obiekt = xmlSer.Deserialize(reader);
            }
            catch (Exception)
            {
                //throw ex;
                obiekt = null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return obiekt;
        }

        public static bool Unload(Type typ, string file, object obiekt)
        {
            string fileLock = file + ".lock";
            if (File.Exists(fileLock))
            {
                //ktos w tym momencie zapisuje plik na dysk
                //powinnismy przerwac i odczytac go pozniej
                return false;
            }
            //co jest nie tak, nawet po sprawdzeniu czy taki plik nie istnieje nadal wyskakuje ze juz ktos go uzywa
            FileStream stream = null;
            try
            {
                stream = File.Create(fileLock, 1024, FileOptions.DeleteOnClose);
            }
            catch (IOException)
            {
                return false;
            }

            XmlSerializer xmlSer = new XmlSerializer(typ);
            StreamWriter writer = new StreamWriter(file);
            xmlSer.Serialize(writer, obiekt);
            writer.Close();
            if(stream != null)
                stream.Close();
            return true;
        }

        public static string GenRtf(Osoba osoba, Umowa umowa, Rachunek rachunek)
        {
            AktywneSzablony szablon = new AktywneSzablony();

            string all = "";
            //all = szablon.get_Szablon(umowa.Rodzaj);
            all = RodzajeUmow.get_Szablon(umowa, rachunek);

            string tytul = "Rachunek ";
            switch (rachunek.Rodzaj)
            {
                case TypRachunku.Częściowy:
                    tytul = "Rachunek częściowy";
                    break;
                case TypRachunku.Ostateczny:
                    tytul = "Rachunek ostateczny";
                    break;
                default:
                    tytul = "Rachunek";
                    break;
            }
            
            switch (umowa.Rodzaj)
            {
                case RodzajUmowy.NaZlecenie:
                    tytul += " do umowy zlecenie";
                    break;
                case RodzajUmowy.NaZlecenieStudent:
                    tytul += " do umowy zlecenie";
                    break;
                case RodzajUmowy.ODzielo:
                    tytul += " do umowy o dzieło";
                    break;
                default:
                    break;
            }

            tytul = tytul.ToUpper();

            all = all.Replace("%OSOBA%", "" + osoba.Imie + " " + osoba.Nazwisko);
            all = all.Replace("%DATA_DNIA%", "" + rachunek.DataDnia.ToString("dd.MM.yyyy"));
            all = all.Replace("%TYTUL_RACHUNKU%", "" + tytul);
            all = all.Replace("%NUMER_RACHUNKU%", "" + umowa.Numer);
            all = all.Replace("%WYNAGRODZENIEB%", "" + 
                string.Format("{0:F2}", rachunek.Wynagrodzenie));
            all = all.Replace("%KOSZTYU_P%", "" + szablon.get_Procenty(umowa.Rodzaj).KosztyUzyskania);
            all = all.Replace("%KOSZTYU%", "" + 
                string.Format("{0:F2}", szablon.get_KosztyUzyskania(umowa.Rodzaj, rachunek.Wynagrodzenie)));
            all = all.Replace("%PODSTAWAO%", "" + 
                string.Format("{0:F2}", szablon.get_PodstawaOpodatkowania(umowa.Rodzaj, rachunek.Wynagrodzenie)));
            all = all.Replace("%PODATEKD_P%", "" + szablon.get_Procenty(umowa.Rodzaj).PodatekDochodowy);

            decimal wyplata = szablon.get_DoWyplaty(umowa.Rodzaj, rachunek.Wynagrodzenie);
            all = all.Replace("%DOWYPLATY%", "" + 
                string.Format("{0:F2}", wyplata));

            decimal d = wyplata;
            long a = (long)(d * 100);
            long zl = a / 100;
            long groszy = a - zl * 100;

            string slownie = Slownie.innerTrim(Slownie.doubleSlownie(zl));

            if (groszy > 0)
            {
                slownie += " zł " + Slownie.innerTrim(Slownie.doubleSlownie(groszy)) + " gr";
            }

            all = all.Replace("%SLOWNIE%", "" + slownie);

            if (rachunek.WKasie)
                all = all.Replace("%WKASIE_LUB_NAKONTO%", "Płatne w kasie");
            else
                all = all.Replace("%WKASIE_LUB_NAKONTO%", osoba.Bank + " " + osoba.Konto);

            all = all.Replace("%DATA_WYKONANIA%", "" + rachunek.DataWykonania.ToShortDateString());
            all = all.Replace("%ROBOTA%", rachunek.Robota);

            all = all.Replace("%UBEZPIECZENIEZ_P%", "" +
                string.Format("{0:F2}", szablon.get_Procenty(umowa.Rodzaj).UbezpieczenieZdrowotne_7));
            all = all.Replace("%UBEZPIECZENIEZ2_P%", "" +
                string.Format("{0:F0}", szablon.get_Procenty(umowa.Rodzaj).UbezpieczenieZdrowotne_9));
            all = all.Replace("%UBEZPIECZENIEZ%", "" + 
                string.Format("{0:F2}", szablon.get_UbezpieczenieZdrowotne7(umowa.Rodzaj, rachunek.Wynagrodzenie)));
            all = all.Replace("%UBEZPIECZENIEZ2%", "" +
                string.Format("{0:F2}", szablon.get_UbezpieczenieZdrowotne9(umowa.Rodzaj, rachunek.Wynagrodzenie)));

            all = all.Replace("%PODATEKD%", "" + 
                string.Format("{0:F2}", 
                    szablon.get_PodatekDochodowy(umowa.Rodzaj, rachunek.Wynagrodzenie)));

            return all;
        }

        public static string GenUmowa(Osoba osoba, Umowa umowa)
        {
            AktywneSzablony szablon = new AktywneSzablony();

            string all = "";
            all = RodzajeUmow.get_SzablonUmowy(umowa);

            all = all.Replace("%IMIE%", osoba.Imie);
            all = all.Replace("%NAZWISKO%", osoba.Nazwisko);
            all = all.Replace("%OPIS_OSOBY%", osoba.Opis);
            all = all.Replace("%DATA_DNIA%", "" + umowa.DataDnia.ToString("dd.MM.yyyy"));
            all = all.Replace("%DATA_URODZENIA%", "" + osoba.DataUrodzenia.ToString("dd.MM.yyyy"));
            all = all.Replace("%NUMER_UMOWY%", "" + umowa.Numer);
            all = all.Replace("%PESEL%", osoba.Pesel);
            all = all.Replace("%ADRES%", osoba.Adres);
            all = all.Replace("%POWIAT%", osoba.Powiat);
            all = all.Replace("%WOJEWODZTWO%", osoba.Wojewodztwo);
            all = all.Replace("%NIP%", osoba.Nip);

            all = all.Replace("%BANK%", osoba.Bank);
            all = all.Replace("%KONTO%", osoba.Konto);
            all = all.Replace("%URZAD_SKARBOWY%", osoba.UrzadSkarbowy);

            all = all.Replace("%OPIS_UMOWY%", umowa.Opis.Replace("\n", " "));
            all = all.Replace("%NUMER_ROBOTY%", umowa.Robota);

            all = all.Replace("%DATA_OD%", umowa.DataOd.ToString("dd.MM.yyyy"));
            all = all.Replace("%DATA_DO%", umowa.DataDo.ToString("dd.MM.yyyy"));
            all = all.Replace("%DATA_ODBIORU%", umowa.DataOdbioru.ToString("dd.MM.yyyy"));
            all = all.Replace("%DATA_UKONCZENIA%", umowa.DataUkonczenia.ToString("dd.MM.yyyy"));

            decimal d = umowa.Kwota;
            long a = (long)(d * 100);
            long zl = a / 100;
            long groszy = a - zl * 100;

            string slownie = Slownie.innerTrim(Slownie.doubleSlownie(zl));

            if (groszy > 0)
            {
                slownie += " zł " + Slownie.innerTrim(Slownie.doubleSlownie(groszy)) + " gr";
            }

            all = all.Replace("%WYNAGRODZENIE%", "" + umowa.Kwota);
            all = all.Replace("%SLOWNIE%", "" + slownie);

            all = all.Replace("%RODZAJ_PLATNOSCI%", umowa.RodzajPlatnosci);
            all = all.Replace("%POSTANOWIENIA_DODATKOWE%", umowa.PostanowieniaDodatkowe);

            return all;
        }
    }
}
