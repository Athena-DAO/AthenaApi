using DataCenterManager.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCenterManager.Test
{
    [TestClass]
    class IPAddressGeneratorUnitTest
    {
        public readonly IIPAddressGenerator _ipAddressGenerator;

        public IPAddressGeneratorUnitTest()
        {
            _ipAddressGenerator = new IPAddressGenerator();
        }

        
    }
}
