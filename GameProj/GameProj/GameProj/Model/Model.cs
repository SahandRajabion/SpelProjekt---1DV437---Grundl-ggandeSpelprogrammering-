using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class Model
    {

        Character m_character;
        Level m_level;
        bool m_collidedWithGround = false;
        public static int Currentlevel = 0;
        public bool levelCompleted;



        public Model(string levelString)
        {
            m_level = new Level(levelString);
            m_character = new Character();
          
        }

        /// <summary>
        /// Gets the current level.
        /// </summary>
        public Level GetLevel
        {
            get { return m_level; }
        }

        /// <summary>
        /// Gets the character.
        /// </summary>
        public Character GetCharacter
        {
            get { return m_character; }
        }

        /// <summary>
        /// Makes character move left.
        /// </summary>
        public void MoveLeft()
        {
            m_character.MoveLeft();
        }

        /// <summary>
        /// Makes character move right.
        /// </summary>
        public void MoveRight()
        {
            m_character.MoveRight();
        }

        /// <summary>
        /// Makes character float.
        /// </summary>

        internal void Float()
        {
            m_character.Float();
        }

        /// <summary>
        /// Makes character jump.
        /// </summary>
        public void Jump()
        {
            m_character.DoJump();
        }

        /// <summary>
        /// Decides if the character jump if it collided with the ground.
        /// </summary>
        /// <returns></returns>
        public bool CanPlayerJump()
        {
            return m_collidedWithGround;
        }



        public void Update(float totalElapsedSeconds)
        {
            //Get the old position.
            Vector2 oldPosition = m_character.Position;
            
            //Get the new character position.
            m_character.Update(totalElapsedSeconds);
            Vector2 newPosition = m_character.Position;

            
            m_collidedWithGround = false;
            Vector2 velocity = m_character.Velocity;

            //Updates the collision detail 
            if (didCollide(newPosition, m_character.Size))
            {
                Collision collision = getCollisionDetails(oldPosition, newPosition, m_character.Size, velocity);
                m_collidedWithGround = collision.m_groundCollide;
               
                //set the new speed and position after collision.
                m_character.Position = collision.m_CollisionPos;
                m_character.Velocity = collision.m_CollisionSpeed;
            }

    
            LevelCompleted();
        }


        /// <summary>
        /// Returns the updated character position.
        /// </summary>
        /// <returns></returns>
        public Vector2 characterPosition() {

            return m_character.Position;
        
        }

        /// <summary>
        /// Restarts the game when called.
        /// </summary>
        public void RestartGame()
        {
            m_character = new Character();
            StartGame();

        }
        //Resets the Character position to default. 
        public Vector2 StartGame()
        {
            return m_character.Position = m_character.DefaultPosition();
            
        }



        /// <summary>
        /// Returns true if the level is completed.
        /// </summary>
        /// <returns>If the character has passed the end of the map.</returns>
        internal void LevelCompleted()
        {
            if (m_character.Position.X >= Level.g_levelWidth)
            {
              
                levelCompleted = true;
            }
          
        }


        
        /// <summary>
        /// Decides if the character is dead or has any life left.
        /// </summary>
        /// <returns></returns>
        public bool IfDead()
        {
          int left = GetCurrentLifes();
            
            if ( left <= 0)
            {
                return true;
            }

            return false;
        }
            
            
 
        /// <summary>
        /// Checks if any Collision has happend.
        /// </summary>
        /// <param name="a_centerBottom"></param>
        /// <param name="a_size"></param>
        /// <returns></returns>
        private bool didCollide(Vector2 a_centerBottom, Vector2 a_size)
        {
            FloatRectangle occupiedArea = FloatRectangle.createFromCenterBottom(a_centerBottom, a_size);
            if (m_level.IsCollidingAt(occupiedArea))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the collision details if DidCollide has happend.
        /// </summary>
        /// <param name="a_oldPos"></param>
        /// <param name="a_newPosition"></param>
        /// <param name="a_size"></param>
        /// <param name="a_velocity"></param>
        /// <returns></returns>
        private Collision getCollisionDetails(Vector2 a_oldPos, Vector2 a_newPosition, Vector2 a_size, Vector2 a_velocity)
        {
            Collision ret = new Collision(a_oldPos, a_velocity);

            Vector2 slidingXPosition = new Vector2(a_newPosition.X, a_oldPos.Y); //Y movement ignored
            Vector2 slidingYPosition = new Vector2(a_oldPos.X, a_newPosition.Y); //X movement ignored

            if (didCollide(slidingXPosition, a_size) == false)
            {
                return doOnlyXMovement(ref a_velocity, ret, ref slidingXPosition);
            }
            else if (didCollide(slidingYPosition, a_size) == false)
            {

                return doOnlyYMovement(ref a_velocity, ret, ref slidingYPosition);
            }
            else
            {
                return doStandStill(ret, a_velocity);
            }

        }

        private static Collision doStandStill(Collision ret, Vector2 a_velocity)
        {
            if (a_velocity.Y > 0)
            {
                ret.m_groundCollide = true;
            }

            ret.m_CollisionSpeed = new Vector2(0, 0);

            return ret;
        }

        private static Collision doOnlyYMovement(ref Vector2 a_velocity, Collision ret, ref Vector2 slidingYPosition)
        {
            a_velocity.X *= -0.5f; //bounce from wall
            ret.m_CollisionSpeed = a_velocity;
            ret.m_CollisionPos = slidingYPosition;
            return ret;
        }

        private static Collision doOnlyXMovement(ref Vector2 a_velocity, Collision ret, ref Vector2 slidingXPosition)
        {
            ret.m_CollisionPos = slidingXPosition;
            //did we slide on ground?
            if (a_velocity.Y > 0)
            {
                ret.m_groundCollide = true;
            }

            ret.m_CollisionSpeed = doSetSpeedOnVerticalCollision(a_velocity);
            return ret;
        }

        private static Vector2 doSetSpeedOnVerticalCollision(Vector2 a_velocity)
        {
            //did we collide with ground?
            if (a_velocity.Y > 0)
            {
                a_velocity.Y = 0; //no bounce
            }
            else
            {
                //collide with roof
                a_velocity.Y *= -1.0f;
            }

            a_velocity.X *= 0.10f;

            return a_velocity;
        }


       


        /// <summary>
        /// Lifes player has left 
        /// </summary>
        /// <returns>Returning players current lifes</returns>
        public int GetCurrentLifes()
        {
            if (characterPosition().Y > Level.g_levelHeight)
            {

                return m_character.m_characterHealth--;

            }

            return Lifes();
        }

       public int Lifes()
        {

            return m_character.m_characterHealth;

        }

     
    }
}
