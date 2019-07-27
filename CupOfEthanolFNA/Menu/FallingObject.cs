namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class FallingObject
    {
		public Vector2 Position;
		public Vector2 Velocity;
		public float Scale;
		public Color Colour;
		String Texture;
		public static List<FallingObject> ObjectList;

        public FallingObject(String texture, Vector2 position, Vector2 velocity, float scale)
        {
			this.Texture = texture;
            this.Position = position;
            this.Velocity = velocity;
			this.Scale = scale;
			this.Colour = new Color(Color.White, 0.7f);
        }

		public void Update()
		{
			this.Position += Velocity;
		}

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            if (ObjectList != null)
            {
                foreach (FallingObject o in ObjectList)
                {
                    spriteBatch.Draw(Textures.GetTexture(o.Texture), o.Position, null, o.Colour, 0f, Vector2.Zero, o.Scale, SpriteEffects.None, 1);
                }
            }
        }
    }
}

