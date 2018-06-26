using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ITler_Ein_mal_Eins.Modules;
using ITler_Ein_mal_Eins.Model;
using System.Net;

namespace ITler_Ein_mal_Eins.Control
{
    class IpCalculator
    {
        public IpCalculator()
        {

        }

        #region IPv4 Digit Kontrolle

        #region IsIPv4Digit - Funktionen
        public static bool isIpV4Digit(System.Windows.Controls.TextBox box, bool isSubnet)
        {
            byte tmp = 0;
            // TryParse: Versucht, string in int zu wandeln und gibt aus, ob dies geklappt hat
            if(byte.TryParse(box.Text, out tmp))
            {
                if (isSubnet)
                {
                    return isLegitSubnetDigit(tmp);
                }
                else
                {
                    return true;
                }         
            }
            else
            {
                return false;
            }
        }

        public static bool isIpV4Digit(int number, bool isSubnet)
        {
            if(number < 0 || number > (int)((Math.Pow(2, 8) - 1)))
            {
                return false;
            }
            if (isSubnet)
            {
                return isLegitSubnetDigit((byte)number);
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region isLegitIpV4SubnetMask - Funktionen
        public static bool isLegitIpV4SubnetMask(TextBox first_txt, TextBox second_txt,
            TextBox third_txt, TextBox forth_txt)
        {
            if (isIpV4Digit(first_txt, true) && isIpV4Digit(second_txt, true) &&
                isIpV4Digit(third_txt, true) && isIpV4Digit(forth_txt, true))
            {
                byte first = byte.Parse(first_txt.Text);
                byte second = byte.Parse(second_txt.Text);
                byte third = byte.Parse(third_txt.Text);
                byte forth = byte.Parse(forth_txt.Text);
                if (first == 255)
                {
                    if (second == 255)
                    {
                        if (third == 255)
                        {
                            return true;
                        }
                        else
                        {
                            if (forth == 0)
                                return true;
                            return false;
                        }
                    }
                    else
                    {
                        if (third == 0 && forth == 0)
                            return true;
                        return false;
                    }
                }
                else
                {
                    if (second == 0 && third == 0 && forth == 0)
                        return true;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool isLegitIpV4SubnetMask(int first, int second, int third, int forth)
        {
            if(isIpV4Digit(first, true) && isIpV4Digit(second, true) &&
                isIpV4Digit(third, true) && isIpV4Digit(forth, true))
            {
                if(first == 255)
                {
                    if(second == 255)
                    {
                        if(third == 255)
                        {
                            return true;
                        }
                        else
                        {
                            if (forth == 0)
                                return true;
                            return false;
                        }
                    }
                    else
                    {
                        if (third == 0 && forth == 0)
                            return true;
                        return false;
                    }
                }
                else
                {
                    if (second == 0 && third == 0 && forth == 0)
                        return true;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool isLegitIpV4SubnetMask(TextBox box)
        {
            int shortDigit = 0;
            if(int.TryParse(box.Text, out shortDigit)) {
                if (shortDigit >= 1 && shortDigit <= 32) { return true; }
                return false;
            }
            else
            {
                return false;
            }
        }

        public static bool isLegitIpV4SubnetMask(int shortDigit)
        {
            if(shortDigit >= 1 && shortDigit <= 32) { return true; }
            return false;
        }

        #endregion

        #region calcEmptySubnetMaskFields

        /*
         * Funktion Berechnet die noch Fehlenden Felder der Subnetzmaske...
         * 
         * VALIDATION VORHER ABGESCHLOSSEN!!!
         * InterNetwork = IpV4 (Laut Google :D)
         */ 
        public static int[] calcEmptySubnetMaskFields(int first, int second, 
            int third, int forth, int shortField, IpV4_FieldStatus fieldStatus)
        {
            switch (fieldStatus)
            {
                case IpV4_FieldStatus.SHORTFILLED:
                    IPNetwork.ToNetmask((byte)shortField, System.Net.Sockets.AddressFamily.InterNetwork);
                    break;
                case IpV4_FieldStatus.LONGFILLED:

                    break;
                default:
                    throw new NotImplementedException();
            }
            return null;
        }

        #endregion

        private static bool isLegitSubnetDigit(byte digit)
        {
            byte[] subnetDigits = new byte[9] {0, 128, (128 + 64), (128 + 64 + 32), (128 + 64 + 32 + 16)
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
