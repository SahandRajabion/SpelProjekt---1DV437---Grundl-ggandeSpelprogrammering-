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

        private Vector2 m_modelCenterPosition = new Vector2(0, 0);

        private float m_scale = 60.0f;

        public Camera()
        {
            
        }

        public float GetScale()
        {
            return m_scale;
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
