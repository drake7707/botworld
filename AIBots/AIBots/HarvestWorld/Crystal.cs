using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots.HarvestWorld
{
    public class Crystal
    {
        private World world;
        public Crystal(World world)
        {
            this.world = world;
        }

        public PointF Position { get; set; }

        public int Value { get; set; }

        private static int MaxValue = 100;
        internal void Randomize(Random rnd)
        {
            Position = new PointF((float)rnd.NextDouble(), (float)rnd.NextDouble());
            Value = rnd.Next(10, MaxValue);
        }

        internal RectangleF GetBounds(Settings Settings)
        {
            float areaX = Settings.CrystalAreaX * (Value / (float)MaxValue);
            float areaY = Settings.CrystalAreaY * (Value / (float)MaxValue);

            return RectangleF.FromLTRB(Position.X - areaX / 2f,
                                       Position.Y - areaY / 2f,
                                       Position.X + areaX / 2f,
                                       Position.Y + areaY / 2f);
        }
    }
}
