namespace LackingPlatforms
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class SaveFile
    {
        public int LevelsCompleted;
        public List<bool[]> MainCoastersCollected;

        public static SaveFile SaveData;

		private static string SaveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/CupOfEthanol/";

        #region Pre-Made CoasterList
        public static List<bool[]> NewCoasterList = new List<bool[]>(){
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false},
            new bool[]{false, false, false}
        };
        #endregion

        public SaveFile(int levelsCompleted, List<bool[]> mainCoasters)
        {
            this.LevelsCompleted = levelsCompleted;
            this.MainCoastersCollected = mainCoasters;
        }


        public int TotalMainCoasters()
        {
            int total = 0;
            foreach (bool[] CoastersPerLevel in MainCoastersCollected)
            {
                foreach (bool IsCollected in CoastersPerLevel)
                    if (IsCollected)
                        total++;
            }

            return total;
        }

        public static void CompleteLevel()
        {
            if (Level.IsMain)
            {
                if (SaveData.LevelsCompleted < Level.Current)
					SaveData.LevelsCompleted++;
            }
            SaveGame();
        }

        public static string ArrayToString(int[] array)
        {
            string x = "";
            for (int i = 0; i < array.Length; i++)
            {
                x = x + array[i].ToString() + "/";
            }
            return x.Substring(0, x.Length - 1);
        }

        private static int[] GetIntArray(XmlNode node)
        {
            string[] stringarray = node.InnerText.Split('/');
            int[] intarray = new int[stringarray.Length];
            for (int i = 0; i < intarray.Length; i++)
            {
                if (stringarray[i] == "")
                {
                    intarray[i] = 0;
                }
                else
                {
                    intarray[i] = int.Parse(stringarray[i]);
                }
            }
            return intarray;
        }

        public static string GetPercentage()
        {
			float LevelsWeightPct = 50;
			float CoastersWeightPct = 50;
			float percent = SaveData.LevelsCompleted * LevelsWeightPct / Level.maxLevels;
			percent += SaveData.TotalMainCoasters() * CoastersWeightPct / (Level.maxLevels * 3f);
			return percent.ToString("0.00");
        }

        public static XmlDocument LoadDocument(string filename, bool useFallback = true)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(filename))
            {
                StreamReader sr = new StreamReader(filename);
                XmlTextReader xr = new XmlTextReader(sr);
                doc.Load(xr);
                sr.Dispose();
                xr.Close();
                return doc;
            }
			if (filename.Contains("SavedData"))
			{
				throw new Exception("Failed to load saved data");
			}
			return LevelSaver.PrepareNewLevelDocument("This level was created because the file was missing. ", filename);
        }

		public static bool SaveFileExists()
		{
			string pathName = SaveDirectory + "/SavedData.xml";
			return File.Exists(pathName);
		}

		public static void CreateSaveFile()
		{
			string pathName = SaveDirectory + "/SavedData.xml";
			Directory.CreateDirectory(SaveDirectory);
			File.Copy(@"Content/SavedData.xml", pathName, true);
			LoadSaveFiles();
		}

		public static void LoadSaveFiles()
		{
			string pathName = SaveDirectory + "/SavedData.xml";
			XmlDocument doc = LoadDocument(pathName);
			
			XmlNode node = doc.FirstChild.NextSibling.FirstChild;
			
			SaveData = new SaveFile(int.Parse(node.InnerText), GetMainCoasterData(node.NextSibling.InnerText));
		}

		private static List<bool[]> GetMainCoasterData(string text)
        {
            List<bool[]> CoasterData = new List<bool[]>();
            string[] strarray = text.Split('/');
            //Now its split into individual levels

            for (int h = 0; h < strarray.Length; h++)
            {
                string[] Coasters = strarray[h].Split('-');

                bool[] CurrentLevel = new bool[3];
                for (int i = 0; i < Coasters.Length; i++)
                {
                    if (Coasters[i] == "t")
                        CurrentLevel[i] = true;
                    else
                        CurrentLevel[i] = false;
                }
                CoasterData.Add(CurrentLevel);
            }

            return CoasterData;
        }

        private static string MainCoasterDataToString(List<bool[]> data)
        {
            string DataString = "";
            for (int h = 0; h < data.Count; h++)
            {
                string CoastersInLevel = "";
                for (int i = 0; i < data[h].Length; i++)
                {
                    if (data[h][i])
                        CoastersInLevel += "t";
                    else
                        CoastersInLevel += "f";
                    if (i != data[h].Length - 1)
                        CoastersInLevel += "-";
                }
                DataString += CoastersInLevel;
                if (h != data.Count - 1)
                    DataString += "/";
            }
            return DataString;
        }

        public static void SaveGame()
        {
            XmlDocument doc = LoadDocument(SaveDirectory + "SavedData.xml");
			XmlNode node = doc.FirstChild.NextSibling.FirstChild;
            node.InnerText = SaveData.LevelsCompleted.ToString(); 
            node.NextSibling.InnerText = MainCoasterDataToString(SaveData.MainCoastersCollected);

            doc.Save(SaveDirectory + "SavedData.xml");
        }
    }
}

