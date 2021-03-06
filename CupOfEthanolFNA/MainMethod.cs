﻿namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
	using Steamworks;
	using System.IO;

	public class MainMethod : Game
    {
        public static GraphicsDevice device;
        public GraphicsDeviceManager graphics;
        public static PopupBox popupBox;
        public static bool Restarting;
        private SpriteBatch spriteBatch;
        public static float TimeSinceLastUpdate;
        public static bool DebugMode;
        public static Color InstanceColour;
        public static Random rand;
        public static Debug debug;
		private bool isAppStarting;

        public MainMethod(string[] args)
        {
            this.graphics = new GraphicsDeviceManager(this);
            base.Content.RootDirectory = "Content";
			rand = new Random();
			
			foreach (string s in args)
            {
                if (s == "-debug")
                    DebugMode = true;
            }
        }
		
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(device);
            Textures.LoadTextures(Content, device);
            Sounds.LoadSounds(Content);
			LevelSaver.Screenshot = new RenderTarget2D(GraphicsDevice, 800, 600, false, SurfaceFormat.Color, DepthFormat.Depth24);
		}

		protected override void Initialize()
		{
			SteamIntegration.Init();
			device = this.graphics.GraphicsDevice;
            this.graphics.IsFullScreen = false;
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
			this.graphics.ApplyChanges();
			this.graphics.GraphicsProfile = GraphicsProfile.Reach;

            if (DebugMode)
            {
                base.Window.Title = "Cup of Ethanol: Debugging";
                //this.IsFixedTimeStep = false;
                debug = new Debug();
            }
            else
                base.Window.Title = "Cup of Ethanol";

			SquareObject.sqObjectArray = new SquareObject[0, 0];
            Collectable.collectableList = new List<Collectable>();
            Entity.EntityList = new List<Entity>();
            Checkpoint.checkpointList = new List<Checkpoint>();
            InputManager.Mousestate = new MouseState[5];
			InputManager.GamepadState = new GamePadState[2];
			this.isAppStarting = true;
            base.Initialize();
        }

        protected override void UnloadContent()
		{
			LevelSaver.Screenshot.Dispose();
		}

        protected override void Update(GameTime gameTime)
		{
			if (this.isAppStarting)
			{
				MainMenu.Activate();
				this.isAppStarting = false;
			}
			else
			{
				SteamIntegration.Update();
			}
			if (ScreenManager.GameClosing)
            {
				SteamAPI.Shutdown();
				base.Exit();
            }
            if (base.IsActive && !SteamIntegration.instance.IsPublishing)
            {
				MainMenu.Update();
				LevelSaver.Update();

				if (popupBox == null)
                {
                    InGame.Update();
                    Editor.Update();
                }
                else if (popupBox.IsFinished)
                {
                    popupBox = null;
                }
                InputManager.CheckInput();
            }
            else if (ScreenManager.Ingame)
            {
                PauseMenu.Pause();
            }
            if (LevelLoader.LevelComplete)
			{
				if (!ScreenManager.Custom)
				{
					SaveFile.CompleteLevel();
				}
				if (Level.Current == Level.maxLevels)
				{
					if (ScreenManager.Custom)
					{
						Level.Current = 0;
						LevelLoader.NextLevel();
					}
					else
					{
						SteamIntegration.Achievements.GameComplete();
						LevelLoader.LevelComplete = false;
						ScreenManager.GameCompleteOn();
					}
				}
				else
				{
					LevelLoader.NextLevel();
				}
			}

            if (DebugMode)
                debug.Update(gameTime);

            TimeSinceLastUpdate = ((float) base.TargetElapsedTime.TotalMilliseconds) / 1000f;
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
			if (LevelSaver.SavingTimeout == 1)
			{
				Console.WriteLine("Taking the screenshot now");
				GraphicsDevice.SetRenderTarget(LevelSaver.Screenshot); // rendering to the render target
				GraphicsDevice.Clear(Color.Transparent);
				spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

				InGame.Draw(this.spriteBatch, gameTime, true);
				Editor.Draw(this.spriteBatch, gameTime, true);

				string path = Level.CurrentLevelButton.Path;
				string newPath = path.Substring(0, path.Length - Level.CurrentLevelButton.Name.Length) + TextInput.TextInputList[0].Text;
				Console.WriteLine(newPath);

				if (!Directory.Exists(newPath))
				{
					Directory.CreateDirectory(newPath);
				}
				this.spriteBatch.End();

				//
				// Take a screenshot
				//
				using (FileStream fs = File.Create(newPath + "\\Thumbnail.png"))
				{
					LevelSaver.Screenshot.SaveAsPng(fs, 160, 120);
				}
				if (LevelSaver.IsPublishingToWorkshop)
				{
					using (FileStream fs = File.OpenWrite(LevelSaver.CustomLevelsPath + "\\Preview.png"))
					{
						LevelSaver.Screenshot.SaveAsPng(fs, 800, 600);
					}
				}
				GraphicsDevice.SetRenderTarget(null);
			}

			GraphicsDevice.Clear(InstanceColour);
			spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

			if (popupBox != null)
            {
                popupBox.Draw(this.spriteBatch);
            }
            InGame.Draw(this.spriteBatch, gameTime, false);

            Editor.Draw(this.spriteBatch, gameTime, false);
            MainMenu.Draw(this.spriteBatch);
            if (DebugMode)
                debug.Draw(this.spriteBatch);
            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

