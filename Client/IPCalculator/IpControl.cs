using System;
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
                return true;
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

        private bool isLegitSubnet()
        {

            return false;
        }

       

        #endregion

    }
}
