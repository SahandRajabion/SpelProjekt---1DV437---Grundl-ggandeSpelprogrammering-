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
       
        float timeElapsed = 0f;
        float delay = 150f;
        int currentFrame = 0;
      
        int spriteWidth = 32;
        int spriteHeight = 48;
     
        Rectangle sourceRect;
        Vector2 position;
      

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
      
     
        
     

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
            set { position = value; }
        }

 
        public Rectangle SourceRect

        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }


        public void AnimationSprite(float timeElapsedMilliSeconds, View.GameView.Movement movement)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

         
            if (movement.Equals(GameProj.View.GameView.Movement.RIGHTMOVE))
            {
                
                if (position.X < 500)
                {
                   
                    AnimateRight(timeElapsedMilliSeconds);
                }
            }

            if (movement.Equals(GameProj.View.GameView.Movement.LEFTMOVE))
            {
                {
                    
                    if (position.X > 10)
                    {
                        AnimateLeft(timeElapsedMilliSeconds);
                    }
                }

            }
        }

     
  

        public void AnimateRight(float timeElapsedMilliSeconds)
        {
            if (currentKeyboardState != previousKeyboardState)
            {
                currentFrame = 9;
            }

            timeElapsed += timeElapsedMilliSeconds;

            if (timeElapsed > delay)
            {
                currentFrame++;

                if (currentFrame > 11)
                {
                    currentFrame = 8;
                }

                timeElapsed = 0f;
            }
        }

       

        public void AnimateLeft(float timeElapsedMilliSeconds)
        {
            if (currentKeyboardState != previousKeyboardState)
            {
                currentFrame = 5;
            }

            timeElapsed += timeElapsedMilliSeconds;

            if (timeElapsed > delay)
            {
                currentFrame++;

                if (currentFrame > 7)
                {
                    currentFrame = 4;
                }
                timeElapsed = 0f;
            }
        }

       

        public bool PlayerPressedJump()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Up);
        }

        public bool PlayerPressedRight()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Right);
        }


        public bool PlayerPressedLeft()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Left);
        }

        public bool PlayerPressedRun()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space);
        }


        internal bool PlayerPressedQuit()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Escape);
        }

       
    }
}
