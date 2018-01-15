using DataCenterManager.Interfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DataCenterManager
{
    public class SimpleHandshaker : IHandshake
    {
        public void PerformHandshake(Data.IPAddress iPAddress)
        {
            byte[] bytes = new byte[1024];
            IPAddress myIPAddress = IPAddress.Parse(iPAddress.FirstOctet + "." + iPAddress.SecondOctet
                + "." + iPAddress.ThridOctet + "." + iPAddress.FourthOctet);
            IPEndPoint localEndPoint = new IPEndPoint(myIPAddress, 5000);
            Socket socket = new Socket(myIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            PerformStageOne(localEndPoint, socket, out byte[] message, out int bytesSent);

            int numberOfContainers = PerformStageTwo(bytes, socket);

            PerformStageThree(socket, out message, out bytesSent);

            socket.Shutdown(SocketShutdown.Send);
            socket.Close();
        }

        private static void PerformStageThree(Socket socket, out byte[] message, out int bytesSent)
        {
            message = Encoding.ASCII.GetBytes("Bye<EOF>");
            bytesSent = socket.Send(message);
        }

        private static int PerformStageTwo(byte[] bytes, Socket socket)
        {
            int bytesRecieved = socket.Receive(bytes);
            int numberOfContainer = int.Parse(Encoding.ASCII.GetString(bytes, 0, bytesRecieved));
            return numberOfContainer;
        }

        private static void PerformStageOne(IPEndPoint localEndPoint, Socket socket, out byte[] message, out int bytesSent)
        {
            socket.Connect(localEndPoint);
            message = Encoding.ASCII.GetBytes("Hello<EOF>");
            bytesSent = socket.Send(message);
        }
    }
}
