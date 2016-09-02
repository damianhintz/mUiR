using System;
using System.Collections.Generic;
using System.Text;

namespace Rachunki
{
    class Slownie
    {
        public static string slownie_999(long n, long s)
        {
            long setki = (long)(n / 100);
            long n_d = (long)(n - setki * 100);
            long dziesiatki = (long)(n_d / 10);
            long n_j = (long)(n_d - dziesiatki * 10);
            long jedynki = (long)(n_j);

            string ile = "";
            if (dziesiatki == 1 && jedynki > 0)
                ile = slownie_11(jedynki);
            else
                ile = slownie_10(dziesiatki) + " " + slownie_1(jedynki);

            string typ = "";
            switch (s)
            {
                case 1000:
                    switch (jedynki)
                    {
                        case 1:
                            if (dziesiatki == 0 && setki == 0)
                                typ = "tysiąc";
                            else
                                typ = "tysięcy";
                            break;
                        case 2:
                        case 3:
                        case 4:
                            if (dziesiatki == 1)
                                typ = "tysięcy";
                            else
                                typ = "tysiące";
                            break;
                        default:
                            typ = "tysięcy";
                            break;
                    }
                    break;
                case 1000000:
                    switch (jedynki)
                    {
                        case 1:
                            if (dziesiatki == 0 && setki == 0)
                                typ = "milion";
                            else
                                typ = "milionów";
                            break;
                        case 2:
                        case 3:
                        case 4:
                            if (dziesiatki == 1)
                                typ = "milionów";
                            else
                                typ = "miliony";
                            break;
                        default:
                            typ = "milionów";
                            break;
                    }
                    break;
                case 1000000000:
                    switch (jedynki)
                    {
                        case 1:
                            if (dziesiatki == 0 && setki == 0)
                                typ = "miliard";
                            else
                                typ = "miliardów";
                            break;
                        case 2:
                        case 3:
                        case 4:
                            if (dziesiatki == 1)
                                typ = "miliardów";
                            else
                                typ = "miliardy";
                            break;
                        default:
                            typ = "miliardów";
                            break;
                    }
                    break;
                default:
                    break;
            }
            return string.Format("{0} {1} {2}", slownie_100(setki), ile, typ);
        }

        public static string doubleSlownie(decimal d)
        {
            long n = (long)Math.Round(d, 0, MidpointRounding.AwayFromZero);
            string slownie = "";

            long setki = (long)((n / 1) % 1000);
            long tysiace = (long)((n / 1000) % 1000);
            long miliony = (long)((n / 1000000) % 1000);
            long miliardy = (long)((n / 1000000000) % 1000);

            if (miliardy > 0)
            {
                slownie += slownie_999(miliardy, 1000000000) + " ";
            }
            if (miliony > 0)
            {
                slownie += slownie_999(miliony, 1000000) + " ";
            }
            if (tysiace > 0)
            {
                slownie += slownie_999(tysiace, 1000) + " ";
            }
            if (setki > 0)
            {
                slownie += slownie_999(setki, 100);
            }

            return slownie;
        }

        public static string slownie_1(long n)
        {
            switch (n)
            {
                case 0:
                    return "";
                case 1:
                    return "jeden";
                case 2:
                    return "dwa";
                case 3:
                    return "trzy";
                case 4:
                    return "cztery";
                case 5:
                    return "pięć";
                case 6:
                    return "sześć";
                case 7:
                    return "siedem";
                case 8:
                    return "osiem";
                case 9:
                    return "dziewięć";
                default:
                    return "";
            }
        }

        public static string slownie_11(long n)
        {
            switch (n)
            {
                case 0:
                    return "";
                case 1:
                    return "jedenaście";
                case 2:
                    return "dwanaście";
                case 3:
                    return "trzynaście";
                case 4:
                    return "czternaście";
                case 5:
                    return "piętnaście";
                case 6:
                    return "szesnaście";
                case 7:
                    return "siedemnaście";
                case 8:
                    return "osiemnaście";
                case 9:
                    return "dziewiętnaście";
                default:
                    return "";
            }
        }

        public static string slownie_10(long n)
        {
            switch (n)
            {
                case 0:
                    return "";
                case 1:
                    return "dziesięć";
                case 2:
                    return "dwadzieścia";
                case 3:
                    return "trzydzieści";
                case 4:
                    return "czterdzieści";
                case 5:
                    return "pięćdziesiąt";
                case 6:
                    return "sześćdziesiąt";
                case 7:
                    return "siedemdziesiąt";
                case 8:
                    return "osiemdziesiąt";
                case 9:
                    return "dziewięćdziesiąt";
                default:
                    return "";
            }
        }

        public static string slownie_100(long n)
        {
            switch (n)
            {
                case 0:
                    return "";
                case 1:
                    return "sto";
                case 2:
                    return "dwieście";
                case 3:
                    return "trzysta";
                case 4:
                    return "czterysta";
                case 5:
                    return "pięćset";
                case 6:
                    return "sześćset";
                case 7:
                    return "siedemset";
                case 8:
                    return "osiemset";
                case 9:
                    return "dziewięćset";
                default:
                    return "";
            }
        }

        public static string innerTrim(string s)
        {
            string n = "";
            char p = ' ';
            foreach (char c in s)
            {
                if (c != p)
                    n += c;
                p = c;
            }
            return n.Trim();
        }

    }
}
