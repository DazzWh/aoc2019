using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IntCode
{
    public class Computer
    {

        private int _pointer; // Position of current operation
        private Stack<int> _inputs; // Used for Input operation
        private readonly int[] _memory; // IntCode program that is being run
        private bool _running; // If the program is still running

        public bool OutputToConsole = true; // Output writes directly to the console log if true
        public bool StopAtOutput = false;   // Stops the program at the first output
        public List<string> OutputLog { get; private set; } // Output logs output to this list

        public Computer(string fileName)
        {
            _memory = File.ReadLines(fileName).Single().Split(",").Select(int.Parse).ToArray();
        }
        
        public Computer(string fileName, int[] inputsArray)
        {
            _memory = File.ReadLines(fileName).Single().Split(",").Select(int.Parse).ToArray();
            AddInputs(inputsArray);
        }

        public void AddInputs(int[] inputsArray)
        {
            if (_inputs == null)
            {
                _inputs = new Stack<int>(inputsArray.Reverse());
            }
            else
            {
                foreach (var i in inputsArray.Reverse())
                {
                    _inputs.Push(i);
                }
            }
        }

        public void Run()
        {
            _running = true;
            OutputLog = new List<string>();

            while (_running)
            {
                // Get the operation at the current position
                var op = IntOp.ParseFromString(_memory[_pointer].ToString());

                // Call the relevant operation
                // Operations move the position value
                RunOperation(op);
            }
        }

        private void RunOperation(IntOp operation)
        {
            switch (operation.Code)
            {
                case IntOp.OpCode.Add: Add(operation); break;
                case IntOp.OpCode.Multiply: Multiply(operation); break;
                case IntOp.OpCode.Input: Input(); break;
                case IntOp.OpCode.Output: Output(operation); break;
                case IntOp.OpCode.JumpIfTrue: JumpIfTrue(operation); break;
                case IntOp.OpCode.JumpIfFalse: JumpIfFalse(operation); break;
                case IntOp.OpCode.LessThan: LessThan(operation); break;
                case IntOp.OpCode.Equals: Equals(operation); break;
                case IntOp.OpCode.End: _running = false; break;

                default:
                    throw new Exception("Invalid OpCode in operation");
            }
        }

        private int GetParam(int i, IntOp o)
        {
            return GetValueAt(_pointer + i, o.GetMode(i - 1));
        }

        private int GetParam(int i, IntOp.ParamMode mode)
        {
            return GetValueAt(_pointer + i, mode);
        }

        private int GetValueAt(int pos, IntOp.ParamMode mode)
        {
            switch (mode)
            {
                case IntOp.ParamMode.Position:
                    // Use the value in the position as a position for the return value
                    var valueLocation = _memory[pos];
                    return _memory[valueLocation];

                case IntOp.ParamMode.Immediate:
                    // Return the value in the position
                    return _memory[pos];

                default:
                    throw new Exception("Invalid ParamMode");
            }
        }

        private void Add(IntOp o)
        {
            var a = GetParam(1, o);
            var b = GetParam(2, o);
            var p = GetParam(3, IntOp.ParamMode.Immediate);
            _memory[p] = a + b;
            _pointer += 4;
        }

        private void Multiply(IntOp o)
        {
            var a = GetParam(1, o);
            var b = GetParam(2, o);
            var p = GetParam(3, IntOp.ParamMode.Immediate);
            _memory[p] = a * b;
            _pointer += 4;
        }

        private void Input()
        {
            // If there is no inputs we need to wait until there is more
            if (_inputs.Count == 0)
            {
                _running = false;
                return;
            }

            // Skip GetParam as input is always immediate mode
            _memory[_memory[_pointer + 1]] = _inputs.Pop();
            _pointer += 2;
        }

        private void Output(IntOp o)
        {
            var output = GetParam(1, o);
            
            OutputLog.Add(output.ToString());
            if(OutputToConsole) Console.WriteLine(output);

            if (StopAtOutput) _running = false;

            _pointer += 2;
        }

        private void JumpIfTrue(IntOp o)
        {
            // If a is true, set position to b
            var a = GetParam(1, o);
            var b = GetParam(2, o);

            if (a != 0)
            {
                _pointer = b;
            }
            else
            {
                _pointer += 3;
            }
        }

        private void JumpIfFalse(IntOp o)
        {
            // If a is false, set position to b
            var a = GetParam(1, o);
            var b = GetParam(2, o);

            if (a == 0)
            {
                _pointer = b;
            }
            else
            {
                _pointer += 3;
            }
        }

        private void LessThan(IntOp o)
        {
            var a = GetParam(1, o);
            var b = GetParam(2, o);
            var p = GetParam(3, IntOp.ParamMode.Immediate);

            _memory[p] = a < b ? 1 : 0;

            _pointer += 4;
        }

        private void Equals(IntOp o)
        {
            var a = GetParam(1, o);
            var b = GetParam(2, o);
            var p = GetParam(3, IntOp.ParamMode.Immediate);

            _memory[p] = a == b ? 1 : 0;

            _pointer += 4;
        }
    }
}
