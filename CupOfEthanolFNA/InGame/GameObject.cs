namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class GameObject
    {
        private Color _colour;
        private SpriteEffects _flipeffect;
        private Vector2 _position;
        private float _rotation;
        private float _size;
        private string _texture;
        public float Layer;

        public GameObject(string texture, Vector2 position, float layer)
        {
            this.texturename = texture;
            this._position = position;
            this._rotation = 0f;
            this._size = 1f;
            this._colour = Color.White;
            this._flipeffect = SpriteEffects.None;
            this.Layer = layer;
        }

        public GameObject(string texture, Vector2 position, float layer, float size)
        {
            this.texturename = texture;
            this._position = position;
            this._rotation = 0f;
            this._size = size;
            this._colour = Color.White;
            this._flipeffect = SpriteEffects.None;
            this.Layer = layer;
        }

        public GameObject(string texture, Vector2 position, float layer, float size, Color colour)
        {
            this.texturename = texture;
            this._position = position;
            this._rotation = 0f;
            this._size = size;
            this._colour = colour;
            this._flipeffect = SpriteEffects.None;
            this.Layer = layer;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position + Level.Offset, null, this.Colour, this.Rotation, Vector2.Zero, this.Size, this._flipeffect, this.Layer);
        }

        public void Draw(SpriteBatch spriteBatch, Color qColour)
        {
            spriteBatch.Draw(this.Texture, this.Position + Level.Offset, null, qColour, this.Rotation, Vector2.Zero, this.Size, this._flipeffect, this.Layer);
        }

        public void DrawFixed(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, null, this.Colour, this.Rotation, Vector2.Zero, this.Size, this.Flipeffect, this.Layer);
        }

        public void DrawFixed(SpriteBatch spriteBatch, Vector2 Offset)
        {
            spriteBatch.Draw(this.Texture, this.Position + Offset, null, this.Colour, this.Rotation, Vector2.Zero, this.Size, this.Flipeffect, this.Layer);
        }

        public Color Colour
        {
            get
            {
                return this._colour;
            }
            set
            {
                this._colour = value;
            }
        }

        public SpriteEffects Flipeffect
        {
            get
            {
                return this._flipeffect;
            }
            set
            {
                this._flipeffect = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this._position;
            }
            set
            {
                this._position = value;
            }
        }

        public float Rotation
        {
            get
            {
                return this._rotation;
            }
            set
            {
                this._rotation = value;
            }
        }

        public float Size
        {
            get
            {
                return this._size;
            }
            set
            {
                this._size = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return Textures.GetTexture(this.texturename);
            }
        }

        public string texturename
        {
            get
            {
                return this._texture;
            }
            set
            {
                this._texture = value;
            }
        }
    }
}

