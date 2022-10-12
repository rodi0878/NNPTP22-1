using System;

namespace NNPTPZ1.NewtonFractals
{
    class NewtonFractalsException : Exception
    {
        public NewtonFractalsException() { }

        public NewtonFractalsException(string message)
            : base(message)
        {

        }
    }
}
