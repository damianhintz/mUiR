using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Rachunki
{
    enum OfficeApp
    {
        Word,
        Excel
    }

    enum OfficeVer
    {
        Office2000 = 9,
        Office2002 = 10,
        Office2003 = 11,
        Office2007 = 12
    }

    class OfficeWraper
    {
        protected static object ObjMissing = Missing.Value;

        #region Variables

        //public static object ObjMissing = Missing.Value;
        public static object ObjFalse = false;
        public static object ObjTrue = true;
        //public static UtilOffice ObjOffice;

        #endregion

        /// <summary>
        /// Sprawdü, czy jest zainstalowany Microsoft Office
        /// </summary>
        /// <param name="app"></param>
        /// <param name="ver"></param>
        /// <returns></returns>
        /// <example></example>
        public static bool IsInstalled(OfficeApp app, OfficeVer ver)
        {
            string userRoot = "HKEY_LOCAL_MACHINE";
            string verName = GetVersionName(ver);
            string appName = GetApplicationName(app);

            string subkey =
                string.Format(@"Software\Microsoft\Office\{0}\{1}\InstallRoot",
                    verName, appName);

            string keyName = userRoot + "\\" + subkey;

            return (string)Registry.GetValue(keyName, "Path", null) != null;
        }

        private static string GetVersionName(OfficeVer ver)
        {
            return (int)ver + ".0";
        }

        private static string GetApplicationName(OfficeApp app)
        {
            return Enum.GetName(typeof(OfficeApp), app);
        }

        public static string GetInstalledWord()
        {
            if (IsInstalled(OfficeApp.Word, OfficeVer.Office2000))
            {
                return "Microsoft Word 2000";
            }
            else if (IsInstalled(OfficeApp.Word, OfficeVer.Office2002))
                return "Microsoft Word 2002";
            else if (IsInstalled(OfficeApp.Word, OfficeVer.Office2003))
                return "Microsoft Word 2003";
            else if (IsInstalled(OfficeApp.Word, OfficeVer.Office2007))
                return "Microsoft Word 2007";
            else
                return "Microsoft Word (brak instalacji lub nieznana wersja)";
        }

        public static string GetInstalledExcel()
        {
            if (IsInstalled(OfficeApp.Excel, OfficeVer.Office2000))
            {
                return "Microsoft Excel 2000";
            }
            else if (IsInstalled(OfficeApp.Excel, OfficeVer.Office2002))
                return "Microsoft Excel 2002";
            else if (IsInstalled(OfficeApp.Excel, OfficeVer.Office2003))
                return "Microsoft Excel 2003";
            else if (IsInstalled(OfficeApp.Excel, OfficeVer.Office2007))
                return "Microsoft Excel 2007";
            else
                return "Microsoft Excel (brak instalacji lub nieznana wersja)";
        }
    }
}
