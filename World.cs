using System;
using Microsoft.Xna.Framework;

namespace Corneroids
{
    public class World
    {
        private string name;
        private Itemset itemset;

        public Matrix matrix { get; set; }


        public World()
        {
            matrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);
        }
    }
}
