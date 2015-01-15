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

        private GameView GameView;
        private PausScreenView pausView;
        private Camera m_camera;

        private Model.Model m_model;
        private GetLevel getLevel;
        private EnemyLine enemyLine;
        SmokeView smokeView;

        private SoundEffect jumpSound;
        private SoundEffect fallingSmash;

        private Song song;

        KeyboardState keyboardState;

        MenuController menuController;
        PausController pausController;
        float speed = 0.3f;
        public int Currentlevel = 1;

        GameState gamestate = GameState.StartScreen;

        private Texture2D enemy;
        private Rectangle enemyBounds;
        private bool isLevelTwoCompleted;

        public int nextLevel = 1;

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

            m_camera = new Camera(GraphicsDevice.Viewport);

            m_model = new Model.Model();
            m_model.LoadLevel(getLevel.GetLevels(Currentlevel));



            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 900;
            graphics.ApplyChanges();
         

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

            enemyLine = new EnemyLine();
            
            GameView = new GameView(GraphicsDevice, Content , m_model);
            pausView = new PausScreenView(GraphicsDevice, Content);

            menuController = new MenuController(new MenuView(GraphicsDevice, Content));
            pausController = new PausController(pausView);
           
            
            jumpSound = Content.Load<SoundEffect>("jumping");
            fallingSmash = Content.Load<SoundEffect>("Smashing");

            enemy = Content.Load<Texture2D>("line");

            smokeView = new View.SmokeView(GraphicsDevice, Content);

            song = Content.Load<Song>("music");
            


            
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
            //Containing Boolian Value = If player pressed Quit button (True/False)
            bool Quit = GameView.PressedQuit();

            //Exit Game
            if (Quit == true)
            {
                this.Exit();
            }



            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            float elapsedTimeSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float elapsedTimeMilliSeconds = (float)gameTime.ElapsedGameTime.TotalMilliseconds;



            enemyBounds = new Rectangle((int)enemyLine.enemyPosition.X, (int)enemyLine.enemyPosition.Y, 50, 1000);

            keyboardState = Keyboard.GetState();
          
            //Start Screen
            if (gamestate == GameState.StartScreen)
            {

                if (menuController.UpdateMenuInput())
                {
                    gamestate = GameState.InGame;
                    MediaPlayer.Play(song);
                    MediaPlayer.Volume = 0.2f;
                    SoundEffect.MasterVolume = 0.5f;


                    
                    menuController.GameState = GameState.BetweenLevels;
                }
                
            }

             if (gamestate == GameState.BetweenLevels)
            {

                if (menuController.UpdateMenuInput())
                {


                    MediaPlayer.Play(song);
                    MediaPlayer.Volume = 0.2f;
                    SoundEffect.MasterVolume = 0.5f;


                    if (Currentlevel == 1)
                    {
                        Currentlevel = 2;
                        m_model = new Model.Model();
                        m_model.LoadLevel(getLevel.GetLevels(Currentlevel));


                    }

                    else
                    {
                        m_model = new Model.Model();
                        m_model.LoadLevel(getLevel.GetLevels(Currentlevel));

                    }



                    if (isLevelTwoCompleted && Currentlevel == 2)
                    {
                        isLevelTwoCompleted = false;
                        Currentlevel = 3;
                        m_model.LoadLevel(getLevel.GetLevels(Currentlevel));

                    }

                    else if (Currentlevel == 2)
                    {
                        

                        isLevelTwoCompleted = true;
                        m_model.LoadLevel(getLevel.GetLevels(Currentlevel));
                    }
              

                    gamestate = GameState.InGame;

                }

            }

            //Reset Game
            if (keyboardState.IsKeyDown(Keys.R))
            {
                m_model.RestartGame();
                m_model.ResetCharacterHealth();
            }

            //Pause Game
            if (keyboardState.IsKeyDown(Keys.P))
            {
                gamestate = GameState.GamePaused;
                
            }

           
            //If Paused...
            if (gamestate == GameState.GamePaused)
            {
                MediaPlayer.Pause();
                pausController.UpdatePauseScreen(keyboardState);
                gamestate = pausController.GameState;
                
                if (menuController.UpdateMenuInput())
                {
                    MediaPlayer.Resume();
                    gamestate = GameState.InGame;
                    pausController.GameState = GameState.GamePaused;
              
                }
            }

                //In Game
                if (gamestate == GameState.InGame)
                {
                   

                    if (Currentlevel == 3)
                    {
                        enemyLine.Update();

                        m_model.checkLineEnemyCollision(enemyBounds, GameView.m_destinationRectangle);
                    }
                 


                    //Containing Boolian Value = If player pressed Right button (True/False)
                    bool RightMove = GameView.PressedRight();
                    //Containing Boolian Value = If player pressed Left button (True/False)
                    bool LeftMove = GameView.PressedLeft();
                    //Containing Boolian Value = If player pressed Run button (True/False)
                    bool RunFaster = GameView.PressedRun();
                    //Containing Boolian Value = If player pressed Jump button (True/False)
                    bool Jump = GameView.PressedJump();

                    
                    //If Player is dead...
                    if (m_model.isGameOver == true || m_model.checkLineDeath(enemyBounds, GameView.m_destinationRectangle) == true)
                    {
                        gamestate = GameState.GameOver;
                        m_model.ResetCharacterHealth();
                    


                       
                    }

                       
                        //If player still alive...
                    else if (m_model.characterPosition().Y > Level.g_levelHeight || GameView.m_destinationRectangle.Intersects(enemyBounds) || m_model.checkSunEnemyCollision())
                    {

                        m_model.IfDead();
                        m_model.StartGame();
                        fallingSmash.Play();
                        enemyLine.enemyPosition = enemyLine.enemyDefaultPosition;
                  }
                       
                       

                    

                    else if (m_model.checkSunEnemyCollision())
                    {

                        m_model.StartGame();
                        fallingSmash.Play();
                        enemyLine.enemyPosition = enemyLine.enemyDefaultPosition;
                    }


                    //Makes character float.
                    if (RunFaster == true)
                    {
                          m_model.Float();


                    }

                    //Right Movement.
                    if (RightMove == true)
                    {
                        m_model.MoveRight();
                        GameView.AnimateRight(elapsedTimeMilliSeconds, GameView.Movement.RIGHTMOVE);
                    }

                    //Left Movement.
                    else if (LeftMove == true)
                    {
                        m_model.MoveLeft();
                        GameView.AnimateLeft(elapsedTimeMilliSeconds, GameView.Movement.LEFTMOVE);

                        if (Currentlevel == 3)
                        {

                            enemyLine.enemyPosition.X += speed * 2.0f;
                        }
                    }

                    //Makes the Character Jump.
                    if (Jump == true)
                    {
                        if (m_model.CanPlayerJump())
                        {
                            m_model.Jump();
                            jumpSound.Play();
                            MediaPlayer.Volume = 0.5f;
                        }
                    }
                  
                 

                    
                    m_model.Update(elapsedTimeSeconds);


                    if (gamestate == GameState.GameFinished)
                    {
                        
                        if (menuController.UpdateMenuInput())
                         {
                             gamestate = GameState.StartScreen;
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

                if (gamestate == GameState.GameOver)
                {
                    Currentlevel = 1;

                    m_model = new Model.Model();
                    m_model.LoadLevel(getLevel.GetLevels(Currentlevel));

                    MediaPlayer.Stop();
                    menuController.GameState = GameState.GameOver;
                    m_model.RestartGame();

                   
                    if (menuController.UpdateMenuInput())
                    {
                        gamestate = GameState.StartScreen;

                    }


                }

            base.Update(gameTime);
        }
      

        /// <summary>
        /// Draws the levels & other information depending on gamestate & current level.
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


                if (m_model.levelCompleted && Currentlevel == 1)
                {

                    nextLevel = 2;
                    menuController.DrawBetweenLevels(nextLevel);
                }


                if (m_model.levelCompleted && Currentlevel == 2)
                {

                    nextLevel = 3;
                    menuController.DrawBetweenLevels(nextLevel);
                }

                else {

                    menuController.DrawBetweenLevels(nextLevel);

                
                }

                
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

                m_camera.SetZoom(GraphicsDevice.Viewport.Width / 15);
                //Draws game background
                menuController.DrawBackground();
                //Draws current map
                GameView.DrawMap(GraphicsDevice.Viewport, m_camera, m_model.GetLevel, m_model.GetCharacter.Position);

                if (Currentlevel == 3)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(enemy, enemyBounds, Color.White);
                    spriteBatch.End();
                }

                GameView.DrawLevel(Currentlevel);


                if(Currentlevel==2 || Currentlevel == 3)
                {
                smokeView.draw((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                    
                
              
            }


            base.Draw(gameTime);
        }
    }
}
