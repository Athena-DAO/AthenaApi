﻿using DataCenterManager.Constants;
using DataCenterManager.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DataCenterManager
{
    public class SimpleHandshaker : IHandshake
    {
        public int PerformHandshake(Data.IPAddress iPAddress, int timeout = -1)
        {
            byte[] bytes = new byte[1024];
            IPAddress myIPAddress = IPAddress.Parse(iPAddress.ToString());
            IPEndPoint localEndPoint = new IPEndPoint(myIPAddress, PortNumbers.HANDSHAKE_PORT);
            Socket socket = new Socket(myIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            PerformStageOne(localEndPoint, socket, timeout);

            int numberOfContainers = 0;
            try
            {
                numberOfContainers = PerformStageTwo(bytes, socket);
            }
            catch (Exceptions.RougeMachineException e)
            {
                socket.Shutdown(SocketShutdown.Send);
                socket.Close();
                throw e;
            }

            PerformStageThree(socket);

            socket.Shutdown(SocketShutdown.Send);
            socket.Close();

            return numberOfContainers;
        }

        private static void PerformStageThree(Socket socket)
        {
            byte[] message = Encoding.ASCII.GetBytes("Bye<EOF>");
            socket.Send(message);
        }

        private static int PerformStageTwo(byte[] bytes, Socket socket)
        {
            int bytesRecieved = socket.Receive(bytes);
            if (!int.TryParse(Encoding.ASCII.GetString(bytes, 0, bytesRecieved), out int numberOfContainer))
            {
                throw new Exceptions.RougeMachineException();
            }

            return numberOfContainer;
        }

        private static void PerformStageOne(IPEndPoint localEndPoint, Socket socket, int timeout)
        {
            try
            {
                IAsyncResult result = socket.BeginConnect(localEndPoint, null, null);

                bool sucess = result.AsyncWaitHandle.WaitOne(timeout, true);
                if(sucess)
                {
                    socket.EndConnect(result);
                }
                else
                {
                    socket.Close();
                    throw new Exceptions.MachineNotAvailableException();
                }
            }
            catch (SocketException)
            {
                throw new Exceptions.MachineNotAvailableException();
            }

            byte[] message = Encoding.ASCII.GetBytes("Hello<EOF>");
            socket.Send(message);
        }
    }
}
