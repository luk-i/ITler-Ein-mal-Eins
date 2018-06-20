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
        TextBox txbox_active;
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
            txbox_active = Active_TextBox();
            int mode = 2; //Modus für Gleitkommazahlen mit Vorzeichen
            if (i == 1 && control.checkTextboxIfNumeric(txbox_active, mode) == true)
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

        public void DisplayUnitCalculator_BitsBytes()
        {

        }

        public void Event_Btn_Click_Calculate ()
        {
            if (CanWeStart() == true)
            {
                unitCalculatorControl = new UnitCalculatorControl();
                txbox_active = Active_TextBox();
                if (unitCalculatorControl.CalculateBits(txbox_active).noError == false)
                {
                    System.Windows.Forms.MessageBox.Show("Leider kann der Rechner aktuell keine so großen Zahlen verarbeiten.");
                }
                else if (unitCalculatorControl.CalculateBits(txbox_active).noError == true)
                {
                    DisplayUnitCalculator_BitsBytes();
                }
            }
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
        }

        //Beim Schließen die Parameter über die Position zum Öffnen des Hauptfensters übergeben
        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Show();
            origin.Left = this.Left;
            origin.Top = this.Top;
        }

        //Welche Box wurde gefüllt und wird übergeben?
        private TextBox Active_TextBox()
        {
            if (txbox_bit.Text != "") { txbox_active = txbox_bit; }
            if (txbox_kilobit.Text != "") { txbox_active = txbox_kilobit; }
            if (txbox_megabit.Text != "") { txbox_active = txbox_megabit; }
            if (txbox_gigabit.Text != "") { txbox_active = txbox_gigabit; }
            if (txbox_terabit.Text != "") { txbox_active = txbox_terabit; }
            if (txbox_byte.Text != "")     { txbox_active = txbox_byte; }
            if (txbox_kilobyte.Text != "") { txbox_active = txbox_kilobyte; }
            if (txbox_megabyte.Text != "") { txbox_active = txbox_megabyte; }
            if (txbox_gigabyte.Text != "") { txbox_active = txbox_gigabyte; }
            if (txbox_terabyte.Text != "") { txbox_active = txbox_terabyte; }
            return txbox_active;
        }

        //Modul über eigenen Button schließen
        private void Btn_Exit_Click(object sender, RoutedEventArgs e) => Close();

        //Alle Werte löschen
        private void Btn_Reset_Click(object sender, RoutedEventArgs e) => Clear_Txbox_UnitC();

        //Werte berechen
        private void Btn_Calculate_Click(object sender, RoutedEventArgs e) => Event_Btn_Click_Calculate();

        //Ist die Eingabe numerisch?

        private void Txbox_UnitCalculator_TextChanged(object sender, TextChangedEventArgs e)
        {
            int mode = 2;
            control.checkTextboxIfNumeric((TextBox)e.Source, mode);
        }
        #endregion


    }
}
