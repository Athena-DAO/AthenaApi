using DataCenterManager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterManager.Test
{
    [TestClass]
    public class IPAddressGeneratorUnitTest
    {
        public readonly IIPAddressGenerator _ipAddressGenerator;

        public IPAddressGeneratorUnitTest()
        {
            _ipAddressGenerator = new IPAddressGenerator();
        }

        [TestMethod]
        public void TestGetSetAddressSeries()
        {
            _ipAddressGenerator.IPAddressSeries = Data.IPAddressSeries.OneNinerTwo;

            Assert.AreEqual(_ipAddressGenerator.IPAddressSeries, Data.IPAddressSeries.OneNinerTwo);
        }

        [TestMethod]
        public void TestIPAddressCountInOneNinerTwo()
        {
            _ipAddressGenerator.IPAddressSeries = Data.IPAddressSeries.OneNinerTwo;

            List<Data.IPAddress> IPAddress = new List<Data.IPAddress>(_ipAddressGenerator.GetIPAddresses());

            Assert.AreEqual(IPAddress.Count, 255 * 255);
        }
    }
}
