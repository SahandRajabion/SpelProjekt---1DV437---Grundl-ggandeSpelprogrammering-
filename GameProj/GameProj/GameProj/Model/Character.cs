using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class Character
    {
        
        private Vector2 m_gravity;
        private int m_Lifes = 3;
        private Vector2 m_modelPosition;
        private Vector2 m_velocity;
      
        public Character()
        {
            
            m_gravity = new Vector2(0,3f);
            SpriteSize = new Vector2(0.9f, 0.9f);
            
        }

        
        public Vector2 Position
        {
            get { return m_modelPosition; }
            set { m_modelPosition = value; }
        }

        public Vector2 Velocity
        {
            get { return m_velocity; }
            set { m_velocity = value; }
        }


        /// <summary>
        /// Removing lifes when the player has died. 
        /// </summary>
        public void EliminateLife()
        {
            m_Lifes--;
        }

       /// <summary>
       /// Lifes player has left 
       /// </summary>
       /// <returns>Returning players current lifes</returns>
        public int GetCurrentLifes()
        {
            return m_Lifes;
        }

      
        public void Update(float totalElapsedSeconds)
        {
            m_modelPosition += Velocity * totalElapsedSeconds;
            m_velocity += m_gravity * totalElapsedSeconds;
        }

        /// <summary>
        /// Player goes left with decided Velocity
        /// </summary>
        public void MoveLeft()
        {
            m_velocity.X = -3.5f;
        }

        /// <summary>
        /// Player goes right with decided velocity
        /// </summary>
        public void MoveRight()
        {
            m_velocity.X = 3.5f;
        }

        internal void Run()
        {
            m_velocity.X = 5f;
        }

        public Vector2 SpriteSize
        {
            get;
            set;
        }
        /// <summary>
        /// Makes Character Jump in decided velocity when called.
        /// </summary>
        public void DoJump()
        {
            m_velocity.Y = -5f;
           
        }

      

        
    }
}
