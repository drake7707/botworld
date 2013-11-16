using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace AIBots.BotWorld
{
    public class World : IWorld<Bot, Settings>
    {
        public List<Bot> Bots { get; set; }
        public List<Mine> Mines { get; set; }
        public List<Food> Food { get; set; }

        public List<WorldParticle> Particles { get; set; }

        public Settings Settings { get; set; }

        public World()
        {

        }

        public void Initialize(Settings settings, IEnumerable<Bot> bots = null)
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

            Mines = new List<Mine>();
            for (int i = 0; i < Settings.NrOfMines; i++)
                SpawnMine();

            Food = new List<Food>();
            for (int i = 0; i < Settings.NrOfFood; i++)
                SpawnFood();

            Particles = new List<WorldParticle>();
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

        public void DestroyMine(int mineIdx)
        {
            Mine m = Mines[mineIdx];
            Mines.RemoveAt(mineIdx);
            SpawnMine();

            WorldParticle p = new WorldParticle() { Life = 20, MaxLife = 20, Position = m.Position };
            Particles.Add(p);
        }

        private void SpawnMine()
        {
            Mine m = new Mine(this);
            m.Randomize(RandomManager.Instance.Random);
            Mines.Add(m);
        }

        public void DestroyFood(int foodIdx)
        {
            Food m = Food[foodIdx];
            Food.RemoveAt(foodIdx);
            SpawnFood();
        }

        private void SpawnFood()
        {
            Food m = new Food(this);
            m.Randomize(RandomManager.Instance.Random);
            Food.Add(m);
        }

        public void Update()
        {
            //Parallel.ForEach(Bots, b => {
            foreach (Bot b in Bots)
            {
                b.Update();
                //});
            }

            foreach (var p in Particles.ToArray())
            {
                p.Life--;
                if (p.Life < 0)
                    Particles.Remove(p);
            }
        }

        public void Randomize()
        {
            foreach (var b in Bots)
                b.Randomize(RandomManager.Instance.Random);

            foreach (var m in Mines)
                m.Randomize(RandomManager.Instance.Random);
        }

        public int GetClosestMineIndex(PointF pos, out float closestDistance)
        {
            float minDist = float.MaxValue;
            int index = -1;
            for (int i = 0; i < Mines.Count; i++)
            {
                float distance = MathHelper.Distance(pos, Mines[i].Position);
                if (distance < minDist)
                {
                    index = i;
                    minDist = distance;
                }
            }
            closestDistance = minDist;
            return index;
        }

        public int GetClosestFoodIndex(PointF pos, out float closestDistance)
        {
            float minDist = float.MaxValue;
            int index = -1;
            for (int i = 0; i < Food.Count; i++)
            {
                float distance = MathHelper.Distance(pos, Food[i].Position);
                if (distance < minDist)
                {
                    index = i;
                    minDist = distance;
                }
            }
            closestDistance = minDist;
            return index;
        }


        public int GetClosestBotIndex(Bot b, out float closestDistance)
        {
            float minDist = float.MaxValue;
            int index = -1;
            for (int i = 0; i < Bots.Count; i++)
            {
                if (Bots[i] != b)
                {
                    float distance = MathHelper.Distance(b.Position, Bots[i].Position);
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


        private float tMaxFitness;
        private bool hasMaxFitness;
        public float TheoreticalMaxFitness
        {
            get
            {
                if (!hasMaxFitness)
                {
                    float val = (1f / (float)Math.Sqrt(Settings.NrOfFood)) / Settings.MaxBotSpeed;
                    tMaxFitness =  Settings.NrOfIterationsPerGeneration / val;
                    hasMaxFitness = true;
                }
                return tMaxFitness;
            }

        }


        public void Draw(Graphics g, int w, int h, int selectedBot)
        {
            HashSet<Bot> best5 = new HashSet<Bot>(Bots.OrderByDescending(b => b.Fitness).Take(5));
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            // g.Clear(Color.White);

            float botSizeRadius = Settings.BotWithinRadius * w;

            
            int cur = 0;
            foreach (var b in Bots.ToArray())
            {
                PointF picPos = new PointF(b.Position.X * w, b.Position.Y * h);
                RectangleF r = RectangleF.FromLTRB(picPos.X - botSizeRadius, picPos.Y - botSizeRadius, picPos.X + botSizeRadius, picPos.Y + botSizeRadius);

                if (b.Life > 0)
                {

                    Color col;
                    if (cur == selectedBot)
                        col = Color.Blue;
                    else if (best5.Contains(b))
                        col = Color.Gold;
                    else
                        col = Color.Red;

                    using (Brush br = new SolidBrush(col))
                        g.FillEllipse(br, r);

                    using (Pen p = new Pen(col, 2))
                    {
                        g.DrawLine(p, picPos, new PointF(picPos.X + b.LookAt.X * 1.5f * botSizeRadius,
                                                                picPos.Y + b.LookAt.Y * 1.5f * botSizeRadius));
                    }
                }
                else
                {
                    g.DrawLine(Pens.Gray, new PointF(r.Left, r.Top + 0.2f * r.Height), new PointF(r.Right, r.Top + 0.2f * r.Height));
                    g.DrawLine(Pens.Gray, new PointF(r.Left + r.Width / 2f, r.Top), new PointF(r.Left + r.Width / 2f, r.Bottom));
                }

                cur++;
            }

            float mineSizeRadius = 3;
            foreach (var m in Mines.ToArray())
            {
                PointF picPos = new PointF(m.Position.X * w, m.Position.Y * h);
                RectangleF r = RectangleF.FromLTRB(picPos.X - mineSizeRadius, picPos.Y - mineSizeRadius, picPos.X + mineSizeRadius, picPos.Y + mineSizeRadius);
                g.FillEllipse(Brushes.Purple, r);
            }

            float foodSizeRadius = 2;
            foreach (var m in Food.ToArray())
            {
                PointF picPos = new PointF(m.Position.X * w, m.Position.Y * h);
                RectangleF r = RectangleF.FromLTRB(picPos.X - foodSizeRadius, picPos.Y - foodSizeRadius, picPos.X + foodSizeRadius, picPos.Y + foodSizeRadius);
                g.FillEllipse(Brushes.DarkGreen, r);
            }

            foreach (var p in Particles.ToArray())
            {
                PointF picPos = new PointF(p.Position.X * w, p.Position.Y * h);
                float radius = p.Radius * w;
                RectangleF r = RectangleF.FromLTRB(picPos.X - radius, picPos.Y - radius, picPos.X + radius, picPos.Y + radius);
                g.DrawEllipse(Pens.Purple, r);
            }
        }

        public void DrawOverlay(Graphics g, int w, int h, int selectedBot)
        {
            if (selectedBot < Bots.Count)
            {
                Bot b = Bots[selectedBot];
                b.DrawBotVision(g, w, h);
            }
        }
    }

}
