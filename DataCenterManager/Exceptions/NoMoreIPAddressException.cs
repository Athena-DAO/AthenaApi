using System;

namespace DataCenterManager.Exceptions
{
    public class NoMoreIPAddressException : Exception
    {
        public NoMoreIPAddressException()
        {
        }

        public NoMoreIPAddressException(string message) : base("No more IP addresses available in the current series")
        {
        }

        public NoMoreIPAddressException(string message, Exception innerException) : base("No more IP addresses available in the current series", innerException)
        {
        }
    }
}
