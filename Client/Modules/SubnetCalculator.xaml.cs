using System;
using System.Windows;
using System.Windows.Controls;
using ITler_Ein_mal_Eins.Control;
using ITler_Ein_mal_Eins.Model;

namespace ITler_Ein_mal_Eins.Modules
{

    public partial class SubnetCalculator : Window
    {
        #region Variables
        Window origin;
        Control.Control control;


        private bool TextChanged_Event_isLocked = false;

        #endregion

        public SubnetCalculator(Window _origin, Control.Control _control)
        {
            origin = _origin;
            control = _control;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            InitializeTags();
            InitializeEvents();
            InitializeTextboxes();
        }


        #region Functions

        private void startCalculation()
        {
            if (IPv4_calculateBits())
            {
                onValidIpV4Head();
                FillRightContent();
                Subnet_ipv4_Right.Opacity = 1;
            }
        }

        private void EnterPressPerformed()
        {
            if (btn_ipv4_calculate.IsEnabled == true)
            {
                startCalculation();
            }
            else
            {
                InitializeTextboxes();
            }
        }



        #region Control
        //
        //  Umwandlung der Ip-Adresse und der Subnetzmaske in Bits
        //
        private bool IPv4_calculateBits()
        {
            byte[] tmp;           
            if(IsValidInput_IpV4() && isValidInput_IpV4SubnetMask())
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
        private void TextBox_BottomLeft_onTextChanged(Textbox_FieldType fieldType)
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
                        if (IpCalculator.isLegitSubnetNo(Subnet_desired, Subnet_textBox_ShortWritten))
                        {
                            Subnet_textBox_ShortWritten_new.Text = IpCalculator.calcSubnetShort(Subnet_desired, Subnet_textBox_ShortWritten).ToString();
                        }
                        TextChanged_Event_isLocked = false;
                        break;
                    case Textbox_FieldType.DESIRED_HOSTNO:

                        break;
                    case Textbox_FieldType.NEW_SUBNETMASK_LONG:

                        break;
                    case Textbox_FieldType.NEW_SUBNETMASK_SHORT:

                        break;
                    default:
                        throw new NotImplementedException();

                }
            }
            else
            {
                //
                //  Berechnung wurde bereits durch ein Textchanged Event ausgelöst.
                //
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
            Textboxes_LeftTop_Brush("#FFFFFFFF");
            Subnet_ipv4_Left_Top.Background.Opacity = 0;
        }

