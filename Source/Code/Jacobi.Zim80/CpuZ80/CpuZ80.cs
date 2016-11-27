using System;

namespace Jacobi.Zim80.CpuZ80
{
    public class CpuZ80
    {
        private readonly Die _die;

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

            // bond wires
            _die = new Die(
                Clock, Reset, Interrupt, NonMaskableInterrupt, 
                Wait, BusRequest, BusAcknowledge,
                MemoryRequest, IoRequest, Read, Write, 
                MachineCycle1, Refresh, Halt, Address, Data);
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
        { get { return _die.Registers; } }

        public event EventHandler<InstructionExecutedEventArgs> InstructionExecuted
        {
            add { _die.Engine.InstructionExecuted += value; }
            remove { _die.Engine.InstructionExecuted -= value; }
        }
    }
}
