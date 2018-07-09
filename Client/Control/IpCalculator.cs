using System;
using System.Windows.Controls;
using ITler_Ein_mal_Eins.Model;
using System.Net;
using System.Windows.Media;

namespace ITler_Ein_mal_Eins.Control
{
    class IpCalculator
    {
        public IpCalculator()
        {

        }

        #region IPv4 Digit Kontrolle

        #region IsIPv4Digit
        public static bool isIpV4Digit(System.Windows.Controls.TextBox box, bool isSubnet)
        {
            byte tmp = 0;
            // TryParse: Versucht, string in int zu wandeln und gibt aus, ob dies geklappt hat
            if (byte.TryParse(box.Text, out tmp))
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
            if (number < 0 || number > (int)((Math.Pow(2, 8) - 1)))
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

        #region isLegitIpV4SubnetMask
        public static bool isLegitIpV4SubnetMask(IPAddressTextboxes subnetMask)
        {
            if (isIpV4Digit(subnetMask.first, true) && isIpV4Digit(subnetMask.second, true) &&
                isIpV4Digit(subnetMask.third, true) && isIpV4Digit(subnetMask.forth, true))
            {
                byte first = byte.Parse(subnetMask.first.Text);
                byte second = byte.Parse(subnetMask.second.Text);
                byte third = byte.Parse(subnetMask.third.Text);
                byte forth = byte.Parse(subnetMask.forth.Text);
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
            if (isIpV4Digit(first, true) && isIpV4Digit(second, true) &&
                isIpV4Digit(third, true) && isIpV4Digit(forth, true))
            {
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

        public static bool isLegitIpV4SubnetMask(TextBox box)
        {
            int shortDigit = 0;
            if (int.TryParse(box.Text, out shortDigit))
            {
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
            if (shortDigit >= 1 && shortDigit <= 32) { return true; }
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
        public static byte[] calcEmptySubnetMaskFields(int shortField)
        {
            IPAddress address = IPNetwork.ToNetmask((byte)shortField, System.Net.Sockets.AddressFamily.InterNetwork);
            return address.GetAddressBytes();
        }

        public static byte[] calcEmptySubnetMaskFields(TextBox shortField)
        {
            byte tmp;
            byte.TryParse(shortField.Text, out tmp);
            IPAddress address = IPNetwork.ToNetmask(tmp, System.Net.Sockets.AddressFamily.InterNetwork);
            return address.GetAddressBytes();
        }

        public static byte[] calcEmptySubnetMaskFields(TextBox b_first, TextBox b_second, TextBox b_third, TextBox b_forth)
        {
            byte first = 0;
            byte second = 0;
            byte third = 0;
            byte forth = 0;
            byte.TryParse(b_first.Text, out first);
            byte.TryParse(b_second.Text, out second);
            byte.TryParse(b_third.Text, out third);
            byte.TryParse(b_forth.Text, out forth);
            byte[] result = new byte[1];
            byte[] ip = new byte[4] { first, second, third, forth };
            IPAddress address = new IPAddress(ip);
            byte tmp = IPNetwork.ToCidr(address);
            result[0] = tmp;
            return result;
        }

        public static byte[] calcEmptySubnetMaskFields(int first, int second, int third, int forth)
        {
            byte[] result = new byte[1];
            byte[] ip = new byte[4] { (byte)first, (byte)second, (byte)third, (byte)forth };
            IPAddress address = new IPAddress(ip);
            byte tmp = IPNetwork.ToCidr(address);
            result[0] = tmp;
            return result;
        }

        #endregion

        #region getFieldStatus
        public static IpV4_FieldStatus getFieldStatus(IPAddressTextboxes subnet_long, IPAddressTextboxes subnet_shortwritten)
        {
            bool shortFieldFilled = false;
            bool longFieldFilled = false;

            if (subnet_shortwritten.first.Text != "") { shortFieldFilled = true; }
            if (subnet_long.first.Text != "" ||
                subnet_long.second.Text != "" ||
                subnet_long.third.Text != "" ||
                subnet_long.forth.Text != "") { longFieldFilled = true; }

            if (shortFieldFilled == true && longFieldFilled == false) { return IpV4_FieldStatus.SHORTFILLED; }
            if (shortFieldFilled == false && longFieldFilled == true) { return IpV4_FieldStatus.LONGFILLED; }
            if (shortFieldFilled == false && longFieldFilled == false) { return IpV4_FieldStatus.NOFIELDSFILLED; };
            return IpV4_FieldStatus.BOTHFILLED;
        }

        #endregion

        #region getMaxSubnetNo

        public static int getMaxSubnetNo(int subnet_short)
        {

            return 0;
        }

        #endregion

        #region Textbox Control

        public static IPAddressTextboxes Textboxes_Enabled(IPAddressTextboxes textboxes)
        {
            IPAddressTextboxes tmp = textboxes;
            if (tmp.type == Textbox_FieldType.IP_ADDRESSBLOCK || tmp.type == Textbox_FieldType.NEW_SUBNETMASK_LONG ||
                tmp.type == Textbox_FieldType.SUBNETMASK_LONG)
            {
                tmp.first.IsReadOnly = false;
                tmp.second.IsReadOnly = false;
                tmp.third.IsReadOnly = false;
                tmp.forth.IsReadOnly = false;
                return tmp;
            }
            else if (tmp.type == Textbox_FieldType.DESIRED_HOSTNO || tmp.type == Textbox_FieldType.DESIRED_SUBNETNO
               || tmp.type == Textbox_FieldType.NEW_SUBNETMASK_SHORT || tmp.type == Textbox_FieldType.SUBNETMASK_SHORT)
            {
                tmp.first.IsReadOnly = false;
                return tmp;
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        public static IPAddressTextboxes Textboxes_Disabled(IPAddressTextboxes textboxes)
        {
            IPAddressTextboxes tmp = textboxes;
            if (tmp.type == Textbox_FieldType.IP_ADDRESSBLOCK || tmp.type == Textbox_FieldType.NEW_SUBNETMASK_LONG ||
                tmp.type == Textbox_FieldType.SUBNETMASK_LONG)
            {
                tmp.first.IsReadOnly = true;
                tmp.second.IsReadOnly = true;
                tmp.third.IsReadOnly = true;
                tmp.forth.IsReadOnly = true;
                return tmp;
            }
            else if (tmp.type == Textbox_FieldType.DESIRED_HOSTNO || tmp.type == Textbox_FieldType.DESIRED_SUBNETNO
               || tmp.type == Textbox_FieldType.NEW_SUBNETMASK_SHORT || tmp.type == Textbox_FieldType.SUBNETMASK_SHORT)
            {
                tmp.first.IsReadOnly = true;
                return tmp;
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        public static IPAddressTextboxes brushTextboxes(string brush_string, IPAddressTextboxes textboxes)
        {
            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString(brush_string);
            IPAddressTextboxes tmp = textboxes;
            if (tmp.type == Textbox_FieldType.IP_ADDRESSBLOCK || tmp.type == Textbox_FieldType.NEW_SUBNETMASK_LONG ||
                tmp.type == Textbox_FieldType.SUBNETMASK_LONG)
            {
                tmp.first.Background = brush;
                tmp.second.Background = brush;
                tmp.third.Background = brush;
                tmp.forth.Background = brush;
                return tmp;
            }
            else if (tmp.type == Textbox_FieldType.DESIRED_HOSTNO || tmp.type == Textbox_FieldType.DESIRED_SUBNETNO
               || tmp.type == Textbox_FieldType.NEW_SUBNETMASK_SHORT || tmp.type == Textbox_FieldType.SUBNETMASK_SHORT)
            {
                tmp.first.Background = brush;
                return tmp;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        private static bool isLegitSubnetDigit(byte digit)
        {
            byte[] subnetDigits = new byte[9] {0, 128, (128 + 64), (128 + 64 + 32), (128 + 64 + 32 + 16)
                , (128 + 64 + 32 + 16 + 8), (128 + 64 + 32 + 16 + 8 + 4), (128 + 64 + 32 + 16 + 8 + 4 + 2),
                (128 + 64 + 32 + 16 + 8 + 4 + 2 + 1) };
            for (int i = 0; i < subnetDigits.Length; i++)
            {
                if (digit == subnetDigits[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static int tryParseTextboxToInt(TextBox box)
        {
            int tmp = 0;
            int.TryParse(box.Text, out tmp);
            return tmp;
        }

        #endregion

        #region Ip zu Binär

        public string InputToBinary(string input)
        {
            string output = Convert.ToString(Convert.ToInt32(input), 2);
            output = output.PadLeft(8, '0');
            return output;
        }

        public string FormatIPv4String (int subnetMask, string ipv4)
        {
            string output;
            string netzanteil = "";
            string hostanteil = "";

            foreach (char x in ipv4)
            {
                if (subnetMask > 0)
                {
                        netzanteil = netzanteil + x;
                }
                else
                {
                    hostanteil = hostanteil + x;
                }
                if (x != ' ')
                {
                    subnetMask--;
                }
            }
            output = netzanteil + " | " + hostanteil.TrimStart(new char[] { ' ' }); ;
            return output;
        }
        #endregion

    }
}
