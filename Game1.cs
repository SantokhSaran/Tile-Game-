using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace DragonSwapF
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game

    {   //gfx
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        
        //Creating instance of new tiles
        List<Tile> Tiles = new List<Tile>();
        Tile BlankTile = new Tile(16, 100, 100, true);
        Tile shuffle = new Tile(17, 100, 100, false); //Button to shuffle the tiles


        //Mouse states 
        private MouseState mouseState;
        private MouseState preMouseState;
        private int mouseX;
        private int mouseY;


        //variables 
        private int moves;
        private Vector2 StorageVector;
        Random rnd = new Random();

        public void Tileload() //Loading tiles 
        {
            for (int i = 1; i < 17; i++)
            {
                if (i < 16)
                {
                    Tiles.Add(new Tile(0, 100, 100, true));
                    Tiles[i - 1].setContentManager(Content);
                }
                else
                {
                    Tiles.Add(new Tile(i, 100, 100, true));
                    Tiles[i - 1].setContentManager(Content);
                }

            }
            shuffle.setContentManager(Content);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this); //gfx
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 480; // makes window to size
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            
            Tileload (); //this function allows the tiles to know the boundries of the board
            IsMouseVisible = true;

            int x = 100; //constants 
            int y = 0;

            for (int i = 0; i < 15; i++)
            {

                if (i < 4)
                {
                    Tiles[i].m_Pos = new Vector2(70 + (x * y), 90);
                }
                else if (i < 8)
                {
                    if (i > 3)
                    {
                        Tiles[i].m_Pos = new Vector2(-330 + (x * y), 190);
                    }
                }
                else if (i < 12)
                {
                    if (i > 7)
                    {
                        Tiles[i].m_Pos = new Vector2(-730 + (x * y), 290);
                    }
                }
                else if (i < 16)
                {
                    if (i > 11)
                    {
                        Tiles[i].m_Pos = new Vector2(-1130 + (x * y), 390);
                    }
                }
                y++;
            }
                Tiles[15].m_Pos = new Vector2(370, 390); 

                shuffle.m_Pos = new Vector2(200, 600); 

                base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// // TODO: use this.Content to load your game content here
        protected override void LoadContent()
        {
            Song song = Content.Load<Song>("JirenTheme");  
            MediaPlayer.Play(song);
            Tile.Cm = this.Content;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Draw function for the images 
            for (int i = 1; i < 17; i++)
            {
                if (i < 16)
                {
                    Tiles[i - 1].setTexture("Tile" + i);
                }
                else
                {
                    Tiles[i - 1].setTexture("BlankTile");
                }
            }
            shuffle.setTexture("shuffle");


            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //Mouse states 
            preMouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseX = mouseState.X;
            mouseY = mouseState.Y;

            //So the code knows the postition of the blank tile 
            StorageVector = Tiles[15].m_Pos;

            //Click function to swap and move tiles 
            Click();
            int tileClicked = Click();

            if (Tiles[tileClicked].m_Movable == true)
            {
                Tiles[15].m_Pos = Tiles[tileClicked].m_Pos;
                Tiles[tileClicked].m_Pos = StorageVector;
               
            }
            if (mouseState.LeftButton == ButtonState.Pressed && preMouseState.LeftButton == ButtonState.Released)
            {

                if (shuffle.rect.Contains(mouseX, mouseY))
                {
                    for (int x = 0; x < 500; x++)
                    {
                        int i = rnd.Next(0, 14);
                        StorageVector = Tiles[15].m_Pos;
                       

                        if (Tiles[i].m_Movable == true)
                        {
                            Tiles[15].m_Pos = Tiles[i].m_Pos;
                            Tiles[i].m_Pos = StorageVector;
                           
                        }
                    }

                }
                moves++;
            }
            base.Update(gameTime);
        }
        public int Click()
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && preMouseState.LeftButton == ButtonState.Released)
                {
                    if (Tiles[i].rect.Contains(mouseX, mouseY))
                    {
                        return i;
                    }
                }
            }
            return 15;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < 16; i++)
            {
                Tiles[i].Draw(spriteBatch);
            }
            shuffle.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
