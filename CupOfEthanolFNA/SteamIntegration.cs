using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Steamworks;

namespace LackingPlatforms
{
	public static class SteamIntegration
	{
		static bool IsRunning;
		public static void Init()
		{
			IsRunning = SteamAPI.Init();
			if (IsRunning)
			{
				Console.WriteLine(SteamUtils.GetAppID());
			}
		}

		public static class Achievements
		{
			private static void Unlock(string achievementId)
			{
				if (SteamAPI.IsSteamRunning() && IsRunning)
				{
					SteamUserStats.SetAchievement(achievementId);
				}
			}
			public static void AdventureStarted()
			{
				Unlock("GAME_STARTED");
			}
			public static void IceClimbStarted()
			{
				Unlock("ICE_CLIMB_STARTED");
			}
			public static void EthanolTempleStarted()
			{
				Unlock("ETHANOL_TEMPLE_STARTED");
			}
			public static void MinistryInfiltrationStarted()
			{
				Unlock("MINISTRY_INFILTRATION_STARTED");
			}
			public static void GameComplete()
			{
				Unlock("GAME_COMPLETE");
			}
			public static void CoastersCollected10()
			{
				Unlock("COASTERS_COLLECTED_10");
			}
			public static void CoastersCollected25()
			{
				Unlock("COASTERS_COLLECTED_25");
			}
			public static void CoastersCollected40()
			{
				Unlock("COASTERS_COLLECTED_40");
			}
			public static void CoastersCollected72()
			{
				Unlock("COASTERS_COLLECTED_72");
			}
			public static void LEVEL_CREATED()
			{
				Unlock("LEVEL_CREATED");
			}
		}
	}
}
