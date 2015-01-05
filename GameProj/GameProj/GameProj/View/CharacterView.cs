using GameProj.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.View
{
    class CharacterView
    {
        Texture2D CharacterTexture;
        CharacterMovement m_charMovement;
        SpriteBatch spriteBatch;
        Character character;
        private Rectangle m_destinationRectangle;
        
        private Texture2D m_tileTexture;
        private Texture2D m_BoxTexture;
        private Texture2D m_BackTexture;
        private SpriteFont lifeSprite;
        private Model.Model model;

        public CharacterView(GraphicsDevice GraphicsDevice, ContentManager Content, Model.Model m_model)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
         
            CharacterTexture = Content.Load<Texture2D>("player");
            m_BackTexture = Content.Load<Texture2D>("trans");


            m_BoxTexture = Content.Load<Texture2D>("cloud1");
            lifeSprite = Content.Load<SpriteFont>("levelTime");

            this.model = m_model;
            character = new Character();
           
            //TODO:Scale camera.
            //9= currentFrame, 32=  spriteWidth, 48= spriteHeight:  scale in camera.
            m_charMovement = new CharacterMovement(CharacterTexture, 9, 32, 48);
            m_charMovement.Position = new Vector2(100, 350);
            
        }

        public enum Movement
        {
            RIGHTMOVE = 0,
            LEFTMOVE,
            STANDING

        };

        public void AnimateRight(float timeElapsedMilliSeconds, Movement movement)
        {
             m_charMovement.AnimationSprite(timeElapsedMilliSeconds, movement);
        
        }

        internal void AnimateLeft(float timeElapsedMilliSeconds, Movement movement)
        {
             m_charMovement.AnimationSprite(timeElapsedMilliSeconds, movement);
        }

       internal void AnimateStill(float timeElapsedMilliSeconds, Movement movement)
        {
            m_charMovement.AnimationSprite(timeElapsedMilliSeconds, movement);
        }

       
        internal bool PressedQuit()
        {
            return m_charMovement.PlayerPressedQuit();
        }

        internal bool PressedJump()
        {
            return m_charMovement.PlayerPressedJump();
        }

        internal bool PressedRight()
        {
            return m_charMovement.PlayerPressedRight();
        }

        internal bool PressedLeft()
        {
            return m_charMovement.PlayerPressedLeft();
        }


        internal bool PressedRun()
        {
            return m_charMovement.PlayerPressedRun();
        }


    
        /// <summary>
        /// Draws the level map.
        /// </summary>
        /// <param name="viewport"></param>
        /// <param name="m_camera"></param>
        /// <param name="level"></param>
        /// <param name="playerPosition"></param>
        internal void DrawMap(Viewport viewport, Camera m_camera, Model.Level level, Vector2 playerPosition)
        {
            DrawLife(character);
            Vector2 viewPortSize = new Vector2(viewport.Width, viewport.Height);
            float scale = m_camera.GetScale();

            spriteBatch.Begin();

            for (int x = 0; x < Level.g_levelWidth; x++)
            {
                for (int y = 0; y < Level.g_levelHeight; y++)
                {
                    Vector2 viewPosition = m_camera.GetViewPosition(x, y, viewPortSize);
                    DrawTile(viewPosition.X, viewPosition.Y, level.m_tiles[x, y], scale);
                }
            }

            Vector2 characterViewPosition = m_camera.GetViewPosition(playerPosition.X, playerPosition.Y, viewPortSize);
            DrawCharacterPos(characterViewPosition, scale);
            spriteBatch.End();

        }

        /// <summary>
        /// Draws the current life left. 
        /// </summary>
        /// <param name="character"></param>
        private void DrawLife(Character character)
        {
            Vector2 position = new Vector2(5, 5);

            int test2 = model.Lifes();

            spriteBatch.Begin();
            spriteBatch.DrawString(lifeSprite, "Life: " + test2, position, Color.White);
            spriteBatch.End();
        }


        /// <summary>
        /// Draws the map tiles. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tile"></param>
        /// <param name="scale"></param>
        private void DrawTile(float x, float y, Model.Level.Tile tile, float scale)
        {
            if (tile == Level.Tile.FILLED)
            {
                m_tileTexture = m_BoxTexture;
            }

            else
            {
                m_tileTexture = m_BackTexture;
            }

            Rectangle destinationRectangle = new Rectangle((int)x, (int)y, (int)scale, (int)scale);

            spriteBatch.Draw(m_tileTexture, destinationRectangle, Color.White);
        }


        /// <summary>
        /// Draws the character position.
        /// </summary>
        /// <param name="characterViewPosition"></param>
        /// <param name="scale"></param>
        private void DrawCharacterPos(Vector2 characterViewPosition, float scale)
        {
            m_destinationRectangle = new Rectangle((int)(characterViewPosition.X - scale / 2.0f), (int)(characterViewPosition.Y - scale), (int)scale, (int)scale);

            spriteBatch.Draw(CharacterTexture, m_destinationRectangle, m_charMovement.SourceRect, Color.White);
        }






       
    }
}
