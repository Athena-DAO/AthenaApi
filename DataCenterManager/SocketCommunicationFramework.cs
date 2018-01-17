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
        public string RecieveResults(string IPAddress, string port)
        {
            throw new NotImplementedException();
        }

        public void SendParameters(string communicationParameters, string algorithmParameters, string loggingParameters, string IPAddress, int port)
        {
            IPAddress iPAddress = System.Net.IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, Constants.PortNumbers.PARAMETER_EXCHANGE_PORT);
            Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(localEndPoint);

            byte[] message = Encoding.ASCII.GetBytes(communicationParameters + "<EOL>" + algorithmParameters + "<EOL>" + loggingParameters + "<EOF>");
            socket.Send(message);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
