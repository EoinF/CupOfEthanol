namespace LackingPlatforms
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    internal class LevelSaver
    {
		// TODO: Replace usage of '-' as a separator so negative positions can be saved in a level.

		public static string CustomLevelsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/CupOfEthanol/Levels/Custom/";
        public static XmlDocument PrepareNewLevelDocument(string errormsg, string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlTextWriter writer = new XmlTextWriter(filename, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            writer.WriteStartElement("Main");
            writer.Close();
            doc.Load(filename);
            doc.DocumentElement.InnerXml = "<Settings><BackgroundTexture>SkyA</BackgroundTexture><Gravity>1</Gravity><AirResistance>0.1</AirResistance></Settings><Entities></Entities><Static>28#21-f~14#12-a~14#15-f~15#11-a~15#15-b~15#16-f~16#6-f~16#10-A~16#13-a~16#15-b~16#16-f~17#10-a~17#15-b~17#16-f~18#10-a~18#15-b~18#16-f~19#10-a~19#15-b~19#16-f~20#10-a~20#15-b~20#16-f~21#6-f~21#10-a~21#15-b~21#16-f~22#11-a~22#15-b~22#16-f~23#12-a~23#15-" + errormsg + "~22#14</Static>";
            doc.Save(filename);
            doc.Load(filename);
            return doc;
        }

        private static XmlElement Save_Collectables(XmlElement child)
        {
            foreach (Collectable col in Collectable.collectableList)
            {
                string text = "";
                if (col.Type == 2)
                {
                    text = col.Text;
                }
                else
                {
					break;
                }
                string var3 = child.InnerText;
                string[] stringlist = new string[8];
                stringlist[0] = var3;
                stringlist[1] = "{";
                stringlist[2] = text;
                stringlist[3] = "~";
                stringlist[4] = ((int)((col.Position.X + 12.5f) / 25f)).ToString();
                stringlist[5] = "#";
                stringlist[6] = ((int)((col.Position.Y + 12.5f) / 25f)).ToString();
                stringlist[7] = "-";
                child.InnerText = string.Concat(stringlist);
            }
            return child;
        }

        private static XmlDocument Save_Entities(XmlDocument doc)
        {
            XmlNode root = doc.DocumentElement;
            XmlElement child = doc.CreateElement("Entities");
			List<Entity> nonNullEntities = Entity.EntityList.FindAll(entity => entity != null);

			for (int i = 0; i < nonNullEntities.Count; i++)
            {
                if (nonNullEntities[i] != null)
                {
                    string dir = "t";
                    if (nonNullEntities[i].sqobject.Flipeffect == SpriteEffects.None)
                    {
                        dir = "f";
                    }
                    else
                    {
                        dir = "t";
                    }

                    string text = child.InnerText;
                    text +=
						nonNullEntities[i].Job + "~"
                        + nonNullEntities[i].sqobject.Position.X.ToString()
                        + "#"
                        + nonNullEntities[i].sqobject.Position.Y.ToString()
                        + "~"
                        + dir
                        + "~"
                        + nonNullEntities[i].StartCheckpoint
                        + "~" + nonNullEntities[i].EndCheckpoint;

                    if (nonNullEntities[i].StartDelay > 0)
                        text += "~" + nonNullEntities[i].StartDelay;

                    if (i != nonNullEntities.Count - 1)
                        text += "-";
                    child.InnerText = text;
                }
            }
                root.AppendChild(child);
            
            return doc;
        }

        private static XmlElement Save_MapSize(XmlElement child)
        {
            SquareObject[,] sq = SquareObject.sqObjectArray;
            int x = 0;
            int y = 0;
            for (int i = 0; i < sq.GetLength(0); i++)
            {
                for (int j = 0; j < sq.GetLength(1); j++)
                {
                    if (sq[i, j] != null)
                    {
                        if (i > x)
                        {
                            x = i;
                        }
                        if (j > y)
                        {
                            y = j;
                        }
                    }
                }
            }
            string text = child.InnerText;
            string[] textlist = new string[] { text, (x + 5).ToString(), "#", (y + 5).ToString(), "-" };
            child.InnerText = string.Concat(textlist);
            return child;
        }

        private static XmlDocument Save_Objects(XmlDocument doc)
        {
            doc = Save_Settings(doc);
            doc = Save_Entities(doc);
            return doc;
        }

        private static XmlDocument Save_Settings(XmlDocument doc)
        {
            XmlNode root = doc.DocumentElement;
            root.InnerXml = "";
            XmlElement child = doc.CreateElement("Settings");
            root.AppendChild(child);
            root = root.FirstChild;

            child = doc.CreateElement("BackgroundTexture");
            XmlText text = doc.CreateTextNode(Level._backgroundTexture);
            root.AppendChild(child);
            child.AppendChild(text);

            child = doc.CreateElement("Gravity");
            text = doc.CreateTextNode(Level.Gravity.Y.ToString());
            root.AppendChild(child);
            child.AppendChild(text);

			child = doc.CreateElement("AirResistance");
			text = doc.CreateTextNode(Level.AirResistance.ToString());
			root.AppendChild(child);
			child.AppendChild(text);

			child = doc.CreateElement("SongName");
			text = doc.CreateTextNode(Level.SongName.ToString());
			root.AppendChild(child);
			child.AppendChild(text);

			return doc;
        }

        private static XmlElement Save_SqObjects(XmlElement child)
        {
            SquareObject[,] sq = SquareObject.sqObjectArray;
            for (int x = 0; x < sq.GetLength(0); x++)
            {
                for (int y = 0; y < sq.GetLength(1); y++)
                {
                    if (sq[x, y] != null)
                    {
                        string text = child.InnerText;
                        if (sq[x, y].texturename == "Checkpoint")
                            child.InnerText = text + sq[x, y].texturename + sq[x,y].frictionforce + "~" + x.ToString() + "#" + y.ToString() + "-";
                        else
                            child.InnerText = text + sq[x, y].texturename + "~" + x.ToString() + "#" + y.ToString() + "-";
                    }
                }
            }
            return child;
        }

        private static XmlDocument Save_StaticObjects(XmlDocument doc)
        {
            XmlElement root = doc.DocumentElement;
            XmlElement child = Save_Collectables(Save_SqObjects(Save_MapSize(doc.CreateElement("Static"))));
            root.AppendChild(child);
            return doc;
        }

        public static void SaveMap()
        {
			SteamIntegration.Achievements.LevelCreated();
			Save_StaticObjects(Save_Objects(SaveFile.LoadDocument(CustomLevelsPath + Level.Current.ToString() + "/LevelData.xml"))).Save(CustomLevelsPath + Level.Current.ToString() + "/LevelData.xml");
            MessageBox.StatusMessage = new MessageBox("Save Successful!", new Vector2(217, 190), 120);
        }
    }
}

