
using Microsoft.Xna.Framework;

namespace Corneroids
{
    public sealed class Voxel
    {
        public static readonly Vector2[] uv =
           {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0)
        };

        public static readonly Vector3[] point =
        {
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(1, 1, 0),
                new Vector3(1, 0, 0),
                // 4 - 7
                new Vector3(0, 0, 1),
                new Vector3(0, 1, 1),
                new Vector3(1, 1, 1),
                new Vector3(1, 0, 1)
         };
    }
}
