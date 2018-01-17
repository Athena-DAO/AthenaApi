using DataCenterManager.Interfaces;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
