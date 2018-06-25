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

        /*
         *   Modus 1: Nur vorzeichenlose Ganzzahlen erlaubt
         *   Modus 2: Gleitkommazahlen exklusive Vorzeichen erlaubt
         *   Modus 3: IP-Segmente - Nur vorzeichenlose Ganzzahlen im Bereich 0-255 erlaubt 
         *   Modus 4: Binär inklusive Komma ohne Vorzeichen
         *   Modus 5: Oktal inklusive Komma ohne Vorzeichen
         *   Modus 6: Dezimal inklusive Komma ohne Vorzeichen
         */   
        public enum digitTag
        {
            UNSIGNEDINTEGER,
            UNSIGNEDFLOAT,
            BYTE,
            BINARYFLOAT,
            OKTAL,
            DECIMAL
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
                    case "1":
                        int ipRange = 0;
                        if (!Char.IsDigit(x))
                        {
                            noDigit++;
                        }
                        break;
                    case "2":                        
                        if (!Char.IsDigit(x))
                        {
                            if (x == '.')
                            {
                                comma++;
                                if ((comma > 1 || place == 0)||(signed == 1 && place == 1))
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
                    case "3":
                        if (!Char.IsDigit(x))
                        {
                            noDigit++;
                        }
                        else
                        {
                            if (Int32.TryParse(box.Text, out ipRange))
                            {
                                if (ipRange < 0 || ipRange > 255)
                                {
                                    noDigit++;
                                }
                            }
                        }
                        break;
                    case "4":
                        if (!Char.IsDigit(x))
                        {
                            if (x == '.')
                            {
                                comma++;
                                if ((comma > 1 || place == 0) || (signed == 1 && place == 1))
                                {
                                    noDigit++;
                                }
                            }
                        }
                        if (x != '0' && x != '1' && x != '.')
                        {
                            noDigit++;
                        }
                        place++;
                        break;
                    case "5":
                        if (!Char.IsDigit(x))
                        {
                            if (x == '.')
                            {
                                comma++;
                                if ((comma > 1 || place == 0) || (signed == 1 && place == 1))
                                {
                                    noDigit++;
                                }
                            }
                            else
                            {
                                noDigit++;
                            }
                        }
                        if (x == '8' || x == '9')
                        {
                            noDigit++;
                        }
                        place++;
                        break;
                    case "6":
                        if (!Char.IsDigit(x))
                        {
                            if (x == '.')
                            {
                                comma++;
                                if ((comma > 1 || place == 0) || (signed == 1 && place == 1))
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
                    case "7":
                        if (!Char.IsDigit(x))
                        {
                            if (x == '.')
                            {
                                comma++;
                                if ((comma > 1 || place == 0) || (signed == 1 && place == 1))
                                {
                                    noDigit++;
                                }
                            }
                            else if (x != 'A' && x != 'B' && x != 'C' && x != 'D' && x != 'E' && x != 'F' && x != 'a' && x != 'b' && x != 'c' && x != 'd' && x != 'e' && x != 'f')
                            {
                                noDigit++;
                            }
                        }
                        place++;
                        break;
                }
            }

            if (noDigit == 0)
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FFFFFFFF"); //#FFFFFFFF white
                box.Background = brush;
                return true;
            }
            else
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FFFF0000"); //#FFFF0000 red
                box.Background = brush;
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
