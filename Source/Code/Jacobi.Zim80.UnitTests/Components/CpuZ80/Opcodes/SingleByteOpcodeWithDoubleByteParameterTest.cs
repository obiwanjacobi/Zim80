using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FluentAssertions;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes.UnitTests
{
    [TestClass]
    public class SingleByteOpcodeWithDoubleByteParameterTest
    {
        private OpcodeBuilder _builder;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _builder = new OpcodeBuilder();
        }

        [TestMethod]
        public void Decode_AllSingleByteWithParametersOpcodes()
        {
            var defs = OpcodeDefinition.Defintions
                .Where(od => od.nn && !od.HasExtension)
                .OrderBy(od => od.Value);

            UInt16 p = 0;
            // this is a hubble test! (use its own data to test itself)
            foreach (var def in defs)
            {
                TestContext.WriteLine("{0:X} - {1}.", def.Value, string.Format(def.Text, p));
                ExecuteSingleByteDecode(def.Value, p++);
            }
        }

        private void ExecuteSingleByteDecode(byte singleByte, UInt16 parameter)
        {
            var opcode = ExecuteDecode(singleByte, parameter);
            opcode.Definition.IsEqualTo(new OpcodeByte(singleByte)).Should().BeTrue();
        }

        private Opcode ExecuteDecode(byte singleByteInstruction, UInt16 parameter)
        {
            _builder.Clear();
            _builder.Add(singleByteInstruction);
            _builder.Add((byte)(parameter & 0xFF));
            _builder.Add((byte)(parameter >> 8));

            return _builder.Opcode;
        }
    }
}
