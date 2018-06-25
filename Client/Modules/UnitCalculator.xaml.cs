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
        UnitCalculatorControl.BitByteStrings bitByteStrings;
        UnitCalculatorControl.Systems systems;

        public UnitCalculator(Window _origin, Control.Control _control)
        {
            origin  = _origin;
            control = _control;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            InitializeTags();
            InitializeEvents();
        }



        #region Methods Bits
        //Textboxen zurücksetzen Bits
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
        private bool CanWeStart_bits()
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
            txbox_bit.Text              = bitByteStrings._bit;
            txbox_bit.IsReadOnly        = true;
            txbox_kilobit.Text          = bitByteStrings.kiloBit;
            txbox_kilobit.IsReadOnly    = true;
            txbox_megabit.Text          = bitByteStrings.megaBit;
            txbox_megabit.IsReadOnly    = true;
            txbox_gigabit.Text          = bitByteStrings.gigaBit;
            txbox_gigabit.IsReadOnly    = true;
            txbox_terabit.Text          = bitByteStrings.teraBit;
            txbox_terabit.IsReadOnly    = true;
            txbox_byte.Text             = bitByteStrings._byte;
            txbox_byte.IsReadOnly       = true;
            txbox_kilobyte.Text         = bitByteStrings.kiloByte;
            txbox_kilobyte.IsReadOnly   = true;
            txbox_megabyte.Text         = bitByteStrings.megaByte;
            txbox_megabyte.IsReadOnly   = true;
            txbox_gigabyte.Text         = bitByteStrings.gigaByte;
            txbox_gigabyte.IsReadOnly   = true;
            txbox_terabyte.Text         = bitByteStrings.teraByte;
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

        public void Event_Btn_Click_Calculate_Bits ()
        {
            if (CanWeStart_bits() == true)
            {
                unitCalculatorControl = new UnitCalculatorControl();
                txbox_active = Active_TextBox();
                bitByteStrings = unitCalculatorControl.CalculateBits(txbox_active);
                if (bitByteStrings.noError == false)
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

        #region Methods Systems

        //Textboxen zurücksetzen Bits
        private void Clear_Txbox_UnitC_Systems()
        {
            txbox_binaer.Clear();
            txbox_oktal.Clear();
            txbox_dezimal.Clear();
            txbox_hexadezimal.Clear();
            
            txbox_binaer.IsReadOnly = false;
            txbox_oktal.IsReadOnly = false;
            txbox_dezimal.IsReadOnly = false;
            txbox_hexadezimal.IsReadOnly = false;
        }

        public void Event_Btn_Click_Calculate_Systems()
        {
            if (CanWeStart_Systems() == true)
            {
                unitCalculatorControl = new UnitCalculatorControl();
                txbox_active = Active_TextBox();
                systems = unitCalculatorControl.CalculateSystems(txbox_active);
                if (systems.noError == false)
                {
                    System.Windows.Forms.MessageBox.Show("Something, somewhere went terribly wrong :(");
                }
                else
                {
                    DisplayUnitCalculator_Systems();
                }
            }
        }

        //Abfragen, ob die Eingaben korrekt sind.
        private bool CanWeStart_Systems()
        {
            bool letUsStart = false;
            int i = 0;
            //Wenn Textbox ausgefüllt, dann erhöhe i
            if (txbox_binaer.Text != "") { i++; }
            if (txbox_oktal.Text != "") { i++; }
            if (txbox_dezimal.Text != "") { i++; }
            if (txbox_hexadezimal.Text != "") { i++; }

            //Es darf nur genau eine Box ausgefüllt sein und es muss ein erlaubter Wert sein
            txbox_active = Active_TextBox();
            if (i == 1 && control.CheckTextboxIfNumeric(txbox_active) == true)
            {
                letUsStart = true;
            }
            else
            {
                //Fehlermeldung und zurücksetzen
                System.Windows.Forms.MessageBox.Show
                ("Bitte füllen Sie ein einzelnes Feld aus und achten Sie darauf, nur positive Werte innerhalb des gewählten Zahlensystems einzutragen. " + Environment.NewLine +
                 "Benutzen Sie bitte einen Punkt '.' Als Dezimaltrennzeichen. " + Environment.NewLine +
                 "Seperatoren zwischen den Vorstellen werden nicht akzeptiert. " + Environment.NewLine + Environment.NewLine +
                 "Beispiel: 123.AFFE (Bei Hexadezimal)");
            }
            return letUsStart;
        }

        public void DisplayUnitCalculator_Systems()
        {
            txbox_binaer.Text               = systems.binaer;
            txbox_binaer.IsReadOnly         = true;
            txbox_oktal.Text                = systems.oktal;
            txbox_oktal.IsReadOnly          = true;
            txbox_dezimal.Text              = systems.dezimal;
            txbox_dezimal.IsReadOnly        = true;
            txbox_hexadezimal.Text          = systems.hexadezimal;
            txbox_hexadezimal.IsReadOnly    = true;
           
            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#00000000");
            txbox_binaer.Background = brush;
            txbox_oktal.Background = brush;
            txbox_dezimal.Background = brush;
            txbox_hexadezimal.Background = brush;
        }

        #endregion

        #region Initialisiation

        //Initialsieren der TextBox Tags. Für die Prüfung, welcher Wert als Eingabe erlaubt wird
        private void InitializeTags()
        {
            txbox_bit.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;
            txbox_kilobit.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;
            txbox_megabit.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;
            txbox_gigabit.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;
            txbox_terabit.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;
            txbox_byte.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;
            txbox_kilobyte.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;
            txbox_megabyte.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;
            txbox_gigabyte.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;
            txbox_terabyte.Tag = Control.Control.digitTag.UNSIGNEDFLOAT;

            txbox_binaer.Tag = Control.Control.digitTag.BINARYFLOAT;
            txbox_oktal.Tag = Control.Control.digitTag.OKTAL;
            txbox_dezimal.Tag = Control.Control.digitTag.DECIMAL;
            txbox_hexadezimal.Tag = Control.Control.digitTag.HEX;
        }

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

        #endregion

        #region Events

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
            if (txbox_bit.Text          != "") { txbox_active = txbox_bit; }
            if (txbox_kilobit.Text      != "") { txbox_active = txbox_kilobit; }
            if (txbox_megabit.Text      != "") { txbox_active = txbox_megabit; }
            if (txbox_gigabit.Text      != "") { txbox_active = txbox_gigabit; }
            if (txbox_terabit.Text      != "") { txbox_active = txbox_terabit; }
            if (txbox_byte.Text         != "") { txbox_active = txbox_byte; }
            if (txbox_kilobyte.Text     != "") { txbox_active = txbox_kilobyte; }
            if (txbox_megabyte.Text     != "") { txbox_active = txbox_megabyte; }
            if (txbox_gigabyte.Text     != "") { txbox_active = txbox_gigabyte; }
            if (txbox_terabyte.Text     != "") { txbox_active = txbox_terabyte; }
            if (txbox_binaer.Text       != "") { txbox_active = txbox_binaer; }
            if (txbox_oktal.Text        != "") { txbox_active = txbox_oktal; }
            if (txbox_dezimal.Text      != "") { txbox_active = txbox_dezimal; }
            if (txbox_hexadezimal.Text  != "") { txbox_active = txbox_hexadezimal; }

            return txbox_active;
        }

        //Modul über eigenen Button schließen
        private void Btn_Exit_Click(object sender, RoutedEventArgs e) => Close();

        //Alle Werte löschen Bits
        private void Btn_Bits_Reset_Click(object sender, RoutedEventArgs e) => Clear_Txbox_UnitC_Bits();

        //Alle Werte löschen Systeme
        private void Btn_Systems_Reset_Click(object sender, RoutedEventArgs e) => Clear_Txbox_UnitC_Systems();

        //Werte berechen Bits
        private void Btn_Bits_Calculate_Click(object sender, RoutedEventArgs e) => Event_Btn_Click_Calculate_Bits();

        //Werte berechen Zahlensysteme
        private void Btn_Systems_Calculate_Click(object sender, RoutedEventArgs e) => Event_Btn_Click_Calculate_Systems();

        //Ist die Eingabe numerisch?
        private void Txbox_UnitCalculator_TextChanged(object sender, TextChangedEventArgs e) => control.CheckTextboxIfNumeric((TextBox)e.Source);
 
        #endregion
    }
}
