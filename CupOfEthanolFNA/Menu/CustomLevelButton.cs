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
		private string LabelText;
		public string Path;
		public CustomLevelButton(string path, Vector2 position, Texture2D thumbnail)
		{
			this.Path = path;
			string[] parts = path.Split('/');
			this.Name = parts[parts.Length - 1];
			if (this.Name.Length > TextInput.maxLevelNameLength)
			{
				this.Name = this.Name.Substring(0, TextInput.maxLevelNameLength);
			}
			LabelText = splitTextAcrossLines(this.Name);
			Console.WriteLine(this.Name + " becomes:");
			Console.WriteLine(LabelText);

			this.Position = position;
			this.Thumbnail = thumbnail;
			this.Status = "Unlocked";
		}

		private string splitTextAcrossLines(string text)
		{
			float MaxNameWidth = 160f;
			string newText = "";
			string[] tokens = text.Split(' ');
			string currentLine = "";

			for (int t = 0; t < tokens.Length; t++)
			{
				if (currentLine.Length == 0)
				{
					currentLine += tokens[t];
				}
				else if (Textures.GetFont("Small").MeasureString(currentLine + ' ' + tokens[t]).X < MaxNameWidth)
				{
					currentLine += ' ' + tokens[t];
				}
				else
				{
					string overflow = "";
					while (Textures.GetFont("Small").MeasureString(currentLine).X > MaxNameWidth)
					{
						overflow += currentLine[currentLine.Length - 1];
						currentLine = currentLine.Substring(0, currentLine.Length - 1);
					}

					newText += currentLine + '\n';
					currentLine = overflow;
					t--; // Repeat for this token as we haven't used it yet
				}
			}
			if (currentLine.Trim().Length == 0)
			{
				newText.Substring(0, newText.Length - 1); // Remove trailing newline character
			}
			else
			{
				if (Textures.GetFont("Small").MeasureString(currentLine).X > MaxNameWidth)
				{
					newText += splitBigWord(currentLine, MaxNameWidth);
				}
				else
				{
					newText += currentLine;
				}
			}
			return newText;
		}

		private string splitBigWord(string word, float MaxNameWidth)
		{
			string newText = "";
			string currentLine = "";
			while (word.Length != 0)
			{
				if (newText.Length > 0)
				{
					newText += '\n';
				}
				while (word.Length > 0 
					&& Textures.GetFont("Small").MeasureString(currentLine + word[word.Length - 1]).X < MaxNameWidth)
				{
					currentLine += word[word.Length - 1];
					word = word.Substring(0, word.Length - 1);
				}
				newText += currentLine;
				currentLine = "";
			}

			return newText;
		}

		public override void DrawLabels(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(Textures.GetFont("Small"), this.LabelText, (this.Position + new Vector2(22f, 140f)) + Level.Offset, Color.White, 0f, Vector2.Zero, 1f, 0, 0.9f);
		}
	}
}
