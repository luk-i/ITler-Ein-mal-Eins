using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ITler_Ein_mal_Eins.Model
{
    #region Enumerations

    public enum ErrorCodeNo
    {
        NOERROR,
        WRONGIPV4,
        WRONGSUBNETCODE,
        MULTIPLEFIELDSFILLED
    }

    public enum IpV4_FieldStatus
    {
        NOFIELDSFILLED,
        LONGFILLED,
        SHORTFILLED,
        BOTHFILLED
    }

    public enum Textbox_FieldType
    {
        IP_ADDRESSBLOCK,
        DESIRED_SUBNETNO,
        DESIRED_HOSTNO,
        SUBNETMASK_LONG,
        SUBNETMASK_SHORT,
        NEW_SUBNETMASK_LONG,
        NEW_SUBNETMASK_SHORT
    }

    #endregion

    #region Structures

    struct IPAddressTextboxes
    {
        public Textbox_FieldType type;
        public TextBox first;
        public TextBox second;
        public TextBox third;
        public TextBox forth;
        public IPAddressTextboxes(TextBox box1, TextBox box2, TextBox box3, TextBox box4, Textbox_FieldType _FieldType)
        {
            type     = _FieldType;
            first    = box1;
            second   = box2;
            third    = box3;
            forth    = box4;
        }

        public IPAddressTextboxes(TextBox box, Textbox_FieldType _FieldType)
        {
            type = _FieldType;
            first = box;
            second = null;
            third = null;
            forth = null;
        }
    }

    #endregion

    class SubnetCalculatorModel
    {
    }
}
