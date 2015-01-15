using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class Collision
    {

        //Dainel Toll (https://code.google.com/p/1dv437arkanoid/source/browse/trunk/Collisions/Collisions2/Model/Collider.cs).
    
        public bool m_groundCollide = false;
        public Vector2 m_CollisionPos;
        public Vector2 m_CollisionSpeed;

        public Collision(Vector2 a_oldPos, Vector2 a_velocity)
        {
            m_CollisionPos = a_oldPos;
            m_CollisionSpeed = a_velocity;
        }
    }
}