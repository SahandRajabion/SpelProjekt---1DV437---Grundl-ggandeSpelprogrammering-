using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


namespace GameProj
{
    class CharacterMovement
    {

        Texture2D spriteTexture;
        float time = 0f;
        float interval = 200f;
        int currentFrame = 0;
        int spriteWidth;
        int spriteHeight;
        int spriteSpeed = 2;
      
        Rectangle sourceRect;
        Vector2 position;
        Vector2 origin;

        KeyboardState currentKeyState;
        KeyboardState previousKeyState;
      
     
       
        public CharacterMovement(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight)
        {
            this.spriteTexture = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
        }


        public Vector2 Position
        {
            get { return position; }
            set { position = value;}
        }

        public Vector2 Origin

        {
             get { return origin; }
             set { origin = value;}
        }

        public Texture2D Texture
        {
            get { return spriteTexture; }
            set { spriteTexture = value;}
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }


        public void AnimationSprite(GameTime gameTime)
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

         
            // Allows the Character to Run faster.
            if (currentKeyState.IsKeyDown(Keys.Space))
            {
                spriteSpeed = 5;
                interval = 100;
            }
            else
            {
                spriteSpeed = 2;
                interval = 200;
            }

            if (currentKeyState.IsKeyDown(Keys.Right) == true)
            {
                AnimateRight(gameTime);
                if (position.X < 780)
                {
                    position.X += spriteSpeed;
                }
            }

            if (currentKeyState.IsKeyDown(Keys.Left) == true)
            {
                AnimateLeft(gameTime);
                if (position.X > 20)
                {
                    position.X -= spriteSpeed;
                }
            }

            //Decides frame depending on pressed key / animates only when key is pressed.
            if (currentKeyState.GetPressedKeys().Length == 0)
            {
                if (currentFrame > 0 && currentFrame < 4)
                {
                    currentFrame = 0;
                }
                if (currentFrame > 4 && currentFrame < 8)
                {
                    currentFrame = 4;
                }
                if (currentFrame > 8 && currentFrame < 12)
                {
                    currentFrame = 8;
                }
                if (currentFrame > 12 && currentFrame < 16)
                {
                    currentFrame = 12;
                }
            }

           
            
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }

     
  

        public void AnimateRight(GameTime gameTime)
        {
            if (currentKeyState != previousKeyState)
            {
                currentFrame = 9;
            }

            time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (time > interval)
            {
                currentFrame++;

                if (currentFrame > 11)
                {
                    currentFrame = 8;
                }
                time = 0f;
            }
        }

      
        public void AnimateLeft(GameTime gameTime)
        {
            if (currentKeyState != previousKeyState)
            {
                currentFrame = 5;
            }

            time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (time > interval)
            {
                currentFrame++;

                if (currentFrame > 7)
                {
                    currentFrame = 4;
                }
                time = 0f;
            }
        }
    }
}
