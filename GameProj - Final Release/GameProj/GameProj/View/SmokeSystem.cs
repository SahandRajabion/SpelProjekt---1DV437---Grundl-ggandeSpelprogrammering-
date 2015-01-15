using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.View
{
    class SmokeSystem
    {

        private SmokeParticle[] smokeParticles;
        private const int NUM_PARTICLES = 100;



        public SmokeSystem()
        {
            smokeParticles = new SmokeParticle[NUM_PARTICLES];

            for (int i = 0; i < NUM_PARTICLES; i++)
            {
                smokeParticles[i] = new SmokeParticle(i);
            }

        }

        internal void Update(float timeElapsed)
        {
            for (int i = 0; i < NUM_PARTICLES; i++)
            {
                smokeParticles[i].Update(timeElapsed);
            }


            for (int i = 0; i < NUM_PARTICLES; i++)
            {
                if (smokeParticles[i].isDead())
                {

                    smokeParticles[i].rePlay();
                }

            }

        }

        internal void Draw(SpriteBatch m_spriteBatch, Camera camera, Texture2D m_SplitterTexture)
        {
            for (int i = 0; i < NUM_PARTICLES; i++)
            {
                smokeParticles[i].Draw(m_spriteBatch, camera, m_SplitterTexture);
            }
        }
    }
}
