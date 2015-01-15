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
        private Vector2 position;
        private Vector2 rDirection;
        private Vector2 acceleration;
        private Vector2 newVelocity;
        private Vector2 newPosition;
        private Model.SmokeModel model;
        private float delayRand;
        private float lifePercent;
        private float Size;
        private float fade;
        private int seed;



        public SmokeParticle(int seed)
        {
            this.seed = seed;
            rePlay();
        }

        internal void rePlay()
        {

            model = new Model.SmokeModel();
            model.totalTime = 0;
            position = new Vector2(0.5f, 0.52f);

            //Randomize direction
            Random rand = new Random(seed);
            delayRand = (float)(rand.NextDouble()) * model.MaxTime;
            rDirection.Normalize();
            rDirection = new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1));

            position += rDirection;

        }

        internal void Draw(SpriteBatch m_spriteBatch, Camera camera, Texture2D m_SplitterTexture)
        {
            if (isAlive())
            {
                Rectangle destrect = camera.translateRec(position.X, position.Y, Size);
                fade = model.endValue * lifePercent + (1.0f - lifePercent) * model.startValue;
                Color color = new Color(fade, fade, fade, fade);
                m_spriteBatch.Draw(m_SplitterTexture, destrect, color);
            }
        }

        private float timeLived()
        {
            if (isAlive())
            {
                return model.totalTime - delayRand;
            }
            else
            {
                return 0;
            }

        }

        internal void Update(float elapsedTime)
        {
            model.totalTime += elapsedTime;


            if (isAlive())
            {
                acceleration = new Vector2(0.2f, -0.5f);
                lifePercent = timeLived() / model.MaxTime;
                Size = model.minSize + lifePercent * model.maxSize;

                newVelocity.X = elapsedTime * acceleration.X + rDirection.X;
                newVelocity.Y = elapsedTime * acceleration.Y + rDirection.Y;

                newPosition.X = elapsedTime * newVelocity.X + position.X;
                newPosition.Y = elapsedTime * newVelocity.Y + position.Y;

                //Get New position in every update
                position = newPosition;
                //Get new particle directions in every update
                rDirection = newVelocity;

               

            }
        }


        public bool isAlive()
        {
            return model.totalTime > delayRand;
        }



        internal bool isDead()
        {
            return model.totalTime >= model.MaxTime;
        }

    }
}
