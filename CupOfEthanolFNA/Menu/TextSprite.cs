namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class TextSprite
    {
        private string _spritefont;
        public Color Colour;
        public Vector2 Position;
        public string Text;
        public static List<TextSprite> TextList;

        public TextSprite(string text, string FontSize, Vector2 position, Color colour)
        {
            this.Position = position;
            this.Text = text;
            this._spritefont = FontSize;
            this.Colour = colour;
        }

        //This constructor is used when the textsprite is associated with a button. The button will handle its position
        public TextSprite(string text, string FontSize, Color colour)
        {
            this.Position = Vector2.Zero;
            this.Text = text;
            this._spritefont = FontSize;
            this.Colour = colour;
        }

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            if (TextList != null)
            {
                foreach (TextSprite ts in TextList)
                {
                    spriteBatch.DrawString(ts.Spritefont, ts.Text, ts.Position, ts.Colour, 0f, Vector2.Zero, 1f, 0, 0.94f);
                }
            }
        }

        public SpriteFont Spritefont
        {
            get
            {
                return Textures.GetFont(this._spritefont);
            }
        }
    }
}

