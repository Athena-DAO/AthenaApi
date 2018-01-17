using DataCenterManager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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

        [TestMethod]
        public void TestRecieve()
        {
            communicationFramework.Connect("127.0.0.1", 5000, -1);

            new Thread(() =>
            {
                IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint localEndPoint = new IPEndPoint(iPAddress, 5000);
                Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(localEndPoint);
                byte[] data = Encoding.ASCII.GetBytes("Hello<EOF>");
                socket.Send(data);
                socket.Close();
            }).Start();

            Assert.AreEqual("Hello", communicationFramework.RecieveMessage());
        }
    }
}
