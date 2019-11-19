using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;

namespace LackingPlatforms
{
	public class SteamIntegration
	{
		public static SteamIntegration instance;
		private Dictionary<UGCHandle_t, WorkshopLevelButton> subscribedItemButtonMap;
		private Dictionary<UGCHandle_t, WorkshopLevelButton> subscribedThumbnailButtonMap;

		private CallResult<RemoteStoragePublishFileResult_t> RemoteStoragePublishFileResult;
		private CallResult<RemoteStorageEnumerateUserSubscribedFilesResult_t> RemoteStorageEnumerateUserSubscribedFilesResult;
		private CallResult<RemoteStorageGetPublishedFileDetailsResult_t> RemoteStorageGetPublishedFileDetailsResult;
		private CallResult<RemoteStorageDownloadUGCResult_t> RemoteStorageDownloadUGCResult;
		private CallResult<RemoteStorageDownloadUGCResult_t> RemoteStorageDownloadUGCThumbnailResult;
		private CallResult<RemoteStorageUnsubscribePublishedFileResult_t> RemoteStorageUnsubscribePublishedFileResult;
		

		bool IsRunning;
		public static void Init()
		{
			if (instance == null)
			{
				instance = new SteamIntegration();
				instance.IsRunning = SteamAPI.Init();
				if (instance.IsRunning)
				{
					Console.WriteLine(SteamUtils.GetAppID());
					instance.RemoteStoragePublishFileResult = CallResult<RemoteStoragePublishFileResult_t>.Create(instance.OnRemoteStoragePublishFileResult);
					instance.RemoteStorageEnumerateUserSubscribedFilesResult = CallResult<RemoteStorageEnumerateUserSubscribedFilesResult_t>.Create(instance.OnRemoteStorageEnumerateUserSubscribedFilesResult);
					instance.RemoteStorageGetPublishedFileDetailsResult = CallResult<RemoteStorageGetPublishedFileDetailsResult_t>.Create(instance.OnRemoteStorageGetPublishedFileDetailsResult);
					instance.RemoteStorageDownloadUGCResult = CallResult<RemoteStorageDownloadUGCResult_t>.Create(instance.OnRemoteStorageDownloadUGCResult);
					instance.RemoteStorageDownloadUGCThumbnailResult = CallResult<RemoteStorageDownloadUGCResult_t>.Create(instance.OnRemoteStorageDownloadUGCThumbnailResult);
					instance.RemoteStorageUnsubscribePublishedFileResult = CallResult<RemoteStorageUnsubscribePublishedFileResult_t>.Create(instance.OnRemoteStorageUnsubscribePublishedFileResult);
				}
			}
		}

		public static void Update()
		{
			if (SteamAPI.IsSteamRunning() && instance.IsRunning)
			{
				SteamAPI.RunCallbacks();
			}
		}

		public static void UploadWorkshopLevel()
		{
			if (SteamAPI.IsSteamRunning() && instance.IsRunning)
			{
				string path = Level.CurrentLevelButton.Path;
				string name = Level.CurrentLevelButton.Name;
				bool fileExists = SteamRemoteStorage.FileExists(name);

				//if (!fileExists)
				{
					Console.WriteLine("Fetching file data...");
					string levelData = File.ReadAllText(path + "/LevelData.xml");
					
					// Upload level data
					byte[] Data = new byte[Encoding.UTF8.GetByteCount(levelData)];
					Encoding.UTF8.GetBytes(levelData, 0, levelData.Length, Data, 0);
					Console.WriteLine("Starting file upload...");
					bool isSuccess = SteamRemoteStorage.FileWrite(name, Data, Data.Length);
					// Upload preview image
					Data = File.ReadAllBytes(LevelSaver.CustomLevelsPath + "/Preview.png");
					Console.WriteLine("Starting preview file upload...");
					string previewFileName = name + "_thumbnail";
					isSuccess = isSuccess && SteamRemoteStorage.FileWrite(previewFileName, Data, Data.Length);

					Console.WriteLine("Finished file upload.. success = " + isSuccess.ToString());

					if (isSuccess)
					{
						string[] tags = new string[0];
						string workshopName = name;
						string workshopDescription = name;

						// Publish to workshop
						SteamAPICall_t handle = SteamRemoteStorage.PublishWorkshopFile(name, previewFileName, SteamUtils.GetAppID(), workshopName, workshopDescription,
						ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic,
						tags, EWorkshopFileType.k_EWorkshopFileTypeCommunity);
						Console.WriteLine("Started the workshop upload...");

						instance.RemoteStoragePublishFileResult.Set(handle);
					}
					else
					{
						System.Windows.Forms.MessageBox.Show(
							"An unexpected error occured while uploading the level data", "Failed to publish workshop file");
					}
				}
			}
		}

