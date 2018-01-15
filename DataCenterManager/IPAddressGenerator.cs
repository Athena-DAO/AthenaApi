using DataCenterManager.Interfaces;
using System.Collections.Generic;
using DataCenterManager.Data;
using DataCenterManager.Exceptions;

namespace DataCenterManager
{
    public class IPAddressGenerator : IIPAddressGenerator
    {
        private IPAddress currentIPAddress;

        private IPAddressSeries _IPAddressSeries;
        public IPAddressSeries IPAddressSeries
        {
            get
            {
                return _IPAddressSeries;
            }
            set
            {
                if(value != _IPAddressSeries)
                {
                    currentIPAddress = null;
                }

                _IPAddressSeries = value;
            }
        }

        public IEnumerable<IPAddress> GetIPAddresses()
        {
            while (true)
            {
                try
                {
                    switch (_IPAddressSeries)
                    {
                        case IPAddressSeries.OneNinerTwo:
                            UpdateCurrentIPAddressInOneNinerTwo();
                            break;
                        case IPAddressSeries.OneSevenTwo:
                            UpdateCurrentIPAddressInOneSevenTwo();
                            break;
                        case IPAddressSeries.OneZero:
                            UpdateCurrentIPAddressInOneZero();
                            break;
                        case IPAddressSeries.None:
                            throw new NoAddressSeriesSetException();
                    }
                }
                catch (NoMoreIPAddressException)
                {
                    break;
                }

                yield return currentIPAddress;
            }
        }


        private void UpdateCurrentIPAddressInOneNinerTwo()
        {
            if (currentIPAddress == null)
            {
                currentIPAddress = new IPAddress
                {
                    FirstOctet = 192,
                    SecondOctet = 168,
                    ThridOctet = 0,
                    FourthOctet = 0
                };
            }
            else
            {
                if (currentIPAddress.FourthOctet == 255 && currentIPAddress.ThridOctet == 255)
                {
                    throw new NoMoreIPAddressException();
                }
                else if (currentIPAddress.FourthOctet == 255 && currentIPAddress.ThridOctet != 255)
                {
                    currentIPAddress.FourthOctet = 0;
                    currentIPAddress.ThridOctet++;
                }
                else if (currentIPAddress.FourthOctet != 255)
                {
                    currentIPAddress.FourthOctet++;
                }
            }
        }

        private void UpdateCurrentIPAddressInOneSevenTwo()
        {
            if (currentIPAddress == null)
            {
                currentIPAddress = new IPAddress
                {
                    FirstOctet = 172,
                    SecondOctet = 16,
                    ThridOctet = 0,
                    FourthOctet = 0
                };
            }
            else
            {
                if (currentIPAddress.SecondOctet == 31 && currentIPAddress.ThridOctet == 255
                    && currentIPAddress.FourthOctet == 255)
                {
                    throw new NoMoreIPAddressException();
                }
                else if (currentIPAddress.ThridOctet == 255 && currentIPAddress.FourthOctet == 255)
                {
                    currentIPAddress.SecondOctet++;
                    currentIPAddress.ThridOctet = 0;
                    currentIPAddress.FourthOctet = 0;
                }
                else if (currentIPAddress.FourthOctet == 255)
                {
                    currentIPAddress.ThridOctet++;
                    currentIPAddress.FourthOctet = 0;
                }
                else
                {
                    currentIPAddress.FourthOctet++;
                }
            }
        }

        private void UpdateCurrentIPAddressInOneZero()
        {
            if (currentIPAddress == null)
            {
                currentIPAddress = new IPAddress
                {
                    FirstOctet = 10,
                    SecondOctet = 0,
                    ThridOctet = 0,
                    FourthOctet = 0
                };
            }
            else
            {
                if (currentIPAddress.SecondOctet == 255 && currentIPAddress.ThridOctet == 255
                    && currentIPAddress.FourthOctet == 255)
                {
                    throw new NoMoreIPAddressException();
                }
                else if (currentIPAddress.ThridOctet == 255 && currentIPAddress.FourthOctet == 255)
                {
                    currentIPAddress.SecondOctet++;
                    currentIPAddress.ThridOctet = 0;
                    currentIPAddress.FourthOctet = 0;
                }
                else if (currentIPAddress.FourthOctet == 255)
                {
                    currentIPAddress.ThridOctet++;
                    currentIPAddress.FourthOctet = 0;
                }
                else
                {
                    currentIPAddress.FourthOctet++;
                }
            }
        }
    }
}
