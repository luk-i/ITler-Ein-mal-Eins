using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ITler_Ein_mal_Eins.Control
{
    public class Control
    {
        public Control()
        {

        }

        public enum digitTag
        {
            UNSIGNEDINTEGER,
            SIGNEDFLOAT,
            BYTE,
            UNSIGNEDBINARY,
            UNSIGNEDOCTAL,
            DECIMAL,
            UNSIGNEDHEX,
            SUBNETMASK
        };

        // Funktion sieht nach, ob sich eine Eingabe in einem Textfeld um eine legitime Zahl handelt.
        // Wenn nicht, wird das Feld rot gefärbt und false zurückgegeben
        public bool CheckTextboxIfNumeric(System.Windows.Controls.TextBox box)
        {
            int noDigit = 0;
            int comma = 0;
            int place = 0;
            int signed = 0;

            foreach (char x in box.Text)
            {
                switch (box.Tag)
                {
                    case digitTag.UNSIGNEDINTEGER:
                        if (!Char.IsDigit(x))
                        {
                            noDigit++;
                        }
                        break;
                    case digitTag.SIGNEDFLOAT:                        
                        if (!Char.IsDigit(x))
                        {
                            if (x == '.' || x == ',')
                            {
                                comma++;
                                if ((comma > 1 || place == 0) || (signed == 1 && place == 1))
                                {
                                    noDigit++;
                                }
                            }
                            else if (x == '-' || x == '+')
                            {
                                if (place == 0)
                                {
                                    signed = 1;
                                }
                                else
                                {
                                    noDigit++;
                                }
                            }
                            else
                            {
                                noDigit++;
                            }
                        }
                        place++;
                        break;
                    case digitTag.BYTE:
                        if (!Char.IsDigit(x))
                        {
                            noDigit++;
                        }
                        else
                        {
                            if (!IpCalculator.isIpV4Digit(box, false))
                                noDigit++;
                        }
                        break;
                    case digitTag.UNSIGNEDBINARY:
                        if (!Char.IsDigit(x))
                        {
                            noDigit++;
                        }
                        else if (x != '0' && x != '1')
                        {
                            noDigit++;
                        }
                        place++;
                        break;
                    case digitTag.UNSIGNEDOCTAL:
                        if (!Char.IsDigit(x))
                        {
                            noDigit++;
                        }               
                        else if (x == '8' || x == '9')
                        {
                            noDigit++;
                        }
                        place++;
                        break;
                    case digitTag.DECIMAL:
                        if (!Char.IsDigit(x))
                        {
                            if (x == '-' || x == '+')
                            {
                                if (place != 0)
                                    noDigit++;
                            }
                            else
                            {
                                noDigit++;
                            }
                        }
                        place++;
                        break;
                    case digitTag.UNSIGNEDHEX:
                        if (!Char.IsDigit(x))
                        {
                            if (x != 'A' && x != 'B' && x != 'C' && x != 'D' && x != 'E' && x != 'F' && x != 'a' && x != 'b' && x != 'c' && x != 'd' && x != 'e' && x != 'f')
                            {
                                noDigit++;
                            }
                        }
                        place++;
                        break;
                    case digitTag.SUBNETMASK:
                        if (!Char.IsDigit(x))
                        {
                            noDigit++;
                        }
                        else
                        {
                            if (!IpCalculator.isIpV4Digit(box, true))
                                noDigit++;
                        }
                        break;

                }
                
            }
            return brushTextBoxByBool(noDigit, box);

        }

        public bool brushTextBoxByBool(int _noDigit, System.Windows.Controls.TextBox _box)
        {
            if (_noDigit == 0)
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FFFFFFFF"); //#FFFFFFFF white
                _box.Background = brush;
                return true;
            }
            else
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FFFF0000"); //#FFFF0000 red
                _box.Background = brush;
                return false;
            }
        }

// Hier gab es Probleme mit dem Parsen! So ab zehn bis fünzehn Stellen war Schluss und es wurde false zurückgeliefert, trotz int64!
// Darum obige Version, ich lass es aber hier auskommentiert stehen, bis ich das mit dir besprochen habe.

        /*        public void checkTextboxIfNumeric(System.Windows.Controls.TextBox box)
        {
            Int64 tmp;
            if(Int64.TryParse(box.Text, out tmp) || String.IsNullOrEmpty(box.Text)){
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FFFFFFFF");
                box.Background = brush;
                //#FFFF0000 red
                //#FFFFFFFF white
            }
            else
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FFFF0000");
                box.Background = brush;
            }
        }
*/
    }
}
