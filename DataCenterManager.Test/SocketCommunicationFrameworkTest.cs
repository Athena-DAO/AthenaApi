﻿using DataCenterManager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DataCenterManager.Test
{
    [TestClass]
    public class SocketCommunicationFrameworkTest
    {
        private SocketCommunicationFramework communicationFramework;

        public SocketCommunicationFrameworkTest()
        {
            communicationFramework = new SocketCommunicationFramework();
        }

        [TestMethod]
        public void TestSendParameters()
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, Constants.PortNumbers.PARAMETER_EXCHANGE_PORT);
            Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(localEndPoint);
            socket.Listen(10);

            new Thread(() =>
            {
                communicationFramework.SendParameters("communicationParameters", "algorithmParameters", "loggingParameters", "127.0.0.1", Constants.PortNumbers.PARAMETER_EXCHANGE_PORT);
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

            string[] parameters = data.Split("<EOL>");

            Assert.AreEqual("communicationParameters", parameters[0]);
            Assert.AreEqual("algorithmParameters", parameters[1]);
            Assert.AreEqual("loggingParameters", parameters[2]);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();            
        }

        [TestMethod]
        public void TestRecieveResult()
        {
            new Thread(() =>
            {
                IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint localEndPoint = new IPEndPoint(iPAddress, Constants.PortNumbers.PARAMETER_EXCHANGE_PORT);
                Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(localEndPoint);

                byte[] message = Encoding.ASCII.GetBytes("result<EOF>");
                socket.Send(message);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }).Start();

            string result = communicationFramework.RecieveResults("127.0.0.1", Constants.PortNumbers.PARAMETER_EXCHANGE_PORT);

            Assert.AreEqual("result", result);
        }
    }
}
