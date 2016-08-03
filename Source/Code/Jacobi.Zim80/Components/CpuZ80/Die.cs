using System;

namespace Jacobi.Zim80.Components.CpuZ80
{
    internal class Die
    {
        public Die(
            DigitalSignalConsumer clock,
            DigitalSignalConsumer reset,
            DigitalSignalConsumer interrupt,
            DigitalSignalConsumer nonMaskableInterrupt,
            DigitalSignalConsumer wait,
            DigitalSignalConsumer busRequest,
            DigitalSignalProvider busAcknowledge,
            DigitalSignalProvider memoryRequest,
            DigitalSignalProvider ioRequest,
            DigitalSignalProvider read,
            DigitalSignalProvider write,
            DigitalSignalProvider machineCycle1,
            DigitalSignalProvider refresh,
            DigitalSignalProvider halt,
            BusMaster<BusData16> addressBus,
            BusMasterSlave<BusData8> dataBus)
        {
            _clock = clock;
            _reset = reset;
            _interrupt = interrupt;
            _nonMaskableInterrupt = nonMaskableInterrupt;
            _wait = wait;
            _busRequest = busRequest;
            _busAcknowledge = busAcknowledge;
            _memoryRequest = memoryRequest;
            _ioRequest = ioRequest;
            _read = read;
            _write = write;
            _machineCycle1 = machineCycle1;
            _refresh = refresh;
            _halt = halt;
            _addressBus = addressBus;
            _dataBus = dataBus;

            _execEngine = new ExecutionEngine(this);
            _registers = new Registers();

            Initialize();
        }

        public BusMaster<BusData16> AddressBus
        { get { return _addressBus; } }
        public DigitalSignalProvider BusAcknowledge
        { get { return _busAcknowledge; } }
        public DigitalSignalConsumer BusRequest
        { get { return _busRequest; } }
        public DigitalSignalConsumer Clock
        { get { return _clock; } }
        public BusMasterSlave<BusData8> DataBus
        { get { return _dataBus; } }
        public DigitalSignalProvider Halt
        { get { return _halt; } }
        public DigitalSignalConsumer Interrupt
        { get { return _interrupt; } }
        public DigitalSignalProvider IoRequest
        { get { return _ioRequest; } }
        public DigitalSignalProvider MachineCycle1
        { get { return _machineCycle1; } }
        public DigitalSignalProvider MemoryRequest
        { get { return _memoryRequest; } }
        public DigitalSignalConsumer NonMaskableInterrupt
        { get { return _nonMaskableInterrupt; } }
        public DigitalSignalProvider Read
        { get { return _read; } }
        public DigitalSignalProvider Refresh
        { get { return _refresh; } }
        public DigitalSignalConsumer Reset
        { get { return _reset; } }
        public DigitalSignalConsumer Wait
        { get { return _wait; } }
        public DigitalSignalProvider Write
        { get { return _write; } }

        public Registers Registers
        { get { return _registers; } }

        public ExecutionEngine Engine
        {
            get { return _execEngine; }
        }

        #region Private
        private readonly BusMaster<BusData16> _addressBus;
        private readonly DigitalSignalProvider _busAcknowledge;
        private readonly DigitalSignalConsumer _busRequest;
        private readonly DigitalSignalConsumer _clock;
        private readonly BusMasterSlave<BusData8> _dataBus;
        private readonly DigitalSignalProvider _halt;
        private readonly DigitalSignalConsumer _interrupt;
        private readonly DigitalSignalProvider _ioRequest;
        private readonly DigitalSignalProvider _machineCycle1;
        private readonly DigitalSignalProvider _memoryRequest;
        private readonly DigitalSignalConsumer _nonMaskableInterrupt;
        private readonly DigitalSignalProvider _read;
        private readonly DigitalSignalProvider _refresh;
        private readonly DigitalSignalConsumer _reset;
        private readonly DigitalSignalConsumer _wait;
        private readonly DigitalSignalProvider _write;

        private readonly ExecutionEngine _execEngine;
        private readonly Registers _registers;

        private void Initialize()
        {
            // startup state
            _addressBus.IsEnabled = true;
            _busAcknowledge.Write(DigitalLevel.High);
            _halt.Write(DigitalLevel.High);
            _ioRequest.Write(DigitalLevel.High);
            _memoryRequest.Write(DigitalLevel.High);
            _machineCycle1.Write(DigitalLevel.High);
            _refresh.Write(DigitalLevel.High);
            _read.Write(DigitalLevel.High);
            _write.Write(DigitalLevel.High);
        }
        #endregion
    }
}
