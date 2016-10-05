﻿using System;
using Jacobi.Zim80.Components.CpuZ80.States.Instructions;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes
{
    partial class OpcodeDefinition
    {
        // See also: http://z80.info/decoding.htm
        public static readonly OpcodeDefinition[] Defintions = new OpcodeDefinition[]
        {
            #region X = 0
            new OpcodeDefinition { X = 0, Z = 0, Y = 0, Mnemonic = "NOP", Cycles = new[] { 4 }, Instruction = typeof(NopInstruction) },
            new OpcodeDefinition { X = 0, Z = 0, Y = 1, Mnemonic = "EX AF, AF'", Cycles = new[] { 4 }, Instruction = typeof(ExAFAFInstruction) },
            new OpcodeDefinition { X = 0, Z = 0, Y = 2, Mnemonic = "DJNZ {0}", d = true, Cycles = new[] { 5, 3, 5 }, AltCycles = new[] { 5, 3 }, Instruction = typeof(DjnzInstruction) },
            new OpcodeDefinition { X = 0, Z = 0, Y = 3, Mnemonic = "JR {0}", d = true, Cycles = new[] { 4, 3, 5 }, Instruction = typeof(JumpRelativeInstruction) },
            new OpcodeDefinition { X = 0, Z = 0, Y = 4, Mnemonic = "JR NZ, {0}", d = true, Cycles = new[] { 4, 3, 5 }, AltCycles = new[] { 4, 3 }, Instruction = typeof(JumpRelativeInstruction) },
            new OpcodeDefinition { X = 0, Z = 0, Y = 5, Mnemonic = "JR Z, {0}", d = true, Cycles = new[] { 4, 3, 5 }, AltCycles = new[] { 4, 3 }, Instruction = typeof(JumpRelativeInstruction) },
            new OpcodeDefinition { X = 0, Z = 0, Y = 6, Mnemonic = "JR NC, {0}", d = true, Cycles = new[] { 4, 3, 5 }, AltCycles = new[] { 4, 3 }, Instruction = typeof(JumpRelativeInstruction) },
            new OpcodeDefinition { X = 0, Z = 0, Y = 7, Mnemonic = "JR C, {0}", d = true, Cycles = new[] { 4, 3, 5 }, AltCycles = new[] { 4, 3 }, Instruction = typeof(JumpRelativeInstruction) },

            new OpcodeDefinition { X = 0, Z = 1, Q = 0, P = 0, Mnemonic = "LD BC, {0}", nn = true, Cycles = new[] { 4, 3, 3 }, Instruction = typeof(LoadImmediate16Instruction) },
            new OpcodeDefinition { X = 0, Z = 1, Q = 0, P = 1, Mnemonic = "LD DE, {0}", nn = true, Cycles = new[] { 4, 3, 3 }, Instruction = typeof(LoadImmediate16Instruction) },
            new OpcodeDefinition { X = 0, Z = 1, Q = 0, P = 2, Mnemonic = "LD HL, {0}", nn = true, Cycles = new[] { 4, 3, 3 }, Instruction = typeof(LoadImmediate16Instruction) },
            new OpcodeDefinition { X = 0, Z = 1, Q = 0, P = 3, Mnemonic = "LD SP, {0}", nn = true, Cycles = new[] { 4, 3, 3 }, Instruction = typeof(LoadImmediate16Instruction) },
            new OpcodeDefinition { X = 0, Z = 1, Q = 1, P = 0, Mnemonic = "ADD HL, BC", Cycles = new[] { 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 1, Q = 1, P = 1, Mnemonic = "ADD HL, DE", Cycles = new[] { 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 1, Q = 1, P = 2, Mnemonic = "ADD HL, HL", Cycles = new[] { 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 1, Q = 1, P = 3, Mnemonic = "ADD HL, SP", Cycles = new[] { 4, 4, 3 } },

            new OpcodeDefinition { X = 0, Z = 2, Q = 0, P = 0, Mnemonic = "LD (BC), A", Cycles = new[] { 4, 3 }, Instruction = typeof(WriteIndirectRegisterInstruction) },
            new OpcodeDefinition { X = 0, Z = 2, Q = 0, P = 1, Mnemonic = "LD (DE), A", Cycles = new[] { 4, 3 }, Instruction = typeof(WriteIndirectRegisterInstruction) },
            new OpcodeDefinition { X = 0, Z = 2, Q = 0, P = 2, Mnemonic = "LD ({0}), HL", nn = true, Cycles = new[] { 4, 3, 3, 3, 3 } },
            new OpcodeDefinition { X = 0, Z = 2, Q = 0, P = 3, Mnemonic = "LD ({0}), A", nn = true, Cycles = new[] { 4, 3, 3, 3 } },
            new OpcodeDefinition { X = 0, Z = 2, Q = 1, P = 0, Mnemonic = "LD A, (BC)", Cycles = new[] { 4, 3 }, Instruction = typeof(ReadIndirectRegisterInstruction) },
            new OpcodeDefinition { X = 0, Z = 2, Q = 1, P = 1, Mnemonic = "LD A, (DE)", Cycles = new[] { 4, 3 }, Instruction = typeof(ReadIndirectRegisterInstruction) },
            new OpcodeDefinition { X = 0, Z = 2, Q = 1, P = 2, Mnemonic = "LD HL, ({0})", nn = true, Cycles = new[] { 4, 3, 3, 3, 3 } },
            new OpcodeDefinition { X = 0, Z = 2, Q = 1, P = 3, Mnemonic = "LD A, ({0})", nn = true, Cycles = new[] { 4, 3, 3, 3 } },

            new OpcodeDefinition { X = 0, Z = 3, Q = 0, P = 0, Mnemonic = "INC BC", Cycles = new[] { 6 }, Instruction = typeof(Inc16Instruction) },
            new OpcodeDefinition { X = 0, Z = 3, Q = 0, P = 1, Mnemonic = "INC DE", Cycles = new[] { 6 }, Instruction = typeof(Inc16Instruction) },
            new OpcodeDefinition { X = 0, Z = 3, Q = 0, P = 2, Mnemonic = "INC HL", Cycles = new[] { 6 }, Instruction = typeof(Inc16Instruction) },
            new OpcodeDefinition { X = 0, Z = 3, Q = 0, P = 3, Mnemonic = "INC SP", Cycles = new[] { 6 }, Instruction = typeof(Inc16Instruction) },
            new OpcodeDefinition { X = 0, Z = 3, Q = 1, P = 0, Mnemonic = "DEC BC", Cycles = new[] { 6 }, Instruction = typeof(Dec16Instruction) },
            new OpcodeDefinition { X = 0, Z = 3, Q = 1, P = 1, Mnemonic = "DEC DE", Cycles = new[] { 6 }, Instruction = typeof(Dec16Instruction) },
            new OpcodeDefinition { X = 0, Z = 3, Q = 1, P = 2, Mnemonic = "DEC HL", Cycles = new[] { 6 }, Instruction = typeof(Dec16Instruction) },
            new OpcodeDefinition { X = 0, Z = 3, Q = 1, P = 3, Mnemonic = "DEC SP", Cycles = new[] { 6 }, Instruction = typeof(Dec16Instruction) },

            new OpcodeDefinition { X = 0, Z = 4, Y = 0, Mnemonic = "INC B", Cycles = new[] { 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 1, Mnemonic = "INC C", Cycles = new[] { 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 2, Mnemonic = "INC D", Cycles = new[] { 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 3, Mnemonic = "INC E", Cycles = new[] { 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 4, Mnemonic = "INC H", Cycles = new[] { 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 5, Mnemonic = "INC L", Cycles = new[] { 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 6, Mnemonic = "INC (HL)", Cycles = new[] { 4, 4, 3 }, Instruction = typeof(IncDecIndirectInstruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 7, Mnemonic = "INC A", Cycles = new[] { 4 }, Instruction = typeof(Inc8Instruction) },

            new OpcodeDefinition { X = 0, Z = 5, Y = 0, Mnemonic = "DEC B", Cycles = new[] { 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 1, Mnemonic = "DEC C", Cycles = new[] { 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 2, Mnemonic = "DEC D", Cycles = new[] { 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 3, Mnemonic = "DEC E", Cycles = new[] { 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 4, Mnemonic = "DEC H", Cycles = new[] { 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 5, Mnemonic = "DEC L", Cycles = new[] { 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 6, Mnemonic = "DEC (HL)", Cycles = new[] { 4, 4, 3 }, Instruction = typeof(IncDecIndirectInstruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 7, Mnemonic = "DEC A", Cycles = new[] { 4 }, Instruction = typeof(Dec8Instruction) },

            new OpcodeDefinition { X = 0, Z = 6, Y = 0, Mnemonic = "LD B, {0}", n = true, Cycles = new[] { 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            new OpcodeDefinition { X = 0, Z = 6, Y = 1, Mnemonic = "LD C, {0}", n = true, Cycles = new[] { 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            new OpcodeDefinition { X = 0, Z = 6, Y = 2, Mnemonic = "LD D, {0}", n = true, Cycles = new[] { 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            new OpcodeDefinition { X = 0, Z = 6, Y = 3, Mnemonic = "LD E, {0}", n = true, Cycles = new[] { 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            new OpcodeDefinition { X = 0, Z = 6, Y = 4, Mnemonic = "LD H, {0}", n = true, Cycles = new[] { 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            new OpcodeDefinition { X = 0, Z = 6, Y = 5, Mnemonic = "LD L, {0}", n = true, Cycles = new[] { 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            new OpcodeDefinition { X = 0, Z = 6, Y = 6, Mnemonic = "LD (HL), {0}", n = true, Cycles = new[] { 4, 3, 3 } },
            new OpcodeDefinition { X = 0, Z = 6, Y = 7, Mnemonic = "LD A, {0}", n = true, Cycles = new[] { 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },

            new OpcodeDefinition { X = 0, Z = 7, Y = 0, Mnemonic = "RLCA", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 1, Mnemonic = "RRCA", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 2, Mnemonic = "RLA", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 3, Mnemonic = "RRA", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 4, Mnemonic = "DAA", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 5, Mnemonic = "CPL", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 6, Mnemonic = "SCF", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 7, Mnemonic = "CCF", Cycles = new[] { 4 } },
            #endregion

            #region X = 1
            new OpcodeDefinition { X = 1, Z = 0, Y = 0, Mnemonic = "LD B, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 1, Mnemonic = "LD C, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 2, Mnemonic = "LD D, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 3, Mnemonic = "LD E, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 4, Mnemonic = "LD H, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 5, Mnemonic = "LD L, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 6, Mnemonic = "LD (HL), B", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 7, Mnemonic = "LD A, B", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 1, Z = 1, Y = 0, Mnemonic = "LD B, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 1, Mnemonic = "LD C, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 2, Mnemonic = "LD D, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 3, Mnemonic = "LD E, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 4, Mnemonic = "LD H, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 5, Mnemonic = "LD L, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 6, Mnemonic = "LD (HL), C", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 7, Mnemonic = "LD A, C", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 1, Z = 2, Y = 0, Mnemonic = "LD B, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 2, Y = 1, Mnemonic = "LD C, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 2, Y = 2, Mnemonic = "LD D, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 2, Y = 3, Mnemonic = "LD E, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 2, Y = 4, Mnemonic = "LD H, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 2, Y = 5, Mnemonic = "LD L, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 2, Y = 6, Mnemonic = "LD (HL), D", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 2, Y = 7, Mnemonic = "LD A, D", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 1, Z = 3, Y = 0, Mnemonic = "LD B, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 3, Y = 1, Mnemonic = "LD C, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 3, Y = 2, Mnemonic = "LD D, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 3, Y = 3, Mnemonic = "LD E, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 3, Y = 4, Mnemonic = "LD H, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 3, Y = 5, Mnemonic = "LD L, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 3, Y = 6, Mnemonic = "LD (HL), E", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 3, Y = 7, Mnemonic = "LD A, E", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 1, Z = 4, Y = 0, Mnemonic = "LD B, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 4, Y = 1, Mnemonic = "LD C, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 4, Y = 2, Mnemonic = "LD D, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 4, Y = 3, Mnemonic = "LD E, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 4, Y = 4, Mnemonic = "LD H, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 4, Y = 5, Mnemonic = "LD L, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 4, Y = 6, Mnemonic = "LD (HL), H", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 4, Y = 7, Mnemonic = "LD A, H", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 1, Z = 5, Y = 0, Mnemonic = "LD B, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 1, Mnemonic = "LD C, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 2, Mnemonic = "LD D, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 3, Mnemonic = "LD E, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 4, Mnemonic = "LD H, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 5, Mnemonic = "LD L, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 6, Mnemonic = "LD (HL), L", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 7, Mnemonic = "LD A, L", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 1, Z = 6, Y = 0, Mnemonic = "LD B, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 1, Mnemonic = "LD C, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 2, Mnemonic = "LD D, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 3, Mnemonic = "LD E, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 4, Mnemonic = "LD H, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 5, Mnemonic = "LD L, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 6, Mnemonic = "HALT", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 7, Mnemonic = "LD A, (HL)", Cycles = new[] { 4, 3 } },

            new OpcodeDefinition { X = 1, Z = 7, Y = 0, Mnemonic = "LD B, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 1, Mnemonic = "LD C, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 2, Mnemonic = "LD D, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 3, Mnemonic = "LD E, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 4, Mnemonic = "LD H, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 5, Mnemonic = "LD L, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 6, Mnemonic = "LD (HL), A", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 7, Mnemonic = "LD A, A", Cycles = new[] { 4 } },
            #endregion

            #region X = 2
            new OpcodeDefinition { X = 2, Z = 0, Y = 0, Mnemonic = "ADD A, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 1, Mnemonic = "ADC A, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 2, Mnemonic = "SUB A, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 3, Mnemonic = "SBC A, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 4, Mnemonic = "AND A, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 5, Mnemonic = "XOR A, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 6, Mnemonic = "OR A, B", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 7, Mnemonic = "CP A, B", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 2, Z = 1, Y = 0, Mnemonic = "ADD A, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 1, Mnemonic = "ADC A, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 2, Mnemonic = "SUB A, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 3, Mnemonic = "SBC A, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 4, Mnemonic = "AND A, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 5, Mnemonic = "XOR A, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 6, Mnemonic = "OR A, C", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 7, Mnemonic = "CP A, C", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 2, Z = 2, Y = 0, Mnemonic = "ADD A, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 1, Mnemonic = "ADC A, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 2, Mnemonic = "SUB A, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 3, Mnemonic = "SBC A, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 4, Mnemonic = "AND A, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 5, Mnemonic = "XOR A, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 6, Mnemonic = "OR A, D", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 7, Mnemonic = "CP A, D", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 2, Z = 3, Y = 0, Mnemonic = "ADD A, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 1, Mnemonic = "ADC A, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 2, Mnemonic = "SUB A, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 3, Mnemonic = "SBC A, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 4, Mnemonic = "AND A, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 5, Mnemonic = "XOR A, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 6, Mnemonic = "OR A, E", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 7, Mnemonic = "CP A, E", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 2, Z = 4, Y = 0, Mnemonic = "ADD A, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 1, Mnemonic = "ADC A, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 2, Mnemonic = "SUB A, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 3, Mnemonic = "SBC A, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 4, Mnemonic = "AND A, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 5, Mnemonic = "XOR A, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 6, Mnemonic = "OR A, H", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 7, Mnemonic = "CP A, H", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 2, Z = 5, Y = 0, Mnemonic = "ADD A, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 1, Mnemonic = "ADC A, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 2, Mnemonic = "SUB A, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 3, Mnemonic = "SBC A, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 4, Mnemonic = "AND A, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 5, Mnemonic = "XOR A, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 6, Mnemonic = "OR A, L", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 7, Mnemonic = "CP A, L", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 2, Z = 6, Y = 0, Mnemonic = "ADD A, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 1, Mnemonic = "ADC A, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 2, Mnemonic = "SUB A, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 3, Mnemonic = "SBC A, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 4, Mnemonic = "AND A, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 5, Mnemonic = "XOR A, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 6, Mnemonic = "OR A, (HL)", Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 7, Mnemonic = "CP A, (HL)", Cycles = new[] { 4, 3 } },

            new OpcodeDefinition { X = 2, Z = 7, Y = 0, Mnemonic = "ADD A, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 1, Mnemonic = "ADC A, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 2, Mnemonic = "SUB A, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 3, Mnemonic = "SBC A, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 4, Mnemonic = "AND A, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 5, Mnemonic = "XOR A, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 6, Mnemonic = "OR A, A", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 7, Mnemonic = "CP A, A", Cycles = new[] { 4 } },
            #endregion

            #region X = 3
            new OpcodeDefinition { X = 3, Z = 0, Y = 0, Mnemonic = "RET NZ", Cycles = new[] { 5, 3, 3 }, AltCycles = new[] { 5 }, Instruction = typeof(RetInstruction) },
            new OpcodeDefinition { X = 3, Z = 0, Y = 1, Mnemonic = "RET Z", Cycles = new[] { 5, 3, 3 }, AltCycles = new[] { 5 }, Instruction = typeof(RetInstruction) },
            new OpcodeDefinition { X = 3, Z = 0, Y = 2, Mnemonic = "RET NC", Cycles = new[] { 5, 3, 3 }, AltCycles = new[] { 5 }, Instruction = typeof(RetInstruction) },
            new OpcodeDefinition { X = 3, Z = 0, Y = 3, Mnemonic = "RET C", Cycles = new[] { 5, 3, 3 }, AltCycles = new[] { 5 }, Instruction = typeof(RetInstruction) },
            new OpcodeDefinition { X = 3, Z = 0, Y = 4, Mnemonic = "RET PO", Cycles = new[] { 5, 3, 3 }, AltCycles = new[] { 5 }, Instruction = typeof(RetInstruction) },
            new OpcodeDefinition { X = 3, Z = 0, Y = 5, Mnemonic = "RET PE", Cycles = new[] { 5, 3, 3 }, AltCycles = new[] { 5 }, Instruction = typeof(RetInstruction) },
            new OpcodeDefinition { X = 3, Z = 0, Y = 6, Mnemonic = "RET P", Cycles = new[] { 5, 3, 3 }, AltCycles = new[] { 5 }, Instruction = typeof(RetInstruction) },
            new OpcodeDefinition { X = 3, Z = 0, Y = 7, Mnemonic = "RET M", Cycles = new[] { 5, 3, 3 }, AltCycles = new[] { 5 }, Instruction = typeof(RetInstruction) },

            new OpcodeDefinition { X = 3, Z = 1, Q = 0, P = 0, Mnemonic = "POP BC", Cycles = new[] { 4, 3, 3 }, Instruction = typeof(PopInstruction) },
            new OpcodeDefinition { X = 3, Z = 1, Q = 0, P = 1, Mnemonic = "POP DE", Cycles = new[] { 4, 3, 3 }, Instruction = typeof(PopInstruction) },
            new OpcodeDefinition { X = 3, Z = 1, Q = 0, P = 2, Mnemonic = "POP HL", Cycles = new[] { 4, 3, 3 }, Instruction = typeof(PopInstruction) },
            new OpcodeDefinition { X = 3, Z = 1, Q = 0, P = 3, Mnemonic = "POP AF", Cycles = new[] { 4, 3, 3 }, Instruction = typeof(PopInstruction) },
            new OpcodeDefinition { X = 3, Z = 1, Q = 1, P = 0, Mnemonic = "RET", Cycles = new[] { 4, 3, 3 }, Instruction = typeof(RetInstruction) },
            new OpcodeDefinition { X = 3, Z = 1, Q = 1, P = 1, Mnemonic = "EXX", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 3, Z = 1, Q = 1, P = 2, Mnemonic = "JP HL", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 3, Z = 1, Q = 1, P = 3, Mnemonic = "LD SP, HL", Cycles = new[] { 6 } },

            new OpcodeDefinition { X = 3, Z = 2, Y = 0, Mnemonic = "JP NZ, {0}", nn = true, Cycles = new[] { 4, 3, 3 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 1, Mnemonic = "JP Z, {0}", nn = true, Cycles = new[] { 4, 3, 3 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 2, Mnemonic = "JP NC, {0}", nn = true, Cycles = new[] { 4, 3, 3 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 3, Mnemonic = "JP C, {0}", nn = true, Cycles = new[] { 4, 3, 3 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 4, Mnemonic = "JP PO, {0}", nn = true, Cycles = new[] { 4, 3, 3 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 5, Mnemonic = "JP PE, {0}", nn = true, Cycles = new[] { 4, 3, 3 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 6, Mnemonic = "JP P, {0}", nn = true, Cycles = new[] { 4, 3, 3 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 7, Mnemonic = "JP M, {0}", nn = true, Cycles = new[] { 4, 3, 3 } },

            new OpcodeDefinition { X = 3, Z = 3, Y = 0, Mnemonic = "JP {0}", nn = true, Cycles = new[] { 4, 3, 3 } },
            //new OpcodeDefinition { X = 3, Z = 3, Y = 1, Text = "#CB", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 2, Mnemonic = "OUT ({0}), A", n = true, Cycles = new[] { 4, 4 } },    //Cycles?
            new OpcodeDefinition { X = 3, Z = 3, Y = 3, Mnemonic = "IN A,({0})", n = true, Cycles = new[] { 4, 4 } },      //Cycles?
            new OpcodeDefinition { X = 3, Z = 3, Y = 4, Mnemonic = "EX (SP), HL", Cycles = new[] { 4, 3, 4, 3, 5 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 5, Mnemonic = "EX DE, HL", Cycles = new[] { 4 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 6, Mnemonic = "DI", Cycles = new[] { 4 }, Instruction = typeof(InterruptInstruction) },
            new OpcodeDefinition { X = 3, Z = 3, Y = 7, Mnemonic = "EI", Cycles = new[] { 4 }, Instruction = typeof(InterruptInstruction) },

            new OpcodeDefinition { X = 3, Z = 4, Y = 0, Mnemonic = "CALL NZ, {0}", nn = true, Cycles = new[] { 4, 3, 4, 3, 3 }, AltCycles = new[] { 4, 3, 3 }, Instruction = typeof(CallInstruction) },
            new OpcodeDefinition { X = 3, Z = 4, Y = 1, Mnemonic = "CALL Z, {0}", nn = true, Cycles = new[] { 4, 3, 4, 3, 3 }, AltCycles = new[] { 4, 3, 3 }, Instruction = typeof(CallInstruction) },
            new OpcodeDefinition { X = 3, Z = 4, Y = 2, Mnemonic = "CALL NC, {0}", nn = true, Cycles = new[] { 4, 3, 4, 3, 3 }, AltCycles = new[] { 4, 3, 3 }, Instruction = typeof(CallInstruction) },
            new OpcodeDefinition { X = 3, Z = 4, Y = 3, Mnemonic = "CALL C, {0}", nn = true, Cycles = new[] { 4, 3, 4, 3, 3 }, AltCycles = new[] { 4, 3, 3 }, Instruction = typeof(CallInstruction) },
            new OpcodeDefinition { X = 3, Z = 4, Y = 4, Mnemonic = "CALL PO, {0}", nn = true, Cycles = new[] { 4, 3, 4, 3, 3 }, AltCycles = new[] { 4, 3, 3 }, Instruction = typeof(CallInstruction) },
            new OpcodeDefinition { X = 3, Z = 4, Y = 5, Mnemonic = "CALL PE, {0}", nn = true, Cycles = new[] { 4, 3, 4, 3, 3 }, AltCycles = new[] { 4, 3, 3 }, Instruction = typeof(CallInstruction) },
            new OpcodeDefinition { X = 3, Z = 4, Y = 6, Mnemonic = "CALL P, {0}", nn = true, Cycles = new[] { 4, 3, 4, 3, 3 }, AltCycles = new[] { 4, 3, 3 }, Instruction = typeof(CallInstruction) },
            new OpcodeDefinition { X = 3, Z = 4, Y = 7, Mnemonic = "CALL M, {0}", nn = true, Cycles = new[] { 4, 3, 4, 3, 3 }, AltCycles = new[] { 4, 3, 3 }, Instruction = typeof(CallInstruction) },

            new OpcodeDefinition { X = 3, Z = 5, Q = 0, P = 0, Mnemonic = "PUSH BC", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(PushInstruction) },
            new OpcodeDefinition { X = 3, Z = 5, Q = 0, P = 1, Mnemonic = "PUSH DE", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(PushInstruction) },
            new OpcodeDefinition { X = 3, Z = 5, Q = 0, P = 2, Mnemonic = "PUSH HL", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(PushInstruction) },
            new OpcodeDefinition { X = 3, Z = 5, Q = 0, P = 3, Mnemonic = "PUSH AF", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(PushInstruction) },
            new OpcodeDefinition { X = 3, Z = 5, Q = 1, P = 0, Mnemonic = "CALL {0}", nn = true, Cycles = new[] { 4, 3, 4, 3, 3 }, Instruction = typeof(CallInstruction) },
            //new OpcodeDefinition { X = 3, Z = 5, Q = 1, P = 1, Text = "#DD", Cycles = new[] { 4 } },
            //new OpcodeDefinition { X = 3, Z = 5, Q = 1, P = 2, Text = "#ED", Cycles = new[] { 4 } },
            //new OpcodeDefinition { X = 3, Z = 5, Q = 1, P = 3, Text = "#FD", Cycles = new[] { 4 } },

            new OpcodeDefinition { X = 3, Z = 6, Y = 0, Mnemonic = "ADD A, {0}", n = true, Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 1, Mnemonic = "ADC A, {0}", n = true, Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 2, Mnemonic = "SUB A, {0}", n = true, Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 3, Mnemonic = "SBC A, {0}", n = true, Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 4, Mnemonic = "AND A, {0}", n = true, Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 5, Mnemonic = "XOR A, {0}", n = true, Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 6, Mnemonic = "OR A, {0}", n = true, Cycles = new[] { 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 7, Mnemonic = "CP A, {0}", n = true, Cycles = new[] { 4, 3 } },

            new OpcodeDefinition { X = 3, Z = 7, Y = 0, Mnemonic = "RST 00", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(RstInstruction) },
            new OpcodeDefinition { X = 3, Z = 7, Y = 1, Mnemonic = "RST 08", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(RstInstruction) },
            new OpcodeDefinition { X = 3, Z = 7, Y = 2, Mnemonic = "RST 10", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(RstInstruction) },
            new OpcodeDefinition { X = 3, Z = 7, Y = 3, Mnemonic = "RST 18", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(RstInstruction) },
            new OpcodeDefinition { X = 3, Z = 7, Y = 4, Mnemonic = "RST 20", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(RstInstruction) },
            new OpcodeDefinition { X = 3, Z = 7, Y = 5, Mnemonic = "RST 28", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(RstInstruction) },
            new OpcodeDefinition { X = 3, Z = 7, Y = 6, Mnemonic = "RST 30", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(RstInstruction) },
            new OpcodeDefinition { X = 3, Z = 7, Y = 7, Mnemonic = "RST 38", Cycles = new[] { 5, 3, 3 }, Instruction = typeof(RstInstruction) },
            #endregion

            #region CB prefixed 
            #region X = 0
            new OpcodeDefinition { X = 0, Z = 0, Y = 0, Mnemonic = "RLC B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 0, Y = 1, Mnemonic = "RRC B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 0, Y = 2, Mnemonic = "RL B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 0, Y = 3, Mnemonic = "RR B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 0, Y = 4, Mnemonic = "SLA B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 0, Y = 5, Mnemonic = "SRA B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 0, Y = 6, Mnemonic = "SLL B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 0, Y = 7, Mnemonic = "SRL B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 0, Z = 1, Y = 0, Mnemonic = "RLC C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 1, Y = 1, Mnemonic = "RRC C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 1, Y = 2, Mnemonic = "RL C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 1, Y = 3, Mnemonic = "RR C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 1, Y = 4, Mnemonic = "SLA C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 1, Y = 5, Mnemonic = "SRA C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 1, Y = 6, Mnemonic = "SLL C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 1, Y = 7, Mnemonic = "SRL C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 0, Z = 2, Y = 0, Mnemonic = "RLC D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 2, Y = 1, Mnemonic = "RRC D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 2, Y = 2, Mnemonic = "RL D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 2, Y = 3, Mnemonic = "RR D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 2, Y = 4, Mnemonic = "SLA D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 2, Y = 5, Mnemonic = "SRA D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 2, Y = 6, Mnemonic = "SLL D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 2, Y = 7, Mnemonic = "SRL D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 0, Z = 3, Y = 0, Mnemonic = "RLC E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 3, Y = 1, Mnemonic = "RRC E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 3, Y = 2, Mnemonic = "RL E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 3, Y = 3, Mnemonic = "RR E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 3, Y = 4, Mnemonic = "SLA E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 3, Y = 5, Mnemonic = "SRA E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 3, Y = 6, Mnemonic = "SLL E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 3, Y = 7, Mnemonic = "SRL E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 0, Z = 4, Y = 0, Mnemonic = "RLC H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 4, Y = 1, Mnemonic = "RRC H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 4, Y = 2, Mnemonic = "RL H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 4, Y = 3, Mnemonic = "RR H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 4, Y = 4, Mnemonic = "SLA H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 4, Y = 5, Mnemonic = "SRA H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 4, Y = 6, Mnemonic = "SLL H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 4, Y = 7, Mnemonic = "SRL H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 0, Z = 5, Y = 0, Mnemonic = "RLC L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 5, Y = 1, Mnemonic = "RRC L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 5, Y = 2, Mnemonic = "RL L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 5, Y = 3, Mnemonic = "RR L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 5, Y = 4, Mnemonic = "SLA L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 5, Y = 5, Mnemonic = "SRA L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 5, Y = 6, Mnemonic = "SLL L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 5, Y = 7, Mnemonic = "SRL L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 0, Z = 6, Y = 0, Mnemonic = "RLC (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 6, Y = 1, Mnemonic = "RRC (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 6, Y = 2, Mnemonic = "RL (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 6, Y = 3, Mnemonic = "RR (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 6, Y = 4, Mnemonic = "SLA (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 6, Y = 5, Mnemonic = "SRA (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 6, Y = 6, Mnemonic = "SLL (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 0, Z = 6, Y = 7, Mnemonic = "SRL (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },

            new OpcodeDefinition { X = 0, Z = 7, Y = 0, Mnemonic = "RLC A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 1, Mnemonic = "RRC A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 2, Mnemonic = "RL A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 3, Mnemonic = "RR A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 4, Mnemonic = "SLA A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 5, Mnemonic = "SRA A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 6, Mnemonic = "SLL A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 0, Z = 7, Y = 7, Mnemonic = "SRL A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            #endregion

            #region X = 1
            new OpcodeDefinition { X = 1, Z = 0, Y = 0, Mnemonic = "BIT 0, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 0, Y = 1, Mnemonic = "BIT 1, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 0, Y = 2, Mnemonic = "BIT 2, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 0, Y = 3, Mnemonic = "BIT 3, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 0, Y = 4, Mnemonic = "BIT 4, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 0, Y = 5, Mnemonic = "BIT 5, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 0, Y = 6, Mnemonic = "BIT 6, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 0, Y = 7, Mnemonic = "BIT 7, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },

            new OpcodeDefinition { X = 1, Z = 1, Y = 0, Mnemonic = "BIT 0, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 1, Y = 1, Mnemonic = "BIT 1, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 1, Y = 2, Mnemonic = "BIT 2, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 1, Y = 3, Mnemonic = "BIT 3, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 1, Y = 4, Mnemonic = "BIT 4, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 1, Y = 5, Mnemonic = "BIT 5, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 1, Y = 6, Mnemonic = "BIT 6, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 1, Y = 7, Mnemonic = "BIT 7, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },

            new OpcodeDefinition { X = 1, Z = 2, Y = 0, Mnemonic = "BIT 0, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 2, Y = 1, Mnemonic = "BIT 1, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 2, Y = 2, Mnemonic = "BIT 2, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 2, Y = 3, Mnemonic = "BIT 3, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 2, Y = 4, Mnemonic = "BIT 4, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 2, Y = 5, Mnemonic = "BIT 5, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 2, Y = 6, Mnemonic = "BIT 6, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 2, Y = 7, Mnemonic = "BIT 7, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },

            new OpcodeDefinition { X = 1, Z = 3, Y = 0, Mnemonic = "BIT 0, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 3, Y = 1, Mnemonic = "BIT 1, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 3, Y = 2, Mnemonic = "BIT 2, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 3, Y = 3, Mnemonic = "BIT 3, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 3, Y = 4, Mnemonic = "BIT 4, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 3, Y = 5, Mnemonic = "BIT 5, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 3, Y = 6, Mnemonic = "BIT 6, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 3, Y = 7, Mnemonic = "BIT 7, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },

            new OpcodeDefinition { X = 1, Z = 4, Y = 0, Mnemonic = "BIT 0, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 4, Y = 1, Mnemonic = "BIT 1, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 4, Y = 2, Mnemonic = "BIT 2, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 4, Y = 3, Mnemonic = "BIT 3, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 4, Y = 4, Mnemonic = "BIT 4, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 4, Y = 5, Mnemonic = "BIT 5, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 4, Y = 6, Mnemonic = "BIT 6, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 4, Y = 7, Mnemonic = "BIT 7, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },

            new OpcodeDefinition { X = 1, Z = 5, Y = 0, Mnemonic = "BIT 0, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 5, Y = 1, Mnemonic = "BIT 1, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 5, Y = 2, Mnemonic = "BIT 2, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 5, Y = 3, Mnemonic = "BIT 3, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 5, Y = 4, Mnemonic = "BIT 4, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 5, Y = 5, Mnemonic = "BIT 5, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 5, Y = 6, Mnemonic = "BIT 6, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 5, Y = 7, Mnemonic = "BIT 7, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },

            new OpcodeDefinition { X = 1, Z = 6, Y = 0, Mnemonic = "BIT 0, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 1, Mnemonic = "BIT 1, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 2, Mnemonic = "BIT 2, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 3, Mnemonic = "BIT 3, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 4, Mnemonic = "BIT 4, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 5, Mnemonic = "BIT 5, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 6, Mnemonic = "BIT 6, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 7, Mnemonic = "BIT 7, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4 } },

            new OpcodeDefinition { X = 1, Z = 7, Y = 0, Mnemonic = "BIT 0, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 7, Y = 1, Mnemonic = "BIT 1, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 7, Y = 2, Mnemonic = "BIT 2, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 7, Y = 3, Mnemonic = "BIT 3, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 7, Y = 4, Mnemonic = "BIT 4, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 7, Y = 5, Mnemonic = "BIT 5, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 7, Y = 6, Mnemonic = "BIT 6, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            new OpcodeDefinition { X = 1, Z = 7, Y = 7, Mnemonic = "BIT 7, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 }, Instruction = typeof(BitInstruction) },
            #endregion

            #region X = 2
            new OpcodeDefinition { X = 2, Z = 0, Y = 0, Mnemonic = "RES 0, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 1, Mnemonic = "RES 1, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 2, Mnemonic = "RES 2, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 3, Mnemonic = "RES 3, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 4, Mnemonic = "RES 4, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 5, Mnemonic = "RES 5, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 6, Mnemonic = "RES 6, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 7, Mnemonic = "RES 7, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 2, Z = 1, Y = 0, Mnemonic = "RES 0, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 1, Mnemonic = "RES 1, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 2, Mnemonic = "RES 2, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 3, Mnemonic = "RES 3, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 4, Mnemonic = "RES 4, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 5, Mnemonic = "RES 5, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 6, Mnemonic = "RES 6, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 7, Mnemonic = "RES 7, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 2, Z = 2, Y = 0, Mnemonic = "RES 0, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 1, Mnemonic = "RES 1, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 2, Mnemonic = "RES 2, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 3, Mnemonic = "RES 3, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 4, Mnemonic = "RES 4, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 5, Mnemonic = "RES 5, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 6, Mnemonic = "RES 6, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 7, Mnemonic = "RES 7, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 2, Z = 3, Y = 0, Mnemonic = "RES 0, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 1, Mnemonic = "RES 1, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 2, Mnemonic = "RES 2, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 3, Mnemonic = "RES 3, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 4, Mnemonic = "RES 4, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 5, Mnemonic = "RES 5, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 6, Mnemonic = "RES 6, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 7, Mnemonic = "RES 7, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 2, Z = 4, Y = 0, Mnemonic = "RES 0, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 1, Mnemonic = "RES 1, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 2, Mnemonic = "RES 2, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 3, Mnemonic = "RES 3, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 4, Mnemonic = "RES 4, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 5, Mnemonic = "RES 5, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 6, Mnemonic = "RES 6, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 4, Y = 7, Mnemonic = "RES 7, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 2, Z = 5, Y = 0, Mnemonic = "RES 0, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 1, Mnemonic = "RES 1, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 2, Mnemonic = "RES 2, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 3, Mnemonic = "RES 3, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 4, Mnemonic = "RES 4, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 5, Mnemonic = "RES 5, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 6, Mnemonic = "RES 6, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 5, Y = 7, Mnemonic = "RES 7, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 2, Z = 6, Y = 0, Mnemonic = "RES 0, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 1, Mnemonic = "RES 1, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 2, Mnemonic = "RES 2, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 3, Mnemonic = "RES 3, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 4, Mnemonic = "RES 4, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 5, Mnemonic = "RES 5, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 6, Mnemonic = "RES 6, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 2, Z = 6, Y = 7, Mnemonic = "RES 7, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },

            new OpcodeDefinition { X = 2, Z = 7, Y = 0, Mnemonic = "RES 0, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 1, Mnemonic = "RES 1, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 2, Mnemonic = "RES 2, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 3, Mnemonic = "RES 3, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 4, Mnemonic = "RES 4, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 5, Mnemonic = "RES 5, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 6, Mnemonic = "RES 6, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 2, Z = 7, Y = 7, Mnemonic = "RES 7, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            #endregion

            #region X = 3
            new OpcodeDefinition { X = 3, Z = 0, Y = 0, Mnemonic = "SET 0, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 0, Y = 1, Mnemonic = "SET 1, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 0, Y = 2, Mnemonic = "SET 2, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 0, Y = 3, Mnemonic = "SET 3, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 0, Y = 4, Mnemonic = "SET 4, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 0, Y = 5, Mnemonic = "SET 5, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 0, Y = 6, Mnemonic = "SET 6, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 0, Y = 7, Mnemonic = "SET 7, B", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 3, Z = 1, Y = 0, Mnemonic = "SET 0, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 1, Y = 1, Mnemonic = "SET 1, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 1, Y = 2, Mnemonic = "SET 2, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 1, Y = 3, Mnemonic = "SET 3, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 1, Y = 4, Mnemonic = "SET 4, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 1, Y = 5, Mnemonic = "SET 5, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 1, Y = 6, Mnemonic = "SET 6, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 1, Y = 7, Mnemonic = "SET 7, C", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 3, Z = 2, Y = 0, Mnemonic = "SET 0, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 1, Mnemonic = "SET 1, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 2, Mnemonic = "SET 2, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 3, Mnemonic = "SET 3, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 4, Mnemonic = "SET 4, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 5, Mnemonic = "SET 5, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 6, Mnemonic = "SET 6, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 2, Y = 7, Mnemonic = "SET 7, D", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 3, Z = 3, Y = 0, Mnemonic = "SET 0, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 1, Mnemonic = "SET 1, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 2, Mnemonic = "SET 2, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 3, Mnemonic = "SET 3, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 4, Mnemonic = "SET 4, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 5, Mnemonic = "SET 5, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 6, Mnemonic = "SET 6, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 3, Y = 7, Mnemonic = "SET 7, E", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 3, Z = 4, Y = 0, Mnemonic = "SET 0, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 4, Y = 1, Mnemonic = "SET 1, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 4, Y = 2, Mnemonic = "SET 2, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 4, Y = 3, Mnemonic = "SET 3, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 4, Y = 4, Mnemonic = "SET 4, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 4, Y = 5, Mnemonic = "SET 5, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 4, Y = 6, Mnemonic = "SET 6, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 4, Y = 7, Mnemonic = "SET 7, H", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 3, Z = 5, Y = 0, Mnemonic = "SET 0, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 5, Y = 1, Mnemonic = "SET 1, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 5, Y = 2, Mnemonic = "SET 2, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 5, Y = 3, Mnemonic = "SET 3, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 5, Y = 4, Mnemonic = "SET 4, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 5, Y = 5, Mnemonic = "SET 5, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 5, Y = 6, Mnemonic = "SET 6, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 5, Y = 7, Mnemonic = "SET 7, L", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 3, Z = 6, Y = 0, Mnemonic = "SET 0, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 1, Mnemonic = "SET 1, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 2, Mnemonic = "SET 2, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 3, Mnemonic = "SET 3, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 4, Mnemonic = "SET 4, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 5, Mnemonic = "SET 5, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 6, Mnemonic = "SET 6, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 3, Z = 6, Y = 7, Mnemonic = "SET 7, (HL)", Ext1 = 0xCB, Cycles = new[] { 4, 4, 4, 3 } },

            new OpcodeDefinition { X = 3, Z = 7, Y = 0, Mnemonic = "SET 0, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 7, Y = 1, Mnemonic = "SET 1, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 7, Y = 2, Mnemonic = "SET 2, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 7, Y = 3, Mnemonic = "SET 3, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 7, Y = 4, Mnemonic = "SET 4, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 7, Y = 5, Mnemonic = "SET 5, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 7, Y = 6, Mnemonic = "SET 6, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 3, Z = 7, Y = 7, Mnemonic = "SET 7, A", Ext1 = 0xCB, Cycles = new[] { 4, 4 } },
            #endregion
            #endregion // CB

            #region ED prefixed
            #region X = 1
            new OpcodeDefinition { X = 1, Z = 0, Y = 0, Mnemonic = "IN B, (C)", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 1, Mnemonic = "IN C, (C)", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 2, Mnemonic = "IN D, (C)", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 3, Mnemonic = "IN E, (C)", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 4, Mnemonic = "IN H, (C)", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 5, Mnemonic = "IN L, (C)", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 0, Y = 6, Mnemonic = "IN (C)", Ext1 = 0xED, Cycles = new[] { 4, 4 } },    // cycles?
            new OpcodeDefinition { X = 1, Z = 0, Y = 7, Mnemonic = "IN A, (C)", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },

            new OpcodeDefinition { X = 1, Z = 1, Y = 0, Mnemonic = "OUT (C), B", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 1, Mnemonic = "OUT (C), C", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 2, Mnemonic = "OUT (C), D", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 3, Mnemonic = "OUT (C), E", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 4, Mnemonic = "OUT (C), H", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 5, Mnemonic = "OUT (C), L", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 1, Y = 6, Mnemonic = "OUT (C), 0", Ext1 = 0xED, Cycles = new[] { 4, 4 } },    // cycles?
            new OpcodeDefinition { X = 1, Z = 1, Y = 7, Mnemonic = "OUT (C), A", Ext1 = 0xED, Cycles = new[] { 4, 4, 4 } },

            new OpcodeDefinition { X = 1, Z = 2, Q = 0, P = 0, Mnemonic = "SBC HL, BC", Ext1 = 0xED, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 2, Q = 0, P = 1, Mnemonic = "SBC HL, DE", Ext1 = 0xED, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 2, Q = 0, P = 2, Mnemonic = "SBC HL, HL", Ext1 = 0xED, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 2, Q = 0, P = 3, Mnemonic = "SBC HL, SP", Ext1 = 0xED, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 2, Q = 1, P = 0, Mnemonic = "ADC HL, BC", Ext1 = 0xED, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 2, Q = 1, P = 1, Mnemonic = "ADC HL, DE", Ext1 = 0xED, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 2, Q = 1, P = 2, Mnemonic = "ADC HL, HL", Ext1 = 0xED, Cycles = new[] { 4, 4, 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 2, Q = 1, P = 3, Mnemonic = "ADC HL, SP", Ext1 = 0xED, Cycles = new[] { 4, 4, 4, 3 } },

            new OpcodeDefinition { X = 1, Z = 3, Q = 0, P = 0, Mnemonic = "LD ({0}), BC", nn = true, Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 3, Q = 0, P = 1, Mnemonic = "LD ({0}), DE", nn = true, Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 3, Q = 0, P = 2, Mnemonic = "LD ({0}), HL", nn = true, Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 3, Q = 0, P = 3, Mnemonic = "LD ({0}), SP", nn = true, Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 3, Q = 1, P = 0, Mnemonic = "LD BC, ({0})", nn = true, Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 3, Q = 1, P = 1, Mnemonic = "LD DE, ({0})", nn = true, Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 3, Q = 1, P = 2, Mnemonic = "LD HL, ({0})", nn = true, Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 3, Q = 1, P = 3, Mnemonic = "LD SP, ({0})", nn = true, Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3, 3, 3 } },

            new OpcodeDefinition { X = 1, Z = 4, Y = 0, Mnemonic = "NEG", Ext1 = 0xED, Cycles = new[] { 4, 4 } },

            new OpcodeDefinition { X = 1, Z = 5, Y = 0, Mnemonic = "RETN", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 1, Mnemonic = "RETI", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 2, Mnemonic = "RETN", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 3, Mnemonic = "RETN", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 4, Mnemonic = "RETN", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 5, Mnemonic = "RETN", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3 } },
            new OpcodeDefinition { X = 1, Z = 5, Y = 6, Mnemonic = "RETN", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 3 } },

            new OpcodeDefinition { X = 1, Z = 6, Y = 0, Mnemonic = "IM 0", Ext1 = 0xED, Cycles = new[] { 4, 4 }, Instruction = typeof(IntModeInstruction) },
            new OpcodeDefinition { X = 1, Z = 6, Y = 1, Mnemonic = "IM 0/1", Ext1 = 0xED, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 2, Mnemonic = "IM 1", Ext1 = 0xED, Cycles = new[] { 4, 4 }, Instruction = typeof(IntModeInstruction) },
            new OpcodeDefinition { X = 1, Z = 6, Y = 3, Mnemonic = "IM 2", Ext1 = 0xED, Cycles = new[] { 4, 4 }, Instruction = typeof(IntModeInstruction) },
            new OpcodeDefinition { X = 1, Z = 6, Y = 4, Mnemonic = "IM 0", Ext1 = 0xED, Cycles = new[] { 4, 4 }, Instruction = typeof(IntModeInstruction) },
            new OpcodeDefinition { X = 1, Z = 6, Y = 5, Mnemonic = "IM 0/1", Ext1 = 0xED, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 6, Y = 6, Mnemonic = "IM 1", Ext1 = 0xED, Cycles = new[] { 4, 4 }, Instruction = typeof(IntModeInstruction) },
            new OpcodeDefinition { X = 1, Z = 6, Y = 7, Mnemonic = "IM 2", Ext1 = 0xED, Cycles = new[] { 4, 4 }, Instruction = typeof(IntModeInstruction) },

            new OpcodeDefinition { X = 1, Z = 7, Y = 0, Mnemonic = "LD I, A", Ext1 = 0xED, Cycles = new[] { 4, 5 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 1, Mnemonic = "LD R, A", Ext1 = 0xED, Cycles = new[] { 4, 5 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 2, Mnemonic = "LD A, I", Ext1 = 0xED, Cycles = new[] { 4, 5 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 3, Mnemonic = "LD A, R", Ext1 = 0xED, Cycles = new[] { 4, 5 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 4, Mnemonic = "RRD", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 5, Mnemonic = "RLD", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 4, 3 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 6, Mnemonic = "NOP*", Ext1 = 0xED, Cycles = new[] { 4, 4 } },
            new OpcodeDefinition { X = 1, Z = 7, Y = 7, Mnemonic = "NOP*", Ext1 = 0xED, Cycles = new[] { 4, 4 } },
            #endregion

            #region X = 2
            new OpcodeDefinition { X = 2, Z = 0, Y = 4, Mnemonic = "LDI", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 5 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 5, Mnemonic = "LDD", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 5 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 6, Mnemonic = "LDIR", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 5, 5 } },
            new OpcodeDefinition { X = 2, Z = 0, Y = 7, Mnemonic = "LDDR", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 5, 5 } },

            new OpcodeDefinition { X = 2, Z = 1, Y = 4, Mnemonic = "CPI", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 5 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 5, Mnemonic = "CPD", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 5 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 6, Mnemonic = "CPIR", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 5, 5 }, AltCycles = new[] { 4, 4, 3, 5 } },
            new OpcodeDefinition { X = 2, Z = 1, Y = 7, Mnemonic = "CPDR", Ext1 = 0xED, Cycles = new[] { 4, 4, 3, 5, 5 }, AltCycles = new[] { 4, 4, 3, 5 } },

            new OpcodeDefinition { X = 2, Z = 2, Y = 4, Mnemonic = "INI", Ext1 = 0xED, Cycles = new[] { 4, 5, 3, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 5, Mnemonic = "IND", Ext1 = 0xED, Cycles = new[] { 4, 5, 3, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 6, Mnemonic = "INIR", Ext1 = 0xED, Cycles = new[] { 4, 5, 3, 4, 5 }, AltCycles = new[] { 4, 5, 3, 4 } },
            new OpcodeDefinition { X = 2, Z = 2, Y = 7, Mnemonic = "INDR", Ext1 = 0xED, Cycles = new[] { 4, 5, 3, 4, 5 }, AltCycles = new[] { 4, 5, 3, 4 } },

            new OpcodeDefinition { X = 2, Z = 3, Y = 4, Mnemonic = "OUTI", Ext1 = 0xED, Cycles = new[] { 4, 5, 3, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 5, Mnemonic = "OUTD", Ext1 = 0xED, Cycles = new[] { 4, 5, 3, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 6, Mnemonic = "OUTIR", Ext1 = 0xED, Cycles = new[] { 4, 5, 3, 4, 5 }, AltCycles = new[] { 4, 5, 3, 4 } },
            new OpcodeDefinition { X = 2, Z = 3, Y = 7, Mnemonic = "OUTDR", Ext1 = 0xED, Cycles = new[] { 4, 5, 3, 4, 5 }, AltCycles = new[] { 4, 5, 3, 4 } },
            #endregion
            #endregion  // ED

            #region DD prefixed (IX)
            #region X = 0
            new OpcodeDefinition { X = 0, Z = 1, Q = 0, P = 2, Mnemonic = "LD IX, {0}", Ext1 = 0xDD, nn = true, Cycles = new[] { 4, 4, 3, 3 }, Instruction = typeof(LoadImmediate16Instruction) },

            new OpcodeDefinition { X = 0, Z = 3, Q = 0, P = 2, Mnemonic = "INC IX", Ext1 = 0xDD, Cycles = new[] { 4, 6 }, Instruction = typeof(Inc16Instruction) },
            new OpcodeDefinition { X = 0, Z = 3, Q = 1, P = 2, Mnemonic = "DEC IX", Ext1 = 0xDD, Cycles = new[] { 4, 6 }, Instruction = typeof(Dec16Instruction) },

            new OpcodeDefinition { X = 0, Z = 4, Y = 4, Mnemonic = "INC IXh", Ext1 = 0xDD, Cycles = new[] { 4, 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 5, Mnemonic = "INC IXl", Ext1 = 0xDD, Cycles = new[] { 4, 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 6, Mnemonic = "INC (IX{0})", Ext1 = 0xDD, d = true, Cycles = new[] { 4, 4, 3, 5, 4, 3 }, Instruction = typeof(IncDecShiftedIndirectInstruction) },

            new OpcodeDefinition { X = 0, Z = 5, Y = 4, Mnemonic = "DEC IXh", Ext1 = 0xDD, Cycles = new[] { 4, 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 5, Mnemonic = "DEC IXl", Ext1 = 0xDD, Cycles = new[] { 4, 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 6, Mnemonic = "DEC (IX{0})", Ext1 = 0xDD, d = true, Cycles = new[] { 4, 4, 3, 5, 4, 3 }, Instruction = typeof(IncDecShiftedIndirectInstruction) },

            new OpcodeDefinition { X = 0, Z = 6, Y = 4, Mnemonic = "LD IXh, {0}", Ext1 = 0xDD, n = true, Cycles = new[] { 4, 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            new OpcodeDefinition { X = 0, Z = 6, Y = 5, Mnemonic = "LD IXl, {0}", Ext1 = 0xDD, n = true, Cycles = new[] { 4, 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            #endregion

            #region X = 3
            new OpcodeDefinition { X = 3, Z = 1, Q = 0, P = 2, Mnemonic = "POP IX", Ext1 = 0xDD, Cycles = new[] { 4, 4, 3, 3 }, Instruction = typeof(PopInstruction) },

            new OpcodeDefinition { X = 3, Z = 5, Q = 0, P = 2, Mnemonic = "PUSH IX", Ext1 = 0xDD, Cycles = new[] { 4, 5, 3, 3 }, Instruction = typeof(PushInstruction) },
            #endregion
            #endregion  //DD

            #region FD prefixed (IY)
            #region X = 0
            new OpcodeDefinition { X = 0, Z = 1, Q = 0, P = 2, Mnemonic = "LD IY, {0}", Ext1 = 0xFD, nn = true, Cycles = new[] { 4, 4, 3, 3 }, Instruction = typeof(LoadImmediate16Instruction) },

            new OpcodeDefinition { X = 0, Z = 3, Q = 0, P = 2, Mnemonic = "INC IY", Ext1 = 0xFD, Cycles = new[] { 4, 6 }, Instruction = typeof(Inc16Instruction) },
            new OpcodeDefinition { X = 0, Z = 3, Q = 1, P = 2, Mnemonic = "DEC IY", Ext1 = 0xFD, Cycles = new[] { 4, 6 }, Instruction = typeof(Dec16Instruction) },

            new OpcodeDefinition { X = 0, Z = 4, Y = 4, Mnemonic = "INC IYh", Ext1 = 0xFD, Cycles = new[] { 4, 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 5, Mnemonic = "INC IYl", Ext1 = 0xFD, Cycles = new[] { 4, 4 }, Instruction = typeof(Inc8Instruction) },
            new OpcodeDefinition { X = 0, Z = 4, Y = 6, Mnemonic = "INC (IY{0})", Ext1 = 0xFD, d = true, Cycles = new[] { 4, 4, 3, 5, 4, 3 }, Instruction = typeof(IncDecShiftedIndirectInstruction) },

            new OpcodeDefinition { X = 0, Z = 5, Y = 4, Mnemonic = "DEC IYh", Ext1 = 0xFD, Cycles = new[] { 4, 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 5, Mnemonic = "DEC IYl", Ext1 = 0xFD, Cycles = new[] { 4, 4 }, Instruction = typeof(Dec8Instruction) },
            new OpcodeDefinition { X = 0, Z = 5, Y = 6, Mnemonic = "DEC (IY{0})", Ext1 = 0xFD, d = true, Cycles = new[] { 4, 4, 3, 5, 4, 3 }, Instruction = typeof(IncDecShiftedIndirectInstruction) },

            new OpcodeDefinition { X = 0, Z = 6, Y = 4, Mnemonic = "LD IYh, {0}", Ext1 = 0xFD, n = true, Cycles = new[] { 4, 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            new OpcodeDefinition { X = 0, Z = 6, Y = 5, Mnemonic = "LD IYl, {0}", Ext1 = 0xFD, n = true, Cycles = new[] { 4, 4, 3 }, Instruction = typeof(LoadImmediate8Instruction) },
            #endregion

            #region X = 3
            new OpcodeDefinition { X = 3, Z = 1, Q = 0, P = 2, Mnemonic = "POP IY", Ext1 = 0xFD, Cycles = new[] { 4, 4, 3, 3 }, Instruction = typeof(PopInstruction) },

            new OpcodeDefinition { X = 3, Z = 5, Q = 0, P = 2, Mnemonic = "PUSH IY", Ext1 = 0xFD, Cycles = new[] { 4, 5, 3, 3 }, Instruction = typeof(PushInstruction) },
            #endregion
            #endregion  //FD

            #region DD-CB prefixed
            #region X = 0
            #endregion
            #endregion  //DD-CB

            #region FD-CB prefixed
            #region X = 0
            #endregion
            #endregion  //FD-CB

        };
    }
}
