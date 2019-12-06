using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Utils
{
    public class IntCodeComputer
    {

        int position = 0; // Position of current operation
        private int inputValue; // Used for Input operation
        private List<int> program;

        public IntCodeComputer(List<int> initProgram)
        {
            program = new List<int>(initProgram);
        }
        
        public void Run()
        {
            while (true)
            {
                // Get the operation at the current position
                var op = IntOp.ParseFromString(program[position].ToString());

                // Call the relevant operation
                RunOperation(op);
            }
        }

        void RunOperation(IntOp operation)
        {
            switch (operation.Code)
            {
                case IntOp.OpCode.Add:
                    var a = GetValueAt(position + 1, operation.ParamModes[0]);
                    // TODO Change functions to handle all this
                    // Keep the switch statement as concise as possible.


                    break;

                default:
                    break;
            }
        }

        int GetValueAt(int pos, IntOp.ParamMode mode)
        {
            switch (mode)
            {
                case IntOp.ParamMode.Immediate:
                    // Return the value in the position
                    return program[pos];

                case IntOp.ParamMode.Position:
                    // Use the value in the position as a position for the return value 
                    return program[program[pos]];

                default:
                    throw new Exception("Invalid ParamMode");
            }
        }
        
        void Add(List<int> input, int a, int b, int pos)
        {
            input[pos] = a + b;
        }

        void Multiply(List<int> input, int a, int b, int pos)
        {
            input[pos] = a * b;
        }

        void Input(List<int> input, int inValue, int pos)
        {
            input[pos] = inValue;
        }

        void Output(List<int> input, int a)
        {
            Console.WriteLine(input[a]);
        }

    }
}
