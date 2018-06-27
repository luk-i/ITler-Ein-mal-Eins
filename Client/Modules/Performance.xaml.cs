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
    public partial class Performance : Window
    {
        //Attributes
        Window origin;
        Control.Control control;
        PerformanceControl performanceControl;
        PerformanceControl.Struct_performanceCPU struct_performanceCPU;

        public Performance(Window _origin, Control.Control _control)
        {
            origin = _origin;
            control = _control;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            //GetPerformanceCPU();
        }

        public void GetPerformanceCPU()
        {
            while (1 == 1)
            {
                performanceControl = new PerformanceControl();
                struct_performanceCPU = performanceControl.PerformanceControl_CPU();
                float tmp = struct_performanceCPU.cpu_total;
                System.Threading.Thread.Sleep(1000); // 1 Sekunde warten
            }
        }

        #region Events #################################################################################################################################################

        //Beim Schließen die Parameter über die Position zum Öffnen des Hauptfensters übergeben
        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Show();
            origin.Left = this.Left;
            origin.Top = this.Top;
        }

        //Modul über eigenen Button schließen
        private void Btn_Exit_Click(object sender, RoutedEventArgs e) => Close();

        #endregion ####################################################################################################################################################
    }
}
