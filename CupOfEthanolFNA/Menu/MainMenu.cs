namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public static class MainMenu
    {
        public static void Activate()
        {
            Sounds.StopBGM();
            PPlayer.CurrentCheckpoint = -1;
            ScreenManager.NoMode();
            ScreenManager.Mainmenu = true;
            Level.Offset = Vector2.Zero;

            TextSprite.TextList = new List<TextSprite>();
            Button.ButtonList = new List<Button>();

            Editor.SelectedEntity = -1;
            Editor.MovingEntity = false;
            Editor.LabelList = null;

            LevelButton.lvButtonList = new List<LevelButton>();
            TextSprite ts = new TextSprite("New Game", "Medium", new Vector2(332f, 183f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(308f, 180f), 1));
            ts = new TextSprite("Continue", "Medium", new Vector2(343f, 253f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(308f, 250f), 1));
            ts = new TextSprite("Instructions", "Medium", new Vector2(330f, 323f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(308f, 320f), 1));
            ts = new TextSprite("Editor", "Medium", new Vector2(365f, 393f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(308f, 390f), 1));
            ts = new TextSprite("Exit", "Medium", new Vector2(375f, 463f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(308f, 460f), 1));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (ScreenManager.Mainmenu || ScreenManager.Levelselect)
            {
                spriteBatch.Draw(Textures.GetTexture("Cursor"), new Vector2((float) InputManager.Mousestate[0].X, (float) InputManager.Mousestate[0].Y), null, Color.White, 0f, Vector2.Zero, 1f, 0, 1f);
                Button.DrawAll(spriteBatch);
                LevelButton.DrawAll(spriteBatch);
                TextSprite.DrawAll(spriteBatch);
            }
        }

        public static void FileSelectingOn()
        {
            Button.ButtonList = new List<Button>();
            TextSprite.TextList = new List<TextSprite>();
            SaveFile.LoadSaveFiles();
			
            TextSprite ts = new TextSprite("File 1", "Medium", new Vector2(255f, 78f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(200f, 75f), 1));
			
            ts = new TextSprite("File 2", "Medium", new Vector2(255f, 178f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(200f, 175f), 1));
			
            ts = new TextSprite("File 3", "Medium", new Vector2(255f, 278f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(200f, 275f), 1));

            ts = new TextSprite("Main Menu", "Medium", new Vector2(225f, 503f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(200f, 500f), 1));
            TextSprite.TextList.Add(new TextSprite("Levels Completed: " + SaveFile.SaveList[0].LevelsCompleted + "/30", "Small", new Vector2(475f, 70f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Levels Completed: " + SaveFile.SaveList[1].LevelsCompleted + "/30", "Small", new Vector2(475f, 170f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Levels Completed: " + SaveFile.SaveList[2].LevelsCompleted + "/30", "Small", new Vector2(475f, 270f), Color.White));

            TextSprite.TextList.Add(new TextSprite("Coasters Collected: " + SaveFile.SaveList[0].TotalMainCoasters() + "/90", "Small", new Vector2(475f, 85f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Coasters Collected: " + SaveFile.SaveList[1].TotalMainCoasters() + "/90", "Small", new Vector2(475f, 185f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Coasters Collected: " + SaveFile.SaveList[2].TotalMainCoasters() + "/90", "Small", new Vector2(475f, 285f), Color.White));
        }

        public static void LevelSelectOn()
        {
            //int i;
            //int j;
            LevelButton.CurrentGroup = 0;
            TextSprite.TextList = new List<TextSprite>();
            Button.ButtonList = new List<Button>();
            LevelButton.lvButtonList = new List<LevelButton>();

            TextSprite ts = new TextSprite("Main Menu", "Medium", Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(525f, 25f), 1));

            ts = new TextSprite("Next", "Medium", Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(565f, 550f), 1));

            ts = new TextSprite("Previous", "Medium", Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(40f, 550f), 1));

            Button.ButtonList[2].Active = false;
            if (!ScreenManager.Editing)
            {
                SaveFile.LoadSaveFiles();
                for (int i = 0; i < Level.maxLevels / 2; i++)
                {
                    int j = 0;

                    while (j < 2)
                    {
                        int collected = 0;
                        for (int h = 0; h < SaveFile.SaveList[SaveFile.Selectedfile].MainCoastersCollected[(i * 2) + j].Length; h++)
                            if (SaveFile.SaveList[SaveFile.Selectedfile].MainCoastersCollected[(i * 2) + j][h])
                                collected++;

                        string status = "Locked";
                        if (SaveFile.SaveList[SaveFile.Selectedfile].LevelsCompleted > ((i * 2) + j))
                        {
                            if (collected == 3)
                                status = "Complete";
                            else
                                status = "Unlocked";
                        }
                        if (SaveFile.SaveList[SaveFile.Selectedfile].LevelsCompleted == ((i * 2) + j))
                        {
                            status = "Unlocked";
                        }

                        LevelButton.lvButtonList.Add(new LevelButton(new Vector2((float)((220 * (i - ((i / 3) * 3))) + 80), (float)((220 * j) + 0x69)), status, collected));
                        j++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        LevelButton.lvButtonList.Add(new LevelButton(new Vector2((float) ((220 * (i - ((i / 3) * 3))) + 80), (float) ((220 * j) + 0x69)), "Unlocked"));
                    }
                }
            }
        }
    }
}

