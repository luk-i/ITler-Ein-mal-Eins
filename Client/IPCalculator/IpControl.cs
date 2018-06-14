﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITler_Ein_mal_Eins.SubnetCalculator
{
    class IpControl
    {
        public IpControl()
        {

        }

        #region IPv4 Digit Kontrolle

        #region IsIPv4Digit - Funktionen
        public bool isIpV4Digit(System.Windows.Controls.TextBox box, bool isSubnet)
        {
            byte tmp = 0;
            // TryParse: Versucht, string in int zu wandeln und gibt aus, ob dies geklappt hat
            if(byte.TryParse(box.Text, out tmp))
            {
                return isLegitSubnet(tmp);
            }
            else
            {
                return false;
            }
        }

        public bool isIpV4Digit(int number, bool isSubnet)
        {
            if(number < 0 || number > (int)((Math.Pow(2, 8) - 1)))
            {
                return false;
            }
            return true;
        }

        #endregion

        private bool isLegitSubnet(byte digit)
        {
            byte[] subnetDigits = new byte[8] { 128, (128 + 64), (128 + 64 + 32), (128 + 64 + 32 + 16)
                , (128 + 64 + 32 + 16 + 8), (128 + 64 + 32 + 16 + 8 + 4), (128 + 64 + 32 + 16 + 8 + 4 + 2),
                (128 + 64 + 32 + 16 + 8 + 4 + 2 + 1) };
            for(int i = 0; i < subnetDigits.Length; i++)
            {
                if(digit == subnetDigits[i])
                {
                    return true;
                }
            }
            return false;
        }

       

        #endregion

    }
}
