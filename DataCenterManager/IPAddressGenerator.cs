using DataCenterManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DataCenterManager.Data;

namespace DataCenterManager
{
    class IPAddressGenerator : IIPAddressGenerator
    {
        public IPAddressSeries IPAddressSeries { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IPAddress> GetIPAddresses()
        {
            throw new NotImplementedException();
        }
    }
}
