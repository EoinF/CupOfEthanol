using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LackingPlatforms
{
	class WorkshopLevelButton : CustomLevelButton
	{
		public string LevelData;
		public bool IsReady;
		public WorkshopLevelButton(string name, Vector2 position, Texture2D thumbnail)
			: base(LevelSaver.CustomLevelsPath + name, position, thumbnail)
		{
			LevelData = null;
			IsReady = false;
		}
		public override void DrawLabels(SpriteBatch spriteBatch)
		{
			if (IsReady)
			{
				base.DrawLabels(spriteBatch);
			}
			else
			{
				spriteBatch.DrawString(Textures.GetFont("Medium"), "Downloading " + this.Name, (this.Position + new Vector2(28f, 148f)) + Level.Offset, Color.Yellow, 0f, Vector2.Zero, 0.7f, 0, 0.9f);
			}
		}
		public override Color Colour
		{
			get
			{
				return Color.DarkSlateGray;
			}
		}
	}
}
