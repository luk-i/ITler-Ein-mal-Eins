using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ITler_Ein_mal_Eins.Control
{
    class Control
    {
        public Control()
        {

        }

        public void checkTextboxIfNumeric(System.Windows.Controls.TextBox box)
        {
            int tmp;
            if(int.TryParse(box.Text, out tmp){
                var converter = new BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FFFFFF90");
                box.Background = brush;
            }
            else
            {

            }
        }

    }
}
