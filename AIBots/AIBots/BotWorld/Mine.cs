using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots.BotWorld
{
    public class Mine
    {
        private World world;
        public Mine(World world)
        {
            this.world = world;
        }

        public PointF Position { get; set; }

        internal void Randomize(Random rnd)
        {
            Position = new PointF((float)rnd.NextDouble(), (float)rnd.NextDouble());
        }
    }
}
