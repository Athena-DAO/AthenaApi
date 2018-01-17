using DataCenterManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterManager
{
    class SocketCommunicationFramework : ICommunicationFramework
    {
        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Connect(string IPAddress, int port)
        {
            throw new NotImplementedException();
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
