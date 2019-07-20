namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using System;

    public static class Level
	{
		// Note: Must be in multiples of 6 or crashes will occur
		public const int maxLevels = 12;
		public const int customLevels = 24; //12


		public static string _backgroundTexture;
        public static float AirResistance;
        public static List<Collectable> ChaliceList;
        public static int Current = -1;
        public static Vector2 Gravity;
        public static Vector2 Offset = Vector2.Zero;
        public static bool IsMain = true;

        public static void DrawBackground(SpriteBatch spriteBatch)
        {
            for (int x = -1; x < 5; x++)
            {
                for (int y = -1; y < 5; y++)
                {
                    //Rectangle? CS$0$0000 = null;
                    spriteBatch.Draw(BackgroundTexture, (new Vector2((float) (0xd93 * x), (float) (0x95b * y)) - new Vector2(((float) BackgroundTexture.Width) / 7f, ((float) BackgroundTexture.Height) / 7f)) + ((Vector2) (Offset / 4f)), null, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0.002f);
                }
            }
        }

        public static Texture2D BackgroundTexture
        {
            get
            {
                return Textures.GetTexture(_backgroundTexture);
            }
        }
    }
}

