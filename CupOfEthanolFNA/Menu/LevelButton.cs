namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class LevelButton
    {
        public static int CurrentGroup;
        public static List<LevelButton> lvButtonList;
        public Vector2 Position;
        public string Status;
        public int CoastersCollected;
        private static Vector2 ImageOffset = new Vector2(20, 20);

        public LevelButton(Vector2 position, string status)
        {
            this.Status = status;
            this.Position = position;
            CoastersCollected = -1;
        }


        public LevelButton(Vector2 position, string status, int coasters)
        {
            this.Status = status;
            this.Position = position;
            CoastersCollected = coasters;
        }

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            for (int i = CurrentGroup * 6; (i < ((CurrentGroup + 1) * 6)) && (i < lvButtonList.Count); i++)
            {
				Texture2D thumbnail = ScreenManager.Custom || ScreenManager.Editing ? Textures.GetCustomThumbnail(i) : Textures.GetThumbnail(i);
                spriteBatch.Draw(thumbnail, lvButtonList[i].Position + Level.Offset + ImageOffset, null, lvButtonList[i].ThumbnailColour, 0, Vector2.Zero, 1, SpriteEffects.None, 0.895f);
                spriteBatch.Draw(Textures.GetTexture("Button2"), lvButtonList[i].Position + Level.Offset, null, lvButtonList[i].Colour, 0f, Vector2.Zero, 1f, 0, 0.89f);
                spriteBatch.DrawString(Textures.GetFont("Medium"), (i + 1).ToString(), (lvButtonList[i].Position + new Vector2(22f, 18f)) + Level.Offset, Color.Black, 0f, Vector2.Zero, 1f, 0, 0.9f);

				if (lvButtonList[i].Status == "Locked")
					spriteBatch.DrawString(Textures.GetFont("Medium"), "Locked", (lvButtonList[i].Position + new Vector2(28f, 148f)) + Level.Offset, Color.LightGray, 0f, Vector2.Zero, 1f, 0, 0.9f);
				else if (lvButtonList[i].CoastersCollected != -1)
					spriteBatch.DrawString(Textures.GetFont("Medium"), "Coasters: " + lvButtonList[i].CoastersCollected.ToString() + "/3", (lvButtonList[i].Position + new Vector2(28f, 148f)) + Level.Offset, Color.Wheat, 0f, Vector2.Zero, 1f, 0, 0.9f);
            }
        }

        public static void NextGroup()
        {
            CurrentGroup++;
            Button.ButtonList[2].Active = true;
            if (CurrentGroup >= (lvButtonList.Count / 6) - 1)
            {
                CurrentGroup = (lvButtonList.Count / 6) - 1;
                Button.ButtonList[1].Active = false;
            }
            else
                Button.ButtonList[1].Active = true;
        }

        public static void PreviousGroup()
        {
            CurrentGroup--;
            Button.ButtonList[1].Active = true;
            if (CurrentGroup <= 0 )
            {
                CurrentGroup = 0;
                Button.ButtonList[2].Active = false;
            }
            else
                Button.ButtonList[2].Active = true;
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)(this.Position.X + Level.Offset.X), (int)(this.Position.Y + Level.Offset.Y), Textures.GetTexture("Button2").Width, Textures.GetTexture("Button2").Height);
            }
        }

		private static Color LightGreen = new Color(170, 255, 170, 255);
		private static Color LightestGreen = new Color(200, 255, 200, 255);

		public Color ThumbnailColour
		{
			get
			{
				if (MouseClick.Rect.Intersects(this.Rect))
				{
					switch (this.Status)
					{
						case "Complete":
							return LightestGreen;

						case "Unlocked":
							return Color.White;
					}
					return Color.DarkGray;
				}
				switch (this.Status)
				{
					case "Complete":
						return LightGreen;

					case "Unlocked":
						return Color.WhiteSmoke;
				}
				return Color.SlateGray;
			}
		}

		public Color Colour
        {
            get
            {
                if (MouseClick.Rect.Intersects(this.Rect))
                {
                    switch (this.Status)
                    {
                        case "Complete":
                            return Color.Green;

                        case "Unlocked":
                            return Color.Orange;
                    }
                    return Color.DarkGray;
                }
                switch (this.Status)
                {
                    case "Complete":
                        return Color.DarkGreen;

                    case "Unlocked":
                        return Color.DarkOrange;
                }
                return Color.SlateGray;
            }
        }
    }
}

