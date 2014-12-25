using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameProj.View
{
    class Camera
    {
        private Vector2 m_modelCenterPosition = new Vector2(0, 0);
        private float m_scale = 32.0f;



        internal float GetScale()
        {
            return m_scale;
        }



        internal Vector2 GetViewPosition(float x, float y, Vector2 a_viewPortSize)
        {
            Vector2 modelPosition = new Vector2(x, y);

            Vector2 modelViewPortSize = new Vector2(a_viewPortSize.X / m_scale, a_viewPortSize.Y / m_scale);

            //get model top left position
            Vector2 modelTopLeftPosition = m_modelCenterPosition - modelViewPortSize / 2.0f;



            return (modelPosition - modelTopLeftPosition) * m_scale;
        }

        internal void SetZoom(float a_scale)
        {
            m_scale = a_scale;
        }

        internal void CenterOn(Vector2 a_newCenterPosition, Vector2 a_viewPortSize, Vector2 a_levelSize)
        {
            m_modelCenterPosition = a_newCenterPosition;

            Vector2 modelViewPortSize = new Vector2(a_viewPortSize.X / m_scale, a_viewPortSize.Y / m_scale);

            //check left
            if (m_modelCenterPosition.X < modelViewPortSize.X / 2.0f)
            {
                m_modelCenterPosition.X = modelViewPortSize.X / 2.0f;
            }

            //check bottom
            if (m_modelCenterPosition.Y > a_levelSize.Y - modelViewPortSize.Y / 2.0f)
            {
                m_modelCenterPosition.Y = a_levelSize.Y - modelViewPortSize.Y / 2.0f;
            }

            //check top
            if (m_modelCenterPosition.Y < modelViewPortSize.Y / 2.0f)
            {
                m_modelCenterPosition.Y = modelViewPortSize.Y / 2.0f;
            }
        }
       
    }
}
