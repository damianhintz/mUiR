using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace muir.Model
{
    public interface Interfejs : ICloneable
    {
        string Nazwa
        {
            get;
            set;
        }

        string Opis
        {
            get;
            set;
        }

        string Typ
        {
            get;
            //set;
        }

        void Zachowaj();
        void Przywroc();
        EdytorObiektu get_Edytor();
        string get_Info();
        int CompareTo(object obj);
    }
}
