﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITler_Ein_mal_Eins.Control
{
    class UnitCalculatorControl
    {
        //Grundgerüst für das Aufrufen der Funktion zum Rechnen von Bits zu Bytes
        public bool calculateBits(string value, string name)
        {
            bool noError = true;
            try
            {

            }
            catch
            {
                //Hier sollten wir besser nicht landen!
                //Falls doch wird ein Popup in der Klasse UnitCalculator.xaml.cs generiert,
                //aus welcher diese Funktion aufgerufen wurde.
                noError = false;
            }
            return noError;
        }
    }
}
