using System;

namespace DataCenterManager.Exceptions
{
    public class RougeMachineException : Exception
    {
        public RougeMachineException()
        {
        }

        public RougeMachineException(string message) : base("Rouge machine")
        {
        }

        public RougeMachineException(string message, Exception innerException) : base("Rouge machine", innerException)
        {
        }
    }
}
