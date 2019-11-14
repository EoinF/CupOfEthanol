namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using System;

    public static class Level
	{
		// Note: Must be in multiples of 6 or crashes will occur
		public const int maxLevels = 24;

		public static string _backgroundTexture;
        public static float AirResistance;
        public static List<Collectable> ChaliceList;
        public static int Current = -1;
		public static CustomLevelButton CurrentLevelButton;
        public static Vector2 Gravity;
        public static Vector2 Offset = Vector2.Zero;
		public static String SongName;

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

		public static void MoveAllObjects(int deltaX, int deltaY)
		{
			Vector2 positionDelta = new Vector2(25 * deltaX, 25 * deltaY);
			SquareObject[,] sq = new SquareObject[2000, 2000];
			for (int i = Math.Max(0,-deltaX); i < Math.Min(2000, SquareObject.sqObjectArray.GetLength(0) - deltaX); i++)
			{
				for (int j = Math.Max(0, -deltaY); j < Math.Min(2000, SquareObject.sqObjectArray.GetLength(1) - deltaY); j++) {
					sq[i + deltaX, j + deltaY] = SquareObject.sqObjectArray[i, j];
					if (sq[i + deltaX, j + deltaY] != null)
					{
						sq[i + deltaX, j + deltaY].Position = sq[i + deltaX, j + deltaY].Position + positionDelta;
					}
				}
			}
			SquareObject.sqObjectArray = sq;

			foreach (Checkpoint checkpoint in Checkpoint.checkpointList)
			{
				checkpoint.collectable.Position = checkpoint.collectable.Position + positionDelta;
			}

			foreach (Collectable chalice in ChaliceList)
			{
				chalice.Position = chalice.Position + positionDelta;
			}
			foreach (Collectable collectable in Collectable.collectableList)
			{
				collectable.Position = collectable.Position + positionDelta;
			}
			foreach (Entity entity in Entity.EntityList)
			{
				entity.sqobject.Position = entity.sqobject.Position + positionDelta;
			}
		}
    }
}

