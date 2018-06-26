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
        #region Allgemein

        //Umwandeln von Komma in Punkt und Entfernen von Vorzeichen
        //BigFloat kommt mit Vorzeichen leider nicht richtig klar
        public string WorkString (string workString)
        {
            workString = workString.Replace('.', ',');
            workString = workString.Trim('-','+');
            return workString;
        }

        public string EraseFollowingZeroes (string withFollowingZeroes)
        {
            string zeroesErased;
            char[] charsToTrim = {'0'};
            zeroesErased = withFollowingZeroes.TrimEnd(charsToTrim);
            zeroesErased = zeroesErased + '0';
            return zeroesErased;
        }

        #endregion

        #region Bits zu Bytes
        //Struktur zum Speichern der Werte des Bit Byte Rechners, bevor Ausgabe in anderer Klasse erfolgt
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
        //Instanz der Struktur erstellen
        BitByteStrings bitByteStrings;

        //Grundgerüst für das Aufrufen der Funktion zum Rechnen von Bits zu Bytes
        public BitByteStrings CalculateBits(TextBox txbox_eingabe)
        {
            bitByteStrings.noError = true;
            try
            {
                decimal eingabeInBit = CalculateAnythingToBits(txbox_eingabe);
                FillResults_Bits(eingabeInBit);
                decimal wasInputSigned = decimal.Parse(txbox_eingabe.Text);
                WasTheInputSigned_Bits (wasInputSigned);
            }
            catch
            {
                //Hier sollten wir besser nicht landen!
                //Falls doch wird ein Popup in der Klasse UnitCalculator.xaml.cs generiert,
                //aus welcher diese Funktion aufgerufen wurde. (whs. Eingabe zu groß)
                bitByteStrings.noError = false;
            }
            return bitByteStrings;
        }


        private decimal CalculateAnythingToBits(TextBox txbox_eingabe)
        {
            //Initiales Umrechnen des erhaltenen Wertes in Bits                         
            string workString = WorkString (txbox_eingabe.Text);
            decimal eingabe = Convert.ToDecimal(workString);
            decimal ausgabe = 0;
            decimal b = 8;       // Die Variablen sollen die Rechnung übersichtlicher gestalten,
            decimal k = 1024;    // sowie vor einem Überlauf des Standard schützen
                switch (txbox_eingabe.Name)
                {
                    case "txbox_bit": ausgabe = eingabe; break; //Wert bereits in Einheit Bits
                    case "txbox_kilobit": ausgabe = eingabe * k; break;
                    case "txbox_megabit": ausgabe = eingabe * k * k; break;
                    case "txbox_gigabit": ausgabe = eingabe * k * k * k; break;
                    case "txbox_terabit": ausgabe = eingabe * k * k * k * k; break;

                    case "txbox_byte": ausgabe = eingabe * b; break;
                    case "txbox_kilobyte": ausgabe = eingabe * b * k; break;
                    case "txbox_megabyte": ausgabe = eingabe * b * k * k; break;
                    case "txbox_gigabyte": ausgabe = eingabe * b * k * k * k; break;
                    case "txbox_terabyte": ausgabe = eingabe * b * k * k * k * k; break;
                }
                return ausgabe;
        }

        private void FillResults_Bits(decimal eingabeInBit)
        {
            decimal b = 8;       // Die Variablen sollen die Rechnung übersichtlicher gestalten,
            decimal k = 1024;    // sowie vor einem Überlauf des Standard schützen

            bitByteStrings._bit     = EraseFollowingZeroes((eingabeInBit.ToString("F99")));
            
            bitByteStrings.kiloBit  = EraseFollowingZeroes((eingabeInBit / (k)).ToString("F99"));
            bitByteStrings.megaBit  = EraseFollowingZeroes((eingabeInBit / (k * k)).ToString("F99"));
            bitByteStrings.gigaBit  = EraseFollowingZeroes((eingabeInBit / (k * k * k)).ToString("F99"));
            bitByteStrings.teraBit  = EraseFollowingZeroes((eingabeInBit / (k * k * k * k)).ToString("F99"));

            bitByteStrings._byte    = EraseFollowingZeroes((eingabeInBit / (b)).ToString("F99"));
            bitByteStrings.kiloByte = EraseFollowingZeroes((eingabeInBit / (b * k)).ToString("F99"));
            bitByteStrings.megaByte = EraseFollowingZeroes((eingabeInBit / (b * k * k)).ToString("F99"));
            bitByteStrings.gigaByte = EraseFollowingZeroes((eingabeInBit / (b * k * k * k)).ToString("F99"));
            bitByteStrings.teraByte = EraseFollowingZeroes((eingabeInBit / (b * k * k * k * k)).ToString("F99"));           
        }

        public void WasTheInputSigned_Bits(decimal input)
        {
            if (input < 0)
            {
                bitByteStrings._bit     = "-" + bitByteStrings._bit;
                bitByteStrings.kiloBit  = "-" + bitByteStrings.kiloBit;
                bitByteStrings.megaBit  = "-" + bitByteStrings.megaBit;
                bitByteStrings.gigaBit  = "-" + bitByteStrings.gigaBit;
                bitByteStrings.teraBit  = "-" + bitByteStrings.teraBit;
                bitByteStrings._byte    = "-" + bitByteStrings._byte;
                bitByteStrings.kiloByte = "-" + bitByteStrings.kiloByte;
                bitByteStrings.megaByte = "-" + bitByteStrings.megaByte;
                bitByteStrings.gigaByte = "-" + bitByteStrings.gigaByte;
                bitByteStrings.teraByte = "-" + bitByteStrings.teraByte;
            }
        }

        #endregion

        #region Einheitenrechner

        //Struktur zum Speichern der Wertedes Zahlensystem Rechners, bevor Ausgabe in anderer Klasse erfolgt
        public struct Systems
        {
            public string binaer, oktal, dezimal, hexadezimal;
            public bool noError;
            public Systems(string bin, string okt, string dez, string hex, bool noErrors)
            {
                binaer = bin;
                oktal = okt;
                dezimal = dez;
                hexadezimal = hex;
                noError = noErrors;
            }
        }
        //Erstellen eine Instanz zum Abspeichern der Ergebnisse
        Systems systems;

        //Grundgerüst für das Aufrufen der Funktion zum Umrechnen von Zahlensystemen
        public Systems CalculateSystems(TextBox txbox_eingabe)
        {
            systems.noError = true;
            try
            {
                string eingabeInBinary = CalculateAnythingToDecimal(txbox_eingabe);
                FillResults_Decimal(eingabeInBinary);
            }
            catch
            {
                //Hier sollten wir besser nicht landen!
                //Falls doch wird ein Popup in der Klasse UnitCalculator.xaml.cs generiert,
                //aus welcher diese Funktion aufgerufen wurde. (whs. Eingabe zu groß)
                systems.noError = false;
            }
            return systems;
        }

        private string CalculateAnythingToDecimal(TextBox txbox_eingabe)
        {
            //Initiales Umrechnen des erhaltenen Wertes in Dezimal              
            string eingabe = txbox_eingabe.Text;
            string ausgabe = null;
            switch (txbox_eingabe.Name)
            {
                case "txbox_binaer": ausgabe = StringUmrechnen(eingabe, 2); break; 
                case "txbox_oktal": ausgabe = StringUmrechnen(eingabe, 8); ; break; 
                case "txbox_dezimal": ausgabe = eingabe; break; //Wert bereits in Dezimal
                case "txbox_hexadezimal": ausgabe = StringUmrechnen(eingabe, 16); ; break; 
            }
            return ausgabe;
        }

        private string StringUmrechnen (string eingabe, int system)
        {
            string ergebnis = null;



            return ergebnis;
        }

        private void FillResults_Decimal(string eingabeInDecimal)
        {
            systems.binaer = (eingabeInDecimal.ToString());
            systems.oktal = (eingabeInDecimal.ToString());
            systems.dezimal = (eingabeInDecimal.ToString());
            systems.hexadezimal = (eingabeInDecimal.ToString());
        }

        #endregion
    }
}

