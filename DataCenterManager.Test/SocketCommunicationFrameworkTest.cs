using DataCenterManager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCenterManager.Test
{
    [TestClass]
    public class SocketCommunicationFrameworkTest
    {
        private ICommunicationFramework communicationFramework;

        public SocketCommunicationFrameworkTest()
        {
            communicationFramework = new SocketCommunicationFramework();
        }


    }
}
