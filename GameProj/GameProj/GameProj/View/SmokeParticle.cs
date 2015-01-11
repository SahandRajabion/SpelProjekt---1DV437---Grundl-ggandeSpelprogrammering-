using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.View
{
    class SmokeParticle
    {

        private Vector2 newPosition;
        private Vector2 newVelocity;

        private static float maxSpeed = 0.2f;
        private float timeLived = 0;
        private float size = 0;
        private float maxLifeTime = 3.0f;
        private float lifePercent;
        private float minSize = 3f;
        private float maxSize = 7f;
        private float rotation = 0;

        public SmokeParticle()
        {
            Replay();
        }

        //Determines, if the particle has reached the maximum life time or not.
        public bool IsDead()
        {
            return timeLived >= maxLifeTime;
        }

        public void Update(float timeElapsed)
        {
            timeLived += timeElapsed;
            Vector2 acceleration = new Vector2(0, -0.4f);

            /*Calulates the lifePrecent (the state wich the particle has during the game) 
            of each particle based on timeLived so...*/
            lifePercent = timeLived / maxLifeTime;

            //...We can change the size of each particle during the "maxLifeTime".
            size = minSize + lifePercent * maxSize;

            //Sets the rotation of each particle every time it gets updated.
            rotation += 0.02f;

            //Sets a new Velosity to each Particle based on the Acceleration.
            newVelocity.X = newVelocity.X + timeElapsed * acceleration.X;
            newVelocity.Y = newVelocity.Y + timeElapsed * acceleration.Y;

            //Sets a new Position to each Particle based on the Velosity.
            newPosition.X = newPosition.X + timeElapsed * newVelocity.X;
            newPosition.Y = newPosition.Y + timeElapsed * newVelocity.Y;
        }

        public void Replay()
        {
            //Sets the "timeLived", "position" and "size" to the origin state after the particle has reached the Maxtime. 
            timeLived = 0;
            newPosition = new Vector2(0.5f, 1f);
            size = 0;

            //Randomizes the Velocity of the particle.
            Random random = new Random();
            newVelocity = new Vector2(((float)random.NextDouble() - 0.5f), ((float)random.NextDouble() - 0.5f));
            newVelocity.Normalize();
            newVelocity = newVelocity * ((float)random.NextDouble() * maxSpeed);
        }


        public void Draw(SpriteBatch spriteBatch, Texture2D smokeTexture, Camera camera)
        {
            //Creates the color effect of the particle depending on the lifePercent (the state wich the particle has during the game).
            float startValue = 1.0f;
            float endValue = 0.0f;
            float fade = endValue * lifePercent + (1.0f - lifePercent) * startValue;
            Color color = new Color(fade, fade, fade, fade);

            //Gets the visual coordinates for each particle to be drawn via the camera -class.
            int visualX = (int)camera.ToVisualX(newPosition.X);
            int visualY = (int)camera.ToVisualY(newPosition.Y);

            Vector2 targetRectangle = new Vector2(smokeTexture.Width / 2, smokeTexture.Height / 2);
            Rectangle sourceRectangle = new Rectangle(0, 0, smokeTexture.Width, smokeTexture.Height);
            Vector2 visualPosition = new Vector2(visualX, visualY);

            spriteBatch.Begin();
            spriteBatch.Draw(smokeTexture, visualPosition, sourceRectangle, color, rotation, targetRectangle, size, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
