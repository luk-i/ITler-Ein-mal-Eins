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
        //Struct
        UnitCalculatorControl.BitByteStrings results;

        public UnitCalculator(Window _origin, Control.Control _control)
        {
            origin  = _origin;
            control = _control;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            InitializeEvents();
        }

        #region Methods
        //Textboxen zurücksetzen
        private void Clear_Txbox_UnitC_Bits()
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
            txbox_bit.IsReadOnly        = false;
            txbox_kilobit.IsReadOnly    = false;
            txbox_megabit.IsReadOnly    = false;
            txbox_gigabit.IsReadOnly    = false;
            txbox_terabit.IsReadOnly    = false;
            txbox_byte.IsReadOnly       = false;
            txbox_kilobyte.IsReadOnly   = false;
            txbox_megabyte.IsReadOnly   = false;
            txbox_gigabyte.IsReadOnly   = false;
            txbox_terabyte.IsReadOnly   = false;
        }

        //Abfragen, ob die Eingaben korrekt sind.
        private bool CanWeStart()
        {
            bool letUsStart = false;
            int i = 0;
            //Wenn Textbox ausgefüllt, dann erhöhe i
            if (txbox_bit.Text      != "") { i++; }
            if (txbox_kilobit.Text  != "") { i++; }
            if (txbox_megabit.Text  != "") { i++; }
            if (txbox_gigabit.Text  != "") { i++; }
            if (txbox_terabit.Text  != "") { i++; }
            if (txbox_byte.Text     != "") { i++; }
            if (txbox_kilobyte.Text != "") { i++; }
            if (txbox_megabyte.Text != "") { i++; }
            if (txbox_gigabyte.Text != "") { i++; }
            if (txbox_terabyte.Text != "") { i++; }
            //Es darf nur genau eine Box ausgefüllt sein und es muss ein numerischer Wert sein
            txbox_active = Active_TextBox();
            if (i == 1 && control.CheckTextboxIfNumeric(txbox_active) == true)
            {
                letUsStart = true;
            }
            else
            {
                //Fehlermeldung und zurücksetzen
                System.Windows.Forms.MessageBox.Show
                ("Bitte füllen Sie ein einzelnes Feld aus und achten Sie darauf, nur positive, numerische Werte einzutragen. " + Environment.NewLine +
                 "Benutzen Sie bitte einen Punkt '.' Als Dezimaltrennzeichen. " + Environment.NewLine +
                 "Seperatoren zwischen den Vorstellen werden nicht akzeptiert. " + Environment.NewLine  +Environment.NewLine +
                 "Beispiel: 42.481516");
            }   
            return letUsStart;
        }

        public void DisplayUnitCalculator_BitsBytes()
        {
            txbox_bit.Text              = results._bit;
            txbox_bit.IsReadOnly        = true;
            txbox_kilobit.Text          = results.kiloBit;
            txbox_kilobit.IsReadOnly    = true;
            txbox_megabit.Text          = results.megaBit;
            txbox_megabit.IsReadOnly    = true;
            txbox_gigabit.Text          = results.gigaBit;
            txbox_gigabit.IsReadOnly    = true;
            txbox_terabit.Text          = results.teraBit;
            txbox_terabit.IsReadOnly    = true;
            txbox_byte.Text             = results._byte;
            txbox_byte.IsReadOnly       = true;
            txbox_kilobyte.Text         = results.kiloByte;
            txbox_kilobyte.IsReadOnly   = true;
            txbox_megabyte.Text         = results.megaByte;
            txbox_megabyte.IsReadOnly   = true;
            txbox_gigabyte.Text         = results.gigaByte;
            txbox_gigabyte.IsReadOnly   = true;
            txbox_terabyte.Text         = results.teraByte;
            txbox_terabyte.IsReadOnly   = true;
            var converter               = new BrushConverter();
            var brush                   = (Brush)converter.ConvertFromString("#00000000");
            txbox_bit.Background        = brush;
            txbox_kilobit.Background    = brush;
            txbox_megabit.Background    = brush;
            txbox_gigabit.Background    = brush;
            txbox_terabit.Background    = brush;
            txbox_byte.Background       = brush;
            txbox_kilobyte.Background   = brush;
            txbox_megabyte.Background   = brush;
            txbox_gigabyte.Background   = brush;
            txbox_terabyte.Background   = brush;
        }

        public void Event_Btn_Click_Calculate ()
        {
            if (CanWeStart() == true)
            {
                unitCalculatorControl = new UnitCalculatorControl();
                txbox_active = Active_TextBox();
                results = unitCalculatorControl.CalculateBits(txbox_active);
                if (results.noError == false)
                {
                    System.Windows.Forms.MessageBox.Show("Something, somewhere went terribly wrong :(");
                }
                else 
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
            txbox_bit.TextChanged           += Txbox_UnitCalculator_TextChanged;
            txbox_byte.TextChanged          += Txbox_UnitCalculator_TextChanged;
            txbox_kilobit.TextChanged       += Txbox_UnitCalculator_TextChanged;
            txbox_kilobyte.TextChanged      += Txbox_UnitCalculator_TextChanged;
            txbox_megabit.TextChanged       += Txbox_UnitCalculator_TextChanged;
            txbox_megabyte.TextChanged      += Txbox_UnitCalculator_TextChanged;
            txbox_gigabit.TextChanged       += Txbox_UnitCalculator_TextChanged;
            txbox_gigabyte.TextChanged      += Txbox_UnitCalculator_TextChanged;
            txbox_terabit.TextChanged       += Txbox_UnitCalculator_TextChanged;
            txbox_terabyte.TextChanged      += Txbox_UnitCalculator_TextChanged;
            txbox_binaer.TextChanged        += Txbox_UnitCalculator_TextChanged;
            txbox_oktal.TextChanged         += Txbox_UnitCalculator_TextChanged;
            txbox_dezimal.TextChanged       += Txbox_UnitCalculator_TextChanged;
            txbox_hexadezimal.TextChanged   += Txbox_UnitCalculator_TextChanged;
        }

        //Beim Schließen die Parameter über die Position zum Öffnen des Hauptfensters übergeben
        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Show();
            origin.Left = this.Left;
            origin.Top  = this.Top;
        }

        //Welche Box wurde gefüllt und wird übergeben?
        private TextBox Active_TextBox()
        {
            if (txbox_bit.Text      != "") { txbox_active = txbox_bit; }
            if (txbox_kilobit.Text  != "") { txbox_active = txbox_kilobit; }
            if (txbox_megabit.Text  != "") { txbox_active = txbox_megabit; }
            if (txbox_gigabit.Text  != "") { txbox_active = txbox_gigabit; }
            if (txbox_terabit.Text  != "") { txbox_active = txbox_terabit; }
            if (txbox_byte.Text     != "") { txbox_active = txbox_byte; }
            if (txbox_kilobyte.Text != "") { txbox_active = txbox_kilobyte; }
            if (txbox_megabyte.Text != "") { txbox_active = txbox_megabyte; }
            if (txbox_gigabyte.Text != "") { txbox_active = txbox_gigabyte; }
            if (txbox_terabyte.Text != "") { txbox_active = txbox_terabyte; }
            return txbox_active;
        }

        //Modul über eigenen Button schließen
        private void Btn_Exit_Click(object sender, RoutedEventArgs e) => Close();

        //Alle Werte löschen
        private void Btn_Bits_Reset_Click(object sender, RoutedEventArgs e) => Clear_Txbox_UnitC_Bits();

        //Werte berechen
        private void Btn_Bits_Calculate_Click(object sender, RoutedEventArgs e) => Event_Btn_Click_Calculate();

        //Ist die Eingabe numerisch?
        private void Txbox_UnitCalculator_TextChanged(object sender, TextChangedEventArgs e) => control.CheckTextboxIfNumeric((TextBox)e.Source);
 
        #endregion
    }
}
