using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.UnitTests
{
    internal sealed class InstructionLogger
    {
        private readonly TestContext _testContext;

        public InstructionLogger(CpuZ80 cpu)
        {
            Attach(cpu);
        }

        public InstructionLogger(TestContext testContext)
        {
            _testContext = testContext;
        }

        public void Attach(CpuZ80 cpu)
        {
            cpu.InstructionExecuted += Cpu_InstructionExecuted;
        }

        private void Cpu_InstructionExecuted(object sender, InstructionExecutedEventArgs e)
        {
            if (_testContext != null)
                _testContext.WriteLine(e.Opcode.Text);
            else
                Console.WriteLine(e.Opcode.Text);
        }
    }
}
