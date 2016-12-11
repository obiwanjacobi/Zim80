using System;

namespace Jacobi.Zim80.CpuZ80
{
    public class CpuZ80 : INamedObject
    {
        private readonly ExecutionEngine _execEngine;
        private readonly Alu _alu;
        private readonly Registers _registers;

        public CpuZ80()
        {
            Clock = new DigitalSignalConsumer("CLK");

            Reset = new DigitalSignalConsumer("RST");
            Interrupt = new DigitalSignalConsumer("INT");
            NonMaskableInterrupt = new DigitalSignalConsumer("NMI");
            Wait = new DigitalSignalConsumer("WAIT");

            BusRequest = new DigitalSignalConsumer("BREQ");
            BusAcknowledge = new DigitalSignalProvider("BACK");

            MemoryRequest = new DigitalSignalProvider("MREQ");
            IoRequest = new DigitalSignalProvider("IORQ");
            Read = new DigitalSignalProvider("RD");
            Write = new DigitalSignalProvider("WR");

            MachineCycle1 = new DigitalSignalProvider("M1");
            Refresh = new DigitalSignalProvider("RFSH");
            Halt = new DigitalSignalProvider("HALT");

            Address = new BusMaster<BusData16>("Address");
            Data = new BusMasterSlave<BusData8>("Data");

            _execEngine = new ExecutionEngine(this);
            _registers = new Registers();
            _alu = new Alu(_registers);

            Initialize();
        }

        public string Name { get; set; }

        public DigitalSignalConsumer Clock { get; }

        public DigitalSignalConsumer Reset { get; }
        public DigitalSignalConsumer Interrupt { get; }
        public DigitalSignalConsumer NonMaskableInterrupt { get; }
        public DigitalSignalConsumer Wait { get; }

        public DigitalSignalConsumer BusRequest { get; }
        public DigitalSignalProvider BusAcknowledge { get; }

        public DigitalSignalProvider MemoryRequest { get; }
        public DigitalSignalProvider IoRequest { get; }
        public DigitalSignalProvider Read { get; }
        public DigitalSignalProvider Write { get; }

        public DigitalSignalProvider MachineCycle1 { get; }
        public DigitalSignalProvider Refresh { get; }
        public DigitalSignalProvider Halt { get; }

        public BusMaster<BusData16> Address { get; }
        public BusMasterSlave<BusData8> Data { get; }

        public Registers Registers
        { get { return _registers; } }

        internal ExecutionEngine Engine
        { get { return _execEngine; } }

        internal Alu Alu
        { get { return _alu; } }

        public event EventHandler<InstructionExecutedEventArgs> InstructionExecuted
        {
            add { Engine.InstructionExecuted += value; }
            remove { Engine.InstructionExecuted -= value; }
        }

        private void Initialize()
        {
            // startup state
            Address.IsEnabled = true;
            BusAcknowledge.Write(DigitalLevel.High);
            Halt.Write(DigitalLevel.High);
            IoRequest.Write(DigitalLevel.High);
            MemoryRequest.Write(DigitalLevel.High);
            MachineCycle1.Write(DigitalLevel.High);
            Refresh.Write(DigitalLevel.High);
            Read.Write(DigitalLevel.High);
            Write.Write(DigitalLevel.High);
        }
    }
}
