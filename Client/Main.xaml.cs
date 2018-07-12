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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ITler_Ein_mal_Eins.Control;


namespace ITler_Ein_mal_Eins
{
    public partial class Main : Window
    {
        Control.Control control;
        IpCalculator ipCalculator;
        public Main()
        {
            control = new Control.Control();
            ipCalculator = new IpCalculator(control);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }


        #region Events

        private void Calculator_button_Click(object sender, RoutedEventArgs e)
        {
            Modules.UnitCalculator calculator = new Modules.UnitCalculator(this, control);
            this.Hide();
            calculator.Show();
            calculator.Left         = this.Left;
            calculator.Top          = this.Top;
        }
        private void SubnetCalculator_Button_Click(object sender, RoutedEventArgs e)
        {
            Modules.SubnetCalculator sCalculator = new Modules.SubnetCalculator(this, control);
            this.Hide();
            sCalculator.Show();
            sCalculator.Left            = this.Left;
            sCalculator.Top             = this.Top;
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion

    }
}
