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
            ts = new TextSprite("Play Custom", "Medium", new Vector2(330f, 323f), Color.White);
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

		internal static void GameCompleteOn()
		{
			Button.ButtonList = new List<Button>();
			TextSprite.TextList = new List<TextSprite>();
			LevelButton.lvButtonList = new List<LevelButton>();

			SaveFile.LoadSaveFiles();

			TextSprite.TextList.Add(new TextSprite("After all your efforts. You finally made it...", "Medium", new Vector2(50, 78f), Color.White));
			TextSprite.TextList.Add(new TextSprite("Pint paradise!", "Large", new Vector2(50, 108f), Color.Yellow));
			

			TextSprite ts = new TextSprite("Main Menu", "Medium", new Vector2(225f, 503f), Color.White);
			Button.ButtonList.Add(new Button(ts, new Vector2(100f, 500f), 1));

			String coastersCollected = SaveFile.SaveList[SaveFile.Selectedfile].TotalMainCoasters().ToString();

			float extraOffset = 0;
			if (coastersCollected.Length == 2)
			{
				extraOffset = 10f;
			}
			TextSprite.TextList.Add(new TextSprite("You collected", "Medium", new Vector2(50f, 410f), Color.White));
			TextSprite.TextList.Add(new TextSprite(coastersCollected, "Medium", new Vector2(185f, 410f), Color.Yellow));
			TextSprite.TextList.Add(new TextSprite("of", "Medium", new Vector2(205f + extraOffset, 410f), Color.White));
			TextSprite.TextList.Add(new TextSprite("72", "Medium", new Vector2(230f + extraOffset, 410f), Color.Yellow));
			TextSprite.TextList.Add(new TextSprite("coasters!", "Medium", new Vector2(260f + extraOffset, 410f), Color.White));
			TextSprite.TextList.Add(new TextSprite("Go back through old levels to find em all!", "Medium", new Vector2(50f, 440f), Color.White));
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
            TextSprite.TextList.Add(new TextSprite("Levels Completed: " + SaveFile.SaveList[0].LevelsCompleted + "/" + Level.maxLevels, "Medium", new Vector2(475f, 70f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Levels Completed: " + SaveFile.SaveList[1].LevelsCompleted + "/" + Level.maxLevels, "Medium", new Vector2(475f, 170f), Color.Aqua));
            TextSprite.TextList.Add(new TextSprite("Levels Completed: " + SaveFile.SaveList[2].LevelsCompleted + "/" + Level.maxLevels, "Medium", new Vector2(475f, 270f), Color.LightSalmon));

            TextSprite.TextList.Add(new TextSprite("Coasters Collected: " + SaveFile.SaveList[0].TotalMainCoasters() + "/" + Level.maxLevels * 3, "Medium", new Vector2(475f, 95f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Coasters Collected: " + SaveFile.SaveList[1].TotalMainCoasters() + "/" + Level.maxLevels * 3, "Medium", new Vector2(475f, 195f), Color.Aqua));
            TextSprite.TextList.Add(new TextSprite("Coasters Collected: " + SaveFile.SaveList[2].TotalMainCoasters() + "/" + Level.maxLevels * 3, "Medium", new Vector2(475f, 295f), Color.LightSalmon));
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
            if (!ScreenManager.Editing && !ScreenManager.Custom)
            {
                SaveFile.LoadSaveFiles();
                for (int j = 0; j < Level.maxLevels / 3; j++)
				{
					for (int i = 0; i < 3; i++)
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

                        LevelButton.lvButtonList.Add(new LevelButton(new Vector2((float)((220 * (i % 3)) + 80), (float)((220 * (j % 2)) + 0x69)), status, collected));
                    }
                }
            }
            else
            {
                for (int j = 0; j < Level.customLevels / 3; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        LevelButton.lvButtonList.Add(new LevelButton(new Vector2((float) ((220 * (i %3 )) + 80), (float) ((220 * (j % 2)) + 0x69)), "Unlocked"));
                    }
                }
            }
        }
    }
}

