using GameProj.View;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProj.Controller
{
    class PausController
    {
        private PausScreenView view;
        private KeyboardState oldKeyboardState;
        private GameState gameState = GameState.GamePaused;

        public PausController(View.PausScreenView pausScreenView)
        {
            this.view = pausScreenView;
            oldKeyboardState = new KeyboardState();
        }

        /// <summary>
        /// Updates the pausing screen.
        /// </summary>
        /// <param name="keyboardState"></param>
        internal void UpdatePauseScreen(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = keyboardState;
            }
            else if (keyboardState.IsKeyUp(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter))
            {
                oldKeyboardState = new KeyboardState();
                gameState = GameState.InGame;
            }
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
        /// Draws pause Screen.
        /// </summary>
        internal void DrawPauseScreen()
        {
            view.DrawPausScreen();
        }
    }
}
