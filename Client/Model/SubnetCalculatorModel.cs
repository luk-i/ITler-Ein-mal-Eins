using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    #endregion
    class SubnetCalculatorModel
    {
    }
}
