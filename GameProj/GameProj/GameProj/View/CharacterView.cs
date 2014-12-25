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
        Texture2D characterTexture;
        CharacterMovement charMovement;
        SpriteBatch spriteBatch;
        
        public CharacterView(GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            characterTexture = Content.Load<Texture2D>("player");

            //Texture, Currentframe, spriteWidth, spriteHeight and Scale in Camera(HardCoded)
            charMovement = new CharacterMovement(characterTexture, 9, 32, 48);
            charMovement.Position = new Vector2(100, 350);
            
        }

        internal void Draw(GameTime gameTime)
        {
            charMovement.AnimationSprite(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(charMovement.Texture, charMovement.Position, charMovement.SourceRect, Color.White, 0f, charMovement.Origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            
        }
    }
}
