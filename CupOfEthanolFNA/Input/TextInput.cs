using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LackingPlatforms
{
	class TextInput
	{
		public static List<TextInput> TextInputList;

		public const int maxLevelNameLength = 32;

		private TextSprite textSprite;
		private TextSprite label;
		private Vector2 position;
		public string Text;
		public TextInput(string labelText, Vector2 labelPosition, TextSprite textSprite, Vector2 position)
		{
			this.label = new TextSprite(labelText, "Tiny", labelPosition, Color.White);
			this.position = position;
			this.textSprite = textSprite;
			this.Text = this.textSprite.Text.Substring(0, this.textSprite.Text.Length - 1);
		}

		public static void Update()
		{
			if (TextInputList.Count > 0)
			{
				InputBox.InputToText(ref TextInputList[0].textSprite.Text);
				InputBox.CheckSpKeys(ref TextInputList[0].textSprite.Text);
				TextInputList[0].textSprite.Text = TextInputList[0].textSprite.Text
					.Substring(0, Math.Min(TextInputList[0].textSprite.Text.Length - 1, maxLevelNameLength))
					.Replace("£", "")
					.Replace("*", "")
					.Replace("/", "")
					.Replace("\\", "")
					.Replace("\"", "")
					.Replace("'", "")
					.Replace(",", "")
					.Replace(".", "");

				TextInputList[0].Text = TextInputList[0].textSprite.Text;
				TextInputList[0].textSprite.Text += "|";
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(this.label.Spritefont, this.label.Text, this.label.Position, this.label.Colour, 0f, Vector2.Zero, 1f, 0, 0.94f);
			spriteBatch.DrawString(textSprite.Spritefont, textSprite.Text, textSprite.Position, textSprite.Colour, 0f, Vector2.Zero, 1f, 0, 0.991f);
			spriteBatch.Draw(Textures.GetTexture("1"), this.position, null, Color.White, 0f, Vector2.Zero, 1f, 0, 0.99f);
		}
	}
}
