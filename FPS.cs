using Microsoft.Xna.Framework;

namespace Corneroids
{
    class FPS
    {
        
        private float fps = 0f;
        private float totalTime;
        private float displayFPS;

        public FPS()
        {
            totalTime = 0f;
            displayFPS = 0f;
        }
    
        public void DrawFpsCount(GameTime gameTime)
        {

            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            totalTime += elapsed;

            if (totalTime >= 1000)
            {
                displayFPS = fps;
                fps = 0;
                totalTime = 0;
            }
            fps++;

            Engine.spriteBatch.DrawString(Engine.font, displayFPS.ToString() + " FPS", new Vector2(10f, 10f), Color.White);
        }
    }
}