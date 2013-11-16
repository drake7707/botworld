using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIBots.BotWorld
{
    public class Settings : AbstractSettings
    {

        public Settings()
        {
            MaxBotSpeed = 0.01f;
     
            NrOfMines = 20;
            NrOfFood = 40;
            BotWithinRadius = 0.01f;
            MaxTurnAngle = 3.14f;
        }

     
        public float MaxBotSpeed { get; set; }

     
        public int NrOfMines { get; set; }

        public float BotWithinRadius { get; set; }
        public float MaxTurnAngle { get; set; }

     
        public int NrOfFood { get; set; }

    }
}
