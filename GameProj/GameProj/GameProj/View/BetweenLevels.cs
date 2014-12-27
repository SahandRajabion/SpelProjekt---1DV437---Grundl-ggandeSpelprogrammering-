using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;


namespace GameProj.View
{
    class BetweenLevels
    {
        private Texture2D m_startingScreenTexture;
        private GraphicsDevice m_graphicsDevice;
        private SpriteBatch m_spriteBatch;

        public BetweenLevels(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            m_graphicsDevice = GraphicsDevice;
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_startingScreenTexture = Content.Load<Texture2D>("xnaBackgroundGame");

        }






        /// <summary>
        /// Draws the starting screen
        /// </summary>
        internal void DrawStartingScreen()
        {
            DrawTexture(m_startingScreenTexture);
        }


        /// <summary>
        /// Takes a texture and draws a full sized screen
        /// </summary>
        /// <param name="texture">Texture2D texture to draw</param>
        private void DrawTexture(Texture2D m_startingScreenTexture)
        {
            Rectangle tileRectangle = new Rectangle(0, 0, m_graphicsDevice.Viewport.Width, m_graphicsDevice.Viewport.Height);

            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_startingScreenTexture, tileRectangle, Color.White);
            m_spriteBatch.End();
        }
    }
}
