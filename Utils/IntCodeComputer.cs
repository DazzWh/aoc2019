using System;
using System.Collections.Generic;

namespace Utils
{
    public class IntCodeComputer
    {

        int position = 0; // Position of current operation
        private int inputValue; // Used for Input operation
        private List<int> program;
        private bool running = false;

        public IntCodeComputer(List<int> initProgram)
        {
            program = new List<int>(initProgram);
        }

        public IntCodeComputer(List<int> initProgram, int input)
        {
            program = new List<int>(initProgram);
            inputValue = input;
        }

        public void Run()
        {
            running = true;

            while (running)
            {
                // Get the operation at the current position
                var op = IntOp.ParseFromString(program[position].ToString());

                // Call the relevant operation
                // Operations move the position value
                RunOperation(op);
            }
        }

        void RunOperation(IntOp operation)
        {
            switch (operation.Code)
            {
                case IntOp.OpCode.Add:
                    Add(operation);
                    break;

                case IntOp.OpCode.Multiply:
                    Multiply(operation);
                    break;

                case IntOp.OpCode.Input:
                    Input(operation);
                    break;

                case IntOp.OpCode.Output:
                    Output(operation);
                    break;

                case IntOp.OpCode.End:
                    running = false;
                    break;

                default:
                    throw new Exception("Invalid OpCode in operation");
            }
        }

        int GetValueAt(int pos, IntOp.ParamMode mode)
        {
            switch (mode)
            {
                case IntOp.ParamMode.Position:
                    // Use the value in the position as a position for the return value
                    var valueLocation = program[pos];
                    return program[valueLocation];

                case IntOp.ParamMode.Immediate:
                    // Return the value in the position
                    return program[pos];

                default:
                    throw new Exception("Invalid ParamMode");
            }
        }
        
        void Add(IntOp o)
        {
            var a = GetValueAt(position + 1, o.GetMode(0));
            var b = GetValueAt(position + 2, o.GetMode(1));
            var p = GetValueAt(position + 3, IntOp.ParamMode.Immediate);
            program[p] = a + b;
            position += 4;
        }

        void Multiply(IntOp o)
        {
            var a = GetValueAt(position + 1, o.GetMode(0));
            var b = GetValueAt(position + 2, o.GetMode(1));
            var p = GetValueAt(position + 3, IntOp.ParamMode.Immediate);
            program[p] = a * b;
            position += 4;
        }

        void Input(IntOp o)
        {
            // Skip GetValueAt as input is always immediate mode
            program[program[position + 1]] = inputValue;
            position += 2;
        }

        void Output(IntOp o)
        {
            Console.WriteLine(GetValueAt(position + 1, o.GetMode(0)));
            position += 2;
        }

    }
}
