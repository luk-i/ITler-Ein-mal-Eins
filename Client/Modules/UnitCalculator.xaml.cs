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
        //Textboxen zurücksetzen
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

        //Abfragen, ob die Eingaben korrekt sind.
        private bool CanWeStart()
        {
            bool letUsStart = false;
            int i = 0;
            //Wenn Textbox ausgefüllt, dann erhöhe i
            if (txbox_bit.Text != "") { i++; }
            if (txbox_kilobit.Text != "") { i++; }
            if (txbox_megabit.Text != "") { i++; }
            if (txbox_gigabit.Text != "") { i++; }
            if (txbox_terabit.Text != "") { i++; }
            if (txbox_byte.Text != "") { i++; }
            if (txbox_kilobyte.Text != "") { i++; }
            if (txbox_megabyte.Text != "") { i++; }
            if (txbox_gigabyte.Text != "") { i++; }
            if (txbox_terabyte.Text != "") { i++; }
            //Es darf nur genau eine Box ausgefüllt sein und es muss ein numerischer Wert sein
            if (i == 1 && control.checkTextboxIfNumeric(lastActive) == true)
            {
                letUsStart = true;
            }
            else
            {
                //Fehlermeldung und zurücksetzen
                System.Windows.Forms.MessageBox.Show("Bitte füllen Sie nur ein einzelnes Feld aus und achten Sie darauf, nur numerische Werte einzutragen.");
                Clear_Txbox_UnitC();
            }
    

            return letUsStart;
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

        //Fokus der letzten aktiven, veränderten TextBox einfangen
        private void Txbox_LostFocus(object sender, EventArgs e)
        {
            TextBox tmp = sender as TextBox;
            if (tmp.Text != "")
            {
                lastActive = tmp;
            }
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
            if (CanWeStart() == true)
            {
                unitCalculatorControl = new UnitCalculatorControl();
                if (unitCalculatorControl.calculateBits(lastActive) == false)
                {
                    System.Windows.Forms.MessageBox.Show("Da ging irgendwas, irgendwo schrecklich schief! Das tut und leid :(");
                }
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
