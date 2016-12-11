using Jacobi.Zim80.CpuZ80.Opcodes;
using Jacobi.Zim80.CpuZ80.States;

namespace Jacobi.Zim80.CpuZ80
{
    // RESET/BUSREQ/NMI/INT
    internal class InterruptManager : CpuState // for OnClock
    {
        private CpuInterrupt _pendingRES;
        private CpuInterrupt _pendingBRQ;
        private CpuInterrupt _pendingNMI;
        private CpuInterrupt _pendingINT;

        public InterruptManager(CpuZ80 cpu)
            : base(cpu)
        {
            // NMI is edge triggered
            cpu.NonMaskableInterrupt.OnChanged += NonMaskableInterupt_OnChanged;
        }

        public bool HasInterruptWaiting
        {
            get { return _pendingINT != null || _pendingNMI != null; }
        }

        internal void EnableInterrupt()
        {
            Cpu.Registers.Interrupt.IFF1 = true;
            Cpu.Registers.Interrupt.IFF2 = true;
        }

        internal void DisableInterrupt()
        {
            Cpu.Registers.Interrupt.IFF1 = false;
            Cpu.Registers.Interrupt.IFF2 = false;
        }

        // nmi
        internal void PushNmi()
        {
            Cpu.Registers.Interrupt.IFF1 = false;
        }
        // int
        internal void PushInt()
        {
            DisableInterrupt();
        }
        // reti/retn (ret?)
        internal void PopInt()
        {
            Cpu.Registers.Interrupt.IFF1 = Cpu.Registers.Interrupt.IFF2;
        }

        // number of instructions the interrupts remain disabled.
        private int _interruptSuspendedInstructionCount;

        internal void SuspendInterrupts(int numberOfInstructions = 2)
        {
            _interruptSuspendedInstructionCount = numberOfInstructions;
            Cpu.Registers.Interrupt.IsSuspended = true;
        }

        internal CpuInterrupt PopInterrupt()
        {
            DetermineINT(Cpu.Interrupt.Level);

            if (_pendingRES != null) return Clear(ref _pendingRES);
            if (_pendingBRQ != null) return Clear(ref _pendingBRQ);
            if (_pendingNMI != null) return Clear(ref _pendingNMI);
            if (_pendingINT != null) return Clear(ref _pendingINT);
            return null;
        }

        private static CpuInterrupt Clear(ref CpuInterrupt cpuInterrupt)
        {
            var interrupt = cpuInterrupt;
            cpuInterrupt = null;
            return interrupt;
        }

        internal void ReleaseInterrupts()
        {
            if (_interruptSuspendedInstructionCount > 0)
            {
                _interruptSuspendedInstructionCount--;

                if (_interruptSuspendedInstructionCount == 0)
                {
                    Cpu.Registers.Interrupt.IsSuspended = false;
                }
            }
        }

        private void NonMaskableInterupt_OnChanged(object sender, DigitalLevelChangedEventArgs e)
        {
            DetermineNMI(e.Level);
        }

        private void DetermineNMI(DigitalLevel level)
        {
            // NMI is edge triggered
            if (level == DigitalLevel.NegEdge)
            {
                var def = OpcodeDefinition.GetNmiDefinition();
                _pendingNMI = new CpuInterrupt(Cpu, def);
            }
        }

        protected override void OnClockPos()
        {
            // sample INT on the last T-cycle of the last M-cycle.
            if (ExecutionEngine.Cycles.IsLastMachineCycle &&
                ExecutionEngine.Cycles.IsLastCycle)
            {
                DetermineINT(Cpu.Interrupt.Level);
            }

            base.OnClockPos();
        }

        private void DetermineINT(DigitalLevel level)
        {
            if (Cpu.Registers.Interrupt.IFF1 &&
                level == DigitalLevel.Low)
            {
                var def = OpcodeDefinition.GetInterruptDefinition(Cpu.Registers.Interrupt.InterruptMode);
                _pendingINT = new CpuInterrupt(Cpu, def);
            }
        }
    }
}
