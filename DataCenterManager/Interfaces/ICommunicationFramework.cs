namespace DataCenterManager.Interfaces
{
    interface ICommunicationFramework
    {
        void SendParameters(string communicationParameters, string algorithmParameters, string loggingParameters, string IPAddress, int port);
        string RecieveResults(string IPAddress, string port);
    }
}
