using DataCenterManager.Data;
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
    public class SimpleComputingNodeDiscovererTest
    {
        private SimpleComputingNodeDiscoverer _computingNodeDiscoverer;
        private IPAddressGenerator _addressGenerator;
        private SimpleHandshaker _handshake;

        public SimpleComputingNodeDiscovererTest()
        {
            _computingNodeDiscoverer = new SimpleComputingNodeDiscoverer();
            _addressGenerator = new IPAddressGenerator();
            _handshake = new SimpleHandshaker();
        }

        [TestMethod]
        public void TestNodeDiscovery()
        {
            new Thread(() =>
            {
                CreateNode();
            }).Start();

            _addressGenerator.IPAddressSeries = Data.IPAddressSeries.OneNinerTwo;
            List<ComputingNode> res = _computingNodeDiscoverer.GetAllComputingNodes(_handshake, _addressGenerator, 1000);
            Assert.AreEqual(1, res.Count);
        }

        private void CreateNode()
        {
            System.Net.IPAddress iPAddress = System.Net.IPAddress.Parse("192.168.0.111");
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, 5000);
            Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(localEndPoint);
            socket.Listen(10);

            Socket handler = socket.Accept();
            byte[] bytes = new byte[255];
            string data = null;
            while (true)
            {
                int byteRecieved = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, byteRecieved);
                if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }
            data = data.Replace("<EOF>", "");
            Assert.AreEqual("Hello", data);

            byte[] numberOfContainers = Encoding.ASCII.GetBytes("2");
            handler.Send(numberOfContainers);

            bytes = new byte[255];
            data = null;
            while (true)
            {
                int byteRecieved = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, byteRecieved);
                if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }
            data = data.Replace("<EOF>", "");
            Assert.AreEqual("Bye", data);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
    }
}
