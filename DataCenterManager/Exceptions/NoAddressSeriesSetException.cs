using System;

namespace DataCenterManager.Exceptions
{
    public class NoAddressSeriesSetException : Exception
    {
        public NoAddressSeriesSetException()
        {
        }

        public NoAddressSeriesSetException(string message) : base("No IP address series is set")
        {
        }

        public NoAddressSeriesSetException(string message, Exception innerException) : base("No IP address series is set", innerException)
        {
        }
    }
}
