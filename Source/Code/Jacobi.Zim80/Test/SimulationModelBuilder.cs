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

        public virtual void New()
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
        public void AddLogicAnalyzer(bool start = true)
        {
            Model.LogicAnalyzer = new LogicAnalyzer();
            Model.LogicAnalyzer.Clock.ConnectTo(Model.Cpu.Clock.DigitalSignal);

            foreach (var input in Model.Cpu.Inputs())
            {
                if (!input.IsConnected) continue;
                if (input == Model.Cpu.Clock) continue; //skip clock
                Model.LogicAnalyzer.ConnectInput(input.DigitalSignal);
            }

            foreach (var output in Model.Cpu.Outputs())
            {
                if (!output.IsConnected) continue;
                Model.LogicAnalyzer.ConnectInput(output.DigitalSignal);
            }

            foreach (var outputPort in Model.OutputPorts.Values)
            {
                if (!outputPort.PortEnable.IsConnected) continue;
                Model.LogicAnalyzer.ConnectInput(outputPort.PortEnable.DigitalSignal);
            }

            if (Model.Address != null)
                Model.LogicAnalyzer.ConnectInput(Model.Address);

            if (Model.Data != null)
                Model.LogicAnalyzer.ConnectInput(Model.Data);

            if (start)
                Model.LogicAnalyzer.Start();
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

        public OutputPort AddOutputPort(ushort ioAddress, string name = null)
        {
            Bus address = Model.Address;
            Bus data = Model.Data;

            return AddOutputPort(address, data, ioAddress, name);
        }

        public OutputPort AddOutputPort(Bus address, Bus data, ushort ioAddress, string name = null)
        {
            if (name == null) name = string.Empty;

            var decoder = new BusDecoder(address, name + "-OutputAddressDecoder");
            decoder.AddValue(ioAddress);
            AddComponent(decoder);

            var invertor = new InvertorGate() {
                Name = name + "-OutAddressDecodeInvertor"
            };
            AddComponent(invertor);
            decoder.Output.ConnectTo(invertor.Input);

            var or = new OrGate() {
                Name = name + "-OutPortEnableIO"
            };
            or.AddInput().ConnectTo(Model.Cpu.IoRequest);
            or.AddInput().ConnectTo(invertor.Output);
            AddComponent(or);

            var outputPort = new OutputPort(data, name);
            AddOutputPort(outputPort);
            outputPort.PortEnable.ConnectTo(or.Output);
            return outputPort;
        }

        public InputPort AddInputPort(ushort ioAddress, string name = null)
        {
            Bus address = Model.Address;
            Bus data = Model.Data;

            return AddInputPort(address, data, ioAddress, name);
        }

        public InputPort AddInputPort(Bus address, Bus data, ushort ioAddress, string name = null)
        {
            if (name == null) name = string.Empty;

            var decoder = new BusDecoder(address, name + "-InputAddressDecoder");
            decoder.AddValue(ioAddress);
            AddComponent(decoder);

            var invertor = new InvertorGate()
            {
                Name = name + "-InAddressDecodeInvertor"
            };
            AddComponent(invertor);
            decoder.Output.ConnectTo(invertor.Input);

            var or = new OrGate()
            {
                Name = name + "-InPortEnableIO"
            };
            or.AddInput().ConnectTo(Model.Cpu.IoRequest);
            or.AddInput().ConnectTo(invertor.Output);
            AddComponent(or);

            var inputPort = new InputPort(data, name);
            AddInputPort(inputPort);
            inputPort.PortEnable.ConnectTo(or.Output);

            return inputPort;
        }

        public void AddComponent(INamedObject component)
        {
            Model.Components.Add(component.Name, component);
        }

        public void AddOutputPort(OutputPort outputPort)
        {
            Model.OutputPorts.Add(outputPort.Name, outputPort);
        }

        public void AddInputPort(InputPort inputPort)
        {
            Model.InputPorts.Add(inputPort.Name, inputPort);
        }

        public SimulationModel Model { get; protected set; }

        private void SetModelAddressAndDataBus()
        {
            if (Model.Cpu.Address.IsConnected && Model.Address == null)
            {
                Model.Address = Model.Cpu.Address.Bus;
                if (Model.Address.Name == null) Model.Address.Name = "Address";
            }
            if (Model.Cpu.Data.IsConnected && Model.Data == null)
            {
                Model.Data = Model.Cpu.Data.Bus;
                if (Model.Data.Name == null) Model.Data.Name = "Data";
            }
        }
    }
}
