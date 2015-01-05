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
        public int m_characterHealth = 3;
        private Vector2 m_modelPosition;
        private Vector2 m_velocity;
        private Vector2 m_defaultPosition;
    
        public Character()
        {
            
            m_gravity = new Vector2(0,3f);
            Size = new Vector2(0.9f, 0.9f);
            m_modelPosition = new Vector2(5,9);
            m_defaultPosition = new Vector2(5,9);
            
           
        }

        /// <summary>
        /// Resets the character health.
        /// </summary>
        internal void ResetCharacterHealth()
        {
             m_characterHealth = 3;
        }

        /// <summary>
        /// Get&set the current Character position.
        /// </summary>
        public Vector2 Position
        {
            get { return m_modelPosition; }
            set { m_modelPosition = value; }
        }

        /// <summary>
        /// Character default Position
        /// </summary>
        /// <returns></returns>
        public Vector2 DefaultPosition()
        {
             
          return m_defaultPosition; 
           
        }

        //Get&Set the velocity of player.
        public Vector2 Velocity
        {
            get { return m_velocity; }
            set { m_velocity = value; }
        }


        //Updates the character position and velocity during the game.
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

        /// <summary>
        /// Player velocity when floating
        /// </summary>
        internal void Float()
        {
            m_velocity.X = 5f;
        }

       
        public Vector2 Size
        {
            get;
            set;
        }
        /// <summary>
        /// Makes Character Jump in decided velocity when called.
        /// </summary>
        public void DoJump()
        {
            m_velocity.Y = -4f;
        }

      

        
    }
}
