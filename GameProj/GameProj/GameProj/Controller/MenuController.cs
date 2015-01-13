using GameProj.View;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Controller
{
    class MenuController
    {
        private MenuView view;
       
        private GameState gameState = GameState.StartScreen;

        public MenuController(MenuView menuView)
        {
            view = menuView;
        }



        /// <summary>
        /// Get & set the gamestate during the game in this controller.
        /// </summary>
        internal GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }



        /// <summary>
        /// Updates the starting screen 
        /// </summary>
        /// <param name="keyboard">KeyboardState</param>
        internal bool UpdateMenuInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                return true;

            }
            else 

            {
                return false;

               
            }
        }


        /// <summary>
        /// Draws the starting screen.
        /// </summary>
        internal void DrawStartingScreen()
        {
            view.DrawStartingScreen();
        }

        /// <summary>
        /// Draws the Background
        /// </summary>
        internal void DrawBackground()
        {
            view.DrawGameBackground();
        }



        /// <summary>
        /// Draws the screen between the levels.
        /// </summary>
        /// <param name="nextLevel"></param>
        internal void DrawBetweenLevels(int nextLevel)
        {
            view.DrawBetweenLevels(nextLevel);
        }


        /// <summary>
        /// Draws the Final screen when game is done.
        /// </summary>
        internal void DrawGameFinished()
        {
            view.DrawGameFinished();

        }

        /// <summary>
        /// Draws Game Over screen
        /// </summary>
        internal void DrawGameOver()
        {
            view.DrawGameOver();
        }


      
    }
}
