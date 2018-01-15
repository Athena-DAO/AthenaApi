using DataCenterManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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
                            throw new NotImplementedException();
                            break;
                        case IPAddressSeries.OneZero:
                            throw new NotImplementedException();
                            break;
                    }
                }catch(NoMoreIPAddressException)
                {
                    break;
                }
                yield return currentIPAddress;
            }

        }


        private void UpdateCurrentIPAddressInOneNinerTwo()
        {
            if(currentIPAddress == null)
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
                if(currentIPAddress.FourthOctet == 255 && currentIPAddress.ThridOctet == 255)
                {
                    throw new NoMoreIPAddressException();
                }
                else if(currentIPAddress.FourthOctet == 255 && currentIPAddress.ThridOctet != 255)
                {
                    currentIPAddress.FourthOctet = 0;
                    currentIPAddress.ThridOctet++;
                }
                else if(currentIPAddress.FourthOctet != 255)
                {
                    currentIPAddress.FourthOctet++;
                }
            }
        }
    }
}
