﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace genscript
{
    class Common
    {
        public static int rows = 8;
        public static int cols = 12;
        public static int GetWellID(int rowIndex, int colIndex)
        {
            return colIndex * 8 + rowIndex + 1;
        }

        public static bool Mix2Plate
        {
            get
            {
                bool mix2PlateFlag = ConfigurationManager.AppSettings.AllKeys.Contains("Mix2Plate");
                if(!mix2PlateFlag)
                    return false;
                return bool.Parse(ConfigurationManager.AppSettings["Mix2Plate"]);
            }
        }

        public static int PlateCnt
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["plateCnt"]);
            }
        }

        public static string GetPlateName(string sCSVFile)
        {
            int pos = sCSVFile.LastIndexOf("\\");
            string sName = sCSVFile.Substring(pos + 1);
            pos = sName.IndexOf("_");
            sName = sName.Substring(0, pos);
            return sName;
        }

        public static string GetWellDesc(int wellID)
        {
            int colIndex = (wellID - 1) / 8;
            int rowIndex = wellID - colIndex * 8 - 1;
            return string.Format("{0}{1}", (char)('A' + rowIndex), colIndex + 1);
        }
        public static string FormatWellID(int wellID)
        {
            return string.Format("{0:D3}", wellID);
        }
        public static int GetWellID(string sWell)
        {
            int rowIndex = sWell.First() - 'A';
            int colIndex = int.Parse(sWell.Substring(1))- 1;
            return GetWellID(rowIndex, colIndex);
        }

        internal static bool IsInvalidWellID(string s)
        {
            if (s.Length > 3)
                return true;
            int wellID = -1;
            try
            {
                wellID = GetWellID(s);
            }
            catch(Exception ex)
            {
                return true;
            }
            return wellID < 0 || wellID > 96;
        }

        
    }
}