        private void Textboxes_LeftBottom_Enabled()
        {
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
            readStruct(IpCalculator.Textboxes_Enabled(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
            Textboxes_LeftBottom_Brush("#FFFFFFFF");
            Subnet_ipv4_Left_Bottom.Background.Opacity = 0;
        }

        private void Textboxes_LeftTop_Disabled()
        {
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.IP_ADDRESSBLOCK)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.SUBNETMASK_LONG)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.SUBNETMASK_SHORT)));
            Textboxes_LeftTop_Brush("#DDDDDDDD");
            Subnet_ipv4_Left_Top.Background.Opacity = 0.3;
        }

        private void Textboxes_LeftBottom_Disabled()
        {
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_LONG)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.DESIRED_SUBNETNO)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.DESIRED_HOSTNO)));
            readStruct(IpCalculator.Textboxes_Disabled(writeStruct(Textbox_FieldType.NEW_SUBNETMASK_SHORT)));
            Textboxes_LeftBottom_Brush("#DDDDDDDD");
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
            string ipv4;
            string subnetmask_new;
            int netmask = Convert.ToInt32(Subnet_textBox_ShortWritten.Text); ;

            ipv4 = IpCalculator.InputToBinary(Ip4_textBox1.Text) + ' ';
            ipv4 = ipv4 + IpCalculator.InputToBinary(Ip4_textBox2.Text) + ' ';
            ipv4 = ipv4 + IpCalculator.InputToBinary(Ip4_textBox3.Text) + ' ';
            ipv4 = ipv4 + IpCalculator.InputToBinary(Ip4_textBox4.Text);
            subnetmask_new = IpCalculator.InputToBinary(Subnet_textBox1_new.Text) + ' ';
            subnetmask_new = subnetmask_new + IpCalculator.InputToBinary(Subnet_textBox1_new.Text) + ' ';
            subnetmask_new = subnetmask_new + IpCalculator.InputToBinary(Subnet_textBox1_new.Text) + ' ';
            subnetmask_new = subnetmask_new + IpCalculator.InputToBinary(Subnet_textBox1_new.Text);

            txblock_ip_binaer.Text = IpCalculator.FormatIPv4String_Netmask(netmask, ipv4);
            txblock_subnet_binaer.Text = IpCalculator.FormatIPv4String_Netmask(netmask, subnetmask_new);
        }

        #endregion

        #endregion

        #region Initialisation

        private void InitializeTextboxes()
        {
            Textboxes_LeftBottom_Disabled();
            Textboxes_LeftTop_Enabled();
            TextBoxes_Clear();
            btn_ipv4_calculate.IsEnabled = true;
            Subnet_ipv4_Right.Opacity = 0;
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
            Subnet_textBox_ShortWritten.Tag     = Control.Control.digitTag.UNSIGNEDINTEGER;
            Subnet_textBox_ShortWritten_new.Tag = Control.Control.digitTag.UNSIGNEDINTEGER;
            Subnet_desired.Tag                  = Control.Control.digitTag.UNSIGNEDINTEGER;
            Hosts_desired.Tag                   = Control.Control.digitTag.UNSIGNEDINTEGER;
        }

        private void InitializeEvents()
        {
            //
            // TextBox_LeftTop, alle losen ein Event aus.
            // 
            Ip4_textBox1.TextChanged += Ip4_textBox_TextChanged;
            Ip4_textBox2.TextChanged += Ip4_textBox_TextChanged;
            Ip4_textBox3.TextChanged += Ip4_textBox_TextChanged;
            Ip4_textBox4.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox1.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox2.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox3.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox4.TextChanged += Ip4_textBox_TextChanged;
            //
            //  TextBox_LeftBotton, jeder Block wirft ein eigenes Event!
            //
            Subnet_desired.TextChanged += Subnet_desired_TextChanged;
            Hosts_desired.TextChanged += Hosts_desired_TextChanged;
            Subnet_textBox1_new.TextChanged += Subnet_textBox_new_TextChanged;
            Subnet_textBox2_new.TextChanged += Subnet_textBox_new_TextChanged;
            Subnet_textBox3_new.TextChanged += Subnet_textBox_new_TextChanged;
            Subnet_textBox4_new.TextChanged += Subnet_textBox_new_TextChanged;
            Subnet_textBox_ShortWritten_new.TextChanged += Subnet_textBox_ShortWritten_new_TextChanged;
            //
            //  Entereingabe Berechnet Felder
            //
            Ip4_textBox1.KeyDown += Enterpressed;
            Ip4_textBox2.KeyDown += Enterpressed;
            Ip4_textBox3.KeyDown += Enterpressed;
            Ip4_textBox4.KeyDown += Enterpressed;
            Subnet_textBox1.KeyDown += Enterpressed;
            Subnet_textBox2.KeyDown += Enterpressed;
            Subnet_textBox3.KeyDown += Enterpressed;
            Subnet_textBox4.KeyDown += Enterpressed;
            Subnet_textBox_ShortWritten.KeyDown += Enterpressed;
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

        private void readStruct(IPAddressTextboxes boxes)
        {
            switch (boxes.type)
            {
                case Textbox_FieldType.IP_ADDRESSBLOCK:
                    Ip4_textBox1                      = boxes.first;
                    Ip4_textBox2                      = boxes.second;
                    Ip4_textBox3                      = boxes.third;
                    Ip4_textBox4                      = boxes.forth;
                    break;
                case Textbox_FieldType.SUBNETMASK_LONG:
                    Subnet_textBox1                   = boxes.first;
                    Subnet_textBox2                   = boxes.second;
                    Subnet_textBox3                   = boxes.third;
                    Subnet_textBox4                   = boxes.forth;
                    break;
                case Textbox_FieldType.NEW_SUBNETMASK_LONG:
                    Subnet_textBox1_new               = boxes.first;
                    Subnet_textBox2_new               = boxes.second;
                    Subnet_textBox3_new               = boxes.third;
                    Subnet_textBox4_new               = boxes.forth;
                    break;
                case Textbox_FieldType.SUBNETMASK_SHORT:
                    Subnet_textBox_ShortWritten       = boxes.first;
                    break;
                case Textbox_FieldType.NEW_SUBNETMASK_SHORT:
                    Subnet_textBox_ShortWritten_new   = boxes.first;
                    break;
                case Textbox_FieldType.DESIRED_SUBNETNO:
                    Subnet_desired                    = boxes.first;
                    break;
                case Textbox_FieldType.DESIRED_HOSTNO:
                    Hosts_desired                     = boxes.first;
                    break;
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
            origin.Show();
            origin.Left         = this.Left;
            origin.Top          = this.Top;
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

        private void Ip4_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.CheckTextboxIfNumeric((TextBox)e.Source);
        }

        private void Subnet_desired_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.CheckTextboxIfNumeric((TextBox)e.Source);
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.DESIRED_SUBNETNO);
        }

        private void Hosts_desired_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.CheckTextboxIfNumeric((TextBox)e.Source);
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.DESIRED_HOSTNO);
        }

        private void Subnet_textBox_new_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.CheckTextboxIfNumeric((TextBox)e.Source);
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.NEW_SUBNETMASK_LONG);
        }

        private void Subnet_textBox_ShortWritten_new_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.CheckTextboxIfNumeric((TextBox)e.Source);
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.NEW_SUBNETMASK_SHORT);
        }

        #endregion

    }
}
