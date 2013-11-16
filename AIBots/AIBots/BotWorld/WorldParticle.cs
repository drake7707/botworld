using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots.BotWorld
{
    public class WorldParticle
    {
        public PointF Position { get; set; }
        public int MaxLife { get; set; }
        public int Life { get; set; }

        public float Radius
        {
            get
            {
                return 0.05f * (1 - (Life / (float)MaxLife));
            }
        }
    }
}
