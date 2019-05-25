namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public abstract class ScreenManager
    {
        public static bool Creating;
        public static bool Editing;
        public static bool GameClosing = false;
        public static bool Ingame;
        public static bool Levelselect;
        public static bool Loading;
        public static bool Mainmenu;
        public static bool Paused;

        //protected ScreenManager()
        //{
        //}

        public static void CreatingOn()
        {
            NoMode();
            Mainmenu = true;
            Creating = true;
            Level.Offset = Vector2.Zero;
            MainMenu.FileSelectingOn();
        }

        public static void LoadingOn()
        {
            NoMode();
            Mainmenu = true;
            Loading = true;
            Level.Offset = Vector2.Zero;
            MainMenu.FileSelectingOn();
        }

        public static void EditingOn()
        {
            NoMode();
            Mainmenu = true;
            Levelselect = true;
            Editing = true;
            MainMenu.LevelSelectOn();
        }

        public static void ExitGame()
        {
            GameClosing = true;
        }

        public static void InstructionsOn()
        {
            Button.ButtonList = new List<Button>();
            TextSprite.TextList = new List<TextSprite>();
            TextSprite ts = new TextSprite("Main Menu", "Medium", new Vector2(225f, 503f), Color.White);
            Button.ButtonList.Add(new Button(ts, new Vector2(200f, 500f), 1));
            TextSprite.TextList.Add(new TextSprite("' Instructions in game...", "Medium", new Vector2(20f, 40f), Color.White));
        }

        /// <summary>
        /// Makes every screenmanager state false so a new one(or more than one) can be selected
        /// </summary>
        public static void NoMode()
        {
            Paused = false;
            Ingame = false;
            Mainmenu = false;
            Loading = false;
            Creating = false;
            Levelselect = false;
            Editing = false;
        }
    }
}

