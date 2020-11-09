using System;
namespace Corneroids
{
    public class Player
    {
        private byte id;
        private string name;

        private Inventory inventory;
        private Camera camera;
        // picked block

        public Player()
        {
            inventory = new Inventory(5,5);
            
        }
    }
}
