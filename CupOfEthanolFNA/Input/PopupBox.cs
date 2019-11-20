namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
	using System.IO;

	public enum PopupType
	{
		QUIT,
		ERASE_ALL_OBJECTS,
		START_NEW_GAME,
		DELETE_LEVEL,
		OVERWRITE_LEVEL
	}

	public class PopupBox
	{
		public List<Button> ButtonList;
		public bool IsFinished = false;
		public PopupType type;

		private TextSprite[] MessageList;


        public PopupBox(string[] message, PopupType type)
        {
            this.MessageList = new TextSprite[message.Length];
            //this.MessageList[0] = new TextSprite(message[0], "Small", new Vector2(300f, 110f), Color.Red);
            for (int i = 0; i < message.Length; i++)
            {
                float PositionX = (Textures.GetTexture("Pause_Menu").Width - Textures.GetFont("Medium").MeasureString(message[i]).X) / 2f + 200;
                this.MessageList[i] = new TextSprite(message[i], "Medium", new Vector2(PositionX, (float)(140 + (30 * i))), Color.Red);
            }
            this.ButtonList = new List<Button>();
            this.ButtonList.Add(new Button(new TextSprite("Yes", "Small", new Vector2(200f, 250f), Color.Red), new Vector2(263f, 255f), 4, 0.9998f));
            this.ButtonList.Add(new Button(new TextSprite("No", "Small", new Vector2(445f, 250f), Color.Red), new Vector2(415f, 255f), 4, 0.9998f));
            this.type = type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.GetTexture("Pause_Menu"), new Vector2(200f, 100f), null, Color.WhiteSmoke, 0f, Vector2.Zero, 1f, 0, 0.9996f);
            for (int h = 0; h < this.MessageList.Length; h++)
            {
                spriteBatch.DrawString(this.MessageList[h].Spritefont, this.MessageList[h].Text, this.MessageList[h].Position, this.MessageList[h].Colour, 0f, Vector2.Zero, 1f, 0, 0.9998f);
            }
            for (int i = 0; i < this.ButtonList.Count; i++)
            {
                this.ButtonList[i].Draw(spriteBatch);
            }
        }

		public void DeleteLevel(bool Choice)
		{
			this.IsFinished = true;
			if (Choice)
			{
				Directory.Delete(Level.CurrentLevelButton.Path, true);
				MainMenu.Activate();
			}
		}

		public void OverwriteLevel(bool Choice)
		{
			this.IsFinished = true;
			if (Choice)
			{
				CustomLevelButton oldData = null;
				foreach (CustomLevelButton customLevelButton in LevelButton.lvButtonList)
				{
					if (customLevelButton != Level.CurrentLevelButton // Don't compare the button with itself
						&& customLevelButton.Name == Level.CurrentLevelButton.Name) // If the level name already exists
					{
						oldData = customLevelButton;
						break;
					}
				}
				LevelButton.lvButtonList.Remove(oldData);

				LevelSaver.SaveMap();
			}
		}

        public void EraseAllObjects(bool Choice)
        {
            this.IsFinished = true;
            if (Choice)
            {
                SquareObject.sqObjectArray = new SquareObject[0x7d0, 0x7d0];
				Entity.EntityList = new List<Entity>();
				Collectable.collectableList.Clear();
			}
        }
		
		public void NewGame(bool Choice)
		{
			this.IsFinished = true;
			if (Choice)
			{
				ScreenManager.NewGame();
			}
		}

		public void QuitGame(bool Choice)
        {
            this.IsFinished = true;
            if (Choice)
            {
                MainMenu.Activate();
                Editor.CurrentBlock = null;
                Editor.CurrentEntity = null;
                Editor.CurrentCollectable = null;
            }
        }
    }
}

