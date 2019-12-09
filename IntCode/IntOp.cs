using System.Linq;

namespace IntCode
{
    internal class IntOp
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
            MoveRelative = 9,
            End = 99
        }

        public enum ParamMode
        {
            Position = 0, Immediate = 1, Relative = 2
        }

        public  readonly OpCode Code;
        private readonly ParamMode[] _modes;

        public IntOp(OpCode code, ParamMode[] modes)
        {
            Code = code;
            _modes = modes;
        }

        public static IntOp ParseFromString(string s)
        {
            // An IntOp comes in the form of 5 digits
            // ABCDE A B C are parameter modes, DE is the double digit operation code

            // Enter out any omitted 0s 
            s = s.PadLeft(5).Replace(' ', '0');
            
            // Get digits DE as the OpCode
            var code = (OpCode)int.Parse(s.Substring(3));

            // Get digits ABC as ParamModes
            var modes = new ParamMode[]{0, 0, 0};
            for (var i = 0; i < 3; i++)
            {
                modes[i] = (ParamMode) int.Parse(s.Substring(i, 1));
            }

            // ABC are read right to left, to correspond to 
            // A being param 0, reverse array.
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
