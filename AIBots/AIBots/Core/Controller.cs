using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIBots
{
    public class Controller<World, Bot, S> 
        where S : AbstractSettings
        where World : IWorld<Bot, S>, new()
        where Bot : AbstractBot<World,S>, IGenetic
    {

        public Controller(S settings)
        {
            this.Settings = settings;
            Reset();
        }

        public S Settings { get; private set; }
        public List<World> Worlds { get; private set; }
        public GeneticEvolution<Bot> GeneticController { get; private set; }

        private int nrOfWorlds;


        public bool DoEvolve { get; set; }

        private bool stopped;

        public void Stop()
        {
            stopped = true;
        }
        public  void Start()
        {
            stopped = false;
            Task.Factory.StartNew(() =>
            {
                stopped = false;
                while (!stopped)
                {
                    if (DoEvolve)
                    {
                        Evolve();
                    }
                    else
                        System.Threading.Thread.Sleep(1000);
                }
            });
        }

        private void Evolve()
        {   
            Parallel.ForEach(Worlds, new ParallelOptions() { MaxDegreeOfParallelism = nrOfWorlds }, w =>
                {
                    //foreach (var w in Worlds)
                    //{
                        for (int i = 0; i < Settings.NrOfIterationsPerGeneration; i++)
                            w.Update();
                    //}
                });
            
            GeneticController.Breed();

            Worlds.Clear();
            for (int i = 0; i < nrOfWorlds; i++)
            {
                World w = new World();
                w.Initialize(Settings, GeneticController.Population.Skip(i * Settings.NrOfBots).Take(Settings.NrOfBots));
                Worlds.Add(w);
            }
        }

        public void Reset()
        {
            nrOfWorlds = Settings.NrOfConcurrentWorlds;
            Worlds = new List<World>();
            for (int i = 0; i < nrOfWorlds; i++)
            {
                World w = new World();
                w.Initialize(Settings);
                Worlds.Add(w);
            }
            GeneticController = new GeneticEvolution<Bot>(Settings, Worlds.SelectMany(w => w.Bots));
        }

    }
}
