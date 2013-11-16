using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIBots
{
    public abstract class AbstractSettings
    {

        public AbstractSettings()
        {
            Bias = -1;
            MaxMutationDeviance = 0.3f;
            MutationRate = 0.1f;
            NrOfElitistBastards = 4;
            NrOfIterationsPerGeneration = 500;
            UseHallOfFame = true;
            NrOfConcurrentWorlds = 4;
            NrOfBots = 30;
            NrOfHiddenLayers = 1;
            NrOfNeuronsPerHiddenLayer = 10;

            LearningRate = 0.02f;
        }

        public float Bias { get; set; }

        public float MaxMutationDeviance { get; set; }

        public float MutationRate { get; set; }

        public int NrOfIterationsPerGeneration { get; set; }

        public int NrOfHiddenLayers { get; set; }
        public int NrOfNeuronsPerHiddenLayer { get; set; }

        public float LearningRate { get; set; }

        public int NrOfElitistBastards { get; set; }

        public bool UseHallOfFame { get; set; }

        public int NrOfConcurrentWorlds { get; set; }

        public int NrOfBots { get; set; }
     
    }
}
