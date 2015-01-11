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
        private float scale;
        private float scaleX;
        private float scaleY;
        private static int frame = 12;


        public float XPosition = 0.5f;
        public float YPosition = 0.6f;
        private Model.SmokeModel model;


        private Vector2 m_modelCenterPosition = new Vector2(0, 0);

        private float m_scale = 60.0f;

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
            return m_scale;
        }

        public float ToVisualX(float posX)
        {
            return scale * posX;
        }

        public float ToVisualY(float posY)
        {
            return scale * posY;
        }


        /*
        internal Microsoft.Xna.Framework.Rectangle translatRec(float x, float y, float p_3)
        {
            float vX = p_3 * scaleX;
            float vY = p_3 * scaleY;

            int screenX = (int)((x * scaleX + XPosition) - vX);
            int screenY = (int)((y * scaleY + YPosition) - vY);

            return new Microsoft.Xna.Framework.Rectangle(screenX, screenY, (int)(vX * 1f), (int)(vY * 1f));
        }
        */



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

            Vector2 modelScreenSize = new Vector2(viewScreenSize.X / m_scale, viewScreenSize.Y / m_scale);
            
            //get model top left position
            Vector2 modelTopLeftPosition = m_modelCenterPosition - (modelScreenSize / 2.0f);

            return (modelPosition - modelTopLeftPosition) * m_scale;
        }

        public void CenterOn(Vector2 newCenterPosition, Viewport viewScreenSize, Vector2 levelSize)
        {
            m_modelCenterPosition = newCenterPosition;

            Vector2 modelScreenSize = new Vector2(viewScreenSize.Width / m_scale, viewScreenSize.Height / m_scale);

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
