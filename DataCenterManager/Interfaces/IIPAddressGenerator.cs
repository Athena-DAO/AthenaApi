using DataCenterManager.Data;
using System.Collections.Generic;

namespace DataCenterManager.Interfaces
{
    public interface IIPAddressGenerator
    {
        IPAddressSeries IPAddressSeries { get; set; }

        IEnumerable<IPAddress> GetIPAddresses();
    }
}
