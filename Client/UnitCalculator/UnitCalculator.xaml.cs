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

namespace ITler_Ein_mal_Eins.UnitCalculator
{
    /// <summary>
    /// Interaktionslogik für UnitCalculator.xaml
    /// </summary>
    public partial class UnitCalculator : Window
    {
        // Variables
        Window origin;

        public UnitCalculator(Window _origin)
        {
            origin = _origin;
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Show();
        }
    }
}
