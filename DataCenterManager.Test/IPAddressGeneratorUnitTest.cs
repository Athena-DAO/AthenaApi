using DataCenterManager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCenterManager.Test
{
    [TestClass]
    public class IPAddressGeneratorUnitTest
    {
        public readonly IPAddressGenerator _ipAddressGenerator;

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

        [TestMethod]
        [ExpectedException(typeof(Exceptions.NoAddressSeriesSetException))]
        public void TestNoIPAddressSeriesSet()
        {
            IPAddressGenerator ipAddressGenerator = new IPAddressGenerator();

            foreach(var x in ipAddressGenerator.GetIPAddresses())
            {
            }
        }

        [TestMethod]
        public void ChangeIPAddressSeriesMidway()
        {
            _ipAddressGenerator.IPAddressSeries = Data.IPAddressSeries.OneNinerTwo;
            foreach(var x in _ipAddressGenerator.GetIPAddresses())
            {
                Assert.AreEqual(192, x.FirstOctet);
                Assert.AreEqual(168, x.SecondOctet);
                Assert.AreEqual(0, x.ThridOctet);
                Assert.AreEqual(0, x.FourthOctet);
                break;
            }

            _ipAddressGenerator.IPAddressSeries = Data.IPAddressSeries.OneZero;
            foreach (var item in _ipAddressGenerator.GetIPAddresses())
            {
                Assert.AreEqual(10, item.FirstOctet);
                Assert.AreEqual(0, item.SecondOctet);
                Assert.AreEqual(0, item.ThridOctet);
                Assert.AreEqual(0, item.FourthOctet);
                break;
            }
        }
    }
}
