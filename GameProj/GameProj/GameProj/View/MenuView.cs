using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;


namespace GameProj.View
{
    class MenuView
    {
        private Texture2D m_startingScreenTexture;
        private Texture2D m_gameBackground;
        private Texture2D m_gameBetween1;
        private Texture2D m_gameBetween2;
        private Texture2D m_gameFinishedTexture;
        private Texture2D m_gameOver;
        private Texture2D m_pauseScreenTexture;

        //private Camera m_camera;


        private GraphicsDevice m_graphicsDevice;
        private SpriteBatch m_spriteBatch;

        public MenuView(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            m_graphicsDevice = GraphicsDevice;
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_startingScreenTexture = Content.Load<Texture2D>("startingScreen");
            m_gameBackground = Content.Load<Texture2D>("xnaBackgroundGame");
            m_gameBetween1 = Content.Load<Texture2D>("level2");
            m_gameBetween2 = Content.Load<Texture2D>("level3");
            m_gameFinishedTexture = Content.Load<Texture2D>("GameFinished");
            m_gameOver = Content.Load<Texture2D>("GameOver");
            m_pauseScreenTexture = Content.Load<Texture2D>("pausScreen");
            //m_camera = new Camera(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));

        }

        /// <summary>
        /// Draws the game background.
        /// </summary>
        internal void DrawGameBackground()
        {
            DrawTexture(m_gameBackground);
        }


        /// <summary>
        /// Draws level finished screen.
        /// </summary>
        internal void DrawGameFinished()
        {

            DrawTexture(m_gameFinishedTexture);
        }

        /// <summary>
        /// Draws game over screen.
        /// </summary>
        internal void DrawGameOver()
        {
            DrawTexture(m_gameOver);
        }


        /// <summary>
        /// Draws the starting screen.
        /// </summary>
        internal void DrawStartingScreen()
        {
            DrawTexture(m_startingScreenTexture);
        }

        /// <summary>
        /// Draws the Pause screen.
        /// </summary>
        internal void DrawPauseScreenTexture()
        {
            DrawTexture(m_pauseScreenTexture);
        }

        /// <summary>
        /// Takes a texture and draws the full screen.
        /// </summary>
        /// <param name="texture"></param>
        private void DrawTexture(Texture2D gameTexture)
        {
            Rectangle tileRectangle = new Rectangle(0, 0, m_graphicsDevice.Viewport.Width, m_graphicsDevice.Viewport.Height);

            m_spriteBatch.Begin();
            m_spriteBatch.Draw(gameTexture, tileRectangle, Color.White);
            m_spriteBatch.End();
        }


        /// <summary>
        /// Draws the between level texture depending on the current level.
        /// </summary>
        /// <param name="nextLevel"></param>
        internal void DrawBetweenLevels(int nextLevel)
        {
            Rectangle tileRectangle = new Rectangle(0, 0, m_graphicsDevice.Viewport.Width, m_graphicsDevice.Viewport.Height);

            if (nextLevel == 2)
            {
                DrawTexture(m_gameBetween1);

            }
            if (nextLevel == 3)
            {
                DrawTexture(m_gameBetween2);
            }


        }

    }
}
