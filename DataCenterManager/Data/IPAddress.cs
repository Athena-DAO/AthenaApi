namespace DataCenterManager.Data
{
    public class IPAddress
    {
        public byte FirstOctet { get; set; }
        public byte SecondOctet { get; set; }
        public byte ThridOctet { get; set; }
        public byte FourthOctet { get; set; }

        public override string ToString()
        {
            return FirstOctet + "." + SecondOctet + "." + ThridOctet + "." + FourthOctet;
        }
    }
}
