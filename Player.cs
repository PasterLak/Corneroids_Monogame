using System;

namespace Corneroids
{
    public class Player : Mob
    {
        private byte id;

        private Inventory inventory;
        private Camera camera;
        

        public Player(string name, short hp) : base (name, hp)
        {
            inventory = new Inventory(5,5);
            
        }
    }
}
