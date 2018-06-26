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

namespace ITler_Ein_mal_Eins.Modules
{

    public partial class SubnetCalculator : Window
    {
        #region Variables
        Window origin;
        IpCalculator ipControl;
        Control.Control control;

        #endregion

        #region Enum

        private enum ErrorCodeNo
        {
            NOERROR,
            WRONGIPV4,
            WRONGSUBNETCODE,
            MULTIPLEFIELDSFILLED
        }

        private enum FieldStatus
        {
            NOFIELDSFILLED,
            LONGFILLED,
            SHORTFILLED,
            BOTHFILLED
        }

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
        }


        #region Functions

        #region Control
        /*
         *  Umwandlung der Ip-Adresse und der Subnetzmaske in Bits
         */
        private void IPv4_calculateBits()
        {
            if(IsValidInput_IpV4() && isValidInput_IpV4SubnetMask())
            {
                // Befüllen Subnetzmaske und Eventwurf
            }

            bool tmp = IpCalculator.isIpV4Digit(Ip4_textBox1, true);

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
                case FieldStatus.SHORTFILLED:

                    return true;
                case FieldStatus.LONGFILLED:

                    return true;
                case FieldStatus.BOTHFILLED:

                    return false;
                case FieldStatus.NOFIELDSFILLED:

                    return false;
                default:
                    throw new NotImplementedException();
                
            }


            if (IpCalculator.isIpV4Digit(Subnet_textBox1, true) && IpCalculator.isIpV4Digit(Subnet_textBox2, true) &&
                 IpCalculator.isIpV4Digit(Subnet_textBox3, true) && IpCalculator.isIpV4Digit(Subnet_textBox4, true))
            {
                if (IpCalculator.isLegitIpV4SubnetMask(Subnet_textBox1, Subnet_textBox2,
                    Subnet_textBox3, Subnet_textBox4))
                {
                    createErrorLabel(ErrorCodeNo.NOERROR);
                }
                else
                {
                    createErrorLabel(ErrorCodeNo.WRONGSUBNETCODE);
                }
            }
            else
            {
                createErrorLabel(ErrorCodeNo.WRONGSUBNETCODE);
            }
            return false;
        }

        private FieldStatus getFieldStatus()
        {
            bool shortFieldFilled = false;
            bool longFieldFilled = false;

            if (Subnet_textBox_ShortWritten.Text != "") { shortFieldFilled = true; }
            if (Subnet_textBox1.Text != "" &&
                Subnet_textBox2.Text != "" &&
                Subnet_textBox3.Text != "" &&
                Subnet_textBox4.Text != "")             { longFieldFilled = true; }

            if(shortFieldFilled == true  && longFieldFilled == false) { return FieldStatus.SHORTFILLED; }
            if(shortFieldFilled == false && longFieldFilled == true ) { return FieldStatus.LONGFILLED; }
            if(shortFieldFilled == false && longFieldFilled == false) { return FieldStatus.NOFIELDSFILLED; };
            return FieldStatus.BOTHFILLED;
        }

        #endregion

        #region Manipulation

        /*
         * Ausgabe der Fehlermeldungen erfolgt über Ressourcen!!!
         * Funktion gib je nach Übergebenem Fehlercode eine Message aus.
         */
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

        #endregion

        #endregion

        #region Initialisation

        private void InitializeTags()
        {
            Ip4_textBox1.Tag = Control.Control.digitTag.BYTE;
            Ip4_textBox2.Tag = Control.Control.digitTag.BYTE;
            Ip4_textBox3.Tag = Control.Control.digitTag.BYTE;
            Ip4_textBox4.Tag = Control.Control.digitTag.BYTE;
            Subnet_textBox1.Tag = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox2.Tag = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox3.Tag = Control.Control.digitTag.SUBNETMASK;
            Subnet_textBox4.Tag = Control.Control.digitTag.SUBNETMASK;
        }

        private void InitializeEvents()
        {
            Ip4_textBox1.TextChanged += Ip4_textBox_TextChanged;
            Ip4_textBox2.TextChanged += Ip4_textBox_TextChanged;
            Ip4_textBox3.TextChanged += Ip4_textBox_TextChanged;
            Ip4_textBox4.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox1.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox2.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox3.TextChanged += Ip4_textBox_TextChanged;
            Subnet_textBox4.TextChanged += Ip4_textBox_TextChanged;
        }

        #endregion

        #region Events
        /*
         * Button - Events
         * 
         * IN EVENTS WIRD NIX PROGRAMMIERT!!! DAS IST SCHNELLER WIEDER WEG
         * ALS EINEM LIEB IST,
         * 
         * nutze lieber Finktionsaufrufe, z.B. eine Funktion welche von 
         * "Enter" und "Verlassen" aufgerufen wird
         */
        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Show();
            origin.Left = this.Left;
            origin.Top = this.Top;
        }

        private void button_Submit_Click(object sender, RoutedEventArgs e)
        {
            IPv4_calculateBits();
        }

        private void Ip4_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.CheckTextboxIfNumeric((TextBox)e.Source);
        }

        #endregion

    }
}
