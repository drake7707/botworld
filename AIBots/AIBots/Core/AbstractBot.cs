using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AIBots
{
    public abstract class AbstractBot<W,S> : IGenetic, IBot
        where S : AbstractSettings
    {

        public W World { get; set; }
        
        public long Token { get; private set; }
        public NeuralNetwork Network { get; protected set; }

        private S settings;

        protected S Settings
        {
            get { return settings; }
        }

        public AbstractBot(W world, S settings, long token = -1)
        {
            this.World = world;
            this.settings = settings;

            if (token == -1)
                Token = BitConverter.ToInt64(System.Guid.NewGuid().ToByteArray(), 0);
            else
                Token = token;
        }

        public IGenetic BreedWith(IGenetic kind)
        {
            AbstractBot<W,S> other = (AbstractBot<W,S>)kind;
            
            float[] weights1 = this.Network.GetAllWeights();
            float[] weights2 = other.Network.GetAllWeights();

            int crossoverPoint = RandomManager.Instance.Random.Next(weights2.Length);

            float[] newWeights = new float[weights1.Length];
            for (int i = 0; i < crossoverPoint; i++)
            {
                newWeights[i] = weights1[i];
                Mutate(newWeights, i);
            }
            for (int i = crossoverPoint; i < weights2.Length; i++)
            {
                newWeights[i] = weights2[i];
                Mutate(newWeights, i);
            }
            return CreateBot(newWeights);
        }

        private void Mutate(float[] newWeights, int i)
        {
            // mutate
            if (RandomManager.Instance.Random.NextDouble() < settings.MutationRate)
            {
                newWeights[i] += (float)((RandomManager.Instance.Random.NextDouble() * 2 - 1) * settings.MaxMutationDeviance);
                if (newWeights[i] > 1) newWeights[i] = 1;
                if (newWeights[i] < -1) newWeights[i] = -1;
            }
        }

        public abstract float Fitness { get; }

        public abstract float[][] CurrentNeuronState { get; }

        public IGenetic Clone()
        {
            // don't clone score or alive time
            return CreateBot(Network.GetAllWeights(), Token);
        }

        public abstract void Update();

        protected AbstractBot<W, S> CreateBot(float[] newWeights, long token = -1)
        {
            var bot = (AbstractBot<W,S>)this.MemberwiseClone();
            
            var network = new NeuralNetwork(bot.Network.NrOfInputs, bot.Network.NrOfOutputs, bot.Network.NrOfHiddenLayers, bot.Network.NrOfNeuronsPerHiddenLayer, Settings.Bias, newWeights);
            bot.Token = token;
            bot.Network = network;
            bot.Randomize(RandomManager.Instance.Random);
            return bot;
        }
        //protected abstract AbstractBot<World,S> CreateBot(float[] newWeights, long token = -1);

        public abstract void Randomize(Random rnd);

    }
}
