using System;
using System.Collections.Generic;
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
            public string bi, kBi, mBi, gBi, tBi, by, kBy, mBy, gBy, tBy;
            public bool noError;
            public BitByteStrings(string bis, string kBis, string mBis, string gBis, string tBis, string bys, string kBys, string mBys, string gBys, string tBys, bool noErrors)
            {
                bi = bis;
                kBi = kBis;
                mBi = mBis;
                gBi = gBis;
                tBi = tBis;
                by = bys;
                kBy = kBys;
                mBy = mBys;
                gBy = gBys;
                tBy = tBys;
                noError = noErrors;
            }
        }
        
        //Grundgerüst für das Aufrufen der Funktion zum Rechnen von Bits zu Bytes
        public bool calculateBits(TextBox txbox_eingabe)
        {
            bool noError = true;
            try
            {
                Double tmp = calculateAnythingToBits(txbox_eingabe);
                
            }
            catch
            {
                //Hier sollten wir besser nicht landen!
                //Falls doch wird ein Popup in der Klasse UnitCalculator.xaml.cs generiert,
                //aus welcher diese Funktion aufgerufen wurde. (whs. Eingabe zu groß)
                noError = false;
            }
            return noError;
        }


        private Double calculateAnythingToBits(TextBox txbox_eingabe)
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
                    ausgabe = eingabe * 10000;
                    break;
                case "txbox_megabit":
                    ausgabe = eingabe * 10000000;
                    break;
                case "txbox_gigabit":
                    ausgabe = eingabe * 10000000000;
                    break;
                case "txbox_terabit":
                    ausgabe = eingabe * 10000000000000;
                    break;
                case "txbox_byte":
                    ausgabe = eingabe * 8;
                    break;
                case "txbox_kilobyte":
                    ausgabe = eingabe * 8 * 10000;
                    break;
                case "txbox_megabyte":
                    ausgabe = eingabe * 8 * 10000000;
                    break;
                case "txbox_gigabyte":
                    ausgabe = eingabe * 8 * 10000000000;
                    break;
                case "txbox_terabyte":
                    ausgabe = eingabe * 8 * 10000000000000;
                    break;
            }
            return ausgabe;
        }
    }
}

