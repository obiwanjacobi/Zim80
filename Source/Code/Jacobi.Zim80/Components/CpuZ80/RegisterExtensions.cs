using System;

namespace Jacobi.Zim80.Components.CpuZ80
{
    internal static class RegisterExtensions
    {
        public static bool FlagFromOpcodeY(this Registers.RegisterSet regSet, byte y)
        {
            switch (y)
            {
                case 0: // nz
                    return !regSet.Flags.Z;
                case 1: // z
                    return regSet.Flags.Z;
                case 2: // nc
                    return !regSet.Flags.C;
                case 3: // c
                    return regSet.Flags.C;
                case 4: // po
                    return !regSet.Flags.PV;
                case 5: // pe
                    return regSet.Flags.PV;
                case 6: // p
                    return !regSet.Flags.S;
                case 7: // m
                    return regSet.Flags.S;
            }

            throw new InvalidOperationException("Invalid Opcode.Y value.");
        }
    }
}
