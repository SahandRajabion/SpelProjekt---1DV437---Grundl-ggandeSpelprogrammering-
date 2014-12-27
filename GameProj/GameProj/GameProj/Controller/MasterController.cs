using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameProj.View;
using GameProj.Controller;
using GameProj.Model;

namespace GameProj
{
    
    public class MasterController : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CharacterView charView;
        private Camera m_camera;
        private Song jumpSounding;
        private Model.Model m_model;
        KeyboardState keyboardState;
        BetweenLevelController betweenLevelController;
        public static int Currentlevel = 0;
        GameState gamestate = GameState.StartingScreen;

       

        public MasterController()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            m_camera = new Camera();

            m_model = new Model.Model(GetLevel.GetLevels(1));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            charView = new CharacterView(GraphicsDevice, Content , m_model);

            betweenLevelController = new BetweenLevelController(new BetweenLevels(GraphicsDevice, Content));


            jumpSounding = Content.Load<Song>("jumping");
           /* Song song = Content.Load<Song>("sound");
            MediaPlayer.Play(song);*/

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            float elapsedTimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float elapsedTimeMilliSeconds = (float)gameTime.ElapsedGameTime.TotalMilliseconds;


            if (gamestate == GameState.StartingScreen)
            {
                betweenLevelController.UpdateStartingScreen(keyboardState);
                gamestate = betweenLevelController.GameState;
               
                if (gamestate == GameState.InGame)
                {
                    Currentlevel += 1;
                    //play music ?
                    betweenLevelController.GameState = GameState.BetweenLevels;
                }
            }

            //Containing Boolian Value = If player pressed Right button (True/False)
            bool RightMove = charView.PressedRight();
            //Containing Boolian Value = If player pressed Left button (True/False)
            bool LeftMove = charView.PressedLeft();
            //Containing Boolian Value = If player pressed Run button (True/False)
            bool RunFaster = charView.PressedRun();
            //Containing Boolian Value = If player pressed Quit button (True/False)
            bool Quit = charView.PressedQuit();
            //Containing Boolian Value = If player pressed Jump button (True/False)
            bool Jump = charView.PressedJump();


            if (RunFaster == true)
            {

               m_model.Run();
               
            }



            if (RightMove == true)
            {
                m_model.MoveRight();

                charView.AnimateRight(elapsedTimeMilliSeconds, CharacterView.Movement.RIGHTMOVE);
            }

            else if (LeftMove == true)
            {
                m_model.MoveLeft();

                charView.AnimateLeft(elapsedTimeMilliSeconds, CharacterView.Movement.LEFTMOVE);
            }
            
            if (Jump == true)
            {
                if (m_model.CanPlayerJump())
                {
                    m_model.Jump();

                    MediaPlayer.Play(jumpSounding);
                }
            }

            if (Quit == true)
            {
                this.Exit();
            }

           
            m_model.Update(elapsedTimeSeconds);


            base.Update(gameTime);
        }

        /// <summary>z
        /// Draws the levels & other information depending on gamestate.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (gamestate == GameState.StartingScreen)
            {
                betweenLevelController.DrawStartingScreen();
            }

            if (gamestate == GameState.InGame)
            {
              
            }

            //Focus camera on player during the drawing.
            m_camera.CenterOn(m_model.GetCharacter.Position,GraphicsDevice.Viewport,
            new Vector2(Level.g_levelWidth, Level.g_levelHeight));

           //Skriv ut om enter pressed (inGame)? 
            charView.DrawMap(GraphicsDevice.Viewport, m_camera, m_model.GetLevel, m_model.GetCharacter.Position);
            base.Draw(gameTime);
        }
    }
}
