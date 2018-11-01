using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ITler_Ein_mal_Eins.Model;


namespace ITler_Ein_mal_Eins.Exceptions
{
    class WrongTypeException : SystemException, ISerializable
    {
        public WrongTypeException()
        {
            MessageBox.Show(Errorcodes.WRONGTYPE_SINGLE, Errorcodes.WRONGTYPE_HEADER);
        }
        public WrongTypeException(IPAddressTextboxes exp_object, Textbox_FieldType expected)
        {
            MessageBox.Show(String.Format(Errorcodes.WRONGTYPE_ERRORMESSAGE, expected, exp_object.type), Errorcodes.WRONGTYPE_HEADER);
        }
        public WrongTypeException(string message)
        {
            throw new NotImplementedException();
        }
        public WrongTypeException(string message, Exception inner)
        {
            throw new NotImplementedException();
        }
        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    throw new NotImplementedException();
        //
        //}
    }

    internal class BaseException
    {
    }
}
