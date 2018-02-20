﻿using Engine.Engines;
using Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Screens;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Stack<SplashScreen> splashScreens = new Stack<SplashScreen>();
        SplashScreen pauseScreen;
        SplashScreen playScreen;
        ActiveScreenState currentScreenState = ActiveScreenState.PLAY;

        public Game1()
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
            // TODO: Add your initialization logic here
            new InputEngine(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Helpers.Helper.graphicsDevice = graphics.GraphicsDevice;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pauseScreen = new SplashScreen(Vector2.Zero, Content.Load<Texture2D>("pause"), Content.Load<Song>("pauseSong"), Keys.P);
            playScreen = new SplashScreen(Vector2.Zero, Content.Load<Texture2D>("play"), Content.Load<Song>("playSong"), Keys.Escape);
            playScreen.Active = true;
            
            splashScreens.Push(playScreen);
            splashScreens.Push(pauseScreen);

            // TODO: use this.Content to load your game content here
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            // Exit();

            // TODO: Add your update logic here

            if (InputEngine.CurrentKeyState.IsKeyDown(Keys.P) && currentScreenState == ActiveScreenState.PLAY)
            {
                splashScreens.Peek().Update();
                currentScreenState = ActiveScreenState.PAUSE;
                splashScreens.Peek().Active = true;
                splashScreens.Peek().Update();
                Console.WriteLine(splashScreens.Count);
                SplashScreen splash = splashScreens.Pop();
                splashScreens.Peek().Active = false;
                
                splashScreens.Push(splash);

            }
            else if (InputEngine.CurrentKeyState.IsKeyDown(Keys.Escape) && currentScreenState == ActiveScreenState.PAUSE)
            {
                currentScreenState = ActiveScreenState.PLAY;
                
                SplashScreen splash =  splashScreens.Pop();
                splashScreens.Peek().Update();
                splashScreens.Peek().Active = true;
                splashScreens.Peek().Update();
                splashScreens.Push(splash);
                splashScreens.Peek().Active = false;
                
                Console.WriteLine(splashScreens.Count);
            }

            
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (SplashScreen item in splashScreens)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
