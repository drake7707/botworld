using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots.BotWorld
{
    public class Bot : AbstractBot<World, Settings>
    {



        public Bot(World world, float[] weights = null, long token = -1)
            : base(world, world.Settings, token)
        {
            Network = new NeuralNetwork(10, 3, Settings.NrOfHiddenLayers, Settings.NrOfNeuronsPerHiddenLayer, Settings.Bias, weights);

        }


        private float angle;
        public Vector LookAt { get; set; }
        public PointF Position { get; set; }
        public float Speed { get; set; }

        public int Score { get; set; }
        public int MaxLife { get; set; }
        public int Life { get; set; }
        public int AliveTime { get; set; }

        public override void Update()
        {
            // no zombies!
            if (Life <= 0)
                return;

            int foodIdx;
            Food food;
            int mineIdx;
            Mine mine;
            float[] input;
            Bot other;
            GetInput(out foodIdx, out food, out mineIdx, out mine, out other, out input);

            float[] output = Network.Calculate(input);
            UpdateProperties(output);

            if (food != null && IsWithinRange(food.Position))
            {
                World .DestroyFood(foodIdx);
                Score++;
                Life += 200;
            }

            if (mine != null && IsWithinRange(mine.Position))
            {
                World .DestroyMine(mineIdx);
                Score -= 20;
                Life -= 400;
            }

            Life -= 2;
            AliveTime++;

            //if (Life > MaxLife)
            //    Life = MaxLife;

            if (Life < 0)
                Life = 0;
        }

        private void GetInput(out int foodIdx, out Food f, out int mineIdx, out Mine m, out Bot other, out float[] input)
        {
            Vector vToClosestFood;

            f = null;
            float closestDistanceFood;
            foodIdx = World .GetClosestFoodIndex(Position, out closestDistanceFood);
            if (foodIdx == -1)
            {
                closestDistanceFood = 0;
                vToClosestFood = new Vector(0, 0);
            }
            else
            {
                f = World.Food[foodIdx];
                vToClosestFood = Vector.FromPoints(Position, f.Position)
                                              .ScaleTo01Range();
            }

            Vector vToClosestMine;
            m = null;
            float closestDistanceMine;
            mineIdx = World.GetClosestMineIndex(Position, out closestDistanceMine);

            if (mineIdx == -1)
            {
                closestDistanceMine = 0;
                vToClosestMine = new Vector(0, 0);
            }
            else
            {
                m = World.Mines[mineIdx];
                vToClosestMine = Vector.FromPoints(Position, m.Position)
                                              .ScaleTo01Range();
            }


            Vector vToClosestBot;
            other = null;
            float closestDistanceBot;
            int otherIdx = World.GetClosestBotIndex(this, out closestDistanceBot);
            if (otherIdx == -1)
            {
                closestDistanceBot = 0;
                vToClosestBot = new Vector(0, 0);
            }
            else
            {
                other = World.Bots[otherIdx];
                vToClosestBot = Vector.FromPoints(Position, other.Position)
                                     .ScaleTo01Range();
            }

            Vector vLookAt = LookAt.ScaleTo01Range();

            input = new float[] {  vLookAt.X, vLookAt.Y, 
                                  Position.X, Position.Y,
                                  vToClosestFood.X, vToClosestFood.Y,
                                  vToClosestMine.X, vToClosestMine.Y,
                                  vToClosestBot.X, vToClosestBot.Y };


        }

        public bool IsWithinRange(PointF pos)
        {
            float dist = MathHelper.Distance(Position, pos);
            if (dist < Settings.BotWithinRadius)
                return true;
            else
                return false;
        }

        protected virtual void UpdatePosition()
        {
            float x = Position.X + Speed * LookAt.X;
            float y = Position.Y + Speed * LookAt.Y;

            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > 1) x = 1;
            if (y > 1) y = 1;

            //if (x > 0.001 && y > 0.001 && x < 0.999 && y < 0.999)
            //{
            //}
            //else
            //    Life--;

            Position = new PointF(x, y);
        }

        protected virtual void UpdateProperties(float[] output)
        {
            Speed = World.Settings.MaxBotSpeed * output[2];// -world.Settings.MaxBotSpeed * output[3];

            if (Speed < 0)
                Speed = 0;

            float leftTrack = output[0];
            float rightTrack = output[1];
            float rotationForce = rightTrack - leftTrack;
            if (rotationForce < -1) rotationForce = -1;
            if (rotationForce > 1) rotationForce = 1;
            float deltaAngle = rotationForce * Settings.MaxTurnAngle;
            angle += deltaAngle;
            LookAt = new Vector((float)Math.Sin(angle), (float)Math.Cos(angle));



            UpdatePosition();
        }


        public override void Randomize(Random rnd)
        {
            angle = (float)(rnd.NextDouble() * (2 * Math.PI) - Math.PI);
            LookAt = new Vector((float)Math.Sin(angle), (float)Math.Cos(angle))
                     .Normalize();
            Position = new PointF((float)rnd.NextDouble(), (float)rnd.NextDouble());
            Speed = (float)rnd.NextDouble();

            MaxLife = 100;
            Life = MaxLife;
            Score = 0;
        }

        public override float Fitness
        {
            //get { return Score / (Settings.Instance.NrOfIterationsPerGeneration / 1000f); }
            get
            {
                return (Score / World.TheoreticalMaxFitness) * 100;
                //return (Score/ 1) / (Settings.Instance.NrOfIterationsPerGeneration / 1000f);
            }
        }


        public float[] CurrentStateOutput
        {
            get
            {
                int foodIdx;
                Food food;
                int mineIdx;
                Mine mine;
                float[] input;
                Bot other;
                GetInput(out foodIdx, out food, out mineIdx, out mine, out other, out input);
                float[] output = Network.Calculate(input);
                return output;
            }
        }

        public override float[][] CurrentNeuronState
        {
            get
            {
                int foodIdx;
                Food food;
                int mineIdx;
                Mine mine;
                float[] input;
                Bot other;
                GetInput(out foodIdx, out food, out mineIdx, out mine, out other, out input);
                float[][] output = Network.CalculateActivationPerNeuron(input);
                return output;
            }
        }

        public void DrawBotVision(Graphics g, int w, int h)
        {
            int foodIdx;
            Food food;
            int mineIdx;
            Mine mine;
            float[] input;
            Bot other;
            GetInput(out foodIdx, out food, out mineIdx, out mine, out other, out input);

            PointF picPosBot = new PointF(Position.X * w, Position.Y * h);
            PointF picPosLookAt = new PointF(Position.X * w + LookAt.X * (0.05f * w), Position.Y * h + LookAt.Y * (0.05f * h));

            if (food != null)
            {
                PointF picPosFood = new PointF(food.Position.X * w, food.Position.Y * h);
                g.DrawLine(Pens.DarkGreen, picPosBot, picPosFood);
            }

            if (other != null)
            {
                PointF picPosOther = new PointF(other.Position.X * w, other.Position.Y * h);
                g.DrawLine(Pens.Red, picPosBot, picPosOther);
            }


            g.DrawLine(Pens.LightBlue, picPosBot, picPosLookAt);
            if (mine != null)
            {
                PointF picPosMine = new PointF(mine.Position.X * w, mine.Position.Y * h);
                g.DrawLine(Pens.Purple, picPosBot, picPosMine);
            }
        }
    }

}
