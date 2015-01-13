using GameProj.View;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Model
{
    class Level
    {

        public const int g_levelWidth = 80;
        public const int g_levelHeight = 15;

        public Tile[,] m_tiles = new Tile[g_levelWidth, g_levelHeight];

        private char m_filled = 'x';
        private char m_tile = 'y';
        private char m_flag = 'f';
       
        private char m_sun = 's';
        private char m_Enemy = 'e';
        private char m_TileBlock = 't';
        private char m_TileBlock2 = 'l';

        private char m_Build1 = 'b';
        private char m_Build2 = 'q';

        private char m_Tower1 = '1';
        private char m_Tower2 = '2';
        private char m_Tower3 = '3';
        private char m_Tower4 = '4';


       

     

        public enum Tile
        {
            NONE = 0,
            FILLED,
            GAP,
            SUN,
            ENEMY1,
            TILE,
            FLAG,
            TOWER1,
            TOWER2,
            TOWER3,
            TOWER4,
            TILEBLOCK,
            TILEBLOCK2,
            BUILD1,
            BUILD2
            
        };

    

        public void ReadLevel(string level)
        {
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    int tile = y * g_levelWidth + x;

                    if (level[tile] == m_filled)
                    {
                        m_tiles[x, y] = Tile.FILLED;
                    }

                    else if (level[tile] == m_sun)
                    {
                        m_tiles[x, y] = Tile.SUN;
                    }

                    else if (level[tile] == m_Enemy)
                    {
                     
                        m_tiles[x, y] = Tile.ENEMY1;
                    }

                    else if (level[tile] == m_tile)
                    {

                        m_tiles[x, y] = Tile.TILE;
                    }

                    else if (level[tile] == m_flag)
                    {

                        m_tiles[x, y] = Tile.FLAG;
                    }

                    else if (level[tile] == m_Tower1)
                    {

                        m_tiles[x, y] = Tile.TOWER1;
                    }

                    else if (level[tile] == m_Tower2)
                    {

                        m_tiles[x, y] = Tile.TOWER2;
                    }
                    else if (level[tile] == m_Tower3)
                    {

                        m_tiles[x, y] = Tile.TOWER3;
                    }
                    else if (level[tile] == m_Tower4)
                    {

                        m_tiles[x, y] = Tile.TOWER4;
                    }

                    else if (level[tile] == m_TileBlock)
                    {

                        m_tiles[x, y] = Tile.TILEBLOCK;
                    }

                    else if (level[tile] == m_TileBlock2)
                    {

                        m_tiles[x, y] = Tile.TILEBLOCK2;
                    }

                    else if (level[tile] == m_Build1)
                    {

                        m_tiles[x, y] = Tile.BUILD1;
                    }

                    else if (level[tile] == m_Build2)
                    {

                        m_tiles[x, y] = Tile.BUILD2;
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
       public bool CheckTileSunEnemyCollision(FloatRectangle a_rect)
        {
            Vector2 tileSize = new Vector2(1, 1);
            for (int x = 0; x < g_levelWidth; x++)
            {
                for (int y = 0; y < g_levelHeight; y++)
                {
                    FloatRectangle rect = FloatRectangle.createFromTopLeft(new Vector2(x, y), tileSize);
                    if (a_rect.isIntersecting(rect))
                    {
                        if (m_tiles[x, y] == Tile.ENEMY1)
                        {
                            return true;
                        }

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
                        if (m_tiles[x, y] == Tile.FILLED || m_tiles[x, y] == Tile.TILE || m_tiles[x, y] == Tile.TOWER4 || 
                            m_tiles[x, y] == Tile.TOWER3 || m_tiles[x, y] == Tile.TOWER2 || m_tiles[x, y] == Tile.TOWER1 ||
                            m_tiles[x, y] == Tile.TILEBLOCK || m_tiles[x, y] == Tile.TILEBLOCK2 || m_tiles[x, y] == Tile.BUILD1 || m_tiles[x, y] == Tile.BUILD2)
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
