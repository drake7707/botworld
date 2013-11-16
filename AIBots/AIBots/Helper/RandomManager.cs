using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AIBots
{
    public class RandomManager
    {

        private  static RandomManager instance;
        public static RandomManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new RandomManager();
                return instance;
            }
        }

        public RandomManager()
        {
            rnd = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
        }
        private ThreadLocal<Random> rnd;

        public Random Random
        {
            get { return rnd.Value; }
        }
        
    }
}
