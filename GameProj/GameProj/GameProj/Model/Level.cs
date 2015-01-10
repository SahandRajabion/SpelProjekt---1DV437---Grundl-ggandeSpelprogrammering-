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
        public const int g_levelHeight = 15;

        public Tile[,] m_tiles = new Tile[g_levelWidth, g_levelHeight];

        private char m_filled = 'x';
        private char m_tile = 'y';
        private char m_flag = 'f';
       
        private char m_sun = 's';
        private char m_Enemy = 'e';

        Character character;
        private string  m_currentLevel;

     

        public enum Tile
        {
            NONE = 0,
            FILLED,
            GAP,
            SUN,
            ENEMY1,
            TILE,
            FLAG
            
        };

    

        public Level(string levelString)
        {
            m_currentLevel = levelString;
            ReadLevel();
            character = new Character();
        }

       public void ReadLevel()
        {
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    int tile = y * g_levelWidth + x;

                    if (m_currentLevel[tile] == m_filled)
                    {
                        m_tiles[x, y] = Tile.FILLED;
                    }

                    else if (m_currentLevel[tile] == m_sun)
                    {
                        m_tiles[x, y] = Tile.SUN;
                    }

                    else  if (m_currentLevel[tile] == m_Enemy)
                    {
                     
                        m_tiles[x, y] = Tile.ENEMY1;
                    }

                    else  if (m_currentLevel[tile] == m_tile)
                    {

                        m_tiles[x, y] = Tile.TILE;
                    }

                    else if (m_currentLevel[tile] == m_flag)
                    {

                        m_tiles[x, y] = Tile.FLAG;
                    }

                   
                    else
                    {
                        m_tiles[x, y] = Tile.NONE;
                    }
                }
            }
        }


       

        
        /// <summary>
        /// Checks if character sprite has collided with a Enemy(if died).
        /// </summary>
        /// <param name="a_position"></param>
        /// <param name="characterSize"></param>
        /// <returns></returns>
        public bool CheckTileEnemyCollision(Vector2 characterPosition, Vector2 characterSize)
        {

            Vector2 bottomRightPostionOfCharacter = new Vector2(characterPosition.X + characterSize.X / 2.0f, characterPosition.Y);

            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    if (bottomRightPostionOfCharacter.X < (float)x)
                        continue;
                    if (bottomRightPostionOfCharacter.Y < (float)y)
                        continue;

                    Vector2 topLeftPositionOfCharacter = new Vector2(characterPosition.X - characterSize.X / 2.0f, characterPosition.Y - characterSize.Y);

                    if (topLeftPositionOfCharacter.X > (float)x + 1.0f)
                        continue;
                    if (topLeftPositionOfCharacter.Y > (float)y + 1.0f)
                        continue;

                    if (m_tiles[x, y] == Tile.ENEMY1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


    
        /// <summary>
        /// Checks the collision between the player and the map tiles.
        /// </summary>
        /// <param name="a_rect"></param>
        /// <returns></returns>
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
                        if (m_tiles[x, y] == Tile.FILLED || m_tiles[x, y] == Tile.TILE)
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
