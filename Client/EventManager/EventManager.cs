using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ITler_Ein_mal_Eins.EventManager
{
    class EventManager : EventArgs
    {
        #region ENUM
        public enum IP4SubnetAddressType
        {
            NETWORKADDRESS,
            BROADCASTADDRESS
        }
        #endregion

        #region VARIABLES

        public IPAddress Address
        {
            get { return Address; }
            set { this.Address = value; }
        }
        public IPAddress SubnetMask
        {
            get { return SubnetMask; }
            set { this.SubnetMask = value; }
        }
        public IP4SubnetAddressType type
        {
            get { return type; }
            set { this.type = value; }
        }

        public object thisObject
        {
            get { return thisObject; }
            set { this.thisObject = value; }
        }

        #endregion

        #region CLASSES
        public class CalculatedIpV4NetworAddressEventArgs : EventManager
        {

            public CalculatedIpV4NetworAddressEventArgs(IPAddress _NetworkAddress, IPAddress _SubnetMask, IP4SubnetAddressType _type)
            {
                this.Address = _NetworkAddress;
                this.SubnetMask = _SubnetMask;
                this.type = _type;
                this.thisObject = this;
            }
        }

        public class CalculatedIpV4BroadCastAddressEventArgs : EventManager
        {

            public CalculatedIpV4BroadCastAddressEventArgs(IPAddress _BroadCastAddress, IPAddress _SubnetMask, IP4SubnetAddressType _type)
            {
                this.Address = _BroadCastAddress;
                this.SubnetMask = _SubnetMask;
                this.type = _type;
                this.thisObject = this;
            }
        }

        #endregion
    }
}
