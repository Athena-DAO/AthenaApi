using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterManager.Interfaces
{
    public interface ICommunicationFramework
    {
        void Connect(string IPAddress, int port);
        void SendMessage(string data);
        string RecieveMessage();
        void SendFile(string fileLocation);
        string RecieveFile();
        void Close();
    }
}
