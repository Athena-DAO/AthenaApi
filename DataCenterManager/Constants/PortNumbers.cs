namespace DataCenterManager.Constants
{
    public sealed class PortNumbers
    {
        private PortNumbers()
        {

        }

        public static int HANDSHAKE_PORT { get { return 5000; } }

        public static int PARAMETER_EXCHANGE_PORT { get { return 5001; } }
    }
}
