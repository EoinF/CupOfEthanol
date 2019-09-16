using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LackingPlatforms
{
	public class CustomLevelButton : LevelButton
	{
		public string Name;
		public string Path;
		public CustomLevelButton(string path, Vector2 position, Texture2D thumbnail)
		{
			this.Path = path;
			string[] parts = path.Split('/');
			this.Name = parts[parts.Length - 1];
			if (this.Name.Length > 12)
			{
				this.Name = this.Name.Substring(12);
			}
			this.Position = position;
			this.Thumbnail = thumbnail;
			this.Status = "Unlocked";
		}

		public override void DrawLabels(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(Textures.GetFont("Medium"), this.Name, (this.Position + new Vector2(28f, 148f)) + Level.Offset, Color.White, 0f, Vector2.Zero, 1f, 0, 0.9f);
		}
	}
}
