﻿using Jacobi.Zim80.CpuZ80.UnitTests;
using Jacobi.Zim80.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jacobi.Zim80.CpuZ80.Instructions.UnitTests
{
    [TestClass]
    public class NopInstructionTest
    {
        [TestMethod]
        public void Nop()
        {
            var cpuZ80 = new CpuZ80();
            var model = cpuZ80.Initialize(new byte[] { 0x00 });

            cpuZ80.FillRegisters(pc: 0);

            model.ClockGen.SquareWave(4);

            Console.WriteLine(model.LogicAnalyzer.ToWaveJson());

            cpuZ80.AssertRegisters(pc: 1);
        }
    }
}
