using DataCenterManager.Interfaces;
using System.Collections.Generic;
using DataCenterManager.Data;
using DataCenterManager.Exceptions;

namespace DataCenterManager
{
    public class SimpleComputingNodeDiscoverer : IComputingNodeDiscoverer
    {
        public List<ComputingNode> GetAllComputingNodes(IHandshake handshake, IIPAddressGenerator addressGenerator, int timeout)
        {
            List<ComputingNode> nodes = new List<ComputingNode>();

            foreach (IPAddress address in addressGenerator.GetIPAddresses())
            {
                int numberOfContainers = 0;
                try
                {
                    numberOfContainers = handshake.PerformHandshake(address, timeout);
                    nodes.Add(new ComputingNode
                    {
                        IPAddress = address.ToString(),
                        NumberOfContainers = numberOfContainers
                    });
                }
                catch(MachineNotAvailableException)
                {

                }
                catch(RougeMachineException)
                {

                }
            }

            return nodes;
        }
    }
}
