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
        UnitCalculatorControl unitCalculatorControl;
        
        //Struct
        UnitCalculatorControl.BitByteStrings bitByteStrings;
        UnitCalculatorControl.Systems systems;

        public UnitCalculator(Window _origin, Control.Control _control)
        {
            origin  = _origin;
            control = _control;
            InitializeComponent();
            InitializeTags();
            InitializeContent();
            InitializeEvents();
        }

        #region Allgemein ################################################################################################################################################################################### 

        //Welche Box wurde gefüllt und wird übergeben?
        private TextBox Active_TextBox()
        {
            foreach (TextBox tx_box in grid_unitCalculator_bits.Children.OfType<TextBox>())
            {
                if (tx_box.Text != "")
                    txbox_active = tx_box;
            }
            foreach (TextBox tx_box in grid_unitCalculator_systems.Children.OfType<TextBox>())
            {
                if (tx_box.Text != "")
                    txbox_active = tx_box;
            }
            return txbox_active;
        }

        public void EnterPressPerformed()
        {
            TabItem selectedTab = tabControl_unitCalculator.SelectedItem as TabItem;
            if (selectedTab.Name == "tab_bits")
            {
                if (btn_bits_calculate.IsEnabled == true)
                {
                    Event_Btn_Click_Calculate_Bits();
                }
                else
                {
                    Clear_Txbox_UnitC_Bits();
                }
            }
            else if (selectedTab.Name == "tab_systems")
            {
                if (btn_systems_calculate.IsEnabled == true)
                {
                    Event_Btn_Click_Calculate_Systems();
                }
                else
                {
                    Clear_Txbox_UnitC_Systems();
                }
            }
        }

        private void TextBoxBrush(string string_brush, TextBox textBox)
        {
            var converter = new BrushConverter();
            var brush = (Brush)converter.ConvertFromString(string_brush);
            textBox.Background = brush;
        }

        #endregion ##########################################################################################################################################################################################

        #region Methods Bits ################################################################################################################################################################################

        public void Event_Btn_Click_Calculate_Bits()
        {
            if (CanWeStart_bits() == true)
            {
                unitCalculatorControl = new UnitCalculatorControl();
                txbox_active = Active_TextBox();
                bitByteStrings = unitCalculatorControl.CalculateBits(txbox_active);
                if (bitByteStrings.noError == false)
                {
                    System.Windows.Forms.MessageBox.Show(Errorcodes.UNITC_BITS_RANGE_EXCEPTION);
                }
                else
                {
                    DisplayUnitCalculator_BitsBytes();
                }
            }
        }

        //Abfragen, ob die Eingaben korrekt sind.
        private bool CanWeStart_bits()
        {
            bool letUsStart = false;
            int i = 0;
            //Wenn Textbox ausgefüllt, dann erhöhe i
            foreach (TextBox tx_box in grid_unitCalculator_bits.Children.OfType<TextBox>())
            {
                if (tx_box.Text != "")
                    i++;
            }
            //Es darf nur genau eine Box ausgefüllt sein und es muss ein numerischer Wert sein
            txbox_active = Active_TextBox();
            if (i == 1 && control.CheckTextboxIfNumeric(txbox_active) == true)
            {
                letUsStart = true;
            }
            else
            {
                //Fehlermeldung
                System.Windows.Forms.MessageBox.Show(Errorcodes.UNITC_BITS_NO_VALID_INPUT);
            }
            return letUsStart;
        }


        public void DisplayUnitCalculator_BitsBytes()
        {
            txbox_bit.Text                  = bitByteStrings._bit;
            txbox_kilobit.Text              = bitByteStrings.kiloBit;
            txbox_megabit.Text              = bitByteStrings.megaBit;
            txbox_gigabit.Text              = bitByteStrings.gigaBit;
            txbox_terabit.Text              = bitByteStrings.teraBit;
            txbox_byte.Text                 = bitByteStrings._byte;
            txbox_kilobyte.Text             = bitByteStrings.kiloByte;
            txbox_megabyte.Text             = bitByteStrings.megaByte;
            txbox_gigabyte.Text             = bitByteStrings.gigaByte;
            txbox_terabyte.Text             = bitByteStrings.teraByte;
            btn_bits_calculate.IsEnabled    = false;

            foreach (TextBox tx_box in grid_unitCalculator_bits.Children.OfType<TextBox>())
            {
                tx_box.IsReadOnly = true;
                TextBoxBrush("#CCCCCCCC", tx_box);
            }
        }

        //Textboxen zurücksetzen Bits
        private void Clear_Txbox_UnitC_Bits()
        {
            foreach (TextBox tx_box in grid_unitCalculator_bits.Children.OfType<TextBox>())
            {
                tx_box.Clear();
                tx_box.IsReadOnly = false;
            }
            btn_bits_calculate.IsEnabled = true;
        }

        #endregion ##########################################################################################################################################################################################

        #region Methods Systems #############################################################################################################################################################################

        public void Event_Btn_Click_Calculate_Systems()
        {
            if (CanWeStart_Systems() == true)
            {
                unitCalculatorControl = new UnitCalculatorControl();
                txbox_active = Active_TextBox();
                string dataType = WhichDataType();
                systems = unitCalculatorControl.CalculateSystems(txbox_active, dataType);
                WhichDataType();
                if (systems.noError == false)
                {
                    System.Windows.Forms.MessageBox.Show(Errorcodes.UNITC_SYSTEMS_RANGE_EXCEPTION);
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
            foreach (TextBox tx_box in grid_unitCalculator_systems.Children.OfType<TextBox>())
            {
                if (tx_box.Text != "")
                    i++;
            }
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
                (Errorcodes.UNITC_SYSTEMS_NO_VALID_INPUT);
            }
            return letUsStart;
        }

        public string WhichDataType()
        {
            string dataType = combobox_system.SelectedItem.ToString();
            string output = null;
            switch (dataType)
            {
                case "System.Windows.Controls.ComboBoxItem: Int16": output = "Int16"; break;
                case "System.Windows.Controls.ComboBoxItem: Int32": output = "Int32"; break;
                case "System.Windows.Controls.ComboBoxItem: Int64": output = "Int64"; break;
            }
            return output;
        }

        public void DisplayUnitCalculator_Systems()
        {
            txbox_binaer.Text                   = systems.binaer;
            txbox_oktal.Text                    = systems.oktal;
            txbox_dezimal.Text                  = systems.dezimal;
            txbox_hexadezimal.Text              = systems.hexadezimal;
            btn_systems_calculate.IsEnabled     = false;
            combobox_system.IsEnabled           = false;

            foreach (TextBox tx_box in grid_unitCalculator_systems.Children.OfType<TextBox>())
            {
                tx_box.IsReadOnly = true;
                TextBoxBrush("#CCCCCCCC", tx_box);
            }
        }

        //Textboxen zurücksetzen Systeme
        private void Clear_Txbox_UnitC_Systems()
        {
            foreach (TextBox tx_box in grid_unitCalculator_systems.Children.OfType<TextBox>())
            {
                tx_box.Clear();
                tx_box.IsReadOnly = false;
            }
            btn_systems_calculate.IsEnabled = true;
            combobox_system.IsEnabled = true;
        }

        #endregion ##########################################################################################################################################################################################

        #region Initialisiation #############################################################################################################################################################################

        //Initialsieren der TextBox Tags. Für die Prüfung, welcher Wert als Eingabe erlaubt wird
        private void InitializeTags()
        {
            foreach (TextBox tx_box in grid_unitCalculator_bits.Children.OfType<TextBox>())
            {
                tx_box.Tag = Control.Control.digitTag.SIGNEDFLOAT;
            }
            txbox_binaer.Tag        = Control.Control.digitTag.UNSIGNEDBINARY;
            txbox_oktal.Tag         = Control.Control.digitTag.UNSIGNEDOCTAL;
            txbox_dezimal.Tag       = Control.Control.digitTag.DECIMAL;
            txbox_hexadezimal.Tag   = Control.Control.digitTag.UNSIGNEDHEX;
        }

        private void InitializeContent()
        {
            help_bits.Text      = Help.HELP_UNITC_BITS;
            help_systems.Text   = Help.HELP_UNITC_SYSTEMS;
        }

        private void InitializeEvents()
        {
            //Abfrage Textbox geändert für die Färbung bei Falshceingabe
            foreach (TextBox tx_box in grid_unitCalculator_bits.Children.OfType<TextBox>())
            {
                tx_box.TextChanged += Txbox_UnitCalculator_TextChanged;
            }
            foreach (TextBox tx_box in grid_unitCalculator_systems.Children.OfType<TextBox>())
            {
                tx_box.TextChanged += Txbox_UnitCalculator_TextChanged;
            }
        }

        #endregion ##########################################################################################################################################################################################

        #region Events ######################################################################################################################################################################################

        //Beim Schließen die Parameter über die Position zum Öffnen des Hauptfensters übergeben
        private void Window_Closed(object sender, EventArgs e)
        {
            origin.Show();
            origin.Left         = this.Left;
            origin.Top          = this.Top;
        }

        //Modul über eigenen Button schließen
        private void Btn_Exit_Click(object sender, RoutedEventArgs e) => Close();

        //Alle Werte löschen Bits
        private void Btn_Bits_Reset_Click(object sender, RoutedEventArgs e) => Clear_Txbox_UnitC_Bits();
        //Alle Werte löschen Systeme
        private void Btn_Systems_Reset_Click(object sender, RoutedEventArgs e) => Clear_Txbox_UnitC_Systems();

        //Werte berechen Bits bei Button Klick
        private void Btn_Bits_Calculate_Click(object sender, RoutedEventArgs e) => Event_Btn_Click_Calculate_Bits();
        //Werte berechen Zahlensysteme
        private void Btn_Systems_Calculate_Click(object sender, RoutedEventArgs e) => Event_Btn_Click_Calculate_Systems();
        //Werte berechnen Bits bei Enter gedrückt
        private void Enterpressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                EnterPressPerformed();
        }

        //Ist die Eingabe numerisch?
        private void Txbox_UnitCalculator_TextChanged(object sender, TextChangedEventArgs e) => control.CheckTextboxIfNumeric((TextBox)e.Source);

        //Tab beim wechseln zurücksetzen
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tab_bits.IsSelected)
                Clear_Txbox_UnitC_Systems();
            if (tab_systems.IsSelected)
                Clear_Txbox_UnitC_Bits();
        }

        #endregion ##########################################################################################################################################################################################
    }
}
