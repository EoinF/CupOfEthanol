namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class Button
    {
        private static List<Button> _buttonList;
        private TextSprite _text;
        private int _type;
        private float Layer;
        private Vector2 Position;
        public bool Active = true;

        public Button(TextSprite text, Vector2 position, int type)
        {
            this.Layer = 0.99f;
            this.Position = position;
            this.Text = text;
            this._type = type;
        }

        public Button(TextSprite text, Vector2 position, int type, float layer)
        {
            this.Position = position;
            this.Text = text;
            this._type = type;
            this.Layer = layer;
        }

        private Color CheckIfSelected()
        {
            if (this.Rect.Intersects(MouseClick.Rect))
            {
                return Color.Yellow;
            }
            return Color.LightYellow;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Color colour = this.CheckIfSelected();
                spriteBatch.Draw(this.Texture, this.Position, null, colour, 0f, Vector2.Zero, 1f, 0, this.Layer);
                spriteBatch.DrawString(Text.Spritefont, Text.Text, this.Position + ((new Vector2(this.Rect.Width, this.Rect.Height) - Text.Spritefont.MeasureString(Text.Text)) / 2f), colour, 0f, Vector2.Zero, 1f, 0, this.Layer + 0.0001f);
            }
        }

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (Button B in ButtonList)
            {
                if (B.Active)
                {
                    Color colour = Color.LightYellow;
                    if (MainMethod.popupBox == null)
                    {
                        colour = B.CheckIfSelected();
                    }
                    //Rectangle? CS$0$0002 = null;
                    spriteBatch.Draw(B.Texture, B.Position, null, colour, 0f, Vector2.Zero, 1f, 0, B.Layer);
                    spriteBatch.DrawString(B.Text.Spritefont, B.Text.Text, B.Position + ((new Vector2(B.Rect.Width, B.Rect.Height) - B.Text.Spritefont.MeasureString(B.Text.Text)) / 2f), colour, 0f, Vector2.Zero, 1f, 0, B.Layer + 0.0001f);
                }
            }
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Texture.Width, this.Texture.Height);
            }
        }

        public static List<Button> ButtonList
        {
            get
            {
                return _buttonList;
            }
            set
            {
                _buttonList = value;
            }
        }

        public TextSprite Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        private Texture2D Texture
        {
            get
            {
                return Textures.GetTexture(this._type.ToString());
            }
        }
    }
}

