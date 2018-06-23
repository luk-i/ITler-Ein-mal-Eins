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
        //Variables
        Window origin;
        IpCalculator ipControl;
        Control.Control control;

        public SubnetCalculator(Window _origin, Control.Control _control)
        {
            origin = _origin;
            control = _control;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ipControl = new Control.IpCalculator();
            InitializeComponent();
            InitializeEvents();
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


        private void IPv4_calculateBits()
        {
            testFields();
            bool tmp = ipControl.isIpV4Digit(Ip4_textBox1, true);
            // Test, obs geht
            if (tmp)
            {
                tb_1_DecimalData.Content = "Is Allowed";
            }
            else
            {
                tb_1_DecimalData.Content = "Not Allowed";
            }
        }

        private bool testFields()
        {
            if(ipControl.isIpV4Digit(Ip4_textBox1, false) && ipControl.isIpV4Digit(Ip4_textBox2, false) &&
                ipControl.isIpV4Digit(Ip4_textBox3, false) && ipControl.isIpV4Digit(Ip4_textBox4, false))
            {

            }
            else
            {
                // Label Fehlermeldung ausgeben
            }
            return false;
        }

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
            int mode = 3; //Modus 1 frägt ab, ob es sich um eine legitime, positive Ganzzahl innerhalb 0-255 handelt
            control.CheckTextboxIfNumeric((TextBox)e.Source, mode);
        }

        #endregion

    }
}
