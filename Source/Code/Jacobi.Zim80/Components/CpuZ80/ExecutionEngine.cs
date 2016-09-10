using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using Jacobi.Zim80.Components.CpuZ80.States;
using System;
using System.Collections.Generic;

namespace Jacobi.Zim80.Components.CpuZ80
{
    internal class ExecutionEngine
    {
        private readonly Die _die;
        private readonly CycleCounter _cycles = new CycleCounter();
        private readonly OpcodeBuilder _opcodeBuilder = new OpcodeBuilder();
        private readonly List<CpuInterrupt> _interrupts = new List<CpuInterrupt>();
        private CpuState _state;
        private CpuStates _currentState;

        public ExecutionEngine(Die die)
        {
            _die = die;
            StartFetch();

            // the engine that drives it all
            _die.Clock.OnChanged += Clock_OnChanged;
            _die.NonMaskableInterrupt.OnChanged += NonMaskableInterupt_OnChanged;
            _die.Interrupt.OnChanged += Interrupt_OnChanged;
        }

        public event EventHandler<InstructionExecutedEventArgs> InstructionExecuted;

        public Die Die { get { return _die; } }

        public CycleCounter Cycles { get { return _cycles; } }

        public bool AddOpcodeByte(OpcodeByte opcodeByte)
        {
            var valid = _opcodeBuilder.Add(opcodeByte);

            if (valid)
                _cycles.OpcodeDefinition = _opcodeBuilder.Opcode.Definition;

            return valid;
        }

        public bool IsOpcodeComplete
        {
            get { return _opcodeBuilder.IsDone; }
        }

        public Opcode Opcode
        {
            get { return _opcodeBuilder.Opcode; }
        }

        public SingleByteOpcode SingleCycleOpcode
        {
            get { return _opcodeBuilder.Opcode as SingleByteOpcode; }
        }

        public MultiByteOpcode MultiCycleOpcode
        {
            get { return _opcodeBuilder.Opcode as MultiByteOpcode; }
        }

        public void SetPcOnAddressBus()
        {
            SetAddressBus(_die.Registers.PC);
            _die.Registers.PC++;
        }

        public void SetRefreshOnAddressBus()
        {
            SetAddressBus(_die.Registers.IR);
        }

        public void SetAddressBus(UInt16 address)
        {
            _die.AddressBus.Write(new BusData16(address));
        }

        private void Clock_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            _cycles.OnClock(e.Level);
            _state.OnClock(e.Level);

            if (_state.IsComplete)
            {
                SwitchToNextState();
            }
        }

        private void NonMaskableInterupt_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            // NMI is edge triggered
            if (e.Level == DigitalLevel.NegEdge)
            {
                // TODO: check if we're already in NMI?
                var def = OpcodeDefinition.GetInterruptDefinition(InterruptTypes.Nmi);
                var nmi = new CpuInterrupt(_die, def);
                _interrupts.Insert(0, nmi);
            }
        }

        private void Interrupt_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            if (_die.Registers.Interrupt.IFF1 && 
                e.Level == DigitalLevel.Low)
            {
                var def = OpcodeDefinition.GetInterruptDefinition(InterruptTypes.Nmi);
                var nmi = new CpuInterrupt(_die, def);
                _interrupts.Insert(0, nmi);
            }
        }

        internal void NotifyInstructionExecuted()
        {
            InstructionExecuted?.Invoke(this, new InstructionExecutedEventArgs(Opcode));
        }

        private void SwitchToNextState()
        {
            switch (_currentState)
            {
                case CpuStates.Fetch:
                    if (!ContinueFetch())
                        StartExecute();
                    break;
                case CpuStates.Execute:
                    if (!AcceptInterrupt())
                        StartFetch();
                    break;
                case CpuStates.Interrupt:
                    StartFetch();
                    break;
            }
        }

        private void StartFetch()
        {
            _state = new CpuFetch(_die);
            _currentState = CpuStates.Fetch;
            _opcodeBuilder.Clear();
            _cycles.Reset();
        }

        private bool ContinueFetch()
        {
            if (_cycles.IsLastCycle &&
                _cycles.OpcodeDefinition == null)
            {
                _state = new CpuFetch(_die);
                _cycles.Reset();
                return true;
            }

            return false;
        }

        private void StartExecute()
        {
            _state = new CpuExecute(_die);
            _currentState = CpuStates.Execute;
        }

        private bool AcceptInterrupt()
        {
            if (_interrupts.Count > 0)
            {
                var interrupt = _interrupts[0];
                _interrupts.RemoveAt(0);

                _state = interrupt;
                _currentState = CpuStates.Interrupt;

                // prep engine state
                _opcodeBuilder.Clear();
                _cycles.Reset();
                _cycles.OpcodeDefinition = interrupt.Definition;
                return true;
            }

            return false;
        }

        internal enum CpuStates
        {
            Fetch,
            Execute,
            Interrupt
        }
    }
}
