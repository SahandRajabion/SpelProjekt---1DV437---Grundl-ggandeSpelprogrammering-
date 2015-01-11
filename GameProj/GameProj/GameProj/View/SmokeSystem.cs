using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.View
{
    class SmokeSystem
    {

        private List<SmokeParticle> smokeParticles = new List<SmokeParticle>();
        private float NUM_PARTICLES = 100;
        private float totalTime = 0;
        private float delayTime = 0.2f;

        public void Draw(SpriteBatch spriteBatch, Texture2D smokeTexture, Camera camera)
        {
            for (int i = 0; i < smokeParticles.Count; i++)
            {
                smokeParticles[i].Draw(spriteBatch, smokeTexture, camera);
            }
        }

        public void Update(float timeElapsed)
        {
            totalTime += timeElapsed;

            //If the total gameTime is greater or equal to delayTime (The time between each particle) = totalTime is set to zero...
            if (totalTime >= delayTime)
            {
                totalTime = 0;

                /*...and a new "smokeParticles" object is created to the end of the List if 
                the List contains less than 100 particles.*/

                if (smokeParticles.Count < NUM_PARTICLES)
                {
                    smokeParticles.Add(new SmokeParticle());
                }
            }

            /*Looping through the "smokeParticles" List and updates the new Velosity and new Position
             for each particle in smokeParticle.Update*/

            for (int i = 0; i < smokeParticles.Count; i++)
            {
                smokeParticles[i].Update(timeElapsed);

                /*If the particles has lived longer than "MaxLifeTime" (3.0f) = it will respawn and get the 
                Starting Position, Size and the velocity it had from the begining.*/
                if (smokeParticles[i].IsDead())
                {
                    smokeParticles[i].Replay();
                }
            }
        }
    }
}
