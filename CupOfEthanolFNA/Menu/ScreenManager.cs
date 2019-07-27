﻿namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public abstract class ScreenManager
    {
        public static bool Creating;
		public static bool Editing;
		public static bool Custom;
		public static bool GameClosing = false;
		public static bool GameComplete;
		public static bool Ingame;
        public static bool Levelselect;
        public static bool Loading;
        public static bool Mainmenu;
        public static bool Paused;
		public static bool Credits;

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

        public static void LoadingCustomOn()
		{
			NoMode();
			Mainmenu = true;
			Levelselect = true;
			Custom = true;
			MainMenu.LevelSelectOn();
		}

		public static void CreditsOn()
		{
			NoMode();
			Mainmenu = true;
			Credits = true;
			MainMenu.CreditsOn();
		}

		/// <summary>
		/// Makes every screenmanager state false so a new one(or more than one) can be selected
		/// </summary>
		public static void NoMode()
        {
			GameComplete = false;
			Paused = false;
            Ingame = false;
            Mainmenu = false;
            Loading = false;
            Creating = false;
            Levelselect = false;
            Editing = false;
			Custom = false;
			Credits = false;
		}

		public static void GameCompleteOn()
		{
			NoMode();
			Mainmenu = true;
			GameComplete = true;
			MainMenu.GameCompleteOn();
		}
	}
}

