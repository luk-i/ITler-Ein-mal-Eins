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

        #region Is IPv4 Digit
        public bool isIpV4Digit(System.Windows.Controls.TextBox box, int blockSize)
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

        public bool isIpV4Digit(int number, int blockSize)
        {
            if(number < 0 || number > (int)((Math.Pow(2, blockSize) - 1)))
            {
                return false;
            }
            return true;
        }

        #endregion

    }
}
