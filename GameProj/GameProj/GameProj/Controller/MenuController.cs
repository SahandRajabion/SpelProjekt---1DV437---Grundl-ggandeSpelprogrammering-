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
        private KeyboardState oldKeyboardState;
       
        private GameState gameState = GameState.StartScreen;

        public MenuController(MenuView menuView)
        {
            view = menuView;
            oldKeyboardState = new KeyboardState();
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
        internal void UpdateStartScreen(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = keyboard;

            }
            else if (keyboard.IsKeyUp(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = new KeyboardState();
                gameState = GameState.InGame;

               
            }
        }


        /// <summary>
        /// Updates the screen between the levels.
        /// </summary>
        /// <param name="keyboard">KeyboardState</param>
        internal void UpdateBetweenLevelScreen(KeyboardState keyboard)
        {

            if (keyboard.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = keyboard;
            }
            else if (keyboard.IsKeyUp(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = new KeyboardState();
                gameState = GameState.InGame;
            }

          
        }


        /// <summary>
        /// Updates the final screen when the game is done.
        /// </summary>
        /// <param name="keyboard"></param>
        internal void UpdateGameEnd(KeyboardState keyboard)
        {

            if (keyboard.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = keyboard;
            }
            else if (keyboard.IsKeyUp(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = new KeyboardState();
                gameState = GameState.StartScreen;
            }


        }


        /// <summary>
        /// Updates the final screen when the game is done.
        /// </summary>
        /// <param name="keyboard"></param>
        internal void UpdateGameOver(KeyboardState keyboard)
        {

            if (keyboard.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = keyboard;
            }
            else if (keyboard.IsKeyUp(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = new KeyboardState();
                gameState = GameState.StartScreen;
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
