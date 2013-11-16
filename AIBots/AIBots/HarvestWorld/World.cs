using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots.HarvestWorld
{
    public class World : AbstractWorld<Bot, Settings>
    {

        public List<Crystal> Crystals { get; set; }
        public RectangleF BaseArea { get; private set; }

        public override void Update()
        {
            foreach (Bot b in Bots)
                b.Update();
        }

        public override void Initialize(Settings settings, IEnumerable<Bot> bots = null)
        {
            this.Settings = settings;

            if (bots == null)
                CreateBots();
            else
            {
                Bots = bots.ToList();
                foreach (var b in Bots)
                    b.World = this;
            }

            Random rnd = RandomManager.Instance.Random;

            Crystals = new List<Crystal>();

            //Crystal c = new Crystal(this);
            //c.Position = new PointF(0.5f, 0.8f);
            //Crystals.Add(c);

            BaseArea = RectangleF.FromLTRB(0.40f, 0.4f, 0.6f, 0.6f);

            RectangleF baseAreaWithPadding = new RectangleF(BaseArea.Location, BaseArea.Size);
            baseAreaWithPadding.Inflate(0.2f, 0.2f);

            for (int i = 0; i < Settings.NrOfCrystals; i++)
            {
                Crystal c = new Crystal(this);

                RectangleF crystalRect;
                do
                {
                    c.Randomize(rnd);

                    crystalRect = RectangleF.FromLTRB(c.Position.X - Settings.CrystalAreaX / 2f,
                                                                       c.Position.Y - Settings.CrystalAreaY / 2f,
                                                                       c.Position.X + Settings.CrystalAreaX / 2f,
                                                                       c.Position.Y + Settings.CrystalAreaY / 2f);

                } while (baseAreaWithPadding.IntersectsWith(crystalRect));

                Crystals.Add(c);
            }


        }

        private void CreateBots()
        {
            Bots = new List<Bot>();
            for (int i = 0; i < Settings.NrOfBots; i++)
            {
                Bot b = new Bot(this);
                b.Randomize(RandomManager.Instance.Random);
                Bots.Add(b);
            }
        }

        public int GetClosestCrystalIndexThatHasValue(PointF pos, out float closestDistance)
        {
            float minDist = float.MaxValue;
            int index = -1;
            for (int i = 0; i < Crystals.Count; i++)
            {
                if (Crystals[i].Value > 0)
                {
                    float distance = MathHelper.Distance(pos, Crystals[i].Position);
                    if (distance < minDist)
                    {
                        index = i;
                        minDist = distance;
                    }
                }
            }
            closestDistance = minDist;
            return index;
        }


        public override void Draw(System.Drawing.Graphics g, int w, int h, int selectedBot)
        {
            HashSet<Bot> best5 = new HashSet<Bot>(Bots.OrderByDescending(b => b.Fitness).Take(5));
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            RectangleF baseRect = new RectangleF(BaseArea.Left * w, BaseArea.Top * h, BaseArea.Width * w, BaseArea.Height * h);
            using (SolidBrush br = new SolidBrush(Color.FromArgb(128, Color.Salmon)))
            {
                g.FillRectangle(br, baseRect);
            }


            float botSizeRadiusX = Settings.BotAreaX * w;
            float botSizeRadiusY = Settings.BotAreaY * h;

            int cur = 0;
            foreach (var b in Bots.ToArray())
            {
                PointF picPos = new PointF(b.Position.X * w, b.Position.Y * h);
                RectangleF r = RectangleF.FromLTRB(picPos.X - botSizeRadiusX, picPos.Y - botSizeRadiusY, picPos.X + botSizeRadiusX, picPos.Y + botSizeRadiusY);

                Color col;
                if (cur == selectedBot)
                    col = Color.Blue;
                else if (best5.Contains(b))
                    col = Color.Gold;
                else
                    col = Color.Red;

                using (Brush br = new SolidBrush(col))
                    g.FillEllipse(br, r);

                if (b.CarryingCrystal)
                    g.DrawRectangle(Pens.SkyBlue, r.X, r.Y, r.Width, r.Height);


                cur++;
            }

            using (SolidBrush br = new SolidBrush(Color.FromArgb(128, Color.SkyBlue)))
            {
                foreach (var crystal in Crystals)
                {
                    var crystalBounds = crystal.GetBounds(Settings);
                    PointF picPos = new PointF(crystal.Position.X * w, crystal.Position.Y * h);

                    //RectangleF r = RectangleF.FromLTRB(picPos.X - Settings.CrystalAreaX / 2f * w, picPos.Y - Settings.CrystalAreaY / 2f * h, picPos.X + Settings.CrystalAreaX / 2f * w, picPos.Y + Settings.CrystalAreaY / 2f * h);
                    RectangleF r = new RectangleF(crystalBounds.X * w, crystalBounds.Y * h, crystalBounds.Width * w, crystalBounds.Height * h);
                    g.FillEllipse(Brushes.SkyBlue, new RectangleF(picPos.X - 2, picPos.Y - 2, 4, 4));
                    g.FillRectangle(br, r);
                }
            }

        }

        public override void DrawOverlay(System.Drawing.Graphics g, int w, int h, int selectedBot)
        {
            if (selectedBot < Bots.Count)
            {
                Bot b = Bots[selectedBot];
                b.DrawBotVision(g, w, h);
            }
        }

        internal Crystal IsInAreaOfCrystal(Bot bot)
        {
            RectangleF botRect = RectangleF.FromLTRB(bot.Position.X - Settings.BotAreaX / 2f,
                                                     bot.Position.Y - Settings.BotAreaY / 2f,
                                                     bot.Position.X + Settings.BotAreaX / 2f,
                                                     bot.Position.Y + Settings.BotAreaY / 2f);

            foreach (var c in Crystals)
            {
                RectangleF crystalRect = c.GetBounds(Settings);

                if (botRect.IntersectsWith(crystalRect))
                    return c;
            }
            return null;

        }

        internal bool IsInsideBaseRegion(Bot bot)
        {
            RectangleF botRect = RectangleF.FromLTRB(bot.Position.X - Settings.BotAreaX / 2f,
                                                   bot.Position.Y - Settings.BotAreaY / 2f,
                                                   bot.Position.X + Settings.BotAreaX / 2f,
                                                   bot.Position.Y + Settings.BotAreaY / 2f);

            return BaseArea.Contains(botRect);
        }
    }
}
