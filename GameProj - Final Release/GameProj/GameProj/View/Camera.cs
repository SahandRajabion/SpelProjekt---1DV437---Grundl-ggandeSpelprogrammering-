using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProj.View
{
    class Camera
    {


        //Daniel Toll (https://code.google.com/p/1dv437arkanoid/source/browse/trunk/Collisions/Collisions2/View/Camera.cs)


        private float scale;
        private float scaleX;
        private float scaleY;
        private static int frame = 12;


        public float XPosition = 0.5f;
        public float YPosition = 0.6f;
        private Model.SmokeModel model;


        private Vector2 m_modelCenterPosition = new Vector2(0, 0);

       // private float m_scale = 60.0f;

        public Camera(Viewport port)
        {
            model = new Model.SmokeModel();

            scaleX = port.Width - frame * 2;
            scaleY = port.Height - frame * 2;



            scale = scaleX;
            if (scaleY < scaleX)
            {
                scale = scaleY;
            }
        }

        public float GetScale()
        {
            return scale;
        }


        public float ToVisualX(float posX)
        {
            return scale * posX;
        }

        public float ToVisualY(float posY)
        {
            return scale * posY;
        }


        internal void SetZoom(float a_scale)
        {
            scale = a_scale;
        }


        internal Rectangle translateRec(float X, float Y, float Size)
        {
            //Calculating the visual screen coordinates for the particles. 
            float vx = Size * scaleX;
            float vy = Size * scaleY;

            int screenX = (int)((X * scaleX + model.XPosition) - vx);
            int screenY = (int)((Y * scaleY + model.YPosition) - vy);
            return new Rectangle(screenX, screenY, (int)(vx * 1f), (int)(vy * 1f));
        }




        public Vector2 GetViewPosition(float x, float y, Vector2 viewScreenSize)
        {
            Vector2 modelPosition = new Vector2(x, y);

            Vector2 modelScreenSize = new Vector2(viewScreenSize.X / scale, viewScreenSize.Y / scale);
            
            //get model top left position
            Vector2 modelTopLeftPosition = m_modelCenterPosition - (modelScreenSize / 2.0f);

            return (modelPosition - modelTopLeftPosition) * scale;
        }


        public void CenterOn(Vector2 newCenterPosition, Viewport viewScreenSize, Vector2 levelSize)
        {
            m_modelCenterPosition = newCenterPosition;

            Vector2 modelScreenSize = new Vector2(viewScreenSize.Width / scale, viewScreenSize.Height / scale);

            // Checks if the camera is outside the left part of the current level.
            if (m_modelCenterPosition.X < modelScreenSize.X / 2.0f)
            {
                m_modelCenterPosition.X = modelScreenSize.X / 2.0f;
            }

            // Checks if the camera is outside the part of the current level.
            if (m_modelCenterPosition.X > levelSize.X - modelScreenSize.X / 2.0f)
            {
                m_modelCenterPosition.X = levelSize.X - modelScreenSize.X / 2.0f;
            }

            // Checks if the top of the camera is outside the top part of the current level.
            if (m_modelCenterPosition.Y < modelScreenSize.Y / 2.0f)
            {
                m_modelCenterPosition.Y = modelScreenSize.Y / 2.0f;
            }

            // Checks if the top of the camera is outside the bottom part of the current level.
            if (m_modelCenterPosition.Y > levelSize.Y - modelScreenSize.Y / 2.0f)
            {
                m_modelCenterPosition.Y = levelSize.Y - modelScreenSize.Y / 2.0f;
            }

            
        }



    }
}
