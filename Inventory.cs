using System;

namespace Corneroids
{
    public class Inventory
    {

        private byte width;
        private byte height;

        private Item[,] items;
        private Player owner;


        public Inventory(byte width, byte height)
        {
            this.width = width;
            this.height = height;

            items = new Item[width, height];
        }

         
    }
}
