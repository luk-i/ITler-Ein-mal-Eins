using System;
using System.Windows;
using System.Windows.Controls;
using ITler_Ein_mal_Eins.Control;
using ITler_Ein_mal_Eins.Model;

namespace ITler_Ein_mal_Eins.Modules
{

    public partial class SubnetCalculator : Window
    {
        Window origin;
        Control.Control control;
        //
        public SubnetCalculator(Window _origin, Control.Control _control)
        {
            origin = _origin;
            control = _control;
            InitializeComponent();
            InitializeTags();
            InitializeEvents_LeftTop();
            InitializeEvents_LeftBottom();
            InitializeTextboxes();
            InitializeContent();
            InitializeSlider();
        }

        #region Variables

        private bool TextChanged_Event_isLocked = false;

        #endregion


        #region Methods

        private void startCalculation()
        {
            if (IPv4_calculateBits())
            {
                onValidIpV4Head();
                FillRightContent();
                Subnet_ipv4_Right_Scroll.Opacity = 1;
            }
        }

        private void EnterPressPerformed()
        {
            if (btn_ipv4_calculate.IsEnabled == true)
            {
                startCalculation();
            }
            else if (btn_ipv4_calculate.IsEnabled == false)
            {
                InitializeTextboxes();
            }
        }



        #region Control

