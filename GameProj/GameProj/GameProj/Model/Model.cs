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
        bool m_hasCollidedWithGround = false;

        public Model(string levelString)
        {
           m_level = new Level(levelString);
           m_character = new Character();
           m_character.Position = m_level.StartPosition;
        }

        public Level GetLevel
        {
            get { return m_level; }
        }

        public Character GetCharacter
        {
            get { return m_character; }
        }

        public void MoveLeft()
        {
            m_character.MoveLeft();
        }

        public void MoveRight()
        {
            m_character.MoveRight();
        }

        internal void Run()
        {
            m_character.Run();
        }

        public void Jump()
        {
            m_character.DoJump();
        }

        public bool CanPlayerJump()
        {
            return m_hasCollidedWithGround;
        }

        public void Update(float totalElapsedSeconds)
        {
            //Get the old position
            Vector2 oldPosition = m_character.Position;
            
            //Get the new position
            m_character.Update(totalElapsedSeconds);
            Vector2 newPosition = m_character.Position;

            //Collide
            m_hasCollidedWithGround = false;
            Vector2 velocity = m_character.Velocity;

            if (didCollide(newPosition, m_character.SpriteSize))
            {
                Collision collision = getCollisionDetails(oldPosition, newPosition, m_character.SpriteSize, velocity);
                m_hasCollidedWithGround = collision.m_groundCollide;
               
                //set the new speed and position after collision
                m_character.Position = collision.m_CollisionPos;
                m_character.Velocity = collision.m_CollisionSpeed;
            }

            IfDiedInGap(totalElapsedSeconds);
            IfDead();
            IfReachedGoal();
        }

      

       

        private void IfReachedGoal()
        {
            if (m_character.Position.X > Level.g_levelWidth)
            {
                m_character = new Character();
                m_character.Position = m_level.StartPosition;
            }
        }

        private void IfDead()
        {
            if (m_character.GetCurrentLifes() <= 0)
            {
                m_character = new Character();
                m_character.Position = m_level.StartPosition;
            }
        }

        private void IfDiedInGap(float a_elapsedTime)
        {
            if (m_level.CheckTileGapCollision(m_character.Position, m_character.SpriteSize))
            {
                m_character.EliminateLife();
            }
        }

        private bool didCollide(Vector2 a_centerBottom, Vector2 a_size)
        {
            FloatRectangle occupiedArea = FloatRectangle.createFromCenterBottom(a_centerBottom, a_size);
            if (m_level.IsCollidingAt(occupiedArea))
            {
                return true;
            }
            return false;
        }

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

      
    }
}
