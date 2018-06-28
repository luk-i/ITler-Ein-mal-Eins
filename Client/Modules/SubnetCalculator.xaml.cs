using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ITler_Ein_mal_Eins.Control;
using ITler_Ein_mal_Eins.Model;
using System.Net;

namespace ITler_Ein_mal_Eins.Modules
{

    public partial class SubnetCalculator : Window
    {
        #region Variables
        Window origin;
        IpCalculator ipControl;
        Control.Control control;

        private bool TextChanged_Event_isLocked = false;

        #endregion

        public SubnetCalculator(Window _origin, Control.Control _control)
        {
            origin = _origin;
            control = _control;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ipControl = new Control.IpCalculator();
            InitializeComponent();
            InitializeTags();
            InitializeEvents();
            InitializeTextboxes();
        }


        #region Functions

        private void startCalculation()
        {
            if (IPv4_calculateBits()) onValidIpV4Head();
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
                switch (getFieldStatus())
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
            //  Das Berechnen der Fehlenden Felder würde eine Entlosschleife auslösen
            //  (Jedes Eintragen in eine Textbox führt wieder rekursiv in diese Fuktion)
            //
            //  Daher muss geprüft werden, ob bereits eine Berechnung der Felder erfolgt, was
            //  durch die Flag TextChanged_Event_isLocked erfolgt.
            //
            if (!TextChanged_Event_isLocked)
            {

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
            switch (getFieldStatus())
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
                        if (IpCalculator.isLegitIpV4SubnetMask(Subnet_textBox1, Subnet_textBox2,
                             Subnet_textBox3, Subnet_textBox4))
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

        private IpV4_FieldStatus getFieldStatus()
        {
            bool shortFieldFilled = false;
            bool longFieldFilled = false;

            if (Subnet_textBox_ShortWritten.Text != "") { shortFieldFilled = true; }
            if (Subnet_textBox1.Text != "" ||
                Subnet_textBox2.Text != "" ||
                Subnet_textBox3.Text != "" ||
                Subnet_textBox4.Text != "")             { longFieldFilled = true; }

            if(shortFieldFilled == true  && longFieldFilled == false) { return IpV4_FieldStatus.SHORTFILLED; }
            if(shortFieldFilled == false && longFieldFilled == true ) { return IpV4_FieldStatus.LONGFILLED; }
            if(shortFieldFilled == false && longFieldFilled == false) { return IpV4_FieldStatus.NOFIELDSFILLED; };
            return IpV4_FieldStatus.BOTHFILLED;
        }

        #endregion

        #region Manipulation

        //
        //  Ausgabe der Fehlermeldungen erfolgt über Ressourcen!!!
        //  Funktion gib je nach Übergebenem Fehlercode eine Message aus.
        // 
        private void createErrorLabel(ErrorCodeNo _code)
        {
            switch (_code)
            {
                case ErrorCodeNo.NOERROR:
                    label_AdressGrid_IsDataCorrect.Content = Errorcodes.NOERROR;
                    break;
                case ErrorCodeNo.WRONGIPV4:
                    label_AdressGrid_IsDataCorrect.Content = Errorcodes.ERROR_INVALIDINPUT;
                    MessageBox.Show(Errorcodes.ERROR_IPV4DIGITISNOTVALID);
                    break;
                case ErrorCodeNo.WRONGSUBNETCODE:
                    label_AdressGrid_IsDataCorrect.Content = Errorcodes.ERROR_INVALIDINPUT;
                    MessageBox.Show(Errorcodes.ERROR_SUBNETMASKISNOTVALID);
                    break;
                case ErrorCodeNo.MULTIPLEFIELDSFILLED:
                    label_AdressGrid_IsDataCorrect.Content = Errorcodes.ERROR_INVALIDINPUT;
                    MessageBox.Show(Errorcodes.ERROR_MULTIPLEFIELDSFILLED);                       
                    break;
            // Endoftheline
            }
        }

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
            Ip4_textBox1.IsReadOnly = false;
            Ip4_textBox2.IsReadOnly = false;
            Ip4_textBox3.IsReadOnly = false;
            Ip4_textBox4.IsReadOnly = false;
            Subnet_textBox1.IsReadOnly = false;
            Subnet_textBox2.IsReadOnly = false;
            Subnet_textBox3.IsReadOnly = false;
            Subnet_textBox4.IsReadOnly = false;
            Subnet_textBox_ShortWritten.IsReadOnly = false;
            Textboxes_LeftTop_Brush("#FFFFFFFF");
        }

        private void Textboxes_LeftBottom_Enabled()
        {
            Subnet_textBox1_new.IsReadOnly = false;
            Subnet_textBox2_new.IsReadOnly = false;
            Subnet_textBox3_new.IsReadOnly = false;
            Subnet_textBox4_new.IsReadOnly = false;
            Subnet_desired.IsReadOnly = false;
            Hosts_desired.IsReadOnly = false;
            Subnet_textBox_ShortWritten_new.IsReadOnly = false;
            Textboxes_LeftBottom_Brush("#FFFFFFFF");
        }

        private void Textboxes_LeftTop_Disabled()
        {
            Ip4_textBox1.IsReadOnly = true;
            Ip4_textBox2.IsReadOnly = true;
            Ip4_textBox3.IsReadOnly = true;
            Ip4_textBox4.IsReadOnly = true;
            Subnet_textBox1.IsReadOnly = true;
            Subnet_textBox2.IsReadOnly = true;
            Subnet_textBox3.IsReadOnly = true;
            Subnet_textBox4.IsReadOnly = true;
            Subnet_textBox_ShortWritten.IsReadOnly = true;
            Textboxes_LeftTop_Brush("#DDDDDDDD");
        }

        private void Textboxes_LeftBottom_Disabled()
        {
            Subnet_textBox1_new.IsReadOnly = true;
            Subnet_textBox2_new.IsReadOnly = true;
            Subnet_textBox3_new.IsReadOnly = true;
            Subnet_textBox4_new.IsReadOnly = true;
            Subnet_desired.IsReadOnly = true;
            Hosts_desired.IsReadOnly = true;
            Subnet_textBox_ShortWritten_new.IsReadOnly = true;
            Textboxes_LeftBottom_Brush("#DDDDDDDD");
        }

        private void Textboxes_LeftTop_Brush(string brush_string)
        {
            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString(brush_string);
            Ip4_textBox1.Background = brush;
            Ip4_textBox2.Background = brush;
            Ip4_textBox3.Background = brush;
            Ip4_textBox4.Background = brush;
            Subnet_textBox1.Background = brush;
            Subnet_textBox2.Background = brush;
            Subnet_textBox3.Background = brush;
            Subnet_textBox4.Background = brush;
            Subnet_textBox_ShortWritten.Background = brush;
        }

        private void Textboxes_LeftBottom_Brush(string brush_string)
        {
            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString(brush_string);
            Subnet_textBox1_new.Background = brush;
            Subnet_textBox2_new.Background = brush;
            Subnet_textBox3_new.Background = brush;
            Subnet_textBox4_new.Background = brush;
            Subnet_desired.Background = brush;
            Hosts_desired.Background = brush;
            Subnet_textBox_ShortWritten_new.Background = brush;
        }

        #endregion

        #endregion

        #endregion

        #region Initialisation

        private void InitializeTextboxes()
        {
            Textboxes_LeftBottom_Disabled();
            Textboxes_LeftTop_Enabled();
            TextBoxes_Clear();
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

        private void Ip4_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.CheckTextboxIfNumeric((TextBox)e.Source);
        }

        private void Subnet_desired_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.DESIRED_SUBNETNO);
        }

        private void Hosts_desired_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.DESIRED_HOSTNO);
        }

        private void Subnet_textBox_new_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.SUBNETMASK_LONG);
        }

        private void Subnet_textBox_ShortWritten_new_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_BottomLeft_onTextChanged(Textbox_FieldType.SUBNETMASK_SHORT);
        }

        #endregion

    }
}
