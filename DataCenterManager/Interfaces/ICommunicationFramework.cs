﻿namespace DataCenterManager.Interfaces
{
    public interface ICommunicationFramework
    {
        void SendParameters(string communicationParameters, string algorithmParameters, string loggingParameters, string IPAddress, int port);
        string RecieveResults(string IPAddress, int port);
    }
}
