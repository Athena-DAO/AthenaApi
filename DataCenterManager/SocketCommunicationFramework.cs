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
        private IPEndPoint localEndPoint;

        public void Close()
        {
            socket.Close();
        }

        public void Connect(string IPAddress, int port, int timeout)
        {
            IPAddress iPAddress = System.Net.IPAddress.Parse(IPAddress);
            localEndPoint = new IPEndPoint(iPAddress, port);
            socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //IAsyncResult result = socket.BeginConnect(localEndPoint, null, null);
            //bool sucess = result.AsyncWaitHandle.WaitOne(timeout, true);
            //if (sucess)
            //{
            //    socket.EndConnect(result);
            //    socket.Bind(localEndPoint);
            //}
            //else
            //{
            //    socket.Close();
            //    throw new Exceptions.MachineNotAvailableException();
            //}
        }

        public string RecieveFile()
        {
            throw new NotImplementedException();
        }

        public string RecieveMessage()
        {
            string message = "";
            byte[] bytes = new byte[255];
            string data = null;

            socket.Listen(10);
            socket.Bind(localEndPoint);
            Socket handler = socket.Accept();
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
            return message;
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
