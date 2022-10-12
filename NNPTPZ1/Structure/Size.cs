using System;

namespace NNPTPZ1.Structure
{
    public class Size<T>
    {
        public T Width { get; set; }
        public T Height { get; set; }

        public Size(T width, T height)
        {
            Width = width;
            Height = height;
        }
    }
}
