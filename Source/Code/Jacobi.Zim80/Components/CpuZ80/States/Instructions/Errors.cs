﻿using System;

namespace Jacobi.Zim80.Components.CpuZ80.States.Instructions
{
    internal static class Errors
    {
        public static Exception AssignedToIllegalOpcode()
        {
            return new InvalidOperationException("Instruction assigned to an illegal opcode.");
        }

        public static Exception InvalidMachineCycle(MachineCycleNames machineCycle)
        {
            return new InvalidOperationException("Invalid machine cycle: " + machineCycle);
        }
    }
}