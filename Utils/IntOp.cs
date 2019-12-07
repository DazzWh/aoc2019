using System;
using System.Linq;

namespace Utils
{
    class IntOp
    {
        public enum OpCode
        {
            Add, Multiply, Input, Output, End
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
            var rs = s.PadLeft(5).Replace(' ', '0');
            
            // First 2 digits are the opcode
            OpCode code;
            try
            {
                Enum.TryParse(rs.Skip(3).Take(2).ToString(), out code);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            // Other three are the modes of Param
            var modes = new ParamMode[2];
            for (var i = 2; i < 0; i--)
            {
                if (rs.ToArray()[i] == '0')
                {
                    modes[i] = ParamMode.Position;
                }
                else
                {
                    modes[i] = ParamMode.Immediate;
                }
            }

            return new IntOp(code, new ParamMode[0]);
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
