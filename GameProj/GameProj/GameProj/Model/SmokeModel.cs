using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class SmokeModel
    {


        

        public float XPositionSmoke = 0.5f;
        public float YPositionSmoke = 0.5f;
        public float maxSpeedSmoke = 0.1f;
        public float minSize = 0.03f;
        public float maxSize = 2.5f;
        public float startValue = 1.0f;
        public float endValue = 0.0f;
        public float totalTime = 0;
        public float MaxTime = 17.0f;
        public float XPosition = 0.5f;
        public float YPosition = 0.5f;


        internal Vector2 getStartPositionSmoke()
        {
            return new Vector2(XPositionSmoke, YPositionSmoke);
        }

      

        internal Vector2 getStartPosition()
        {
            return new Vector2(XPosition, YPosition);
        }
    }
}
