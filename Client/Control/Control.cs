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

        public void checkTextboxIfNumeric(System.Windows.Controls.TextBox box)
        {
            int i = 0;
            foreach (char x in box.Text)
            {
                if (!Char.IsDigit(x))
                {
                    i++;
                }
            }
            if (i == 0)
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FFFFFFFF"); //#FFFFFFFF white
                box.Background = brush;
                
            }
            else
            {
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FFFF0000"); //#FFFF0000 red
                box.Background = brush;
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
