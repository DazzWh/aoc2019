using System;
using System.Collections.Generic;
using System.Text;

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

        public OpCode Code;
        private ParamMode[] ParamModes;

        public IntOp(OpCode code, ParamMode[] paramModes)
        {
            Code = code;
            ParamModes = paramModes;
        }

        public static IntOp ParseFromString(string s)
        {
            throw new NotImplementedException();
        }

        public ParamMode GetParamModeOf(int index)
        {
            if (index >= 0 && index < ParamModes.Length)
            {
                return ParamModes[index];
            }

            return ParamMode.Position;
        }
    }
}
