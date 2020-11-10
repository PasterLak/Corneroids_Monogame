using System;

namespace Corneroids
{
    public class Mob
    {
        private string name;
        private bool isAlive = true;

        private short hp;
        private short maxHp;

        public Mob(string name, short maxHp)
        {
            this.name = name;
            this.maxHp = maxHp;

            hp = maxHp;
        }

        public void SetDamage(short damage)
        {

        }

    }
}
