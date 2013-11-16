using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIBots.HarvestWorld
{
    public class Settings : AbstractSettings
    {
        public Settings()
        {

            CrystalAreaX = 0.05f;
            CrystalAreaY = 0.05f;

            BotAreaX = 0.005f;
            BotAreaY = 0.005f;

            MaxBotSpeed = 0.01f;

            NrOfCrystals = 10;

            NrOfHiddenLayers = 1;
            NrOfNeuronsPerHiddenLayer = 20;

            TrainBots = true;
        }

        public int NrOfCrystals { get; set; }

        public float CrystalAreaX { get; set; }
        public float CrystalAreaY { get; set; }

        public float BotAreaX { get; set; }
        public float BotAreaY { get; set; }

        public float MaxBotSpeed { get; set; }


        public bool TrainBots { get; set; }
    }
}
