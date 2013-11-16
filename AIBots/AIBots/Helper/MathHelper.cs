using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots
{
    class MathHelper
    {

        public static float Sqrt2 { get; private set; }
        static MathHelper()
        {
            Sqrt2 = (float)Math.Sqrt(2);
        }

        public static float Distance(PointF p1, PointF p2)
        {
            return Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y);
          //  return (float)Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }

    }
}
