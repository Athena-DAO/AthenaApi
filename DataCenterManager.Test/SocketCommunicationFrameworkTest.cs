using DataCenterManager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
