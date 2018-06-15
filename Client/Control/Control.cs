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
            int tmp;
            if(int.TryParse(box.Text, out tmp)){
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

    }
}
