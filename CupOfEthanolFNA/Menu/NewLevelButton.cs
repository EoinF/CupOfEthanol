using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LackingPlatforms
{
	class NewLevelButton : CustomLevelButton
	{
		public NewLevelButton(string name, Vector2 position)
			: base(LevelSaver.CustomLevelsPath + name, position, Textures.GetTexture("6"))
		{

		}
		public override void DrawLabels(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(Textures.GetFont("Medium"), "New Map", (this.Position + new Vector2(28f, 148f)) + Level.Offset, Color.Yellow, 0f, Vector2.Zero, 1f, 0, 0.9f);
		}
	}
}
