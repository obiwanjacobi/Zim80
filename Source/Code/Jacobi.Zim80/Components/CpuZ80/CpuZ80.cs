using System;

namespace Jacobi.Zim80.Components.CpuZ80
{
    public class CpuZ80
    {
        private readonly Die _die;

        public CpuZ80()
        {
            Clock = new DigitalSignalConsumer();

            Reset = new DigitalSignalConsumer();
            Interrupt = new DigitalSignalConsumer();
            NonMaskableInterrupt = new DigitalSignalConsumer();
            Wait = new DigitalSignalConsumer();

            BusRequest = new DigitalSignalConsumer();
            BusAcknowledge = new DigitalSignalProvider();

            MemoryRequest = new DigitalSignalProvider();
            IoRequest = new DigitalSignalProvider();
            Read = new DigitalSignalProvider();
            Write = new DigitalSignalProvider();

            MachineCycle1 = new DigitalSignalProvider();
            Refresh = new DigitalSignalProvider();
            Halt = new DigitalSignalProvider();

            Address = new BusMaster<BusData16>();
            Data = new BusMasterSlave<BusData8>();

            // bond wires
            _die = new Die(
                Clock, Reset, Interrupt, NonMaskableInterrupt, 
                Wait, BusRequest, BusAcknowledge,
                MemoryRequest, IoRequest, Read, Write, 
                MachineCycle1, Refresh, Halt, Address, Data);
        }

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
