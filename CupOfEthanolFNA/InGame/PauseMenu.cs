namespace LackingPlatforms
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;

    public static class PauseMenu
    {
        private static TextSprite Label;

        public static void Draw(SpriteBatch spriteBatch)
        {
            //Rectangle? CS$0$0000 = null;
            spriteBatch.Draw(Textures.GetTexture("Cursor"), new Vector2((float)InputManager.Mousestate[0].X, (float) InputManager.Mousestate[0].Y), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            DrawButtons_Labels(spriteBatch);
            spriteBatch.Draw(Textures.GetTexture("Pause_Menu"), new Vector2(210f, 86f), null, new Color(1f, 1f, 1f, 0.9f), 0f, Vector2.Zero, 1f, 0, 0.9f);
        }

        private static void DrawButtons_Labels(SpriteBatch spriteBatch)
        {
            Button.DrawAll(spriteBatch);
            TextSprite.DrawAll(spriteBatch);
            spriteBatch.DrawString(Label.Spritefont, Label.Text, Label.Position, Label.Colour, 0f, Vector2.Zero, 1f, 0, 0.95f);
        }

        public static void Load()
        {
            Load_Labels();
            Load_Buttons();
        //    Load_TextSprites();
        }

        private static void Load_Buttons()
        {
            Button.ButtonList = new List<Button>();
            Button.ButtonList.Add(new Button(new TextSprite("Resume", "Small", Vector2.Zero, Color.Red), new Vector2(295f, 160f), 3));
            Button.ButtonList.Add(new Button(new TextSprite("Main Menu", "Small", Vector2.Zero, Color.Red), new Vector2(295f, 230f), 3));
        }

        private static void Load_Labels()
        {
            Label = new TextSprite("Game Paused", "Large", new Vector2((800 - Textures.GetFont("Large").MeasureString("Game Paused").X) / 2f, 100), Color.White);
        }

        //private static void Load_TextSprites()
        //{
        //}

        public static void Pause()
        {
            if (ScreenManager.Ingame)
            {
                MediaPlayer.Pause();
                Load();
            }
            for (int i = 0; i < Entity.EntityList.Count; i++)
            {
                Entity.EntityList[i].Walking = false;
            }
            MessageBox.GameMessage = null;
            PPlayer.Player.Walking = false;
            ScreenManager.Paused = true;
        }

        public static void Unpause()
        {
            MediaPlayer.Resume();
            Label = null;
            TextSprite.TextList = null;
            Button.ButtonList = null;
            ScreenManager.NoMode();
            ScreenManager.Ingame = true;
        }
    }
}

