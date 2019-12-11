using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IntCode
{
    public class Computer
    {

        private long _pointer; // Position of current operation
        private Stack<long> _inputs; // Used for Input operation
        private Dictionary<long, long> _memory; // IntCode program that is being run
        private bool _running; // If the program is still running
        private long _relativeBase; // Used for ParamMode.Relative

        public bool OutputToConsole = true; // Output writes directly to the console log if true
        public bool StopAtOutput = false;   // Stops the program when output is given
        public List<string> OutputLog { get; private set; } // Output logs output to this list

        public Computer(string fileName)
        {
            var input = File.ReadLines(fileName).Single().Split(",").Select(long.Parse).ToArray();
            InitiateMemory(input);
        }

        private void InitiateMemory(IReadOnlyList<long> input)
        {
            _memory = new Dictionary<long, long>();
            for (var i = 0; i < input.Count; i++)
            {
                _memory.Add(i, input[i]);
            }
        }

        public void AddInput(long inputs)
        {
            AddInputs(new [] { inputs });
        }

        public void AddInputs(long[] inputsArray)
        {
            if (_inputs == null)
            {
                _inputs = new Stack<long>(inputsArray.Reverse());
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
                case IntOp.OpCode.Input: Input(operation); break;
                case IntOp.OpCode.Output: Output(operation); break;
                case IntOp.OpCode.JumpIfTrue: JumpIfTrue(operation); break;
                case IntOp.OpCode.JumpIfFalse: JumpIfFalse(operation); break;
                case IntOp.OpCode.LessThan: LessThan(operation); break;
                case IntOp.OpCode.Equals: Equals(operation); break;
                case IntOp.OpCode.MoveRelative: MoveRelative(operation); break;

                case IntOp.OpCode.End: 
                    _running = false;
                    OutputLog.Add("Complete");
                    break;

                default:
                    throw new Exception("Invalid OpCode in operation");
            }
        }

        private long GetValueAt(long pos, IntOp.ParamMode mode)
        {
            switch (mode)
            {
                case IntOp.ParamMode.Position:
                    // Use the value in the position as a position for the return value
                    var valueLocation = GetMemoryAtLocation(pos);
                    return GetMemoryAtLocation(valueLocation);

                case IntOp.ParamMode.Immediate:
                    // Return the value in the position
                    return GetMemoryAtLocation(pos);

                case IntOp.ParamMode.Relative:
                    // Use the value in the position plus relativeBase
                    var relativeLocation = GetMemoryAtLocation(pos) + _relativeBase;
                    return GetMemoryAtLocation(relativeLocation);

                default:
                    throw new Exception("Invalid ParamMode");
            }
        }
        private long GetMemoryAtLocation(long position)
        {
            return _memory.TryGetValue(position, out var value) ? value : 0;
        }

        private long GetParam(int i, IntOp o)
        {
            return GetValueAt(_pointer + i, o.GetMode(i - 1));
        }

        /// <summary>
        /// Gets the memory location relevant to the parameter and IntOp, and sets it to value
        /// Setting parameters will never be in ParamMode.Position.
        /// </summary>
        /// <param name="i"> The parameter of the operation, [A, B, C] => [1, 2, 3]</param>
        /// <param name="o"> The operation for the ParamMode </param>
        /// <param name="value"> The value to set the _memory at the location </param>
        private void SetParamAt(int i, IntOp o, long value)
        {
            var p = GetMemoryAtLocation(_pointer + i);
            if (o.GetMode(i - 1).Equals(IntOp.ParamMode.Relative))
            {
                p += _relativeBase;
            }

            _memory[p] = value;
        }

        private void Add(IntOp o)
        {
            var a = GetParam(1, o);
            var b = GetParam(2, o);
            SetParamAt(3, o, a + b);

            _pointer += 4;
        }

        private void Multiply(IntOp o)
        {
            var a = GetParam(1, o);
            var b = GetParam(2, o);
            SetParamAt(3, o, a * b);

            _pointer += 4;
        }

        private void Input(IntOp o)
        {
            // If there is no inputs we need to wait until there is more
            if (_inputs.Count == 0)
            {
                _running = false;
                return;
            }

            SetParamAt(3, o, _inputs.Pop());
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
            SetParamAt(3, o, a < b ? 1 : 0);

            _pointer += 4;
        }

        private void Equals(IntOp o)
        {
            var a = GetParam(1, o);
            var b = GetParam(2, o);
            SetParamAt(3, o,a == b ? 1 : 0);

            _pointer += 4;
        }

        private void MoveRelative(IntOp o)
        {
            var a = GetParam(1, o);
            _relativeBase += a;
            
            _pointer += 2;
        }
    }
}
