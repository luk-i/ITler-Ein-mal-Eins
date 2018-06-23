﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ITler_Ein_mal_Eins.Modules;

namespace ITler_Ein_mal_Eins.Control
{
    #region Bits zu Bytes
    class UnitCalculatorControl
    {
        //Struktur zum Speichern der Werte, bevor Ausgabe in anderer Klasse erfolgt
        public struct BitByteStrings
        {
            public string _bit, kiloBit, megaBit, gigaBit, teraBit, _byte, kiloByte, megaByte, gigaByte, teraByte;
            public bool noError;
            public BitByteStrings(string bis, string kBis, string mBis, string gBis, string tBis, string bys, string kBys, string mBys, string gBys, string tBys, bool noErrors)
            {
                _bit        = bis;
                kiloBit     = kBis;
                megaBit     = mBis;
                gigaBit     = gBis;
                teraBit     = tBis;
                _byte       = bys;
                kiloByte    = kBys;
                megaByte    = mBys;
                gigaByte    = gBys;
                teraByte    = tBys;
                noError     = noErrors;
            }
        }
        //Instanz der Struktur erstellen
        BitByteStrings result;

        //Grundgerüst für das Aufrufen der Funktion zum Rechnen von Bits zu Bytes
        public BitByteStrings CalculateBits(TextBox txbox_eingabe)
        {
            result.noError = true;
            try
            {
                BigFloat eingabeInBit = CalculateAnythingToBits(txbox_eingabe);
                FillResults(eingabeInBit);              
            }
            catch
            {
                //Hier sollten wir besser nicht landen!
                //Falls doch wird ein Popup in der Klasse UnitCalculator.xaml.cs generiert,
                //aus welcher diese Funktion aufgerufen wurde. (whs. Eingabe zu groß)
                result.noError = false;
            }
            return result;
        }


        private BigFloat CalculateAnythingToBits(TextBox txbox_eingabe)
        {
            //Initiales Umrechnen des erhaltenen Wertes in Bits               
            BigFloat eingabe = BigFloat.Parse(txbox_eingabe.Text);
            BigFloat ausgabe = 0;
            BigFloat b = 8;       // Die Variablen sollen die Rechnung übersichtlicher gestalten,
            BigFloat k = 1024;    // sowie vor einem Überlauf des Standard schützen
            switch (txbox_eingabe.Name)
            {
                case "txbox_bit":       ausgabe = eingabe;                      break; //Wert bereits in Einheit Bits
                case "txbox_kilobit":   ausgabe = eingabe * k;                  break;
                case "txbox_megabit":   ausgabe = eingabe * k * k;              break;
                case "txbox_gigabit":   ausgabe = eingabe * k * k * k;          break;
                case "txbox_terabit":   ausgabe = eingabe * k * k * k * k;      break;

                case "txbox_byte":      ausgabe = eingabe * b;                  break;
                case "txbox_kilobyte":  ausgabe = eingabe * b * k;              break;
                case "txbox_megabyte":  ausgabe = eingabe * b * k * k;          break;
                case "txbox_gigabyte":  ausgabe = eingabe * b * k * k * k;      break;
                case "txbox_terabyte":  ausgabe = eingabe * b * k * k * k * k;  break;
            }
            return ausgabe;            
        }

        private void FillResults(BigFloat eingabeInBit)
        {
            BigFloat b      =   8;       // Die Variablen sollen die Rechnung übersichtlicher gestalten,
            BigFloat k      =   1024;    // sowie vor einem Überlauf des Standard schützen
            result._bit     =   (eingabeInBit.ToString());
            result.kiloBit  =   (eingabeInBit /  k).ToString();
            result.megaBit  =   (eingabeInBit / (k * k)).ToString();
            result.gigaBit  =   (eingabeInBit / (k * k * k)).ToString();
            result.teraBit  =   (eingabeInBit / (k * k * k * k)).ToString();       
            result._byte    =   (eingabeInBit /  b).ToString();
            result.kiloByte =   (eingabeInBit / (b * k)).ToString();
            result.megaByte =   (eingabeInBit / (b * k * k)).ToString();
            result.gigaByte =   (eingabeInBit / (b * k * k * k)).ToString();           
            result.teraByte =   (eingabeInBit / (b * k * k * k * k)).ToString();  
        }
    }
    #endregion

    #region Einheitenrechner

    #endregion
}

