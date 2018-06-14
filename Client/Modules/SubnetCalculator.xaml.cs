﻿using System;
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

namespace ITler_Ein_mal_Eins.Modules
{

    public partial class SubnetCalculator : Window
    {
        //Variables
        Window origin;
        Control.IpControl control;

        public SubnetCalculator(Window _origin)
        {
            origin = _origin;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            control = new Control.IpControl();
            InitializeComponent();
        }

        private void IPv4_calculateBits()
        {
            testFields();
            bool tmp = control.isIpV4Digit(Ip4_textBox1, true);
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
            if(control.isIpV4Digit(Ip4_textBox1, false) && control.isIpV4Digit(Ip4_textBox2, false) &&
                control.isIpV4Digit(Ip4_textBox3, false) && control.isIpV4Digit(Ip4_textBox4, false))
            {

            }
            else
            {
                // Label rot färben
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

        #endregion

    }
}