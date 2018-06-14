using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITler_Ein_mal_Eins.SubnetCalculator
{
    class SubnetControl
    {
        public SubnetControl()
        {

        }

        #region IPv4 Digit Kontrolle

        #region IsIPv4Digit - Funktionen
        public bool isIpV4Digit(System.Windows.Controls.TextBox box, int blockSize, bool isSubnet)
        {
            int tmp = 0;
            // TryParse: Versucht, string in int zu wandeln und gibt aus, ob dies geklappt hat
            if(int.TryParse(box.Text, out tmp)){
                if (tmp < 0 || tmp > (int)((Math.Pow(2, blockSize) - 1)))
                {
                    return false;
                }
            }
            return true;
        }

        public bool isIpV4Digit(int number, int blockSize, bool isSubnet)
        {
            if(number < 0 || number > (int)((Math.Pow(2, blockSize) - 1)))
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
