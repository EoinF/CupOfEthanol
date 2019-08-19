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
        public bool HardMode;

        public static List<SaveFile> SaveList;
        public static int Selectedfile;

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

        public SaveFile(int levelsCompleted,bool HardMode, List<bool[]> mainCoasters)
        {
            this.LevelsCompleted = levelsCompleted;
            this.MainCoastersCollected = mainCoasters;
            this.HardMode = HardMode;
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
                if (SaveList[Selectedfile].LevelsCompleted < Level.Current)
                    SaveList[Selectedfile].LevelsCompleted++;
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

        public static string GetPercentage(int FileID)
        {
            int saves = (SaveList[FileID].LevelsCompleted * 100) / 30;
            return saves.ToString();
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

		public static void LoadSaveFiles()
		{
			string pathName = SaveDirectory + "/SavedData.xml";
			if (!File.Exists(pathName))
			{
				Directory.CreateDirectory(SaveDirectory);
				File.Copy(@"Content/SavedData.xml", pathName);
			}
			XmlDocument doc = LoadDocument(pathName);
			
			SaveList = new List<SaveFile>();
			foreach (XmlNode node in doc.FirstChild.NextSibling)
			{
				if (node.Name.Contains("File"))
				{
					SaveList.Add(new SaveFile(int.Parse(node.FirstChild.InnerText), bool.Parse(node.FirstChild.NextSibling.InnerText), GetMainCoasterData(node.FirstChild.NextSibling.NextSibling.InnerText)));
				}
			}
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


        private static XmlDocument PrepareNewSaveDocument(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlTextWriter writer = new XmlTextWriter(filename, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            writer.WriteStartElement("Main");
			SaveGame();
            writer.Close();
            doc.Load(filename);
            doc.FirstChild.NextSibling.InnerXml = 
				"<File1><Level>0</Level><Name>Anonymous</Name><MainCoasters>--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--</MainCoasters> <!-- 30 /'s   =>   30 levels--><SubCoasters></SubCoasters></File1><File2><Level>0</Level><Name>Unnamed File</Name><MainCoasters>--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--</MainCoasters> <!-- 30 /'s   =>   30 levels--><SubCoasters></SubCoasters></File2><File3><Level>0</Level><Name>WhyAmIHere</Name><MainCoasters>--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--</MainCoasters> <!-- 30 /'s   =>   30 levels--><SubCoasters></SubCoasters></File3>";
            return doc;
        }

        public static void SaveGame()
        {
            XmlDocument doc = LoadDocument(SaveDirectory + "SavedData.xml");
            int i = 0;
            foreach (XmlNode node in doc.FirstChild.NextSibling)
            {
                if (node.Name.Contains("File"))
                {
                    node.FirstChild.InnerText = SaveList[i].LevelsCompleted.ToString(); 
                    node.FirstChild.NextSibling.NextSibling.NextSibling.InnerText = SaveList[i].HardMode.ToString();
                    node.FirstChild.NextSibling.NextSibling.InnerText = MainCoasterDataToString(SaveList[i].MainCoastersCollected);
                }
                i++;
                if (i > 2)
                {
                    break;
                }
            }
            doc.Save(SaveDirectory + "SavedData.xml");
        }
    }
}

