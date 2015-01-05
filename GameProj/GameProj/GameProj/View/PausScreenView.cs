using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.View
{
    class PausScreenView
    {
        private SpriteBatch spriteBatch;
        private Texture2D PauseScreenTexture;
        private GraphicsDevice graphicsDevice;

      

        public PausScreenView(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PauseScreenTexture = Content.Load<Texture2D>("pausScreen");
            this.graphicsDevice = GraphicsDevice;
        }

        /// <summary>
        /// Draws the Pause screen when ever player wants to paus the game.
        /// </summary>
        internal void DrawPausScreen()
        {
            Rectangle rectangle = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(PauseScreenTexture, rectangle, Color.White);
            spriteBatch.End();
        }
    }
}
