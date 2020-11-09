using System;
namespace Corneroids.Static
{
    public class Vector3Long
    {
        public long x;
        public long y;
        public long z;

        public Vector3Long(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3Long Zero
        {
            get { return new Vector3Long(0, 0, 0); }
        }



    }
}
