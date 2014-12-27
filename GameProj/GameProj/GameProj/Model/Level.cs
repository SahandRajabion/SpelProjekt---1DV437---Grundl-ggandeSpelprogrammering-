using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class Level
    {

        public const int g_levelWidth = 40;
        public const int g_levelHeight = 7;

        public Tile[,] m_tiles = new Tile[g_levelWidth, g_levelHeight];

        private char m_filled = 'x';
        private char m_spriteStart = 's';
        private char m_space = 'g';
        private string m_levelString;

        public enum Tile
        {
            NONE = 0,
            FILLED,
            GAP
        };

     
       public Level(string levelString)
        {
            m_levelString = levelString;

            ReadLevel();
        }

       public void ReadLevel()
        {
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    int tile = y * g_levelWidth + x;

                    if (m_levelString[tile] == m_filled)
                    {
                        m_tiles[x, y] = Tile.FILLED;
                    }

                    else if (m_levelString[tile] == m_space)
                    {
                        m_tiles[x, y] = Tile.GAP;
                    }

                    else if (m_levelString[tile] == m_spriteStart)
                    {
                        StartPosition = new Vector2(x + 0.5f, y);

                        m_tiles[x, y] = Tile.NONE;
                    }

                   
                    else
                    {
                        m_tiles[x, y] = Tile.NONE;
                    }
                }
            }
        }
        
        /// <summary>
        /// Checks if character sprite has collided with a Tile Gap.
        /// </summary>
        /// <param name="a_position"></param>
        /// <param name="a_size"></param>
        /// <returns></returns>
        public bool CheckTileGapCollision(Vector2 a_position, Vector2 a_size)
        {
          

            Vector2 bottomRightPostion = new Vector2(a_position.X + a_size.X / 2.0f, a_position.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    if (bottomRightPostion.X < (float)x)
                        continue;
                    if (bottomRightPostion.Y < (float)y)
                        continue;

                    Vector2 topLeftPosition = new Vector2(a_position.X - a_size.X / 2.0f, a_position.Y - a_size.Y);

                    if (topLeftPosition.X > (float)x + 1.0f)
                        continue;
                    if (topLeftPosition.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y] == Tile.GAP)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public Vector2 StartPosition
        {
            get;
            set;
        }


        public bool IsCollidingAt(FloatRectangle a_rect)
        {
            Vector2 tileSize = new Vector2(1, 1);
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    FloatRectangle rect = FloatRectangle.createFromTopLeft(new Vector2(x, y), tileSize);
                    if (a_rect.isIntersecting(rect))
                    {
                        if (m_tiles[x, y] == Tile.FILLED)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        
    }
}
