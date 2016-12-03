using System;
using Jacobi.Zim80.Logic;
using Jacobi.Zim80.Memory;

namespace Jacobi.Zim80.Test
{
    public class SimulationModelBuilder
    {
        public SimulationModelBuilder()
        {
            New();
        }

        public void New()
        {
            Model = new SimulationModel();
            Model.Cpu = new CpuZ80.CpuZ80();
        }

        public void AddCpuMemory()
        {
            Model.Memory = new MemoryRam<BusData16, BusData8>();
            Model.Cpu.MemoryRequest.ConnectTo(Model.Memory.ChipEnable);
            Model.Cpu.Read.ConnectTo(Model.Memory.OutputEnable);
            Model.Cpu.Write.ConnectTo(Model.Memory.WriteEnable);
            Model.Cpu.Address.ConnectTo(Model.Memory.Address);
            Model.Cpu.Data.ConnectTo(Model.Memory.Data.Slave);

            SetModelAddressAndDataBus();
        }

        public void AddCpuClockGen()
        {
            Model.ClockGen = new SignalGenerator();
            Model.Cpu.Clock.ConnectTo(Model.ClockGen.Output);
        }

        // Auto-connects to all connected signals of the Cpu.
        public void AddLogicAnalyzer()
        {
            Model.LogicAnalyzer = new LogicAnalyzer();

            foreach (var input in Model.Cpu.Inputs())
            {
                if (!input.IsConnected) continue;
                Model.LogicAnalyzer.ConnectInput(input.DigitalSignal);
            }

            foreach (var output in Model.Cpu.Outputs())
            {
                if (!output.IsConnected) continue;
                Model.LogicAnalyzer.ConnectInput(output.DigitalSignal);
            }

            if (Model.Address != null)
                Model.LogicAnalyzer.ConnectInput(Model.Address);

            if (Model.Data != null)
                Model.LogicAnalyzer.ConnectInput(Model.Data);
        }

        public void AddCpuIoSpace()
        {
            Model.IoSpace = new MemoryRam<BusData16, BusData8>();
            Model.Cpu.IoRequest.ConnectTo(Model.IoSpace.ChipEnable);
            Model.Cpu.Read.ConnectTo(Model.IoSpace.OutputEnable);
            Model.Cpu.Write.ConnectTo(Model.IoSpace.WriteEnable);
            Model.Cpu.Address.ConnectTo(Model.IoSpace.Address);
            Model.Cpu.Data.ConnectTo(Model.IoSpace.Data.Slave);

            SetModelAddressAndDataBus();
        }

        public SimulationModel Model { get; private set; }

        private void SetModelAddressAndDataBus()
        {
            if (Model.Cpu.Address.IsConnected && Model.Address == null)
                Model.Address = Model.Cpu.Address.Bus;
            if (Model.Cpu.Data.IsConnected && Model.Data == null)
                Model.Data = Model.Cpu.Data.Bus;
        }
    }
}
