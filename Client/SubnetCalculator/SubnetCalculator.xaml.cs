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

namespace ITler_Ein_mal_Eins.SubnetCalculator
{

    public partial class SubnetCalculator : Window
    {
        //Variables
        Window origin;

        public SubnetCalculator(Window _origin)
        {
            _origin = _origin;
            InitializeComponent();
        }
    }
}
