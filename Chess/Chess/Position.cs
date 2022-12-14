using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class Position
    {
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }

        public Position() : this(0, 0) { }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
