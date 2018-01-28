using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProcGenHelpers
{
    class Linedef
    {
        public Point start;
        public Point end;

 
        public Linedef(Point a, Point b) {
            start = a;
            end = b;
        }



        ///true: x, false:y
        public bool Direction() {
            if (start.x == end.x) { return false; }
            if (start.y == end.y) { return true; }
            throw new Exception();
        }

        public Point AveragePoint() {
            return new Point((start.x+end.x)/2, (start.y + end.y) / 2);
        }

        public float Length() {            
            return (float)Math.Sqrt(((start.x-end.x)* (start.x - end.x)) + ((start.y - end.y) * (start.y - end.y)));
        }
    }
}
