﻿using DataCenterManager.Constants;
using DataCenterManager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DataCenterManager.Test
{
    [TestClass]
    public class SimpleHandshakerTest
    {
        private SimpleHandshaker _handshaker;

        public SimpleHandshakerTest()
        {
            _handshaker = new SimpleHandshaker();
        }

        [TestMethod]
        public void TestHandshake()
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, 5000);
            Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(localEndPoint);
            socket.Listen(10);

            new Thread(() =>
            {
                _handshaker.PerformHandshake(new Data.IPAddress
                {
                    FirstOctet = 127,
                    SecondOctet = 0,
                    ThridOctet = 0,
                    FourthOctet = 1
                });
            }).Start();

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

        [TestMethod]
        [ExpectedException(typeof(Exceptions.MachineNotAvailableException))]
        public void TestUnavaiableClient()
        {
            _handshaker.PerformHandshake(new Data.IPAddress
            {
                FirstOctet = 127,
                SecondOctet = 0,
                ThridOctet = 0,
                FourthOctet = 1
            });
        }

        [TestMethod]
        public void TestRougeMachine()
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, PortNumbers.HANDSHAKE_PORT);
            Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(localEndPoint);
            socket.Listen(10);

            new Thread(() =>
            {
                _handshaker.PerformHandshake(new Data.IPAddress
                {
                    FirstOctet = 127,
                    SecondOctet = 0,
                    ThridOctet = 0,
                    FourthOctet = 1
                });
            }).Start();

            Socket handler = socket.Accept();
            byte[] bytes = new byte[1024];
            string data = null;
            int byteRecieved = handler.Receive(bytes);
            data += Encoding.ASCII.GetString(bytes, 0, byteRecieved);

            byte[] numberOfContainers = Encoding.ASCII.GetBytes("Hackerman");
            handler.Send(numberOfContainers);

            bytes = new byte[255];
            byteRecieved = handler.Receive(bytes);
            Assert.AreEqual(0, byteRecieved);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
    }
}