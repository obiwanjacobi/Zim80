using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO;
using System.Linq;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes.UnitTests
{
    [TestClass]
    public class SingleByteOpcodeWithNoParameterTest
    {
        private OpcodeBuilder _builder;

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _builder = new OpcodeBuilder();
        }


        [TestMethod]
        public void Decode_AllSingelByteOpcodes()
        {
            var defs = OpcodeDefinition.Defintions
                .Where(od => !od.HasParameters && !od.HasExtension)
                .OrderBy(od => od.Value);

            // this is a hubble test! (use its own data to test itself)
            foreach (var def in defs)
            {
                TestContext.WriteLine("{0:X} - {1}.", def.Value, def.Mnemonic);
                ExecuteSingleByteDecode(def.Value);
            }
        }

        [TestMethod]
        public void Decode_Nop_ReturnsOpcode()
        {
            ExecuteSingleByteDecode(0x00);
        }

        [TestMethod]
        public void Decode_Halt_ReturnsOpcode()
        {
            ExecuteSingleByteDecode(0x76);
        }

        private void ExecuteSingleByteDecode(byte singleByte)
        {
            var opcode = ExecuteDecode(singleByte);
            opcode.Definition.IsEqualTo(new OpcodeByte(singleByte)).Should().BeTrue();
        }

        private Opcode ExecuteDecode(byte singleByteInstruction)
        {
            _builder.Clear();
            _builder.Add(singleByteInstruction);
            return _builder.Opcode;
        }
    }
}
