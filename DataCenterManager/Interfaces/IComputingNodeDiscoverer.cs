using DataCenterManager.Data;
using System.Collections.Generic;

namespace DataCenterManager.Interfaces
{
    public interface IComputingNodeDiscoverer
    {
        List<ComputingNode> GetAllComputingNodes(IHandshake handshake, IIPAddressGenerator addressGenerator);
    }
}
