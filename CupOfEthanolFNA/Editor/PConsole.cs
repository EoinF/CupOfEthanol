namespace LackingPlatforms
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
            int i, j;

            string[] SplitText = CurrentText.Substring(0, CurrentText.Length - 2).Split(' ');
            switch (SplitText[0])
            {
                case "/n":
                    Level.Current++;
                    if (Level.Current > LevelButton.lvButtonList.Count + 1)
                        Level.Current = 1;
                    LevelLoader.LoadEditorLevel();
                    return true;
                case "/p":
                    Level.Current--;
                    if (Level.Current < 1)
                        Level.Current = LevelButton.lvButtonList.Count + 1;
                    LevelLoader.LoadEditorLevel();
                    return true;
					
				case "/moveall":
					if (int.TryParse(SplitText[1], out i) && int.TryParse(SplitText[2], out j))
					{
						Level.MoveAllObjects(i, j);
					}
					return true;
			}

            return false;
        }

        public static void Update()
        {
            BlinkCooldown--;
            if (BlinkCooldown < -40)
                BlinkCooldown = 40;
            InputBox.InputToText(ref CurrentText, false);
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

