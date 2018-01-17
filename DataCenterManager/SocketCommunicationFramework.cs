using DataCenterManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DataCenterManager
{
    public class SocketCommunicationFramework : ICommunicationFramework
    {
        private Socket socket;

        public void Close()
        {
            socket.Close();
        }

        public void Connect(string IPAddress, int port)
        {
            IPAddress iPAddress = System.Net.IPAddress.Parse(IPAddress);
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, port);
            socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public string RecieveFile()
        {
            throw new NotImplementedException();
        }

        public string RecieveMessage()
        {
            throw new NotImplementedException();
        }

        public void SendFile(string fileLocation)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string data)
        {
            throw new NotImplementedException();
        }
    }
}
