namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;

    public abstract class InputManager
    {
        private static KeyboardState[] _keystate = new KeyboardState[20];
        private static MouseState[] _mousestate;

        public static void CheckInput()
        {
            for (int i = Keystate.Length - 1; i > 0; i--)
            {
                Keystate[i] = Keystate[i - 1];
            }
            Keystate[0] = Keyboard.GetState();
            if (MainMethod.popupBox != null)
            {
                CheckMouse();
            }
            else if (ScreenManager.Ingame && !ScreenManager.Paused)
            {
                if (PPlayer.DeathCountdown < 0)
                {
                    InGameControls();
                }
                PauseCheck();
            }
            else if (ScreenManager.Mainmenu)
            {
                CheckMouse();
            }
            else if (ScreenManager.Editing)
            {
                if (!InputBox.Active)
                {
                    EditorControls();
                }
                PauseCheck();
                CheckMouse();
            }
            else if (ScreenManager.Paused)
            {
                PauseCheck();
                CheckMouse();
            }
        }

        public static void CheckMouse()
        {
            for (int i = 0; i < 4; i++)
            {
                Mousestate[i + 1] = Mousestate[i];
            }
            Mousestate[0] = Mouse.GetState();
            if ((((Mousestate[0].X < 800) && (Mousestate[0].X > 0)) && (Mousestate[0].Y < 600)) && (Mousestate[0].Y > 0))
            {
                if (!PConsole.Active)
                {
                    if (Mousestate[0].LeftButton == ButtonState.Pressed)
                    {
                        MouseClick.Left();
                    }
                    if (Mousestate[0].RightButton == ButtonState.Pressed)
                    {
                        MouseClick.Right();
                    }
                }
            }
        }

        public static void EditorControls()
        {
            if (!ScreenManager.Paused)
            {
                if (!PConsole.Active)
                {
                    if (Keystate[0].IsKeyDown(Keys.W) || Keystate[0].IsKeyDown(Keys.Up))
                    {
                        PPlayer.Player.sqobject.Position += (Vector2)(new Vector2(0f, -10f) * PPlayer.Player.sqobject.Velocity.X);
                    }
                    if (Keystate[0].IsKeyDown(Keys.A) || Keystate[0].IsKeyDown(Keys.Left))
                    {
                        PPlayer.Player.sqobject.Position += (Vector2)(new Vector2(-10f, 0f) * PPlayer.Player.sqobject.Velocity.X);
                    }
                    if (Keystate[0].IsKeyDown(Keys.S) || Keystate[0].IsKeyDown(Keys.Down))
                    {
                        PPlayer.Player.sqobject.Position += (Vector2)(new Vector2(0f, 10f) * PPlayer.Player.sqobject.Velocity.X);
                    }
                    if (Keystate[0].IsKeyDown(Keys.D) || Keystate[0].IsKeyDown(Keys.Right))
                    {
                        PPlayer.Player.sqobject.Position += (Vector2)(new Vector2(10f, 0f) * PPlayer.Player.sqobject.Velocity.X);
                    }
                    if (JustPressed(Keys.P))
                    {
                        SquareObject[,] sq = SquareObject.sqObjectArray;
                        for (int x = sq.GetLowerBound(0); x < sq.GetLength(0); x++)
                        {
                            for (int y = 1; y < sq.GetLength(1); y++)
                            {
                                if ((sq[x, y] != null) && (sq[x, y].texturename == "A"))
                                {
                                    PPlayer.Player.sqobject.Position = sq[x, y].Position;
                                }
                            }
                        }
                    }
                    if (JustPressed(Keys.Enter))
                    {
                        PConsole.Activate();
                    }
                    if (Keystate[0].IsKeyDown(Keys.L))
                    {
                        PPlayer.Player.sqobject.Velocity = new Vector2(5f, PPlayer.Player.sqobject.Velocity.Y);
                    }
                    if (Keystate[0].IsKeyDown(Keys.K))
                    {
                        PPlayer.Player.sqobject.Velocity = new Vector2(2f, PPlayer.Player.sqobject.Velocity.Y);
                    }
                }
                else
                {
                    PConsole.Update();

                    if (JustPressed(Keys.Enter))
                    {
                        PConsole.CheckCommands();
                        PConsole.Deactivate();
                    }
                }
            }
        }

        public static void InGameControls()
        {
            PPlayer.Player.Walking = false;
            if (Keystate[0].IsKeyDown(Keys.W) || Keystate[0].IsKeyDown(Keys.Up))
            {
                PlayerJump();
            }
            if (Keystate[0].IsKeyDown(Keys.A) || Keystate[0].IsKeyDown(Keys.Left))
            {
                PlayerLeft();
            }
            if (Keystate[0].IsKeyDown(Keys.D) || Keystate[0].IsKeyDown(Keys.Right))
            {
                PlayerRight();
            }
            if (JustPressed(Keys.P))
            {
                LevelLoader.LevelComplete = true;
            }
            if (JustPressed(Keys.C))
            {
                int checkpoint = PPlayer.CurrentCheckpoint;
                if (checkpoint < Checkpoint.checkpointList.Count - 1)
                    checkpoint++;
                else
                    checkpoint = 0;
                for (int i = 0; i < Checkpoint.checkpointList.Count; i++)
                {
                    if (Checkpoint.checkpointList[i].ID == checkpoint)
                        PPlayer.Player.sqobject.Position = Checkpoint.checkpointList[i].collectable.Position - (PPlayer.Player.sqobject.Texture.Height * Vector2.UnitY);
            }
                }
        }

        public static bool JustPressed(Keys key)
        {
            return (Keystate[0].IsKeyDown(key) && Keystate[1].IsKeyUp(key));
        }

        public static void PauseCheck()
        {
            if ((!ScreenManager.Mainmenu && !InputBox.Active) && JustPressed(Keys.Escape))
            {
                if (ScreenManager.Editing)
                {
                    if (ScreenManager.Paused)
                    {
                        EditorPauseMenu.Unpause();
                    }
                    else
                    {
                        EditorPauseMenu.Pause();
                    }
                    ScreenManager.Editing = true;
                }
                else if (ScreenManager.Paused)
                {
                    PauseMenu.Unpause();
                }
                else
                {
                    PauseMenu.Pause();
                }
            }
        }

        public static void PlayerJump()
        {
            if (PPlayer.Player.sqobject.Velocity.Y < -6f)
            {
                PPlayer.Player.JumpTimeout -= 5;
            }
            if ((PPlayer.Player.JumpTimeout < 1) && PPlayer.Player.OnGround)
            {
                PPlayer.Player.JumpTimeout = 20;
                PPlayer.walkingSound_wave = -2;
                Sounds.Play(3);
                PPlayer.Player.sqobject.Velocity += new Vector2(0f, -0.5f);
                PPlayer.Player.sqobject.Position += new Vector2(0f, -3f);
            }
            if (PPlayer.Player.JumpTimeout > 0)
            {
                PPlayer.Player.sqobject.Velocity += new Vector2(0f, -0.7f + (0.054f / (((float) PPlayer.Player.JumpTimeout) / 20f)));
            }
        }

        public static void PlayerLeft()
        {
            PPlayer.Player.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;


            if (!PPlayer.Player.OnGround)
            {
                if (PPlayer.Player.sqobject.Velocity.X > -3f)
                {
                    PPlayer.Player.sqobject.Velocity -= new Vector2(0.07f, 0f);
                }
            }
            else
			{
				if (PPlayer.walkingSound_wave == 0)
					Sounds.Play(new int[] { 0, 1, 2 });

				PPlayer.Player.Walking = true;

                PPlayer.walkingSound_wave++;

                if (PPlayer.Player.sqobject.Velocity.X > -3f)
                {
                    PPlayer.Player.sqobject.Velocity -= new Vector2(0.033f, 0) * (0.65f + (0.055f * PPlayer.Player.sqobject.frictionforce)); //Acceleration increases as friction increases
                }
            }
        }

        public static void PlayerRight()
        {
            PPlayer.Player.sqobject.Flipeffect = SpriteEffects.None;


            if (!PPlayer.Player.OnGround)
            {
                if (PPlayer.Player.sqobject.Velocity.X < 3f)
                {
                    PPlayer.Player.sqobject.Velocity += new Vector2(0.07f, 0f);
                }
            }
            else
			{
				if (PPlayer.walkingSound_wave == 0)
					Sounds.Play(new int[] { 0, 1, 2 });

				PPlayer.Player.Walking = true;
                PPlayer.walkingSound_wave++;

                if (PPlayer.Player.sqobject.Velocity.X < 3f)
                {
                    PPlayer.Player.sqobject.Velocity += new Vector2(0.033f, 0) * (0.65f + (0.055f * PPlayer.Player.sqobject.frictionforce)); //Acceleration increases as friction increases 
                }
            }
        }

        public static KeyboardState[] Keystate
        {
            get
            {
                return _keystate;
            }
            set
            {
                _keystate = value;
            }
        }

        public static MouseState[] Mousestate
        {
            get
            {
                return _mousestate;
            }
            set
            {
                _mousestate = value;
            }
        }
    }
}

