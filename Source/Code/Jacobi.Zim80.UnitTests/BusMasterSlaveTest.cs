﻿using FluentAssertions;
using Jacobi.Zim80.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Zim80.UnitTests
{
    [TestClass]
    public class BusMasterSlaveTest
    {
        [TestMethod]
        public void Write_LeftToRight()
        {
            var ms1 = new BusMasterSlave<BusData8>();
            var ms2 = new BusMasterSlave<BusData8>();

            var connection = ms1.ConnectTo(ms2.Slave);
            var value = new BusData8(0x55);

            ms1.IsEnabled = true;
            ms1.Write(value);
            ms2.Slave.Value.Should().Be(value);
        }

        [TestMethod]
        public void Write_RightToLeft()
        {
            var ms1 = new BusMasterSlave<BusData8>();
            var ms2 = new BusMasterSlave<BusData8>();

            var connection = ms1.ConnectTo(ms2.Slave);
            var value = new BusData8(0x55);

            ms2.IsEnabled = true;
            ms2.Write(value);
            ms1.Slave.Value.Should().Be(value);
        }
    }
}
