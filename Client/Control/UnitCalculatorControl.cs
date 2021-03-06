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

    class UnitCalculatorControl
    {
        #region Allgemein ###################################################################################################################################################################################

        //Umwandeln von Komma in Punkt und Entfernen von Vorzeichen
        //BigFloat kommt mit Vorzeichen leider nicht richtig klar
        public string WorkString(string workString)
        {
            workString = workString.Replace('.', ',');
            workString = workString.Trim('-', '+');
            return workString;
        }

        public string EraseFollowingZeroes(string withFollowingZeroes)
        {
            string zeroesErased;
            char[] charsToTrim = { '0' };
            zeroesErased = withFollowingZeroes.TrimEnd(charsToTrim);
            zeroesErased = zeroesErased + '0';
            return zeroesErased;
        }

        #endregion ##########################################################################################################################################################################################

        #region Bits zu Bytes ###############################################################################################################################################################################
        //Struktur zum Speichern der Werte des Bit Byte Rechners, bevor Ausgabe in anderer Klasse erfolgt
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
        //Erstellen eine Struktur zum Abspeichern der Ergebnisse
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
                WasTheInputSigned_Bits(wasInputSigned);
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
            string workString = WorkString(txbox_eingabe.Text);
            decimal eingabe = Convert.ToDecimal(workString);
            decimal ausgabe = 0;
            decimal b = 8;       // Die Variablen sollen die Rechnung übersichtlicher gestalten,
            decimal k = 1024;    // sowie vor einem Überlauf des Standard schützen
            switch (txbox_eingabe.Name)
            {
                case "txbox_bit":       ausgabe = eingabe; break; //Wert bereits in Einheit Bits
                case "txbox_kilobit":   ausgabe = eingabe * k; break;
                case "txbox_megabit":   ausgabe = eingabe * k * k; break;
                case "txbox_gigabit":   ausgabe = eingabe * k * k * k; break;
                case "txbox_terabit":   ausgabe = eingabe * k * k * k * k; break;

                case "txbox_byte":      ausgabe = eingabe * b; break;
                case "txbox_kilobyte":  ausgabe = eingabe * b * k; break;
                case "txbox_megabyte":  ausgabe = eingabe * b * k * k; break;
                case "txbox_gigabyte":  ausgabe = eingabe * b * k * k * k; break;
                case "txbox_terabyte":  ausgabe = eingabe * b * k * k * k * k; break;
            }
            return ausgabe;
        }

        private void FillResults_Bits(decimal eingabeInBit)
        {
            decimal b = 8;       // Die Variablen sollen die Rechnung übersichtlicher gestalten,
            decimal k = 1024;    // sowie vor einem Überlauf des Standard schützen

            bitByteStrings._bit = EraseFollowingZeroes((eingabeInBit.ToString("F99")));

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

        #endregion ##########################################################################################################################################################################################

        #region Zahlensysteme-Rechner #######################################################################################################################################################################

        //Struktur zum Speichern der Wertedes Zahlensystem Rechners, bevor Ausgabe in anderer Klasse erfolgt
        public struct Systems
        {
            public string binaer, oktal, dezimal, hexadezimal;
            public bool noError;
            public string comboBox_dataType;
            public Systems(string bin, string okt, string dez, string hex, bool noErrors, string cb_data)
            {
                binaer              = bin;
                oktal               = okt;
                dezimal             = dez;
                hexadezimal         = hex;
                noError             = noErrors;
                comboBox_dataType   = cb_data;
            }
        }
        //Erstellen eine Struktur zum Abspeichern der Ergebnisse
        Systems systems;

        //Grundgerüst für das Aufrufen der Funktion zum Umrechnen von Zahlensystemen
        public Systems CalculateSystems(TextBox txbox_eingabe, string dataType)
        {
            systems.noError = true;
            try
            {
                switch (dataType)
                {
                    case "Int16":
                        Int16 inputInDecimal_Int16 = CalculateAnythingToDecimal_Int16(txbox_eingabe);
                        FillResults_Decimal(inputInDecimal_Int16);
                        break;
                    case "Int32":
                        Int32 inputInDecimal_Int32 = CalculateAnythingToDecimal_Int32(txbox_eingabe);
                        FillResults_Decimal(inputInDecimal_Int32);
                        break;
                    case "Int64":
                        Int64 inputInDecimal_Int64 = CalculateAnythingToDecimal_Int64(txbox_eingabe);
                        FillResults_Decimal(inputInDecimal_Int64);
                        break;
                }               
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

        #region Berechnung Int16 ############################################################################################################################################################################
        private Int16 CalculateAnythingToDecimal_Int16(TextBox txbox_eingabe)
        {
            //Initiales Umrechnen des erhaltenen Wertes in Dezimal              
            string eingabe = txbox_eingabe.Text;
            Int16 ausgabe = 0;
            switch (txbox_eingabe.Name)
            {
                case "txbox_binaer":        ausgabe = Convert.ToInt16(eingabe, 2); break;
                case "txbox_oktal":         ausgabe = Convert.ToInt16(eingabe, 8); break;
                case "txbox_dezimal":       ausgabe = Convert.ToInt16(eingabe, 10); break;
                case "txbox_hexadezimal":   ausgabe = Convert.ToInt16(eingabe, 16); break;
            }
            return ausgabe;
        }
        private void FillResults_Decimal(Int16 eingabeInDecimal)
        {
            systems.binaer      = Convert.ToString(eingabeInDecimal, 2);
            systems.oktal       = Convert.ToString(eingabeInDecimal, 8);
            systems.dezimal     = Convert.ToString(eingabeInDecimal, 10);
            systems.hexadezimal = Convert.ToString(eingabeInDecimal, 16);
        }
        #endregion ##########################################################################################################################################################################################

        #region Berechnung Int32 ############################################################################################################################################################################
        private Int32 CalculateAnythingToDecimal_Int32(TextBox txbox_eingabe)
        {
            //Initiales Umrechnen des erhaltenen Wertes in Dezimal              
            string eingabe = txbox_eingabe.Text;
            Int32 ausgabe = 0;
            switch (txbox_eingabe.Name)
            {
                case "txbox_binaer":        ausgabe = Convert.ToInt32(eingabe, 2); break;
                case "txbox_oktal":         ausgabe = Convert.ToInt32(eingabe, 8); break;
                case "txbox_dezimal":       ausgabe = Convert.ToInt32(eingabe, 10); break;
                case "txbox_hexadezimal":   ausgabe = Convert.ToInt32(eingabe, 16); break;
            }
            return ausgabe;
        }
        private void FillResults_Decimal(Int32 eingabeInDecimal)
        {
            systems.binaer      = Convert.ToString(eingabeInDecimal, 2);
            systems.oktal       = Convert.ToString(eingabeInDecimal, 8);
            systems.dezimal     = Convert.ToString(eingabeInDecimal, 10);
            systems.hexadezimal = Convert.ToString(eingabeInDecimal, 16);
        }
        #endregion ##########################################################################################################################################################################################

        #region Berechnung Int64 ############################################################################################################################################################################
        private Int64 CalculateAnythingToDecimal_Int64(TextBox txbox_eingabe)
        {
            //Initiales Umrechnen des erhaltenen Wertes in Dezimal              
            string eingabe = txbox_eingabe.Text;
            Int64 ausgabe = 0;
            switch (txbox_eingabe.Name)
            {
                case "txbox_binaer":        ausgabe = Convert.ToInt64(eingabe, 2); break;
                case "txbox_oktal":         ausgabe = Convert.ToInt64(eingabe, 8); break;
                case "txbox_dezimal":       ausgabe = Convert.ToInt64(eingabe, 10); break;
                case "txbox_hexadezimal":   ausgabe = Convert.ToInt64(eingabe, 16); break;
            }
            return ausgabe;
        }
        private void FillResults_Decimal(Int64 eingabeInDecimal)
        {
            systems.binaer      = Convert.ToString(eingabeInDecimal, 2);
            systems.oktal       = Convert.ToString(eingabeInDecimal, 8);
            systems.dezimal     = Convert.ToString(eingabeInDecimal, 10);
            systems.hexadezimal = Convert.ToString(eingabeInDecimal, 16);
        }
        #endregion ##########################################################################################################################################################################################

        #endregion ##########################################################################################################################################################################################
    }
}

