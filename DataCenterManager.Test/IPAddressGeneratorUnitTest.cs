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

            int count = 0;
            foreach(var ipAddress in _ipAddressGenerator.GetIPAddresses())
            {
                count++;
            }

            Assert.AreEqual(65536, count);
        }

        [TestMethod]
        public void TestIPAddressCountInOneSevenTwo()
        {
            _ipAddressGenerator.IPAddressSeries = Data.IPAddressSeries.OneSevenTwo;

            int count = 0;
            foreach (var ipAddress in _ipAddressGenerator.GetIPAddresses())
            {
                count++;
            }

            Assert.AreEqual(1048576, count);
        }

        [TestMethod]
        public void TestIPAddressCountInOneZero()
        {
            _ipAddressGenerator.IPAddressSeries = Data.IPAddressSeries.OneZero;

            int count = 0;
            foreach (var ipAddress in _ipAddressGenerator.GetIPAddresses())
            {
                count++;
            }

            Assert.AreEqual(16777216, count);
        }

        
    }
}
