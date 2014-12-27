using GameProj.View;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Controller
{
    class BetweenLevelController
    {
        private BetweenLevels view;
        private KeyboardState oldKeyboardState;
        private GameState gameState = GameState.StartingScreen;

        public BetweenLevelController(BetweenLevels BetweenLevelView)
        {
            view = BetweenLevelView;
            oldKeyboardState = new KeyboardState();
        }



        /// <summary>
        /// Get and set the gamestate
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
        internal void UpdateStartingScreen(KeyboardState keyboard)
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
        /// Draws starting screen
        /// </summary>
        internal void DrawStartingScreen()
        {
            view.DrawStartingScreen();
        }
    }
}
