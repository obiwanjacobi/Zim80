using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Jacobi.Zim80.Components.CpuZ80.States.Instructions;
using System;

namespace Jacobi.Zim80.Components.CpuZ80.Opcodes.UnitTests
{
    [TestClass]
    public class OpcodeDefinitionTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void OpcodeDefinition_Y_TranslatesToP_and_Q_1()
        {
            var od = new OpcodeDefinition(0, 7, 0);
            od.P.Should().Be(3);
            od.Q.Should().Be(1);
        }

        [TestMethod]
        public void OpcodeDefinition_Y_TranslatesToP_and_Q_2()
        {
            var od = new OpcodeDefinition(0, 5, 0);
            od.P.Should().Be(2);
            od.Q.Should().Be(1);
        }

        [TestMethod]
        public void OpcodeDefinition_Y_TranslatesToP_and_Q_3()
        {
            var od = new OpcodeDefinition(0, 3, 0);
            od.P.Should().Be(1);
            od.Q.Should().Be(1);
        }

        [TestMethod]
        public void OpcodeDefinition_P_andQ_TranslatesToY_1()
        {
            var od = new OpcodeDefinition(0, 3, 1, 0);
            od.Y.Should().Be(7);
        }

        [TestMethod]
        public void OpcodeDefinition_P_andQ_TranslatesToY_2()
        {
            var od = new OpcodeDefinition(0, 2, 1, 0);
            od.Y.Should().Be(5);
        }

        [TestMethod]
        public void OpcodeDefinition_P_andQ_TranslatesToY_3()
        {
            var od = new OpcodeDefinition(0, 1, 1, 0);
            od.Y.Should().Be(3);
        }

        [TestMethod]
        public void OpcodeDefinition_CanOnlyHaveMaxTwoParameters()
        {
            foreach (var od in OpcodeDefinition.Defintions)
            {
                var expected = 0;

                if (od.d) expected++;
                if (od.n) expected++;
                if (od.nn) expected++;

                expected.Should().BeInRange(0, 2);
            }
        }

        [TestMethod]
        public void OpcodeDefinition_MustHaveEnoughCycles()
        {
            foreach (var od in OpcodeDefinition.Defintions)
            {
                var expected = 1;

                if (od.d) expected++;
                if (od.n) expected++;
                if (od.nn) expected += 2;
                if (od.Ext1 != 0) expected++;
                if (od.Ext2 != 0) expected++;

                od.Cycles.Count().Should().BeGreaterOrEqualTo(expected, od.Mnemonic, "{0}", "{1}");
            }
        }

        [TestMethod]
        public void OpcodeDefinition_CanCreateInstruction()
        {
            var die = new Die();

            foreach (var od in OpcodeDefinition.Defintions)
            {
                if (od.Instruction == null) continue;

                var mnemonic = string.Format(od.Mnemonic, "{0}", "{1}");
                TestContext.WriteLine("{0}\t\t\t{1}", mnemonic, od.Instruction.Name);

                Action create = () => Instruction.Create<Instruction>(die, od);
                create.ShouldNotThrow(od.ToString());

            }
        }

        [TestMethod]
        public void OpcodeDefinition_MustHaveInstruction()
        {
            var implInstructions = OpcodeDefinition.Defintions
                                        .Where(od => od.Instruction != null);
            TestContext.WriteLine("{0} Implemented Opcode Definitions out of {1} ({2}%).", 
                implInstructions.Count(), 
                OpcodeDefinition.Defintions.Length, 
                ((float)implInstructions.Count()/ OpcodeDefinition.Defintions.Length) * 100);

            var missingInstructions = OpcodeDefinition.Defintions
                                        .Where(od => od.Instruction == null);

            if (missingInstructions.Any())
            {
                TestContext.WriteLine("{0} Opcode Definitions without Instruction types:", missingInstructions.Count());

                foreach (var od in missingInstructions)
                {
                    var mnemonic = string.Format(od.Mnemonic, "{0}", "{1}");
                    TestContext.WriteLine("{0}\t\t\tX={3}, Z={4}, Y={5}\t{1:X}-{2:X}", mnemonic, od.Ext1, od.Ext2, od.X, od.Z, od.Y);
                }

                Assert.Inconclusive();
            }
        }
    }
}
