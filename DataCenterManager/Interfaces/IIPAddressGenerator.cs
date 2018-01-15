using DataCenterManager.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterManager.Interfaces
{
    public interface IIPAddressGenerator
    {
        IPAddressSeries IPAddressSeries { get; set; }

        IEnumerable<IPAddress> GetIPAddresses();
    }
}
