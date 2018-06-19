using ITler_Ein_mal_Eins.Control;
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

namespace ITler_Ein_mal_Eins.Modules
{

    public partial class UnitCalculator : Window
    {
        //Variables
        Window origin;
        Control.Control control;
        TextBox lastActive;

        public UnitCalculator(Window _origin, Control.Control _control)
        {
            origin = _origin;
            control = _control;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            InitializeEvents();
        }

            private void Clear_Txbox_UnitC()
        {
            txbox_bit.Clear();
            txbox_kilobit.Clear();
            txbox_megabit.Clear();
            txbox_gigabit.Clear();
            txbox_terabit.Clear();
            txbox_byte.Clear();
            txbox_kilobyte.Clear();
            txbox_megabyte.Clear();
            txbox_gigabyte.Clear();
            txbox_terabyte.Clear();
        }

        #region Events

        private void InitializeEvents()
        {
            txbox_bit.TextChanged += Txbox_UnitCalculator_TextChanged;
            txbox_byte.TextChanged += Txbox_UnitCalculator_TextChanged;
            txbox_kilobit.TextChanged += Txbox_UnitCalculator_TextChanged;
            txbox_kilobyte.TextChanged += Txbox_UnitCalculator_TextChanged;
            txbox_megabit.TextChanged += Txbox_UnitCalculator_TextChanged;
            txbox_megabyte.TextChanged += Txbox_UnitCalculator_TextChanged;
            txbox_gigabit.TextChanged += Txbox_UnitCalculator_TextChanged;
            txbox_gigabyte.TextChanged += Txbox_UnitCalculator_TextChanged;
            txbox_terabit.TextChanged += Txbox_UnitCalculator_TextChanged;
            txbox_terabyte.TextChanged += Txbox_UnitCalculator_TextChanged;
        }

        //Beim Schließen die Parameter über die Position zum Öffnen des Hauptfensters übergeben
        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Show();
            origin.Left = this.Left;
            origin.Top = this.Top;
        }

        //Modul über eigenen Button schließen
        private void Btn_Exit_Click(object sender, RoutedEventArgs e) => Close();

        //Alle Werte löschen
        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            Clear_Txbox_UnitC();        
        }

        //Ist die Eingabe numerisch?       
        private void Txbox_UnitCalculator_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.checkTextboxIfNumeric((TextBox)e.Source);
        }
        #endregion


    }
}
