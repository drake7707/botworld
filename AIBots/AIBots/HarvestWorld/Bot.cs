using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace AIBots.HarvestWorld
{
    public class Bot : AbstractBot<World, Settings>
    {

        public PointF Position { get; set; }
        public bool CarryingCrystal { get; set; }
        public float Score { get; set; }


        public Bot(World world, float[] weights = null, long token = -1)
            : base(world, world.Settings, token)
        {
            // input X, Y, nearCrystal, inBaseRegion, carrying crystal
            // output vX, vY, takeCrystal, dropCrystal
            Network = new NeuralNetwork(13, 4, Settings.NrOfHiddenLayers, Settings.NrOfNeuronsPerHiddenLayer, Settings.Bias, weights);
        }

        public override float Fitness
        {
            get { return Score; }
        }


        public override void Randomize(Random rnd)
        {
            Position = new PointF((float)rnd.NextDouble(), (float)rnd.NextDouble());
            //Position = new PointF(0.5f, 0.5f);
            Score = 0;
            CarryingCrystal = false;
        }

        public override float[][] CurrentNeuronState
        {
            get
            {

                Crystal nearCrystal;
                bool isInsideBaseRegion;
                float[] input;
                float distanceToCrystal;
                float distanceToBase;
                Vector toCrystal;
                Vector toBaseCenter;
                GetInput(out nearCrystal, out isInsideBaseRegion, out toCrystal, out distanceToCrystal, out toBaseCenter, out distanceToBase, out input);
                return Network.CalculateActivationPerNeuron(input);

            }
        }

        private float oldDistanceToCrystal;
        private float oldDistanceToBase;

        public override void Update()
        {
            Crystal nearCrystal;
            bool isInsideBaseRegion;
            float[] input;

            float distanceToCrystal;
            float distanceToBase;
            Vector toCrystal;
            Vector toBaseCenter;
            GetInput(out nearCrystal, out isInsideBaseRegion, out toCrystal, out distanceToCrystal, out toBaseCenter, out distanceToBase, out input);

            float[] output = Network.Calculate(input);

            if (Settings.TrainBots)
            {
                float[] expectedOutput = GetExpectedOutput(nearCrystal, isInsideBaseRegion, ref toCrystal, ref toBaseCenter);

                Network.Train(input, expectedOutput, Settings.LearningRate);    
                /*
                var n1 = new NeuralNetwork(Network.NrOfInputs, Network.NrOfOutputs, Network.NrOfHiddenLayers, Network.NrOfNeuronsPerHiddenLayer, Settings.Bias, Network.GetAllWeights());
                var n2 = new NeuralNetwork(Network.NrOfInputs, Network.NrOfOutputs, Network.NrOfHiddenLayers, Network.NrOfNeuronsPerHiddenLayer, Settings.Bias, Network.GetAllWeights()); 

                Stopwatch w = new Stopwatch();
                w.Start();
                for (int i = 0; i < 100000; i++)
                {
                    n1.Train(input, expectedOutput, Settings.LearningRate);    
                }
                w.Stop();

                
                Stopwatch w2 = new Stopwatch();
                w2.Start();
                for (int i = 0; i < 100000; i++)
                {
                    n2.TrainOptimized(input, expectedOutput, Settings.LearningRate);
                }
                w2.Stop();
                
                Console.WriteLine(w.ElapsedMilliseconds + " " + w2.ElapsedMilliseconds);
                 */
                //Network.Train(input, expectedOutput, Settings.LearningRate);

                // lets see if expected is correct
                // it sure is!
                //output = expectedOutput;

            }

            float vX = Settings.MaxBotSpeed * (output[0] * 2 - 1f);
            float vY = Settings.MaxBotSpeed * (output[1] * 2 - 1f);
            bool takeCrystal = output[2] > 0.5f;
            bool dropCrystal = output[3] > 0.5f;


            float x = Position.X + vX;
            float y = Position.Y + vY;

            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > 1) x = 1;
            if (y > 1) y = 1;

            Position = new PointF(x, y);


            if (isInsideBaseRegion && CarryingCrystal && dropCrystal)
            {
                Score += 10;
                CarryingCrystal = false;
            }
            else if (nearCrystal != null && nearCrystal.Value > 0 && !CarryingCrystal && takeCrystal)
            {
                Score += 1;
                CarryingCrystal = true;
                nearCrystal.Value--;
            }
            else if (!CarryingCrystal && dropCrystal)
                Score--;
            else if (CarryingCrystal && takeCrystal)
                Score--;

            oldDistanceToCrystal = distanceToCrystal;
            oldDistanceToBase = distanceToBase;
        }

        private float[] GetExpectedOutput(Crystal nearCrystal, bool isInsideBaseRegion, ref Vector toCrystal, ref Vector toBaseCenter)
        {
            float expectedvX;
            float expectedvY;
            float expectedTakeCrystal;
            float expectedDropCrystal;

            if (!CarryingCrystal)
            {
                if (nearCrystal != null)
                {
                    expectedvX = 0;
                    expectedvY = 0;
                    expectedTakeCrystal = 1;
                    expectedDropCrystal = 0;
                }
                else
                {
                    expectedvX = toCrystal.X;
                    expectedvY = toCrystal.Y;
                    expectedTakeCrystal = 0;
                    expectedDropCrystal = 0;
                }
            }
            else
            {
                if (isInsideBaseRegion)
                {
                    expectedvX = 0;
                    expectedvY = 0;
                    expectedTakeCrystal = 0;
                    expectedDropCrystal = 1;
                }
                else
                {
                    expectedvX = toBaseCenter.X;
                    expectedvY = toBaseCenter.Y;
                    expectedTakeCrystal = 0;
                    expectedDropCrystal = 0;
                }
            }
            float[] expectedOutput = new float[] { expectedvX, expectedvY, expectedTakeCrystal, expectedDropCrystal };
            return expectedOutput;
        }



        private void GetInput(out Crystal nearCrystal, out bool isInsideBaseRegion, out Vector vToClosestCrystal, out float distanceToCrystal, out Vector toBaseCenter, out float distanceToBase, out float[] input)
        {
            nearCrystal = World.IsInAreaOfCrystal(this);
            isInsideBaseRegion = World.IsInsideBaseRegion(this);

            int crystalIdx = World.GetClosestCrystalIndexThatHasValue(this.Position, out distanceToCrystal);
            Crystal c;
            if (crystalIdx == -1)
            {
                distanceToCrystal = 0;
                vToClosestCrystal = new Vector(0, 0);
            }
            else
            {
                c = World.Crystals[crystalIdx];
                vToClosestCrystal = Vector.FromPoints(Position, c.Position)
                                              .ScaleTo01Range();
            }

            PointF baseCenter = new PointF(World.BaseArea.Location.X + World.BaseArea.Width / 2f, World.BaseArea.Location.Y + World.BaseArea.Height / 2f);
            toBaseCenter = Vector.FromPoints(Position, baseCenter)
                                        .ScaleTo01Range();

            distanceToBase = MathHelper.Distance(Position, baseCenter);

            input = new float[] { Position.X, Position.Y, vToClosestCrystal.X, vToClosestCrystal.Y, 
                                  toBaseCenter.X, toBaseCenter.Y,
                                  World.BaseArea.X, World.BaseArea.Y, World.BaseArea.Width, World.BaseArea.Height,  (nearCrystal != null) ? 1 : 0, isInsideBaseRegion ? 1 : 0, CarryingCrystal ? 1 : 0 };
        }


        public void DrawBotVision(Graphics g, int w, int h)
        {
            Crystal nearCrystal;
            bool isInsideBaseRegion;
            float[] input;

            float distanceToCrystal;
            float distanceToBase;
            Vector toCrystal;
            Vector toBaseCenter;


            int crystalIdx = World.GetClosestCrystalIndexThatHasValue(Position, out distanceToCrystal);

            if (crystalIdx != -1)
            {
                GetInput(out nearCrystal, out isInsideBaseRegion, out toCrystal, out distanceToCrystal, out toBaseCenter, out distanceToBase, out input);

                Crystal c = World.Crystals[crystalIdx];


                PointF picPosBot = new PointF(Position.X * w, Position.Y * h);
                PointF picPosCrystal = new PointF(c.Position.X * w, c.Position.Y * h);

                PointF picPosBaseCenter = new PointF((World.BaseArea.Left + World.BaseArea.Width / 2) * w,
                                                     (World.BaseArea.Top + World.BaseArea.Height / 2) * h);
                g.DrawLine(Pens.SkyBlue, picPosBot, picPosCrystal);

                g.DrawLine(Pens.Salmon, picPosBot, picPosBaseCenter);
            }
        }

    }

}
