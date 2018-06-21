using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ITler_Ein_mal_Eins.Modules;

namespace ITler_Ein_mal_Eins.Control
{
    class UnitCalculatorControl
    {
        //Struktur zum Speichern der Werte, bevor Ausgabe in anderer Klasse erfolgt
        public struct BitByteStrings
        {
            public string _bit, kiloBit, megaBit, gigaBit, teraBit, _byte, kiloByte, megaByte, gigaByte, teraByte;
            public bool noError;
            public BitByteStrings(string bis, string kBis, string mBis, string gBis, string tBis, string bys, string kBys, string mBys, string gBys, string tBys, bool noErrors)
            {
                _bit = bis;
                kiloBit = kBis;
                megaBit = mBis;
                gigaBit = gBis;
                teraBit = tBis;
                _byte = bys;
                kiloByte = kBys;
                megaByte = mBys;
                gigaByte = gBys;
                teraByte = tBys;
                noError = noErrors;
            }
        }

        BitByteStrings result;

        //Grundgerüst für das Aufrufen der Funktion zum Rechnen von Bits zu Bytes
        public BitByteStrings CalculateBits(TextBox txbox_eingabe)
        {
            result.noError = true;
            try
            {
                Double eingabeInBit = CalculateAnythingToBits(txbox_eingabe);
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


        private Double CalculateAnythingToBits(TextBox txbox_eingabe)
        {
            //Initiales Umrechnen des erhaltenen Wertes in Bits               
            Double eingabe = Convert.ToDouble(txbox_eingabe.Text);
            Double ausgabe = 0;
            switch (txbox_eingabe.Name)
            {
                case "txbox_bit":
                    ausgabe = eingabe;//Wert bereits in Einheit Bits
                    break;
                case "txbox_kilobit":
                    ausgabe = eingabe * 1024;
                    break;
                case "txbox_megabit":
                    ausgabe = eingabe * 1024 * 1024;
                    break;
                case "txbox_gigabit":
                    ausgabe = eingabe * 1024 * 1024 * 1024;
                    break;
                case "txbox_terabit":
                    ausgabe = eingabe * 1024 * 1024 * 1024 * 1024;
                    break;
                case "txbox_byte":
                    ausgabe = eingabe * 8;
                    break;
                case "txbox_kilobyte":
                    ausgabe = eingabe * 8 * 1024;
                    break;
                case "txbox_megabyte":
                    ausgabe = eingabe * 8 * 1024 * 1024;
                    break;
                case "txbox_gigabyte":
                    ausgabe = eingabe * 8 * 1024 * 1024 * 1024;
                    break;
                case "txbox_terabyte":
                    ausgabe = eingabe * 8 * 1024 * 1024 * 1024 * 1024;
                    break;
            }
            return ausgabe;            
        }

        private void FillResults(Double eingabeInBit)
        {
            UInt64 b = 8;       // Die Variablen sollen die Rechnung übersichtlicher gestalten,
            UInt64 k = 1024;    // sowie vor einem Überlauf des Standard schützen
            CultureInfo ci  =   new CultureInfo("de-DE");
            result._bit     =   (eingabeInBit.ToString("N", ci));
            result.kiloBit  =   (eingabeInBit /  k).ToString("N", ci);
            result.megaBit  =   (eingabeInBit / (k * k)).ToString("N", ci);
            result.gigaBit  =   (eingabeInBit / (k * k * k)).ToString("N", ci);
            result.teraBit  =   (eingabeInBit / (k * k * k * k)).ToString("N", ci);       
            result._byte    =   (eingabeInBit /  b).ToString("N", ci);
            result.kiloByte =   (eingabeInBit / (b * k)).ToString("N", ci);
            result.megaByte =   (eingabeInBit / (b * k * k)).ToString("N", ci);
            result.gigaByte =   (eingabeInBit / (b * k * k * k)).ToString("N", ci);           
            result.teraByte =   (eingabeInBit / (b * k * k * k * k)).ToString("N", ci);  
        }
    }
}

