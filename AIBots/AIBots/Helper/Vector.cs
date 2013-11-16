using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots
{


    public struct Vector
    {
        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        private float x;
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public Vector Normalize()
        {
            float length = (float)Math.Sqrt(x * x + y * y);
            return new Vector(x / length, y / length);
        }

        public Vector ScaleTo01Range()
        {
            Vector v = Normalize();
            return new Vector((v.X + 1) / 2, (v.Y + 1) / 2);
        }

        public static Vector FromPoints(PointF p1, PointF p2)
        {
            return new Vector(p2.X - p1.X, p2.Y - p1.Y);
        }
    }

   
}
