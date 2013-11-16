using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIBots
{
    public class GeneticEvolution<T> where T : IGenetic
    {
        
        private List<T> population;

        public List<T> Population
        {
            get { return population; }
        }
        
        public int Generation { get; private set; }

        public List<GeneticHistory> History { get; private set; }

        private AbstractSettings settings;

        public GeneticEvolution(AbstractSettings settings, IEnumerable<T> initialPopulation)
        {
            this.settings = settings;

            population = initialPopulation.ToList();
            History = new List<GeneticHistory>();
        }

        //public void Evolve()
        //{
        //    foreach (var p in population)
        //        p.Update();
        //}

        private T SpinRoulette()
        {
            var rnd = RandomManager.Instance.Random;
            List<T> orderedPopulation = population.OrderByDescending(p => p.Fitness).Take(settings.NrOfElitistBastards).ToList();

            if (settings.UseHallOfFame)
            {
                float avgFitness = orderedPopulation.Average(p => p.Fitness);
                float hofAvgFitness = hallOfFame.Average(p => p.Fitness);
                if (avgFitness < hofAvgFitness)
                {
                    orderedPopulation = orderedPopulation.Where(p => p.Fitness < hofAvgFitness).Concat(hallOfFame.OrderBy(hof => rnd.NextDouble()).Take(1))
                                                         .OrderByDescending(p => p.Fitness)
                                                         .Take(settings.NrOfElitistBastards).ToList();
                }
            }

            float totalFitness = orderedPopulation.Sum(p => p.Fitness);
            double val = rnd.NextDouble() * totalFitness;

            float curFitness = 0;
            foreach (var p in orderedPopulation)
            {
                curFitness += p.Fitness;

                if (curFitness >= val)
                    return p;
            }
            return population.Last();
        }

        private List<T> hallOfFame = new List<T>();

        public List<T> HallOfFame
        {
            get { return hallOfFame; }
        }

        private void UpdateHallOfFame()
        {
            HashSet<T> uniqueItems = new HashSet<T>();
            foreach (var t in population.Concat(hallOfFame)
                                        .OrderByDescending(t => t.Fitness))
	        {
                if (uniqueItems.Count < 10)
                    uniqueItems.Add(t);
                else
                    break;
	        }
            hallOfFame = uniqueItems.ToList();
        }

        public void Breed()
        {
            UpdateHallOfFame();

            List<T> newPopulation = new List<T>(population.Count);
            T dad = SpinRoulette();
            T mom = SpinRoulette();

            newPopulation.Add((T)dad.Clone());
            newPopulation.Add((T)mom.Clone());
            for (int i = 0; i < population.Count - 2; i++)
            {
                T kid = (T)dad.BreedWith(mom);
                newPopulation.Add(kid);
            }


            GeneticHistory gh = new GeneticHistory()
            {
                MinFitness = population.Min(p => p.Fitness),
                MaxFitness = population.Max(p => p.Fitness),
                AvgFitness = population.Average(p => p.Fitness)
            };
            History.Add(gh);

            population = newPopulation;
            Generation++;

        }
    }

    public interface IGenetic
    {
        float Fitness { get; }
        IGenetic Clone();
        IGenetic BreedWith(IGenetic kind);
    }

    public class GeneticHistory
    {
        public float MaxFitness { get; set; }
        public float MinFitness { get; set; }
        public float AvgFitness { get; set; }
    }
}
