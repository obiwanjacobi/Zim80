using Jacobi.Zim80.Components.CpuZ80.Opcodes;
using System;
using System.Globalization;

namespace Jacobi.Zim80.OpcodeCalc
{
    internal class Program
    {
        private string _currentLine;
        private Commands _command;
        private bool _displayHex;
        private Modes _mode;

        static void Main(string[] args)
        {
            PrintLogo();
            //PrintInstructions();

            var program = new Program();

            try
            {
                program.Run();
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteLine(e.ToString());
            }
        }

        private void Run()
        {
            PrintState();

            while (true)
            {
                WriteLine("Enter opcode:");
                WaitForInput();

                if (!DecodeCurrentLine())
                    continue;

                if (!ExecuteCommand())
                    return;
            }
        }

        private bool ExecuteCommand()
        {
            switch (_command)
            {
                case Commands.None:
                    ExecuteMode();
                    break;
                case Commands.DisplayHex:
                    _displayHex = true;
                    PrintState();
                    break;
                case Commands.DisplayDec:
                    _displayHex = false;
                    PrintState();
                    break;
                case Commands.Opcode:
                    _mode = Modes.Opcode;
                    PrintState();
                    break;
            }

            return true;
        }

        private void ExecuteMode()
        {
            switch (_mode)
            {
                case Modes.Opcode:
                    ConvertOpcode();
                    break;
                case Modes.Xyz:
                    break;
            }
        }

        private void ConvertOpcode()
        {
            ulong sprite = 0;

            var ns = _displayHex ? NumberStyles.HexNumber : NumberStyles.Integer;
            if (!ulong.TryParse(_currentLine, ns, CultureInfo.CurrentCulture, out sprite))
            {
                PrintSyntaxError();
                return;
            }

            var opcode = new OpcodeByte((byte)sprite);
            WriteLine(opcode.ToString());
        }

        private bool DecodeCurrentLine()
        {
            _command = Commands.None;
            if (string.IsNullOrEmpty(_currentLine)) return false;

            if (_currentLine.Length == 1)
            {
                if (string.Compare(_currentLine, "H", ignoreCase: true) == 0)
                    _command = Commands.DisplayHex;
                if (string.Compare(_currentLine, "D", ignoreCase: true) == 0)
                    _command = Commands.DisplayDec;

                if (string.Compare(_currentLine, "O", ignoreCase: true) == 0)
                    _command = Commands.Opcode;
            }

            return true;
        }

        private void WaitForInput()
        {
            _currentLine = Console.ReadLine();
        }

        private void PrintState()
        {
            var stateTxt = string.Format("Mode: {0}, Hex: {1}.", _mode, _displayHex);
            WriteLine(stateTxt);
        }

        private void PrintSyntaxError()
        {
            WriteLine("Does not compute... Try again.");
        }

        private static void PrintInstructions()
        {
            WriteLine("Commands:");
            WriteLine("H/h = display values in hexadecimal.");
            WriteLine("D/d = display values in decimal.");
            WriteLine("O/o = enter Opcode get X, Z, Y and P and Q values.");
            WriteLine("Ctrl+C = exit program.");
            WriteLine();
        }

        private static void PrintLogo()
        {
            WriteLine("Z80 Opcode Calculator v1.0");
            WriteLine("Jacobi Software (C) 2016");
            WriteLine();
        }

        private static void WriteLine(string text = null)
        {
            if (text == null) Console.WriteLine();

            Console.WriteLine(text);
        }
    }
}
