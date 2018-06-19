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

    public partial class UnitCalculator : Window
    {
        //Attributes
        Window origin;
        Control.Control control;
        TextBox lastActive;
        Control.UnitCalculatorControl unitCalculatorControl;

        public UnitCalculator(Window _origin, Control.Control _control)
        {
            origin = _origin;
            control = _control;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            InitializeEvents();
        }

        #region Methods
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
        #endregion

        #region Events

        private void InitializeEvents()
        {
            //Abfrage Textbox geändert für die Färbung bei Falshceingabe
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
            //Abfrage des letzten aktiven Fokus
            txbox_bit.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
            txbox_byte.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
            txbox_kilobit.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
            txbox_kilobyte.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
            txbox_megabit.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
            txbox_megabyte.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
            txbox_gigabit.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
            txbox_gigabyte.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
            txbox_terabit.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
            txbox_terabyte.LostFocus += new RoutedEventHandler(Txbox_LostFocus);
        }

        //Beim Schließen die Parameter über die Position zum Öffnen des Hauptfensters übergeben
        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Show();
            origin.Left = this.Left;
            origin.Top = this.Top;
        }

        //Fokus der letzten aktiven TextBox einfangen
        private void Txbox_LostFocus(object sender, EventArgs e)
        {
            lastActive = sender as TextBox;
        }


        //Modul über eigenen Button schließen
        private void Btn_Exit_Click(object sender, RoutedEventArgs e) => Close();

        //Alle Werte löschen
        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            Clear_Txbox_UnitC();        
        }

        //Werte berechen
        private void Btn_Calculate_Click(object sender, RoutedEventArgs e)
        {
            unitCalculatorControl = new UnitCalculatorControl();
            if (unitCalculatorControl.calculateBits(lastActive.Text,lastActive.Name) == false)
            {
                System.Windows.Forms.MessageBox.Show("Da ging irgendwas, irgendwo schrecklich schief! Das tut und leid :(");
            }
           
        }

        //Ist die Eingabe numerisch?       
        private void Txbox_UnitCalculator_TextChanged(object sender, TextChangedEventArgs e)
        {
            control.checkTextboxIfNumeric((TextBox)e.Source);
        }
        #endregion


    }
}
