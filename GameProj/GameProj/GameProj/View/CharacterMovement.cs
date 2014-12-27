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
        float timer = 0f;
        float interval = 200f;
        int currentFrame = 0;
      
        //todo: Koppla till camera 
        int spriteWidth = 32;
        int spriteHeight = 48;
        Rectangle sourceRect;
        Vector2 position;
        Vector2 origin;
        KeyboardState currentKBState;
        KeyboardState previousKBState;
      
     
        
     

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

 

        public Vector2 Origin

        {

             get { return origin; }

             set { origin = value; }

        }

 

        public Texture2D Texture

        {

            get { return spriteTexture; }

            set { spriteTexture = value; }

        }

 

        public Rectangle SourceRect

        {

            get { return sourceRect; }

            set { sourceRect = value; }


        }

        public void AnimationSprite(float timeElapsedMilliSeconds, View.CharacterView.Movement movement)
        {
            previousKBState = currentKBState;
            currentKBState = Keyboard.GetState();

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

            if (currentKBState.GetPressedKeys().Length == 0)
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

        

            if (movement.Equals(GameProj.View.CharacterView.Movement.RIGHTMOVE))
            {
                
                if (position.X < 780)
                {
                    AnimateRight(timeElapsedMilliSeconds);
                }
            }

            if (movement.Equals(GameProj.View.CharacterView.Movement.LEFTMOVE))
            {
                {
                    
                    if (position.X > 20)
                    {
                        AnimateLeft(timeElapsedMilliSeconds);
                    }
                }


            if (movement.Equals(GameProj.View.CharacterView.Movement.STANDING))
            {
                if (currentFrame > 0 && currentFrame < 4)
                {
                    currentFrame = 0;
                }

                    
            }

                origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
            }
        }

     
  

        public void AnimateRight(float timeElapsedMilliSeconds)
        {
            if (currentKBState != previousKBState)
            {
                currentFrame = 9;
            }

            timer += timeElapsedMilliSeconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 11)
                {
                    currentFrame = 8;
                }
                timer = 0f;
            }
        }

       

        public void AnimateLeft(float timeElapsedMilliSeconds)
        {
            if (currentKBState != previousKBState)
            {
                currentFrame = 5;
            }

            timer += timeElapsedMilliSeconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 7)
                {
                    currentFrame = 4;
                }
                timer = 0f;
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
