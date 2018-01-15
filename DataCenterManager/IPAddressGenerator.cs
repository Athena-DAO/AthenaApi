using DataCenterManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataCenterManager.Data;

namespace DataCenterManager
{
    public class IPAddressGenerator : IIPAddressGenerator
    {
        private IPAddressSeries _IPAddressSeries;

        public IPAddressSeries IPAddressSeries
        {
            get
            {
                return _IPAddressSeries;
            }
            set
            {
                _IPAddressSeries = value;
            }
        }

        public IEnumerable<IPAddress> GetIPAddresses()
        {
            throw new NotImplementedException();
        }
    }
}
