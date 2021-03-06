﻿using System;
using System.Windows.Controls;
using ITler_Ein_mal_Eins.Model;
using System.Net;
using System.Windows.Media;
using ITler_Ein_mal_Eins.Exceptions;

namespace ITler_Ein_mal_Eins.Control
{
    class IpCalculator
    {
        Control control;
        public IpCalculator(Control _control)
        {
            control = _control;
        }


        #region Validation

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
            try
            {
                int shortDigit = tryParseTextboxToInt(box);
                if (shortDigit >= 1 && shortDigit <= 32) { return true; }
                return false;
            }catch(System.OverflowException)
            {
                return false;
            }
        }

        public static bool isLegitIpV4SubnetMask(int shortDigit)
        {
            try
            {
                int checked_shortDigit = checked(shortDigit);
            if (checked_shortDigit >= 1 && checked_shortDigit <= 32) { return true; }
                return false;
            }catch (System.OverflowException)
            {
                return false;
            }
        }

        public static bool isLegitIpV4SubnetMask(IPAddressTextboxes newSubnetMask, IPAddressTextboxes oldSubnetMask)
        {
            try
            {
                if (oldSubnetMask.type == Textbox_FieldType.SUBNETMASK_SHORT)
                {
                    if (newSubnetMask.type == Textbox_FieldType.NEW_SUBNETMASK_SHORT)
                    {
                        int shortDigit = tryParseTextboxToInt(newSubnetMask.first);
                        int shortDigit_old = tryParseTextboxToInt(oldSubnetMask.first);
                        if (shortDigit >= shortDigit_old && shortDigit <= 31)
                        {
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        throw new WrongTypeException(newSubnetMask, Textbox_FieldType.NEW_SUBNETMASK_SHORT);
                    }
                }
                else
                {
                    throw new WrongTypeException(oldSubnetMask, Textbox_FieldType.SUBNETMASK_SHORT);
                }
            }catch (System.OverflowException)
            {
                return false;
            }
        }

        #endregion

        #region isLegitSubnetNo
        //
        //  Prüfung als eigene Funktion... Spart Platz in einer Klasse, die eigentlich
        //  nur ein Model ist... :D
        //  Auch hier: Felder Sind Valide. PS: "pfff...Laufzeitoptimierung ist für die,
        //  die nen Scheiß PC haben..."
        //
        public static bool isLegitSubnetNo(TextBox subnetNo, TextBox subnet_short)
        {
            try
            {
                if (tryParseTextboxToInt(subnetNo) >= 0 && tryParseTextboxToInt(subnetNo) <=
                    getMaxSubnetNo(subnet_short))
                {
                    return true;
                }
                return false;
            }
            catch (System.OverflowException)
            {
                return false;
            }
        }

        public static bool isLegitSubnetNo(int subnetNo, int subnet_short)
        {
            try
            {
                if (subnetNo >= 0 && subnetNo <= getMaxSubnetNo(subnet_short))
                {
                    return true;
                }
                return false;
            }catch (System.OverflowException)
            {
                return false;
            }
        }

        #endregion

        #region isLegitHostNo

        public static bool isLegitHostNo(int HostNo, int subnet_short_old)
        {
            try
            {
                int z = checked(HostNo);
                int ckecked_subnet_short_old = checked(subnet_short_old);
                if (z >= 0 && z <= getMaxHostNo(subnet_short_old))
                {
                    return true;
                }
                return false;
            }catch(System.OverflowException)
            {
                return false;
            }
        }

        public static bool isLegitHostNo(IPAddressTextboxes HostNo, IPAddressTextboxes subnet_short_old)
        {
            try
            {
                if (HostNo.type == Textbox_FieldType.DESIRED_HOSTNO)
                {
                    if (subnet_short_old.type == Textbox_FieldType.SUBNETMASK_SHORT)
                    {
                        int maxHostNo = getMaxHostNo(subnet_short_old);
                        int this_HostNo = tryParseTextboxToInt(HostNo.first);
                        if (this_HostNo >= 0 && this_HostNo <= maxHostNo)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        throw new WrongTypeException(subnet_short_old, Textbox_FieldType.SUBNETMASK_SHORT);
                    }
                }
                else
                {
                    throw new WrongTypeException(HostNo, Textbox_FieldType.DESIRED_HOSTNO);
                }
            }catch (System.OverflowException)
            {
                return false;
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

        public static bool IsTextboxFilled (TextBox txbox)
        {
            if (txbox.Text != "")
                return true;
            else
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

        #region calcSubnetShort
        //
        //  Wird als erstes Feld berechnet, da alle anderen Felder darauf aufbauen.
        //  Werte sind zuvor bereits Validiert worden.
        //  Ceiling: Aufrundung
        //
        //  Berechnung aus HostNo:
        //  Netzmaske Neu = 32 - Ceiling(ln(hostno)/ln(2))
        //
        public static int calcSubnetShort(int subnetNo, int subnet_shortwritten_old)
        {
            if (subnetNo == 0) return subnet_shortwritten_old;
            int tmp;
            double short_written = Math.Log(subnetNo, 2);
            tmp = (int) Math.Ceiling(short_written);
            if (tmp == 0) tmp++;
            return subnet_shortwritten_old + tmp;
        }

        public static int calcSubnetShort(IPAddressTextboxes subnetNo, IPAddressTextboxes subnet_shortwritten_old)
        {
            if (subnetNo.type == Textbox_FieldType.DESIRED_SUBNETNO || subnet_shortwritten_old.type == Textbox_FieldType.SUBNETMASK_SHORT)
            {
                if (tryParseTextboxToInt(subnetNo.first) == 0) return tryParseTextboxToInt(subnet_shortwritten_old.first);
                int tmp;
                double short_written = Math.Log(tryParseTextboxToInt(subnetNo.first), 2);
                tmp = (int)Math.Ceiling(short_written);
                if (tmp == 0) tmp++;
                return (tryParseTextboxToInt(subnet_shortwritten_old.first) + tmp);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static int calcSubnetShort(int desired_hostNo)
        {
            if (desired_hostNo > 0)
            {
                double z = Math.Log(desired_hostNo + 2) / Math.Log(2);
                double n = Math.Ceiling(z);
                return (32 - (int)n);
            }
            else
            {
                return 31;
            }
        }

        public static int calcSubnetShort(IPAddressTextboxes desired_hostNo)
        {
            if(desired_hostNo.type == Textbox_FieldType.DESIRED_HOSTNO)
            {
                int x = tryParseTextboxToInt(desired_hostNo.first) + 2;
                if (x > 0)
                {
                    double z = Math.Log(x) / Math.Log(2);
                    double n = Math.Ceiling(z);
                    return (32 - (int)n);
                }
                else
                {
                    return 31;
                }
            }
            else
            {
                throw new WrongTypeException(desired_hostNo, Textbox_FieldType.DESIRED_HOSTNO);
            }
        }
        #endregion

        #region calcSubnetNo

        public static int calcSubnetNo(int shortWritten, int shortWritten_old)
        {
            int tmp; 
            tmp = (int) Math.Pow(2, shortWritten_old - shortWritten);
            return tmp;
        }

        public static IPAddressTextboxes calcSubnetNo(IPAddressTextboxes SubnetNo, IPAddressTextboxes shortWritten, IPAddressTextboxes shortWritten_old)
        {
            if (shortWritten.type == Textbox_FieldType.NEW_SUBNETMASK_SHORT || shortWritten_old.type == Textbox_FieldType.SUBNETMASK_SHORT)
            {
                if (SubnetNo.type == Textbox_FieldType.DESIRED_SUBNETNO)
                {
                    int tmp;
                    int i_shortWritten = tryParseTextboxToInt(shortWritten.first);
                    int i_shortWritten_old = tryParseTextboxToInt(shortWritten_old.first);
                    tmp = (int)Math.Pow(2, i_shortWritten - i_shortWritten_old);
                    SubnetNo.first.Text = tmp.ToString();
                    return SubnetNo;
                }
                else
                {
                    throw new WrongTypeException(SubnetNo, Textbox_FieldType.DESIRED_SUBNETNO);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region calcHostNo
        //
        // Hostzahl: (2 ^ Hostanteil) - 2
        //
        public static int calcHostNo(int subnet_short_new)
        {
            int tmp = 32 - subnet_short_new;
            tmp = ((int) Math.Pow(2, tmp)) - 2;
            return tmp;
        }
        public static int calcHostNo(IPAddressTextboxes subnet_short_new)
        {
            if (subnet_short_new.type == Textbox_FieldType.NEW_SUBNETMASK_SHORT)
            {
                int tmp = 32 - tryParseTextboxToInt(subnet_short_new.first);
                tmp = ((int)Math.Pow(2, tmp)) - 2;
                return tmp;
            }
            else
            {
                throw new WrongTypeException(subnet_short_new, Textbox_FieldType.NEW_SUBNETMASK_SHORT);
            }
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

        //
        //  Berechnung der Maximalen Subnetzzahl, /31 als maximaler Netzanteil
        //  MaxSubnetNo = 2^(31-subnet_short)
        //  Übernimmt Validierten Wert. Prüfung ist damit Hinfällig
        //
        public static int getMaxSubnetNo(TextBox subnet_short)
        {
            int txt_b = tryParseTextboxToInt(subnet_short);
            int tmp = (int)Math.Pow(2, (31 - txt_b));
            return tmp;
        }

        public static int getMaxSubnetNo(int subnet_short)
        {
            int tmp = (int) Math.Pow((double)2, (double)(31 - subnet_short));
            return tmp;
        }

        #endregion

        #region getMaxHostNo
        //
        //  Wie es auf der Verpackung steht...
        //
        public static int getMaxHostNo(int subnet_short_old)
        {
            return calcHostNo(subnet_short_old);
        }

        public static int getMaxHostNo(IPAddressTextboxes subnet_short_old)
        {
            if (subnet_short_old.type == Textbox_FieldType.SUBNETMASK_SHORT)
            {
                return calcHostNo(tryParseTextboxToInt(subnet_short_old.first));
            }
            else
            {
                throw new WrongTypeException(subnet_short_old, Textbox_FieldType.SUBNETMASK_SHORT);
            }
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
                throw new WrongTypeException();
            }
        }

        #endregion

        #region Ip zu Binär und Hostanzahl (rechter Bereich)

        public static string InputToBinary(string input)
        {
            try
            {
                string output = Convert.ToString(Convert.ToInt64(input), 2);
                output = output.PadLeft(8, '0');
                return output;
            }
            catch
            {
                return "";
            }
        }

        public static string FormatIPv4String (Int64 netmask, Int64 subnetmask, string ipv4)
        {
            if (ipv4 != "")
            {
                string output;
                string netzanteil = "";
                string hostanteil = "";
                string hostanteil_subnezt = "";
                bool maskengleich;
                string workstring = "";
                int eightDigits = 0;

                foreach (char x in ipv4)
                {
                    workstring = workstring + x;
                    if (eightDigits < 7)
                    {
                        eightDigits++;
                    }
                    else
                    {
                        workstring = workstring + ' ';
                        eightDigits = 0;
                    }
                }

                if (netmask == subnetmask || netmask > subnetmask) maskengleich = true;
                else maskengleich = false;

                foreach (char x in workstring)
                {
                    if (netmask > 0)
                    {
                        netzanteil = netzanteil + x;
                    }
                    else
                    {
                        if (subnetmask > 0)
                        {
                            hostanteil = hostanteil + x;
                        }
                        else
                        {
                            hostanteil_subnezt = hostanteil_subnezt + x;
                        }
                    }
                    if (x != ' ')
                    {
                        netmask--;
                        subnetmask--;
                    }
                }
                if (maskengleich == false)
                {
                    output = netzanteil + " | " + hostanteil.TrimStart(new char[] { ' ' }).TrimEnd(new char[] { ' ' }) + " | " + hostanteil_subnezt.TrimStart(new char[] { ' ' });
                    return output;
                }
                else
                {
                    output = netzanteil + " | " + hostanteil_subnezt.TrimStart(new char[] { ' ' });
                    return output;
                }
            }
            else
            {
                return "-";
            }
        }

        public static Int64 MaxBinaryBase(decimal input)
        {
            if (input != 1)
            {
                int potenz = 0;
                while (input > 1)
                {
                    input = input / 2;
                    potenz++;
                }
                if (potenz == 0)
                {
                    return 0;
                }
                else
                {
                        return Convert.ToInt64(Math.Pow(2, potenz));
                }
            }
            else
            {
                return 2;
            }
        }

        public static string FirstSubnetIPAdress (string inputIP, Int64 netmask)
        {
            string output = "";
            foreach (char x in inputIP)
            {
                if (netmask > 0)
                {
                    output = output + x;
                    netmask--;
                }
                else
                {
                    output = output + "0";
                }
            }
            return output;
        }

        public static string FirstBroadcastIPAdress (string inputIP, Int64 netmask, Int64 subnetmask)
        {
            if (netmask == subnetmask)
            {
                return "";
            }
            else
            {
                string output = "";
                foreach (char x in inputIP)
                {
                    if (netmask > 0)
                    {
                        output = output + x;
                        netmask--;
                        subnetmask--;
                    }
                    else
                    {
                        if (subnetmask > 0)
                        {
                            output = output + '0';
                            subnetmask--;
                        }
                        else
                        {
                            output = output + "1";
                        }
                    }
                }
                return output;
            }
        }

        public static string LastSubnetIPAdress(string inputIP, Int64 netmask, Int64 subnetmask)
        {
            if (netmask == subnetmask)
            {
                return "";
            }
            else
            {
                string output = "";
                foreach (char x in inputIP)
                {
                    if (netmask > 0)
                    {
                        output = output + x;
                        netmask--;
                        subnetmask--;
                    }
                    else
                    {
                        if (subnetmask > 0)
                        {
                            output = output + '1';
                            subnetmask--;
                        }
                        else
                        {
                            output = output + "0";
                        }
                    }
                }
                return output;
            }
        }

        public static string LastBroadcastIPAdress(string inputIP, Int64 netmask, Int64 subnetmask)
        {
            string output = "";
            foreach (char x in inputIP)
            {
                if (netmask > 0)
                {
                    output = output + x;
                    netmask--;
                    subnetmask--;
                }
                else
                {
                    if (subnetmask > 0)
                    {
                        output = output + '1';
                        subnetmask--;
                    }
                    else
                    {
                        output = output + "1";
                    }
                }
            }
            return output;
        }

        public static string IP_BinaryToDottedDecimal (string inputIP)
        {
            int eightDigits = 0;
            int numberofByte = 1;
            string byte1 = "";
            string byte2 = "";
            string byte3 = "";
            string byte4 = "";
            string output = "";
            foreach (char x in inputIP)
            {
                switch (numberofByte)
                {
                    case 1: byte1 = byte1 + x; break;
                    case 2: byte2 = byte2 + x; break;
                    case 3: byte3 = byte3 + x; break;
                    case 4: byte4 = byte4 + x; break;
                }
                if (eightDigits < 7)
                {
                    eightDigits++;
                }
                else
                {
                    eightDigits = 0;
                    numberofByte++;
                }
            }
            try
            {
                output =
                    Convert.ToString(Convert.ToInt64(byte1, 2)) + '.' +
                    Convert.ToString(Convert.ToInt64(byte2, 2)) + '.' +
                    Convert.ToString(Convert.ToInt64(byte3, 2)) + '.' +
                    Convert.ToString(Convert.ToInt64(byte4, 2));
                return output;
            }
            catch
            {
                return "-";
            }

        }

        #endregion

        #region Supportcalculations

        public static int tryParseTextboxToInt(TextBox box)
        {
            int tmp = -1;
            if(int.TryParse(box.Text, out tmp))
            {
                return tmp;
            }
            else
            {
                throw new System.OverflowException();
            }
        }

        public static IPAddressTextboxes fillAddressBoxByByte(byte[] byte_array, IPAddressTextboxes to_overwrite)
        {
            int a = byte_array[0];
            int b = byte_array[1];
            int c = byte_array[2];
            int d = byte_array[3];
            to_overwrite.first.Text = a.ToString();
            to_overwrite.second.Text = b.ToString();
            to_overwrite.third.Text = c.ToString();
            to_overwrite.forth.Text = d.ToString();
            return to_overwrite;
        }


        #endregion

    }
}
