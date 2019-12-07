using System;
using System.Linq;

namespace Utils
{
    class IntOp
    {
        public enum OpCode
        {
            Add = 1, 
            Multiply = 2, 
            Input = 3, 
            Output = 4, 
            JumpIfTrue = 5, 
            JumpIfFalse = 6,
            LessThan = 7,
            Equals = 8,
            End = 99
        }

        public enum ParamMode
        {
            Position, Immediate
        }

        public readonly OpCode Code;
        private readonly ParamMode[] _modes;

        public IntOp(OpCode code, ParamMode[] modes)
        {
            Code = code;
            _modes = modes;
        }

        public static IntOp ParseFromString(string s)
        {
            
            // Fill the string with spaces, replace the spaces with 0s then reverse
            s = s.PadLeft(5).Replace(' ', '0');
            
            // Last digits are the opcode
            OpCode code = (OpCode)int.Parse(s.Substring(3));

            // Other three are the modes of Param
            var modes = new ParamMode[]{0, 0, 0};
            for (var i = 0; i < 3; i++)
            {
                if (s.ToArray()[i] == '0')
                {
                    modes[i] = ParamMode.Position;
                }
                else
                {
                    modes[i] = ParamMode.Immediate;
                }
            }

            modes = modes.Reverse().ToArray();

            return new IntOp(code, modes);
        }
        
        public ParamMode GetMode(int index)
        {
            if (index >= 0 && index < _modes.Length)
            {
                return _modes[index];
            }

            return ParamMode.Position;
        }
    }
}