		private void OnRemoteStoragePublishFileResult(RemoteStoragePublishFileResult_t pCallback, bool bIOFailure)
		{
			Console.WriteLine("On publish file result");
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				MessageBox.StatusMessage = new MessageBox("Publish Successful!", new Vector2(217, 190), 120);
			}
			else
			{
				System.Windows.Forms.MessageBox.Show(
					"An unexpected error occured while uploading the level data", "Failed to publish workshop file");
			}
		}

		private Stack<PublishedFileId_t> subscriptions;
		private void DownloadNextLevel()
		{
			if (subscriptions.Count > 0)
			{
				PublishedFileId_t f = subscriptions.Peek();
				SteamAPICall_t handle = SteamRemoteStorage.GetPublishedFileDetails(f, 0);
				RemoteStorageGetPublishedFileDetailsResult.Set(handle);
			}
		}

		private void OnRemoteStorageEnumerateUserSubscribedFilesResult(RemoteStorageEnumerateUserSubscribedFilesResult_t pCallback, bool bIOFailure)
		{
			Console.WriteLine("Subscribed list result");
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				Console.WriteLine("Found " + pCallback.m_nTotalResultCount.ToString() + " Items");

				for (int i = 0; i < pCallback.m_nTotalResultCount; i++)
				{
					PublishedFileId_t f = pCallback.m_rgPublishedFileId[i];
					subscriptions.Push(f);
				}
				DownloadNextLevel();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show(
					"An unexpected error occured (subscribed files result)", "Error");
			}
		}

		private Stack<UGCHandle_t> levelDataStack;
		private void DownloadNextLevelData()
		{
			if (levelDataStack.Count > 0)
			{
				UGCHandle_t f = levelDataStack.Pop();
				SteamAPICall_t handle = SteamRemoteStorage.UGCDownload(f, 0);
				RemoteStorageDownloadUGCResult.Set(handle);
			}
		}

		private Stack<UGCHandle_t> levelThumbnailStack;
		private void DownloadNextLevelThumbnail()
		{
			if (levelThumbnailStack.Count > 0)
			{
				UGCHandle_t f = levelThumbnailStack.Pop();
				SteamAPICall_t handle2 = SteamRemoteStorage.UGCDownload(f, 0);
				RemoteStorageDownloadUGCThumbnailResult.Set(handle2);
			}
		}

		private void OnRemoteStorageGetPublishedFileDetailsResult(RemoteStorageGetPublishedFileDetailsResult_t pCallback, bool bIOFailure)
		{
			Console.WriteLine("Got file details for " + pCallback.m_pchFileName + " with preview image " + pCallback.m_hPreviewFile);
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				if (!subscribedItemButtonMap.ContainsKey(pCallback.m_hFile))
				{
					int x = (LevelButton.lvButtonList.Count) % 3;
					int y = (int)Math.Floor((LevelButton.lvButtonList.Count) / 3f) % 2;
					Texture2D thumbnail = Textures.GetCustomThumbnail();
					Vector2 position = new Vector2((float)((220 * (x % 3)) + 80), (float)((220 * (y % 2)) + 0x69));
					WorkshopLevelButton button = new WorkshopLevelButton(pCallback.m_pchFileName, position, thumbnail);
					LevelButton.CalculateGroup();
					LevelButton.lvButtonList.Add(button);

					subscribedItemButtonMap.Add(pCallback.m_hFile, button);
					subscribedThumbnailButtonMap.Add(pCallback.m_hPreviewFile, button);

					levelDataStack.Push(pCallback.m_hFile);
					levelThumbnailStack.Push(pCallback.m_hPreviewFile);
				}
				subscriptions.Pop();
				if (subscriptions.Count > 0)
				{
					DownloadNextLevel();
				}
				else
				{
					DownloadNextLevelData();
					DownloadNextLevelThumbnail();
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show(
					"An unexpected error occured (Get published file details result)", "Error");
			}
		}

		private void OnRemoteStorageDownloadUGCResult(RemoteStorageDownloadUGCResult_t pCallback, bool bIOFailure)
		{
			Console.WriteLine("Downloaded level data for " + pCallback.m_pchFileName);
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				byte[] Data = new byte[pCallback.m_nSizeInBytes];
				int ret = SteamRemoteStorage.UGCRead(pCallback.m_hFile, Data, pCallback.m_nSizeInBytes, 0, EUGCReadAction.k_EUGCRead_Close);

				string itemContent = Encoding.UTF8.GetString(Data, 0, ret);
				instance.subscribedItemButtonMap[pCallback.m_hFile].IsReady = true;
				instance.subscribedItemButtonMap[pCallback.m_hFile].LevelData = itemContent;

				DownloadNextLevelData();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show(
					"An unexpected error occured (Download UGC result)", "Error");
			}
		}

		private void OnRemoteStorageDownloadUGCThumbnailResult(RemoteStorageDownloadUGCResult_t pCallback, bool bIOFailure)
		{
			Console.WriteLine("Downloaded thumbnail for " + pCallback.m_pchFileName);
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
				byte[] Data = new byte[pCallback.m_nSizeInBytes];
				int ret = SteamRemoteStorage.UGCRead(pCallback.m_hFile, Data, pCallback.m_nSizeInBytes, 0, EUGCReadAction.k_EUGCRead_Close);

				Texture2D thumbnail = Texture2D.FromStream(MainMethod.device, new MemoryStream(Data), 160, 120, true);
				instance.subscribedThumbnailButtonMap[pCallback.m_hFile].Thumbnail = thumbnail;
				DownloadNextLevelThumbnail();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show(
					"An unexpected error occured (Download UGC Thumbnail result)", "Error");
			}
		}

		private void OnRemoteStorageUnsubscribePublishedFileResult(RemoteStorageUnsubscribePublishedFileResult_t pCallback, bool bIOFailure)
		{
			if (pCallback.m_eResult == EResult.k_EResultOK)
			{
			}
			else
			{
				System.Windows.Forms.MessageBox.Show(
					"An unexpected error occured", "Error");
			}
		}

		
		public static void LoadWorkshopLevels()
		{
			if (SteamAPI.IsSteamRunning() && instance.IsRunning)
			{
				instance.subscribedItemButtonMap = new Dictionary<UGCHandle_t, WorkshopLevelButton>();
				instance.subscribedThumbnailButtonMap = new Dictionary<UGCHandle_t, WorkshopLevelButton>();
				instance.subscriptions = new Stack<PublishedFileId_t>();
				instance.levelDataStack = new Stack<UGCHandle_t>();
				instance.levelThumbnailStack = new Stack<UGCHandle_t>();
				SteamAPICall_t handle = SteamRemoteStorage.EnumerateUserSubscribedFiles(0);
				instance.RemoteStorageEnumerateUserSubscribedFilesResult.Set(handle);
			}
		}
		
		public class Achievements
		{
			private static void Unlock(string achievementId)
			{
				if (SteamAPI.IsSteamRunning() && instance.IsRunning)
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
			public static void CoastersCollected50()
			{
				Unlock("COASTERS_COLLECTED_50");
			}
			public static void CoastersCollected60()
			{
				Unlock("COASTERS_COLLECTED_60");
			}
			public static void CoastersCollected70()
			{
				Unlock("COASTERS_COLLECTED_70");
			}
			public static void CoastersCollected72()
			{
				Unlock("COASTERS_COLLECTED_72");
			}
			public static void LevelCreated()
			{
				Unlock("LEVEL_CREATED");
			}
		}
	}
}
