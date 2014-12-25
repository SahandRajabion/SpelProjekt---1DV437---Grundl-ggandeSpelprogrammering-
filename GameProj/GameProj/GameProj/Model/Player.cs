using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class Player
    {
        Vector2 m_centerBottomPosition = new Vector2(5.0f, 5);
        Vector2 m_speed = new Vector2(0f, 0);
        public Vector2 m_sizes = new Vector2(0.95f, 0.95f);

        internal Vector2 GetPosition()
        {
            return m_centerBottomPosition;
        }

        internal void Update(float a_elapsedTime)
        {
            Vector2 gravityAcceleration = new Vector2(0.0f, 9.82f);

            //integrate position
            m_centerBottomPosition = m_centerBottomPosition + m_speed * a_elapsedTime + gravityAcceleration * a_elapsedTime * a_elapsedTime;

            //integrate speed
            m_speed = m_speed + a_elapsedTime * gravityAcceleration;


        }

        internal void DoJump()
        {
            m_speed.Y = -10; //speed upwards

        }

        internal void SetPosition(Vector2 a_pos)
        {
            m_centerBottomPosition = a_pos;
        }

        internal Vector2 GetSpeed()
        {
            return m_speed;
        }

        internal void SetSpeed(Vector2 a_speed)
        {
            m_speed = a_speed;
        }
    }
}
