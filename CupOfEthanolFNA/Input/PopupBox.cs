namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

	public enum PopupType
	{
		QUIT,
		ERASE_BLOCKS,
		ERASE_ENTITIES,
		START_NEW_GAME
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
                float PositionX = (Textures.GetTexture("Pause_Menu").Width - Textures.GetFont("Small").MeasureString(message[i]).X) / 2f + 200;
                this.MessageList[i] = new TextSprite(message[i], "Small", new Vector2(PositionX, (float)(140 + (30 * i))), Color.Red);
            }
            this.ButtonList = new List<Button>();
            this.ButtonList.Add(new Button(new TextSprite("Yes", "Small", new Vector2(200f, 250f), Color.Red), new Vector2(263f, 255f), 4, 0.9998f));
            this.ButtonList.Add(new Button(new TextSprite("No", "Small", new Vector2(445f, 250f), Color.Red), new Vector2(415f, 255f), 4, 0.9998f));
            this.type = type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.GetTexture("Pause_Menu"), new Vector2(200f, 100f), null, Color.Yellow, 0f, Vector2.Zero, 1f, 0, 0.9996f);
            for (int h = 0; h < this.MessageList.Length; h++)
            {
                spriteBatch.DrawString(this.MessageList[h].Spritefont, this.MessageList[h].Text, this.MessageList[h].Position, this.MessageList[h].Colour, 0f, Vector2.Zero, 1f, 0, 0.9998f);
            }
            for (int i = 0; i < this.ButtonList.Count; i++)
            {
                this.ButtonList[i].Draw(spriteBatch);
            }
        }

        public void EraseBlocks(bool Choice)
        {
            this.IsFinished = true;
            if (Choice)
            {
                SquareObject.sqObjectArray = new SquareObject[0x7d0, 0x7d0];
            }
        }

		public void EraseEntities(bool Choice)
		{
			this.IsFinished = true;
			if (Choice)
			{
				Entity.EntityList = new List<Entity>();
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

