using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.View
{
    class SmokeView
    {

        private SpriteBatch m_spriteBatch;
        private Texture2D m_SmokeTexture;
        private Camera camera;
        private SmokeSystem smokeSystem;
        private Model.SmokeModel model;


        public SmokeView(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            camera = new Camera(GraphicsDevice.Viewport);
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            model = new Model.SmokeModel();
            smokeSystem = new SmokeSystem();
            m_SmokeTexture = Content.Load<Texture2D>("cloud");
        }


        internal void draw(float timeElapsed)
        {
            smokeSystem.Update(timeElapsed);

            m_spriteBatch.Begin();
            smokeSystem.Draw(m_spriteBatch, camera, m_SmokeTexture);
            m_spriteBatch.End();
        }
    }
}
