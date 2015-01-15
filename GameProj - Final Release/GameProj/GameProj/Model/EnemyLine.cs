using GameProj.View;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class EnemyLine
    {

        public Vector2 enemyPosition;
        public Vector2 enemyDefaultPosition;
        private float speed = 0.2f;


        public EnemyLine()
        
        {

            enemyPosition = new Vector2(1, 1);
            enemyDefaultPosition = new Vector2(1, 1);
        
        
        }

        internal void Update()
        {
            
                enemyPosition.X += speed;

        }

     }

  }
