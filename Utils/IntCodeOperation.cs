using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    class IntCodeOperation
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
        public ParamMode[] ParamModes;

        public IntCodeOperation(OpCode code, ParamMode[] paramModes)
        {
            Code = code;
            ParamModes = paramModes;
        }

        public static IntCodeOperation ParseFromString(string s)
        {
            throw new NotImplementedException();
        }
    }
}
