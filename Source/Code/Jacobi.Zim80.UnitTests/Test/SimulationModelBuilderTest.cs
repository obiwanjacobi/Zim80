using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jacobi.Zim80.Test;
using FluentAssertions;

namespace Jacobi.Zim80.UnitTests.Test
{
    [TestClass]
    public class SimulationModelBuilderTest
    {
        [TestMethod]
        public void Ctor_InstantiatesModelAndCpu()
        {
            var uut = new SimulationModelBuilder();

            uut.Model.Should().NotBeNull();
            uut.Model.Cpu.Should().NotBeNull();
        }

        [TestMethod]
        public void AddCpuClockGen_CpuClock_Connects()
        {
            var uut = new SimulationModelBuilder();

            uut.AddCpuClockGen();

            uut.Model.ClockGen.Should().NotBeNull();
            uut.Model.ClockGen.Output.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Clock.IsConnected.Should().BeTrue();
        }

        [TestMethod]
        public void AddCpuMemory_MemoryConnectsToCpu()
        {
            var uut = new SimulationModelBuilder();

            uut.AddCpuMemory();

            uut.Model.Memory.Should().NotBeNull();
            uut.Model.Memory.Address.IsConnected.Should().BeTrue();
            uut.Model.Memory.Data.IsConnected.Should().BeTrue();
            uut.Model.Memory.ChipEnable.IsConnected.Should().BeTrue();
            uut.Model.Memory.OutputEnable.IsConnected.Should().BeTrue();
            uut.Model.Memory.WriteEnable.IsConnected.Should().BeTrue();

            uut.Model.Cpu.Address.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Data.IsConnected.Should().BeTrue();
            uut.Model.Cpu.MemoryRequest.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Read.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Write.IsConnected.Should().BeTrue();

            uut.Model.Address.Should().NotBeNull();
            uut.Model.Data.Should().NotBeNull();
        }

        [TestMethod]
        public void AddCpuIoSpace_IoSpaceConnectsToCpu()
        {
            var uut = new SimulationModelBuilder();

            uut.AddCpuIoSpace();

            uut.Model.IoSpace.Should().NotBeNull();
            uut.Model.IoSpace.Address.IsConnected.Should().BeTrue();
            uut.Model.IoSpace.Data.IsConnected.Should().BeTrue();
            uut.Model.IoSpace.ChipEnable.IsConnected.Should().BeTrue();
            uut.Model.IoSpace.OutputEnable.IsConnected.Should().BeTrue();
            uut.Model.IoSpace.WriteEnable.IsConnected.Should().BeTrue();

            uut.Model.Cpu.Address.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Data.IsConnected.Should().BeTrue();
            uut.Model.Cpu.IoRequest.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Read.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Write.IsConnected.Should().BeTrue();

            uut.Model.Address.Should().NotBeNull();
            uut.Model.Data.Should().NotBeNull();
        }

        [TestMethod]
        public void AddCpuMemoryAndIoSpace_MemoryAndIoSpaceConnectsToCpu()
        {
            var uut = new SimulationModelBuilder();

            uut.AddCpuMemory();
            uut.AddCpuIoSpace();

            uut.Model.Memory.Should().NotBeNull();
            uut.Model.Memory.Address.IsConnected.Should().BeTrue();
            uut.Model.Memory.Data.IsConnected.Should().BeTrue();
            uut.Model.Memory.ChipEnable.IsConnected.Should().BeTrue();
            uut.Model.Memory.OutputEnable.IsConnected.Should().BeTrue();
            uut.Model.Memory.WriteEnable.IsConnected.Should().BeTrue();

            uut.Model.IoSpace.Should().NotBeNull();
            uut.Model.IoSpace.Address.IsConnected.Should().BeTrue();
            uut.Model.IoSpace.Data.IsConnected.Should().BeTrue();
            uut.Model.IoSpace.ChipEnable.IsConnected.Should().BeTrue();
            uut.Model.IoSpace.OutputEnable.IsConnected.Should().BeTrue();
            uut.Model.IoSpace.WriteEnable.IsConnected.Should().BeTrue();

            uut.Model.Cpu.Address.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Data.IsConnected.Should().BeTrue();
            uut.Model.Cpu.IoRequest.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Read.IsConnected.Should().BeTrue();
            uut.Model.Cpu.Write.IsConnected.Should().BeTrue();

            uut.Model.Address.Should().NotBeNull();
            uut.Model.Data.Should().NotBeNull();
        }
    }
}
