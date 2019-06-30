﻿namespace LackingPlatforms
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;
    using System;

    public static class PConsole
    {
        public static bool Active;
        public static string CurrentText = "|";
        private static int BlinkCooldown;
        private static string[] BlockList = new string[] { "" };

        public static void Activate()
        {
            BlinkCooldown = 40;
            Active = true;
        }

        public static void Deactivate()
        {
            Active = false;
            CurrentText = "|";
        }

        public static bool CheckCommands()
        {
            CurrentText = CurrentText.ToLower();
            int i;

            string[] SplitText = CurrentText.Substring(0, CurrentText.Length - 2).Split(' ');
            switch (SplitText[0])
            {
                case "/n":
                    Level.Current++;
                    if (Level.Current > 30)
                        Level.Current = 1;
                    LevelLoader.LoadEditorLevel();
                    return true;
                case "/p":
                    Level.Current--;
                    if (Level.Current < 1)
                        Level.Current = 30;
                    LevelLoader.LoadEditorLevel();
                    return true;

                case "/load":
                    if (int.TryParse(SplitText[1], out i))
                    {
                        Level.Current = i;
                        if (Level.Current < 1)
                            Level.Current = 1;
                        if (Level.Current > 30)
                            Level.Current = 30;
                        LevelLoader.LoadEditorLevel();
                    }
                    return true;
            }
            try
            {
                if (SplitText[1] == ">>")
                {
                    for (int k = 0; k < BlockList.Length; k++)
                    {
                        if (SplitText[0] == BlockList[k])
                        {
                            for (int l = 0; l < BlockList.Length; l++)
                            {
                                if (SplitText[2] == BlockList[l])
                                    ; //TBC: When the game is actually done...
                            }
                        }
                    }
                }
            }
            catch { }

            return false;
        }



        public static void Update()
        {
            BlinkCooldown--;
            if (BlinkCooldown < -40)
                BlinkCooldown = 40;
            InputBox.InputToText(ref CurrentText);
            InputBox.CheckSpKeys(ref CurrentText);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (BlinkCooldown < 0)
            {
                try
                {
                    spriteBatch.DrawString(Textures.GetFont("Tiny"), CurrentText.Substring(0, CurrentText.Length - 1), new Vector2(30, 560), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.9f);
                }
                catch 
                { 

                }
            }
            else
            {
                try
                {
                    spriteBatch.DrawString(Textures.GetFont("Tiny"), CurrentText, new Vector2(30, 560), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.9f);
                }
                catch
                {

                }
            }
        }
    }
}
