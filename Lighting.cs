using Microsoft.Xna.Framework;

namespace Corneroids
{
    public class Lighting
    {
        public static readonly Color ClearColor = Color.Black;

        public static readonly bool LightingEnabled = true;
        public static readonly bool PreferPerPixelLighting = true;
        public static readonly Vector3 AmbientLightColor = new Color(105, 96, 86).ToVector3();
        

        public static readonly bool FogEnabled = true;
        public static readonly Vector3 FogColor = Color.Black.ToVector3();
        public static readonly float FogStart = 40f;
        public static readonly float FogEnd = 90;

        

        public void LoadParametresFromMod()
        {

        }
            

    }
}
