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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ITler_Ein_mal_Eins
{
    /// <summary>
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        Control.Control control;
        public Main()
        {
            control = new Control.Control();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

        }


        #region Events
        // Mit #region kann man im Editor einzelne Codebereiche Auf und Zuklappen...

        /*
         * Button - Events
         */
        private void Calculator_button_Click(object sender, RoutedEventArgs e)
        {
            Modules.UnitCalculator calculator = new Modules.UnitCalculator(this, control);
            this.Hide();
            calculator.Show();
        }
        private void SubnetCalculator_Button_Click(object sender, RoutedEventArgs e)
        {
            Modules.SubnetCalculator sCalculator = new Modules.SubnetCalculator(this, control);
            this.Hide();
            sCalculator.Show();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion

    }
}
