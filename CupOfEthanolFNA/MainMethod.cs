namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class MainMethod : Game
    {
        private GraphicsDevice device;
        public GraphicsDeviceManager graphics;
        public static PopupBox popupBox;
        public static bool Restarting;
        private SpriteBatch spriteBatch;
        public static float TimeSinceLastUpdate;
        public static bool DebugMode;
        public static Color InstanceColour;
        public static Random rand;
        public static Debug debug;


        public MainMethod(string[] args)
        {
            this.graphics = new GraphicsDeviceManager(this);
            base.Content.RootDirectory = "Content";

            DecideMenuColour();

            foreach (string s in args)
            {
                if (s == "-debug")
                    DebugMode = true;
            }
        }

        private static void DecideMenuColour()
        {
            rand = new Random();

            float r = rand.Next(500) / 1000f;
            float g = rand.Next(400) / 1000f;
            float b = rand.Next(400) / 1000f;

            InstanceColour = new Color(r, g, b, 0);

        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.device);
            Textures.LoadTextures(Content);
            Sounds.LoadSounds(Content);
        }


        protected override void Initialize()
        {
            this.device = this.graphics.GraphicsDevice;
            this.graphics.IsFullScreen = false;
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            this.graphics.ApplyChanges();            

            if (DebugMode)
            {
                base.Window.Title = "Cup of Ethanol: Debugging";
                //this.IsFixedTimeStep = false;
                debug = new Debug();
            }
            else
                base.Window.Title = "Cup of Ethanol";

			MainMenu.Activate();
			SquareObject.sqObjectArray = new SquareObject[0, 0];
            Collectable.collectableList = new List<Collectable>();
            Entity.EntityList = new List<Entity>();
            Checkpoint.checkpointList = new List<Checkpoint>();
            InputManager.Mousestate = new MouseState[5];
            base.Initialize();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (ScreenManager.GameClosing)
            {
                base.Exit();
            }
            if (base.IsActive)
            {
				MainMenu.Update();
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
				SaveFile.CompleteLevel();
				if (Level.Current == Level.maxLevels)
				{
					if (ScreenManager.Custom)
					{
						Level.Current = 1;
						LevelLoader.NextLevel();
					}
					else
					{
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
            GraphicsDevice.Clear(InstanceColour);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            if (popupBox != null)
            {
                popupBox.Draw(this.spriteBatch);
            }
            InGame.Draw(this.spriteBatch, gameTime);
            Editor.Draw(this.spriteBatch, gameTime);
            MainMenu.Draw(this.spriteBatch);
            if (DebugMode)
                debug.Draw(this.spriteBatch);
            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

