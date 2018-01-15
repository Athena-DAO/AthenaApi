using System;

namespace DataCenterManager.Exceptions
{
    public class MachineNotAvailableException : Exception
    {
        public MachineNotAvailableException()
        {
        }

        public MachineNotAvailableException(string message) : base("Machine actively refused the connection")
        {
        }

        public MachineNotAvailableException(string message, Exception innerException) : base("Machine actively refused the connection", innerException)
        {
        }
    }
}
