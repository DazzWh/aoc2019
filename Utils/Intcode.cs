﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public static class Intcode
    {

        /*
         OpCode, beginning of an instruction.
         
        OpCodes:
        99 = End program

        1  = Add
           array[1, x, y, z]
           array[z] = array[x] + array[y]

        2  = Multiply
           array[1, x, y, z]
           array[z] = array[x] * array[y]
        */

        // Address of current instruction is called the instruction pointer. (starts at 0)
        // Instructions can have different sizes of instructions, (some need 3 array some take 5 etc)
        // 

        public static List<int> Compute(List<int> input)
        {
            for (var i = 0; i < input.Count; i += 4)
            {
                switch (input[i])
                {
                    case 99: // End program
                        return input;

                    case 1: // Add
                        input[input[i + 3]] = input[input[i + 1]] + input[input[i + 2]];
                        break;

                    case 2: // Multiply
                        input[input[i + 3]] = input[input[i + 1]] * input[input[i + 2]];
                        break;

                    default:
                        throw new ArgumentException($"{input[i]} is an invalid operation");
                }
            }

            return input;
        }
    }
}