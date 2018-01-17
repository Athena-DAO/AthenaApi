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
        public string RecieveResults(string IPAddress, int port)
        {
            IPAddress iPAddress = System.Net.IPAddress.Parse(IPAddress);
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, port);
            Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(localEndPoint);
            socket.Listen(1);

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

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

            return data;
        }

        public void SendParameters(string communicationParameters, string algorithmParameters, string loggingParameters, string IPAddress, int port)
        {
            IPAddress iPAddress = System.Net.IPAddress.Parse(IPAddress);
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, port);
            Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(localEndPoint);

            byte[] message = Encoding.ASCII.GetBytes(communicationParameters + "<EOL>" + algorithmParameters + "<EOL>" + loggingParameters + "<EOF>");
            socket.Send(message);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