        private bool IPv4_calculateBits()
        {
            byte[] tmp;
            if (IsValidInput_IpV4() && isValidInput_IpV4SubnetMask())
            {
                switch (IpCalculator.getFieldStatus(writeStruct(Textbox_FieldType.SUBNETMASK_LONG), writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)))
                {
                    case IpV4_FieldStatus.SHORTFILLED:
                        tmp = IpCalculator.calcEmptySubnetMaskFields(Subnet_textBox_ShortWritten);
                        int a = tmp[0];
                        int b = tmp[1];
                        int c = tmp[2];
                        int d = tmp[3];
                        Subnet_textBox1.Text = a.ToString();
                        Subnet_textBox2.Text = b.ToString();
                        Subnet_textBox3.Text = c.ToString();
                        Subnet_textBox4.Text = d.ToString();
                        break;
                    case IpV4_FieldStatus.LONGFILLED:
                        tmp = IpCalculator.calcEmptySubnetMaskFields(Subnet_textBox1, Subnet_textBox2, Subnet_textBox3, Subnet_textBox4);
                        int x = tmp[0];
                        Subnet_textBox_ShortWritten.Text = x.ToString();
                        break;
                    default:
                        throw new NotImplementedException();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //
        //  Funktion zum Sperren des Kopfes, der Freigabe des Fußes (mit Startwerten)
        //  und Wurf eines ersten Berechnungsevents. 
        //
        private void onValidIpV4Head()
        {
            Textboxes_LeftTop_Disabled();
            Textboxes_LeftBottom_Enabled();
            btn_ipv4_calculate.IsEnabled = false;
            int i = 0;
            Subnet_desired.Text = i.ToString();
        }

        //
        //  Funktion wird jedesmal ausgeführt, wenn sich eine Eingabe in den Textfeldern ändert
        //  Dabei wird zunächst geprüft, ob die jeweilige Eingabe stimmt. Fehler werden rot markiert
        //  und ein Label weist den Nutzer darauf hin, dass die Eingabe so nicht übernommen werden kann.
        //  Stimmt die Eingabe, so wird auf die fehlenden Felder umgerechnet.
        // 
        //  Als Letztes wird dann eine Berechnung der Ausgabe ausgelöst. Insgesamt ist somit das
        //  Auslesen der Ergebnisse in Echtzeit möglich.
        // 
        private void TextBox_BottomLeft_onTextChanged(Textbox_FieldType fieldType, TextBox txbox)
        {
            //
            //  Das Berechnen der Fehlenden Felder würde eine Endlosschleife auslösen
            //  (Jedes Eintragen in eine Textbox führt wieder rekursiv in diese Fuktion)
            //
            //  Daher muss geprüft werden, ob bereits eine Berechnung der Felder erfolgt, was
            //  durch die Flag TextChanged_Event_isLocked erfolgt.
            //
            if (!TextChanged_Event_isLocked)
            {
                TextChanged_Event_isLocked = true;
                switch (fieldType)
                {
                    case Textbox_FieldType.DESIRED_SUBNETNO:
                        // Validation + Calc restliche Felder, eine FKT in CONTROL welche von hier mehrmals aufgerufen wird.
                        if (IpCalculator.isLegitSubnetNo(Subnet_desired, Subnet_textBox_ShortWritten) && control.CheckTextboxIfNumeric(txbox) && IpCalculator.IsTextboxFilled(txbox))
                        {
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
                            //Falsche Nummer ?
                            string st = IpCalculator.calcSubnetShort(writeStruct(Textbox_FieldType.DESIRED_SUBNETNO),
                                writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)).ToString();
                            Subnet_textBox_ShortWritten_new.Text = st;
                            Hosts_desired.Text = IpCalculator.calcHostNo(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)).ToString();
                            //
                            //UMBAU IN READSTRUCT AUSSTEHEND
                            //
                            byte[] tmp;
                            tmp = IpCalculator.calcEmptySubnetMaskFields(Subnet_textBox_ShortWritten_new);
                            readStruct(IpCalculator.fillAddressBoxByByte(tmp, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
                            FillRightContent();
                        }
                        else
                        {
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.RED, writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
                        }
                        TextChanged_Event_isLocked = false;
                        break;
                    case Textbox_FieldType.DESIRED_HOSTNO:
                        if (IpCalculator.isLegitHostNo(writeStruct(Textbox_FieldType.DESIRED_HOSTNO),
                            writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)) && control.CheckTextboxIfNumeric(txbox) && IpCalculator.IsTextboxFilled(txbox))
                        {
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
                            string st = IpCalculator.calcSubnetShort(writeStruct(Textbox_FieldType.DESIRED_HOSTNO)).ToString();
                            Subnet_textBox_ShortWritten_new.Text = st;
                            readStruct(IpCalculator.calcSubnetNo(writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)
                                ,writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT),
                                writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)));
                            byte[] tmp;
                            tmp = IpCalculator.calcEmptySubnetMaskFields(Subnet_textBox_ShortWritten_new);
                            readStruct(IpCalculator.fillAddressBoxByByte(tmp, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
                            FillRightContent();
                        }
                        else
                        {
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.RED, writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
                        }
                        TextChanged_Event_isLocked = false;
                        break;
                    case Textbox_FieldType.NEW_SUBNETMASK_LONG:
                        if(IpCalculator.isIpV4Digit(Subnet_textBox1_new,true) &&
                           IpCalculator.isIpV4Digit(Subnet_textBox2_new, true) &&
                           IpCalculator.isIpV4Digit(Subnet_textBox3_new, true) &&
                           IpCalculator.isIpV4Digit(Subnet_textBox4_new, true) &&
                           IpCalculator.tryParseTextboxToInt(Subnet_textBox4_new) != 255 &&
                           IpCalculator.isLegitIpV4SubnetMask(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)) && IpCalculator.IsTextboxFilled(txbox))
                        {
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
                            byte[] tmp = IpCalculator.calcEmptySubnetMaskFields(Subnet_textBox1_new, Subnet_textBox2_new, Subnet_textBox3_new, Subnet_textBox4_new);
                            Subnet_textBox_ShortWritten_new.Text = tmp[0].ToString();
                            readStruct(IpCalculator.calcSubnetNo(writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)
                             , writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT),
                             writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)));
                            Hosts_desired.Text = IpCalculator.calcHostNo(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)).ToString();
                            FillRightContent();
                        }
                        else
                        {
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.RED, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
                        }
                        TextChanged_Event_isLocked = false;
                        break;
                    case Textbox_FieldType.NEW_SUBNETMASK_SHORT:
                        //
                        //SubnetNo wird eins zu Hoch gesetzt!
                        //
                        if (IpCalculator.isLegitIpV4SubnetMask(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)
                            , writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)) && IpCalculator.IsTextboxFilled(txbox))
                        {
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.WHITE, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
                            readStruct(IpCalculator.calcSubnetNo(writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)
                              , writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT),
                              writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)));                            
                            Hosts_desired.Text = IpCalculator.calcHostNo(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)).ToString();
                            byte[] tmp;
                            tmp = IpCalculator.calcEmptySubnetMaskFields(Subnet_textBox_ShortWritten_new);
                            readStruct(IpCalculator.fillAddressBoxByByte(tmp, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));

                            FillRightContent();
                        }
                        else
                        {
                            readStruct(IpCalculator.brushTextboxes(ColourCodes.RED, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
                        }

                        TextChanged_Event_isLocked = false;
                        break;
                    default:
                        throw new NotImplementedException();

                }
                setTextboxValuebySlider(Selected_subnet_txbox, Selected_subnet);
            }
            else
            {
                //
                //  Berechnung wurde bereits durch ein Textchanged Event ausgelöst.
                //
            }
        }

        private string calculateFocusedSubnetAddress(Int64 numberOfSubnet, Int64 netmask, Int64 subnetmask, string ipv4)
        {
            if (subnetmask > netmask && numberOfSubnet != 0)
            {
                string firstSubnetAdress = IpCalculator.FormatIPv4String(netmask, subnetmask, (IpCalculator.FirstSubnetIPAdress(ipv4, netmask)));
                firstSubnetAdress.Replace(" ", "");
                Char delimiter = '|';
                String[] substrings = firstSubnetAdress.Split(delimiter);
                String[] part;
                part = new String[3];
                int i = 0;
                foreach (var substring in substrings)
                {
                    part[i] = substring;
                    i++;
                }
                Int64 middlePart_int = Convert.ToInt32(part[1], 2);
                middlePart_int = numberOfSubnet - 1;
                string middlePart_string = Convert.ToString(middlePart_int, 2);

                Int64 endPart_int = Convert.ToInt32(part[2], 2);
                Int64 anzahl = subnetmask - netmask;
                Int64 wert = Convert.ToInt64(Math.Pow(2, anzahl));
                Int64 schritt = wert / anzahl;
                endPart_int = (schritt * numberOfSubnet) - 1;
                string endPart_string = Convert.ToString(endPart_int, 2);

                return part[0] + part[1] + part[2];
            }

            else
            {
                return IpCalculator.FormatIPv4String(netmask, subnetmask, (IpCalculator.FirstSubnetIPAdress(ipv4, netmask))); ;
            }               
        }

        #endregion

        #region Validation
        private bool IsValidInput_IpV4()
        {
            if (IpCalculator.isIpV4Digit(Ip4_textBox1, false) && IpCalculator.isIpV4Digit(Ip4_textBox2, false) &&
                IpCalculator.isIpV4Digit(Ip4_textBox3, false) && IpCalculator.isIpV4Digit(Ip4_textBox4, false))
            {
                return true;
            }
            else
            {
                // Ist falsche IPv4 Adresse
                createErrorLabel(ErrorCodeNo.WRONGIPV4);
                return false;
            }
        }

        private bool isValidInput_IpV4SubnetMask()
        {
            switch (IpCalculator.getFieldStatus(writeStruct(Textbox_FieldType.SUBNETMASK_LONG), writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)))
            {
                case IpV4_FieldStatus.SHORTFILLED:
                    if (IpCalculator.isLegitIpV4SubnetMask(Subnet_textBox_ShortWritten))
                    {
                        createErrorLabel(ErrorCodeNo.NOERROR);
                        return true;
                    }
                    else
                    {
                        createErrorLabel(ErrorCodeNo.WRONGSUBNETCODE);
                        return false;
                    }
                case IpV4_FieldStatus.LONGFILLED:
                    if (IpCalculator.isIpV4Digit(Subnet_textBox1, true) && IpCalculator.isIpV4Digit(Subnet_textBox2, true) &&
                         IpCalculator.isIpV4Digit(Subnet_textBox3, true) && IpCalculator.isIpV4Digit(Subnet_textBox4, true))
                    {
                        if (IpCalculator.isLegitIpV4SubnetMask(writeStruct(Textbox_FieldType.SUBNETMASK_LONG)))
                        {
                            createErrorLabel(ErrorCodeNo.NOERROR);
                            return true;
                        }
                        else
                        {
                            createErrorLabel(ErrorCodeNo.WRONGSUBNETCODE);
                            return false;
                        }
                    }
                    else
                    {
                        createErrorLabel(ErrorCodeNo.WRONGSUBNETCODE);
                        return false;
                    }
                case IpV4_FieldStatus.BOTHFILLED:
                    createErrorLabel(ErrorCodeNo.MULTIPLEFIELDSFILLED);
                    return false;
                case IpV4_FieldStatus.NOFIELDSFILLED:
                    createErrorLabel(ErrorCodeNo.WRONGSUBNETCODE);
                    return false;
                default:
                    throw new NotImplementedException();   
            }
        }

        #endregion

        #region Manipulation

        //
        //  Ausgabe der Fehlermeldungen erfolgt über Ressourcen!!!
        //  Funktion gib je nach Übergebenem Fehlercode eine Message aus.
        // 
        private void createErrorLabel(ErrorCodeNo _code)
        {
            if(_code == ErrorCodeNo.NOERROR)
            {
                label_AdressGrid_IsDataCorrect.Content = Errorcodes.NOERROR;
            }
            else
            {
                label_AdressGrid_IsDataCorrect.Content = Errorcodes.ERROR_INVALIDINPUT;
                Control.Control.getErrorMessage(_code);
            }
        }

        private void redrawSlideBar(IPAddressTextboxes subnet_No)
        {
            int tmp = IpCalculator.tryParseTextboxToInt(subnet_No.first);
            if(tmp > 0)
            {
                Selected_subnet.Minimum = 0;
                int subnet_short = IpCalculator.tryParseTextboxToInt(Subnet_textBox_ShortWritten_new);
                int subnet_short_old = IpCalculator.tryParseTextboxToInt(Subnet_textBox_ShortWritten);
                int maxSNO = IpCalculator.calcSubnetNo(subnet_short_old, subnet_short);
                Selected_subnet.Maximum = maxSNO;
            }
            else
            {
                Selected_subnet.Minimum = 0;
                Selected_subnet.Maximum = 0;
            }
            Selected_subnet.Value = 0.0;
        }

        private void setTextboxValuebySlider(TextBox box, Slider slider)
        {
            int i = (int)slider.Value;
            string st = i.ToString();
            box.Text = st;
        }

        private void setSliderByTextbox(TextBox box, Slider slider)
        {
            int i = IpCalculator.tryParseTextboxToInt(box);
            if(i >= 0 && i <= slider.Maximum)
            {
                slider.Value = i;
            }
            else
            {

            }

        }

        #endregion

        #region Textboxes

        private void TextBoxes_Clear()
        {
            foreach (UIElement element in Subnet_ipv4_Left_Top.Children)
                Textboxes_Clear_Grid(element);
            foreach (UIElement element in Subnet_ipv4_Left_Bottom.Children)
                Textboxes_Clear_Grid(element);
        }
        private void Textboxes_Clear_Grid(UIElement element)
        {
            if (element is StackPanel stackpanel)
            {
                foreach (UIElement element_panel in stackpanel.Children)
                    if (element_panel is TextBox textbox)
                        textbox.Clear();
            }
            else if (element is TextBox textbox)
                textbox.Clear();
        }

        private void Textboxes_LeftTop_Enabled()
        {
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.IP_ADDRESSBLOCK)));
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.SUBNETMASK_LONG)));
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)));
            Textboxes_LeftTop_Brush(ColourCodes.WHITE);
            Subnet_ipv4_Left_Top.Background.Opacity = 0;
        }

        private void Textboxes_LeftBottom_Enabled()
        {
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
            Textboxes_LeftBottom_Brush(ColourCodes.WHITE);
            Subnet_ipv4_Left_Bottom.Background.Opacity = 0;
        }

        private void Textboxes_LeftTop_Disabled()
        {
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.IP_ADDRESSBLOCK)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.SUBNETMASK_LONG)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)));
            Textboxes_LeftTop_Brush(ColourCodes.GRAY);
            Subnet_ipv4_Left_Top.Background.Opacity = 0.3;
        }

        private void Textboxes_LeftBottom_Disabled()
        {
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
            Textboxes_LeftBottom_Brush(ColourCodes.GRAY);
            Subnet_ipv4_Left_Bottom.Background.Opacity = 0.3;
        }

        private void Textboxes_LeftTop_Brush(string brush_string)
        {
            readStruct(IpCalculator.brushTextboxes(brush_string, writeStruct(Textbox_FieldType.IP_ADDRESSBLOCK)));
            readStruct(IpCalculator.brushTextboxes(brush_string, writeStruct(Textbox_FieldType.SUBNETMASK_LONG)));
            readStruct(IpCalculator.brushTextboxes(brush_string, writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)));
        }

        private void Textboxes_LeftBottom_Brush(string brush_string)
        {
            readStruct(IpCalculator.brushTextboxes(brush_string, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
            readStruct(IpCalculator.brushTextboxes(brush_string, writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
            readStruct(IpCalculator.brushTextboxes(brush_string, writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
            readStruct(IpCalculator.brushTextboxes(brush_string, writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
        }

        private void FillRightContent()
        {
            Int64 netmask;
            Int64 subnetmask;
            Int64 numberOfSubnet;
            try     { netmask       = Convert.ToInt64(Subnet_textBox_ShortWritten.Text); }
            catch   { netmask       = 0; };
            try     { subnetmask    = Convert.ToInt64(Subnet_textBox_ShortWritten_new.Text); }
            catch   { subnetmask    = 0;}
            try     { numberOfSubnet   = Convert.ToInt64(Selected_subnet_txbox.Text); }
            catch   { numberOfSubnet    = 0; }

            string ipv4 = IpCalculator.InputToBinary(Ip4_textBox1.Text);
            ipv4 = ipv4 + IpCalculator.InputToBinary(Ip4_textBox2.Text);
            ipv4 = ipv4 + IpCalculator.InputToBinary(Ip4_textBox3.Text);
            ipv4 = ipv4 + IpCalculator.InputToBinary(Ip4_textBox4.Text);
            string subnetmask_string              = IpCalculator.InputToBinary(Subnet_textBox1_new.Text);
            subnetmask_string = subnetmask_string + IpCalculator.InputToBinary(Subnet_textBox2_new.Text);
            subnetmask_string = subnetmask_string + IpCalculator.InputToBinary(Subnet_textBox3_new.Text);
            subnetmask_string = subnetmask_string + IpCalculator.InputToBinary(Subnet_textBox4_new.Text);

            txblock_ip_binaer.Text                  = IpCalculator.FormatIPv4String(netmask, subnetmask, ipv4);
            txblock_subnet_binaer.Text              = IpCalculator.FormatIPv4String(netmask, subnetmask, subnetmask_string);
            try { txblock_subnet_max_hosts.Text     = Convert.ToString((IpCalculator.MaxBinaryBase(Convert.ToDecimal(Hosts_desired.Text) + 2 )) - 2 ); }
            catch { txblock_subnet_max_hosts.Text   = "-"; }
            try { txblock_subnet_number.Text        = Convert.ToString(IpCalculator.MaxBinaryBase(Convert.ToDecimal(Subnet_desired.Text))); }
            catch { txblock_subnet_number.Text      = "-"; }

            txblock_subnet_adress.Text = calculateFocusedSubnetAddress(numberOfSubnet, netmask, subnetmask, ipv4);

            /* Umstrukturierung der Funktion in Arbeit ###############################################
            txblock_first_adress.Text           = IpCalculator.FormatIPv4String(netmask, subnetmask, (IpCalculator.FirstSubnetIPAdress(ipv4, netmask)));
            txblock_first_adress_dez.Text       = IpCalculator.IP_BinaryToDottedDecimal(IpCalculator.FirstSubnetIPAdress(ipv4, netmask));
            txblock_first_bc_adress.Text        = IpCalculator.FormatIPv4String(netmask, subnetmask, (IpCalculator.FirstBroadcastIPAdress(ipv4, netmask, subnetmask)));
            txblock_first_bc_adress_dez.Text    = IpCalculator.IP_BinaryToDottedDecimal(IpCalculator.FirstBroadcastIPAdress(ipv4, netmask, subnetmask));

            txblock_last_adress.Text            = IpCalculator.FormatIPv4String(netmask, subnetmask, (IpCalculator.LastSubnetIPAdress(ipv4, netmask, subnetmask)));
            txblock_last_adress_dez.Text        = IpCalculator.IP_BinaryToDottedDecimal(IpCalculator.LastSubnetIPAdress(ipv4, netmask, subnetmask));
            txblock_last_bc_adress.Text         = IpCalculator.FormatIPv4String(netmask, subnetmask, (IpCalculator.LastBroadcastIPAdress(ipv4, netmask, subnetmask)));
            txblock_last_bc_adress_dez.Text     = IpCalculator.IP_BinaryToDottedDecimal(IpCalculator.LastBroadcastIPAdress(ipv4, netmask, subnetmask));
            */

            redrawSlideBar(writeStruct(Textbox_FieldType.DESIRED_SUBNETNO));
        }

        #endregion

        #endregion
        
        #region Initialisation

        private void InitializeTextboxes()
        {
            DeactivateEvents_LeftBottom();
            TextBoxes_Clear();
            Textboxes_LeftBottom_Disabled();
            Textboxes_LeftTop_Enabled();
            TextChanged_Event_isLocked = false;
            btn_ipv4_calculate.IsEnabled = true;
            Subnet_ipv4_Right_Scroll.Opacity = 0;
            InitializeEvents_LeftBottom();
        }

        private void InitializeSlider()
        {
            Selected_subnet.ValueChanged += Selected_subnet_ValueChanged;
            Selected_subnet.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
            Selected_subnet.TickFrequency = 1;
            Selected_subnet.IsSnapToTickEnabled = true;
            Selected_subnet_txbox.TextChanged += Selected_subnet_txbox_TextChanged;
        }

        private void InitializeTags()
        {
            Ip4_textBox1.Tag                    = Control.Control.digitTag.BYTE;
            Ip4_textBox2.Tag                    = Control.Control.digitTag.BYTE;
            Ip4_textBox3.Tag                    = Control.Control.digitTag.BYTE;
            Ip4_textBox4.Tag                    = Control.Control.digitTag.BYTE;
            Subnet_textBox1.Tag                 = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox2.Tag                 = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox3.Tag                 = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox4.Tag                 = Control.Control.digitTag.SUBNETMASK;           
            Subnet_textBox1_new.Tag             = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox2_new.Tag             = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox3_new.Tag             = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox4_new.Tag             = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox_ShortWritten.Tag     = Control.Control.digitTag.SUBNETMASK_SHORT;
            Subnet_textBox_ShortWritten_new.Tag = Control.Control.digitTag.SUBNETMASK_SHORT;
            Subnet_desired.Tag                  = Control.Control.digitTag.UNSIGNEDINTEGER;
            Hosts_desired.Tag                   = Control.Control.digitTag.UNSIGNEDINTEGER;
        }

        private void InitializeEvents_LeftTop()
        {
            //
            // TextBox_LeftTop, alle loesen ein Event aus.
            // 
            Ip4_textBox1.TextChanged += Ip4_textBox_TextChanged;
            Ip4_textBox2.TextChanged += Ip4_textBox_TextChanged;
            Ip4_textBox3.TextChanged += Ip4_textBox_TextChanged;
            Ip4_textBox4.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox1.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox2.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox3.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox4.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox_ShortWritten.TextChanged += Ip4_textBox_TextChanged;
        }

        private void InitializeEvents_LeftBottom()
        {
            //
            //  TextBox_LeftBottom, jeder Block wirft ein eigenes Event!
            //
            Subnet_desired.TextChanged += Subnet_desired_TextChanged;
            Hosts_desired.TextChanged += Hosts_desired_TextChanged;
            Subnet_textBox1_new.TextChanged += Subnet_textBox_new_TextChanged;
            Subnet_textBox2_new.TextChanged += Subnet_textBox_new_TextChanged;
            Subnet_textBox3_new.TextChanged += Subnet_textBox_new_TextChanged;
            Subnet_textBox4_new.TextChanged += Subnet_textBox_new_TextChanged;
            Subnet_textBox_ShortWritten_new.TextChanged += Subnet_textBox_ShortWritten_new_TextChanged;
        }

        private void DeactivateEvents_LeftBottom()
        {
            //
            //  TextBox_LeftBottom, jeder Block wirft ein eigenes Event!
            //
            Subnet_desired.TextChanged -= Subnet_desired_TextChanged;
            Hosts_desired.TextChanged -= Hosts_desired_TextChanged;
            Subnet_textBox1_new.TextChanged -= Subnet_textBox_new_TextChanged;
            Subnet_textBox2_new.TextChanged -= Subnet_textBox_new_TextChanged;
            Subnet_textBox3_new.TextChanged -= Subnet_textBox_new_TextChanged;
            Subnet_textBox4_new.TextChanged -= Subnet_textBox_new_TextChanged;
            Subnet_textBox_ShortWritten_new.TextChanged -= Subnet_textBox_ShortWritten_new_TextChanged;
        }

        private void InitializeContent()
        {
            help_subnet.Text = Help.HELP_SUBNET_CALCULATOR;
        }

            private IPAddressTextboxes writeStruct(Textbox_FieldType type)
        {
            switch (type)
            {
                case Textbox_FieldType.DESIRED_HOSTNO:
                    return new IPAddressTextboxes(Hosts_desired, Textbox_FieldType.DESIRED_HOSTNO);
                case Textbox_FieldType.DESIRED_SUBNETNO:
                    return new IPAddressTextboxes(Subnet_desired, Textbox_FieldType.DESIRED_SUBNETNO);
                case Textbox_FieldType.IP_ADDRESSBLOCK:
                    return new IPAddressTextboxes(Ip4_textBox1, Ip4_textBox2, Ip4_textBox3, Ip4_textBox4, Textbox_FieldType.IP_ADDRESSBLOCK);
                case Textbox_FieldType.NEW_SUBNETMASK_LONG:
                    return new IPAddressTextboxes(Subnet_textBox1_new, Subnet_textBox2_new, Subnet_textBox3_new,
                        Subnet_textBox4_new, Textbox_FieldType.NEW_SUBNETMASK_LONG);
                case Textbox_FieldType.NEW_SUBNETMASK_SHORT:
                    return new IPAddressTextboxes(Subnet_textBox_ShortWritten_new, Textbox_FieldType.NEW_SUBNETMASK_SHORT);
                case Textbox_FieldType.SUBNETMASK_LONG:
                    return new IPAddressTextboxes(Subnet_textBox1, Subnet_textBox2, Subnet_textBox3, Subnet_textBox4, Textbox_FieldType.SUBNETMASK_LONG);
                case Textbox_FieldType.SUBNETMASK_SHORT:
                    return new IPAddressTextboxes(Subnet_textBox_ShortWritten, Textbox_FieldType.SUBNETMASK_SHORT);
                default:
                    throw new NotImplementedException();
            }
        }

        private bool readStruct(IPAddressTextboxes boxes)
        {
            try
            {
                switch (boxes.type)
                {
                    case Textbox_FieldType.IP_ADDRESSBLOCK:
                        Ip4_textBox1 = checked(boxes.first);
                        Ip4_textBox2 = checked(boxes.second);
                        Ip4_textBox3 = checked(boxes.third);
                        Ip4_textBox4 = checked(boxes.forth);
                        break;
                    case Textbox_FieldType.SUBNETMASK_LONG:
                        Subnet_textBox1 = checked(boxes.first);
                        Subnet_textBox2 = checked(boxes.second);
                        Subnet_textBox3 = checked(boxes.third);
                        Subnet_textBox4 = checked(boxes.forth);
                        break;
                    case Textbox_FieldType.NEW_SUBNETMASK_LONG:
                        Subnet_textBox1_new = checked(boxes.first);
                        Subnet_textBox2_new = checked(boxes.second);
                        Subnet_textBox3_new = checked(boxes.third);
                        Subnet_textBox4_new = checked(boxes.forth);
                        break;
                    case Textbox_FieldType.SUBNETMASK_SHORT:
                        Subnet_textBox_ShortWritten = checked(boxes.first);
                        break;
                    case Textbox_FieldType.NEW_SUBNETMASK_SHORT:
                        Subnet_textBox_ShortWritten_new = checked(boxes.first);
                        break;
                    case Textbox_FieldType.DESIRED_SUBNETNO:
                        Subnet_desired = checked(boxes.first);
                        break;
                    case Textbox_FieldType.DESIRED_HOSTNO:
                        Hosts_desired = checked(boxes.first);
                        break;               
                }
                return true;
            }catch(System.OverflowException)
            {
                return false;
            }
        }

        #endregion


        #region Events
        //
        // Events: Button und TextChanged
        // 
        // IN EVENTS WIRD NIX PROGRAMMIERT!!! DAS IST SCHNELLER WIEDER WEG
        // ALS EINEM LIEB IST,
        // 
        // nutze lieber Finktionsaufrufe, z.B. eine Funktion welche von 
        // "Enter" und "Verlassen" aufgerufen wird
        //
        private void Btn_Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Left         = this.Left;
            origin.Top          = this.Top;
            origin.Show();
        }

        private void button_Submit_Click(object sender, RoutedEventArgs e)
        {
            startCalculation();
        }

        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            InitializeTextboxes();
        }

        private void Enterpressed(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                EnterPressPerformed();
            }
        }

        private void Selected_subnet_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            setTextboxValuebySlider(Selected_subnet_txbox, Selected_subnet);
            FillRightContent();
        }

        private void Ip4_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.CheckTextboxIfNumeric((TextBox)e.Source);
        }

        private void Subnet_desired_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.DESIRED_SUBNETNO, (TextBox)e.Source);           
        }

        private void Hosts_desired_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.DESIRED_HOSTNO, (TextBox)e.Source);
        }

        private void Subnet_textBox_new_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.NEW_SUBNETMASK_LONG, (TextBox)e.Source);
        }

        private void Subnet_textBox_ShortWritten_new_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.NEW_SUBNETMASK_SHORT, (TextBox)e.Source);
        }
        
        private void Selected_subnet_txbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            setSliderByTextbox(Selected_subnet_txbox, Selected_subnet);
            FillRightContent();
        }

        #endregion

    }
}
