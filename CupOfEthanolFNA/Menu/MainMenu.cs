﻿namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
	using System.IO;

	public static class MainMenu
    {
		static int pintTimer = 0;
		static Random rand = new Random();
		public static void Update()
		{
			if (ScreenManager.Mainmenu)
			{
				for (int i = 0; i < FallingObject.ObjectList.Count; i++)
				{
					FallingObject fallingObject = FallingObject.ObjectList[i];
					fallingObject.Update();
					if (fallingObject.Position.Y > 700)
					{
						FallingObject.ObjectList.RemoveAt(i);
						i--;
					}
				}

				if (pintTimer >= 200)
				{
					int x = 50 + rand.Next(700);
					float velX = (float) rand.NextDouble();
					float scale = 0.4f + (float) rand.NextDouble() * 0.6f;
					FallingObject.ObjectList.Add(new FallingObject("Chalice", new Vector2(x, -10), new Vector2(0.01f, 0.78f), scale));
					pintTimer = 0;
				}
				else
				{
					if (ScreenManager.GameComplete)
					{
						pintTimer += rand.Next(7);
					}
					pintTimer += rand.Next(2);
				}
			}
		}

        public static void Activate()
		{
			if (!ScreenManager.Mainmenu)
			{
				Sounds.PlayBGM("bensound-pianomoment");
			}

			PPlayer.CurrentCheckpoint = -1;

			if (!ScreenManager.Mainmenu)
			{
				FallingObject.ObjectList = new List<FallingObject>();
			}

            ScreenManager.NoMode();
            ScreenManager.Mainmenu = true;
            Level.Offset = Vector2.Zero;

            TextSprite.TextList = new List<TextSprite>();
            Button.ButtonList = new List<Button>();

            Editor.SelectedEntity = -1;
            Editor.MovingEntity = false;
            Editor.LabelList = null;

			int currentY = 143;
            LevelButton.lvButtonList = new List<LevelButton>();

			string newOrContinueText = "New Game";
			if (SaveFile.SaveFileExists())
			{
				newOrContinueText = "Continue";
			}

			TextSprite ts = new TextSprite(newOrContinueText, "Medium", new Vector2(332f, currentY), Color.White);
			Button.ButtonList.Add(new Button(ts, new Vector2(308f, currentY - 3), 1));
			currentY += 70;
			ts = new TextSprite("Play Custom", "Medium", new Vector2(330f, currentY), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(308f, currentY - 3), 1));
			currentY += 70;
			ts = new TextSprite("Editor", "Medium", new Vector2(365f, currentY), Color.White);
			Button.ButtonList.Add(new Button(ts, new Vector2(308f, currentY - 3), 1));
			currentY += 70;
			ts = new TextSprite("Stats", "Medium", new Vector2(332f, currentY), Color.White);
			Button.ButtonList.Add(new Button(ts, new Vector2(308f, currentY - 3), 1));
			currentY += 70;
			ts = new TextSprite("Credits", "Medium", new Vector2(365f, currentY), Color.White);
			Button.ButtonList.Add(new Button(ts, new Vector2(308f, currentY - 3), 1));
			currentY += 70;
			ts = new TextSprite("Exit", "Medium", new Vector2(375f, currentY), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(308f, currentY - 3), 1));
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (ScreenManager.Mainmenu || ScreenManager.Levelselect)
            {
				if (ScreenManager.Credits)
				{
					spriteBatch.Draw(Textures.GetTexture("SkyB"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2f, 0, 0.8f);
				}
				else
				{
					spriteBatch.Draw(Textures.GetTexture("SkyA"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2f, 0, 0.8f);
				}

					spriteBatch.Draw(Textures.GetTexture("Cursor"), new Vector2((float) InputManager.Mousestate[0].X, (float) InputManager.Mousestate[0].Y), null, Color.White, 0f, Vector2.Zero, 1f, 0, 1f);
                Button.DrawAll(spriteBatch);
                LevelButton.DrawAll(spriteBatch);
                TextSprite.DrawAll(spriteBatch);
				FallingObject.DrawAll(spriteBatch);
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

			string coastersCollected = SaveFile.SaveData.TotalMainCoasters().ToString();

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

		internal static void StatsOn()
		{
			Button.ButtonList = new List<Button>();
			TextSprite.TextList = new List<TextSprite>();
			LevelButton.lvButtonList = new List<LevelButton>();

			if (SaveFile.SaveFileExists())
			{
				SaveFile.LoadSaveFiles();

				TextSprite.TextList.Add(new TextSprite("Game Completion: " + SaveFile.GetPercentage() + "%", "Medium", new Vector2(175f, 70f), Color.Black));

				TextSprite.TextList.Add(new TextSprite("Levels Completed: " + SaveFile.SaveData.LevelsCompleted + "/" + Level.maxLevels, "Medium", new Vector2(175f, 110f), Color.Black));
				TextSprite.TextList.Add(new TextSprite("Coasters Collected: " + SaveFile.SaveData.TotalMainCoasters() + "/" + Level.maxLevels * 3, "Medium", new Vector2(175f, 150f), Color.Black));
			}
			else
			{
				TextSprite.TextList.Add(new TextSprite("Start a new game to see stats", "Medium", new Vector2(275f, 70f), Color.Black));
			}
			TextSprite ts = new TextSprite("Main Menu", "Medium", new Vector2(225f, 503f), Color.White);
			Button.ButtonList.Add(new Button(ts, new Vector2(100f, 500f), 1));
		}

		internal static void CreditsOn()
		{
			Button.ButtonList = new List<Button>();
			TextSprite.TextList = new List<TextSprite>();
			LevelButton.lvButtonList = new List<LevelButton>();

			int currentX = 100;
			TextSprite.TextList.Add(new TextSprite("Credits", "Large", new Vector2(currentX, 78f), Color.Yellow));

			int currentY = 120;
			int stepY = 40;
			TextSprite.TextList.Add(new TextSprite("Programming: Eoin Flanagan", "Medium", new Vector2(currentX, currentY), Color.White));
			currentY += stepY;
			TextSprite.TextList.Add(new TextSprite("Art: Eoin Flanagan", "Medium", new Vector2(currentX, currentY), Color.White));
			currentY += stepY;
			TextSprite.TextList.Add(new TextSprite("Music: https://www.bensound.com", "Medium", new Vector2(currentX, currentY), Color.White));
			currentY += stepY;
			TextSprite.TextList.Add(new TextSprite("Sound Effects: Eoin Flanagan", "Medium", new Vector2(currentX, currentY), Color.White));
			currentY += stepY;
			TextSprite.TextList.Add(new TextSprite("Level Design: Eoin Flanagan", "Medium", new Vector2(currentX, currentY), Color.White));


			TextSprite ts = new TextSprite("Main Menu", "Medium", new Vector2(225f, 503f), Color.White);
			Button.ButtonList.Add(new Button(ts, new Vector2(100f, 500f), 1));
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

			if (!ScreenManager.Custom && !ScreenManager.Editing)
			{
				ts = new TextSprite("Erase Save Data", "Medium", Color.White);
				Button.ButtonList.Add(new Button(ts, new Vector2(40f, 25f), 1));
			}

			Button.ButtonList[2].Active = false;
            if (!ScreenManager.Editing && !ScreenManager.Custom)
            {
                SaveFile.LoadSaveFiles();
                for (int j = 0; j < Level.maxLevels / 3; j++)
				{
					for (int i = 0; i < 3; i++)
					{
						int levelIndex = (j * 3) + i;
						int collected = 0;
                        for (int h = 0; h < SaveFile.SaveData.MainCoastersCollected[levelIndex].Length; h++)
                            if (SaveFile.SaveData.MainCoastersCollected[levelIndex][h])
                                collected++;

                        string status = "Locked";
                        if (SaveFile.SaveData.LevelsCompleted > (levelIndex))
                        {
                            if (collected == 3)
                                status = "Complete";
                            else
                                status = "Unlocked";
                        }
                        if (SaveFile.SaveData.LevelsCompleted == (levelIndex))
                        {
                            status = "Unlocked";
                        }

                        LevelButton.lvButtonList.Add(new LevelButton(levelIndex + 1, new Vector2((float)((220 * (i % 3)) + 80), (float)((220 * (j % 2)) + 0x69)), status, collected));
                    }
                }
            }
            else
            {
				if (!Directory.Exists(LevelSaver.CustomLevelsPath))
				{
					Directory.CreateDirectory(LevelSaver.CustomLevelsPath);
					string[] premadeCustoms = Directory.GetDirectories(@"Content\Levels\Custom");
					foreach (string custom in premadeCustoms)
					{
						string[] parts = custom.Split('\\');
						string customName = parts[parts.Length - 1];
						Directory.CreateDirectory(LevelSaver.CustomLevelsPath + customName);
						if (File.Exists(custom + "\\LevelData.xml"))
						{
							File.Copy(custom + "\\LevelData.xml", LevelSaver.CustomLevelsPath + customName + "\\LevelData.xml", true);
						}
						if (File.Exists(custom + "\\Thumbnail.png"))
						{
							File.Copy(custom + "\\Thumbnail.png", LevelSaver.CustomLevelsPath + customName + "\\Thumbnail.png", true);
						}
					}
				}

				SteamIntegration.LoadWorkshopLevels();

				//Load custom levels here
				String[] levels = Directory.GetDirectories(LevelSaver.CustomLevelsPath);
				int levelIndex = 0;
				Texture2D thumbnail;
                for (int j = 0; j < Math.Ceiling(levels.Length / 3f); j++)
                {
					for (int i = 0; i < 3; i++)
					{
						while (levelIndex < levels.Length && !File.Exists(levels[levelIndex] + "/LevelData.xml"))
						{
							// Delete folders that don't contain a level
							Directory.Delete(levels[levelIndex], true);
							levelIndex++;
						}
						if (levelIndex < levels.Length)
						{
							if (File.Exists(levels[levelIndex] + "/Thumbnail.png"))
							{
								FileStream filestream = new FileStream(levels[levelIndex] + "/Thumbnail.png", FileMode.Open);
								thumbnail = Texture2D.FromStream(MainMethod.device, filestream, 160, 120, true);
								filestream.Close();
							}
							else
							{
								thumbnail = Textures.GetCustomThumbnail();
							}
							LevelButton.lvButtonList.Add(new CustomLevelButton(levels[levelIndex], new Vector2((float)((220 * (i % 3)) + 80), (float)((220 * (j % 2)) + 0x69)), thumbnail));
							levelIndex++;
						}
					}
				}
				int x = (LevelButton.lvButtonList.Count) % 3;
				int y = (int)Math.Floor((LevelButton.lvButtonList.Count) / 3f) % 2;
				int levelSuffix = LevelButton.lvButtonList.Count;
				string name = "";
				do
				{
					levelSuffix++;
					name = "Untitled" + levelSuffix;
				} while (LevelButton.lvButtonList.Exists(item => (item as CustomLevelButton).Name == name));

				LevelButton.lvButtonList.Add(new NewLevelButton(name, new Vector2((float)((220 * (x % 3)) + 80), (float)((220 * (y % 2)) + 0x69))));
			}

			if (LevelButton.lvButtonList.Count < 7)
			{
				Button.ButtonList[1].Active = false;
			}
        }
    }
}

