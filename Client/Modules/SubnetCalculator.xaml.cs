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

                // Lock all fields
                
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

        private void InitializeTextboxes()
        {

        }

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

        //Modul über eigenen Button schließen
        private void Btn_Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Show();
            origin.Left         = this.Left;
            origin.Top          = this.Top;
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
