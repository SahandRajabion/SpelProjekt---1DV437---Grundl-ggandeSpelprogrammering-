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

        private CharacterView characterView;
        private PausScreenView pausView;
        private Camera m_camera;

        private Character character;
        private Model.Model m_model;
        private Level level;
        private GetLevel getLevel;

        private SoundEffect jumpSound;
        private Song song;
       
        KeyboardState keyboardState;

        MenuController menuController;
        PausController pausController;

        public int Currentlevel = 1;

        GameState gamestate = GameState.StartScreen;
   
       
       

        
    
        public MasterController()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            getLevel = new GetLevel();
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
            m_model = new Model.Model(getLevel.GetLevels(Currentlevel));
            level = new Level(getLevel.GetLevels(Currentlevel));
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
            character = new Character();
            
            characterView = new CharacterView(GraphicsDevice, Content , m_model);
            pausView = new PausScreenView(GraphicsDevice, Content);

            menuController = new MenuController(new MenuView(GraphicsDevice, Content));
            pausController = new PausController(pausView);

            jumpSound = Content.Load<SoundEffect>("jumping");

            song = Content.Load<Song>("music");
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.2f;
            SoundEffect.MasterVolume = 0.5f;

           
            
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
            keyboardState = Keyboard.GetState();
          
            //START SKÄRM
            if (gamestate == GameState.StartScreen)
            {
                menuController.UpdateStartScreen(keyboardState);
                gamestate = menuController.GameState;
             
              
                if (gamestate == GameState.InGame)
                {
                   
                    menuController.GameState = GameState.BetweenLevels;
                }
                
            }

            else if (gamestate == GameState.BetweenLevels)
            {
               
                menuController.UpdateBetweenLevelScreen(keyboardState);
                gamestate = menuController.GameState;
                
              
                if (gamestate == GameState.InGame)
                {
                    Currentlevel += 1;
                 
                   
                    m_model = new Model.Model(getLevel.GetLevels(Currentlevel));
                    getLevel.GetLevels(Currentlevel);
                    m_model.GetLevel.ReadLevel();

                    menuController.GameState = GameState.BetweenLevels;


                }

            }

            //STARTA OM NIVÅ
            if (keyboardState.IsKeyDown(Keys.R))
            {
                m_model.RestartGame();
                character.ResetCharacterHealth();
            }

            //PAUSA SPELET
            if (keyboardState.IsKeyDown(Keys.P))
            {
                gamestate = GameState.GamePaused;
                
            }

           
            //OM SPELET ÄR PAUSAD.
            if (gamestate == GameState.GamePaused)
            {
                MediaPlayer.Pause();
                pausController.UpdatePauseScreen(keyboardState);
                gamestate = pausController.GameState;
                
                if (gamestate == GameState.InGame)
                {
                    MediaPlayer.Resume();
                    pausController.GameState = GameState.GamePaused;
              
                }
            }

                // I SPELET
                if (gamestate == GameState.InGame)
                {


                    //Containing Boolian Value = If player pressed Right button (True/False)
                    bool RightMove = characterView.PressedRight();
                    //Containing Boolian Value = If player pressed Left button (True/False)
                    bool LeftMove = characterView.PressedLeft();
                    //Containing Boolian Value = If player pressed Run button (True/False)
                    bool RunFaster = characterView.PressedRun();
                    //Containing Boolian Value = If player pressed Quit button (True/False)
                    bool Quit = characterView.PressedQuit();
                    //Containing Boolian Value = If player pressed Jump button (True/False)
                    bool Jump = characterView.PressedJump();

                    
                    //Om spelaren är död...
                    if (m_model.IfDead() == true)
                    {
                        gamestate = GameState.GameOver;
                        // audio.StopMusic();
                    }
                        //Om spelaren fortfarande lever...
                    else if (m_model.characterPosition().Y > Level.g_levelHeight || level.CheckTileEnemyCollision(character.Position, character.Size))
                    {

                        m_model.StartGame();              
                    }
                   
                    // Float funktion i spelet.
                    if (RunFaster == true)
                    {
                          m_model.Float();
                    }

                    //Höger rörelse
                    if (RightMove == true)
                    {
                        m_model.MoveRight();
                        characterView.AnimateRight(elapsedTimeMilliSeconds, CharacterView.Movement.RIGHTMOVE);
                    }

                    //Vänster rörelse
                    else if (LeftMove == true)
                    {
                        m_model.MoveLeft();
                        characterView.AnimateLeft(elapsedTimeMilliSeconds, CharacterView.Movement.LEFTMOVE);
                    }

                    //Spelare hoppar.
                    if (Jump == true)
                    {
                        if (m_model.CanPlayerJump())
                        {
                            m_model.Jump();
                            jumpSound.Play();
                            MediaPlayer.Volume = 0.5f;
                        }
                    }
                  
                    //Spel avslutas.
                    if (Quit == true)
                    {
                        this.Exit();
                    }

                    
                    m_model.Update(elapsedTimeSeconds);


                    if (gamestate == GameState.GameFinished)
                    {

                        Currentlevel = 0;
                        menuController.UpdateGameEnd(keyboardState);
                        gamestate = menuController.GameState;

                        if (gamestate == GameState.StartScreen)
                        {
                            menuController.UpdateStartScreen(keyboardState);
                            gamestate = menuController.GameState;

                        }

                    }
          
                    if (m_model.levelCompleted)
                    {
                        if (Currentlevel == 3)
                        {
                            gamestate = GameState.GameFinished;
                        }
                            

                     else {

                            gamestate = GameState.BetweenLevels;
                            menuController.GameState = GameState.BetweenLevels;
                        }
                        
                    }
                          
                }



                    if (gamestate == GameState.GameOver) {
                        MediaPlayer.Stop();
                           Currentlevel = 0;
                            menuController.GameState = GameState.GameOver;
                            menuController.UpdateGameOver(keyboardState);
                            gamestate = menuController.GameState;
                          
                        if (gamestate == GameState.InGame)
                            {
                                
                             
                                character.ResetCharacterHealth();
                                m_model.RestartGame();
                                menuController.UpdateStartScreen(keyboardState);
                                gamestate = menuController.GameState;
                            }
                        }
                    

            base.Update(gameTime);
        }
      

        /// <summary>
        /// Draws the levels & other information depending on gamestate.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (gamestate == GameState.StartScreen)
            {
                menuController.DrawStartingScreen();
            }

            if (gamestate == GameState.GamePaused)
            {
                pausController.DrawPauseScreen();
            }

            if (gamestate == GameState.BetweenLevels)
            {
                menuController.DrawBetweenLevels(Currentlevel + 1);
            }

            if (gamestate == GameState.GameFinished)
            {
                menuController.DrawGameFinished();
            }

            if (gamestate == GameState.GameOver)
            {
                menuController.DrawGameOver();
            }

            if (gamestate == GameState.InGame)
            {
              

                //Focus camera on player during the drawing.
                m_camera.CenterOn(m_model.GetCharacter.Position, GraphicsDevice.Viewport,
                new Vector2(Level.g_levelWidth, Level.g_levelHeight));
                //Draws game background
                menuController.DrawBackground();
                //Draws current map
                characterView.DrawMap(GraphicsDevice.Viewport, m_camera, m_model.GetLevel, m_model.GetCharacter.Position);
            
            }

           
            base.Draw(gameTime);
        }
    }
}
