using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Corneroids
{
    public class World
    {
        private string name;
        private Itemset itemset;
        private int seed;

        public Random random;


        private List<Player> players = new List<Player>();

        public Matrix matrix { get; set; }

        
        public World(int seed)
        {
            matrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);
            this.seed = seed;

            random = new Random(seed);

            players.Add(new Player("You!", 100));
        }

        public int GetSeed()
        {
            return seed;
        }
    }
}
