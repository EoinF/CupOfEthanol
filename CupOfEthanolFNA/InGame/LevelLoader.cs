namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    internal class LevelLoader
    {
        public static bool LevelComplete = false;


        public static Vector2 GetCoords(string s)
        {
            string[] coords = s.Split(',');
            return new Vector2((float) int.Parse(coords[0]), (float) int.Parse(coords[1]));
        }

        private static Objectdata[] GetEntityObjectData(XmlNode node)
        {
                List<string[]> info = new List<string[]>();
                string[] singobj = node.InnerText.Split('-');

                if (singobj[singobj.Length - 1] == "")
                    Array.ConstrainedCopy(singobj, 0, singobj, 0, singobj.Length - 1);

                Objectdata[] objectlist = new Objectdata[singobj.Length];

                if (singobj.Length == 1 && singobj[0] == "")
                {
                    return null;
                }
                //try
                {
                    for (int i = 0; i < singobj.Length; i++)
                    {
                        info.Add(singobj[i].Split(new char[] { '~', '#' }));
                        if (info[i].Length == 6)
                        {
                            objectlist[i] = new Objectdata(new Vector2((float)int.Parse(info[i][1]), (float)int.Parse(info[i][2])), info[i][0], byte.Parse(info[i][4]), byte.Parse(info[i][5]), info[i][3]);
                        }
                        else
                        {
                            if (info[i].Length == 4)
                                throw new Exception();
                            if (info[i].Length == 7)
                                objectlist[i] = new Objectdata(new Vector2((float)int.Parse(info[i][1]), (float)int.Parse(info[i][2])), info[i][0], byte.Parse(info[i][4]), byte.Parse(info[i][5]), info[i][3], int.Parse(info[i][6]));
                        }
                    }
                }
                try { }
                catch (Exception e)
                {
                    ErrorReporter.LogException(new string[] { "Failed to load the objectdata list", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                    throw e;
                }
            return objectlist;
        }

        private static Objectdata[] GetStaticObjectData(XmlNode node)
        {
                List<string[]> coordsNtexture = new List<string[]>();
                string[] singobj = node.InnerText.Split(new char[] { '-' });
                coordsNtexture.Add(new string[] { "", singobj[0].Split('#')[0], singobj[0].Split('#')[1] });
                Objectdata[] objectlist = new Objectdata[singobj.Length + 1];
            try
            {
                objectlist[0] = new Objectdata(new Vector2((float) int.Parse(coordsNtexture[0][1]), (float) int.Parse(coordsNtexture[0][2])), coordsNtexture[0][0], 0, 0);
                for (int i = 1; i < singobj.Length; i++)
                {

                    coordsNtexture.Add(singobj[i].Split(new char[] { '~', '#' }));
                    if (coordsNtexture[i].Length == 3)
                    {
                        objectlist[i] = new Objectdata(new Vector2((float) int.Parse(coordsNtexture[i][1]), (float) int.Parse(coordsNtexture[i][2])), coordsNtexture[i][0], 0, 0);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed to load the objectdata list", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
            return objectlist;
        }

        public static void LoadEditorLevel()
        {
            //try
            {
                Editor.Reset();
                LoadNewLevel();
                SquareObject.sqObjectArray[(int) (PPlayer.Player.sqobject.Position.X / 25f), (int) ((PPlayer.Player.sqobject.Position.Y + 25f) / 25f)] = new SquareObject("A", PPlayer.Player.sqobject.Position + new Vector2(0f, 25f), 0.89f, 1f);
                foreach (Checkpoint ch in Checkpoint.checkpointList)
                {
                    if (ch.collectable.texturename != "")
                    {
                        SquareObject.sqObjectArray[(int) (ch.collectable.Position.X / 25f), (int) (ch.collectable.Position.Y / 25f)] = new SquareObject("Checkpoint", ch.collectable.Position, 0.82f, 0.5f, ch.ID);
                    }
                }

                c:
                foreach (Collectable col in Collectable.collectableList)
                {
                    switch (col.texturename)
                    {
                        case "Coaster":
                            SquareObject.sqObjectArray[(int)(col.Position.X / 25f), (int)(col.Position.Y / 25f)] = new SquareObject("Coaster", col.Position, 0.82f, 1);
                            Collectable.collectableList.Remove(col);
                            goto c;
                        case "RedKey":
                            SquareObject.sqObjectArray[(int)(col.Position.X / 25f), (int)(col.Position.Y / 25f)] = new SquareObject("RedKey", col.Position, 0.82f, 1);
                            Collectable.collectableList.Remove(col);
                            goto c;
                        case "BlueKey":
                            SquareObject.sqObjectArray[(int)(col.Position.X / 25f), (int)(col.Position.Y / 25f)] = new SquareObject("BlueKey", col.Position, 0.82f, 1);
                            Collectable.collectableList.Remove(col);
                            goto c;
                        case "GreenKey":
                            SquareObject.sqObjectArray[(int)(col.Position.X / 25f), (int)(col.Position.Y / 25f)] = new SquareObject("GreenKey", col.Position, 0.82f, 1);
                            Collectable.collectableList.Remove(col);
                            goto c;
                        case "YellowKey":
                            SquareObject.sqObjectArray[(int)(col.Position.X / 25f), (int)(col.Position.Y / 25f)] = new SquareObject("YellowKey", col.Position, 0.82f, 1);
                            Collectable.collectableList.Remove(col);
                            goto c;
                    }
                }
                for (int i = 0; i < Level.ChaliceList.Count; i++)
                {
                    SquareObject.sqObjectArray[(int)(Level.ChaliceList[i].Position.X / 25f), (int)(Level.ChaliceList[i].Position.Y / 25f)] = new SquareObject("Chalice", Level.ChaliceList[i].Position, 0.83f, 0.8f);
                }
                Editor.Mouse_Move = true;
                PPlayer.Player.sqobject.Velocity = new Vector2(2f, 0f);
            }
            try { }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed on LoadEditorLevel() method", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        public static void LoadEntities(XmlDocument doc)
        {
            XmlNode node = doc.DocumentElement;
            Objectdata[] datalist = new Objectdata[0];
            if (node != null)
            {
                datalist = GetEntityObjectData(node.FirstChild.NextSibling);
            }
            if (datalist != null)
            {
                //try
                {
                    for (int k = 0; k < datalist.Length; k++)
                    {
                        Objectdata od = datalist[k];
                        int x = (int) od.Position.X;
                        int y = (int) od.Position.Y;
                        switch (od.Texturename.Substring(0, od.Texturename.Length - 1))
                        {
                            case "F": //Dog
                                Entity.EntityList.Add(new Entity("F", "", "Fw", od.Position, 1, 1f, od.Texturename, new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(70, 5, 5, 10), 33, 5, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;
                            case "C": //Bird
                                Entity.EntityList.Add(new Entity("C", "", "", od.Position, 1, 1f, od.Texturename, new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(70, 5, 5, 10), 30f, 5, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "H": //Flamer
                                Entity.EntityList.Add(new Entity("H", "", "Hw", od.Position, 1, 1f, od.Texturename, new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(0, 0, 0, 0), 33f, 5, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "J": //Troller
                                Entity.EntityList.Add(new Entity("J", "", "", od.Position, 1, 1f, od.Texturename, new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(20, 10, 10, 10), 60f, 5, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "G": //Wolf
                                Entity.EntityList.Add(new Entity("G", "", "Gw", od.Position, 2, 1f, od.Texturename, new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(40, 5, 5, 10), 60f, 5, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "I": //Stealth
                                Entity.EntityList.Add(new Entity("I", "", "Iw", od.Position, 1, 1f, od.Texturename, new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(40, 5, 5, 10), 30f, 5, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "K": //Tentacle
                                Entity.EntityList.Add(new Entity("K", "", "K", od.Position, 1, 1f, od.Texturename, new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(50, 20, 20, 40), 15f, 5, od.Direction, false, Entity.MaxID, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                Entity.MaxID++;
                                break;

                            case "D": //Platform
                                Entity.EntityList.Add(new Entity("D", "", "", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(0, 5, 5, 10), 30f, 70, od.Direction, true, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "E": //ConcretePlatform
                                Entity.EntityList.Add(new Entity("E", "", "", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(0, 5, 5, 10), 50f, 70, od.Direction, true, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "M": //Cannon
                                Entity.EntityList.Add(new Entity("M", "", "", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(80, 0, 0, 30), 0f, 255, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                Entity.EntityList[Entity.EntityList.Count - 1].VariableD = 500;
                                break;

                            case "N": //Wheelbot
                                Entity.EntityList.Add(new Entity("N", "", "Nw", od.Position, 5, 1f, od.Texturename, new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(40, 20, 20, 5), 30f, 255, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "O": //Robot
                                Entity.EntityList.Add(new Entity("O", "", "", od.Position, 5, 1f, od.Texturename, new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(0x37, 50, 50, 30), 20f, 255, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "P": //Shadow
                                Entity.EntityList.Add(new Entity("P", "P", "Pw", od.Position, 5, 1f, od.Texturename, new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(0, 0, 0, 0), 40f, 255, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "B": //ShadowPlayer
                                Entity.EntityList.Add(new Entity("B", "Bj", "Bw", od.Position, 5, 1f, od.Texturename, new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(0, 0, 0, 0), 50f, 255, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "Q": //Clancy
                                Entity.EntityList.Add(new Entity("Q", "Qj", "Qw", od.Position, 10, 1f, od.Texturename, new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(0, 0, 0, 0), 50f, 255, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "R": //Vanish Block
                                Entity.EntityList.Add(new Entity("R", "", "", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(0, 0, 0, 0), 50f, 255, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "S": //Raining Ember
                                Entity.EntityList.Add(new Entity("S", "", "", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(40, 40, 40, 40), 50f, 255, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "T": //Icicle
                                Entity.EntityList.Add(new Entity("T", "", "", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(0, 1, 0, 0), new SquareObject.Bounce(30, 0, 0, 20), 50f, 255, od.Direction, true, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "U": //Maureen
                                Entity.EntityList.Add(new Entity("U", "Uj", "Uw", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(0, 0, 0, 0), 50f, 255, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "V": //GuinnessVan
                                Entity.EntityList.Add(new Entity("V", "", "Vw", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(0, 0, 0, 0), 0f, 200, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "W": //Blaster
                                Entity.EntityList.Add(new Entity("W", "", "", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(0, 0, 0, 0), 0f, 200, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, Color.White, od.StartDelay));
                                break;

                            case "X": //Door
                                {
                                    Color colour = Color.Yellow;

                                    if (od.Texturename == "X1")
                                        colour = Color.Red;
                                    if (od.Texturename == "X2")
                                        colour = Color.Blue;
                                    if (od.Texturename == "X3")
                                        colour = Color.Green;

                                    Entity.EntityList.Add(new Entity("X", "", "", od.Position, -1, 1f, od.Texturename, new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(0, 0, 0, 0), 0f, 100, od.Direction, false, 0, od.StartCheckpoint, od.EndCheckpoint, colour, od.StartDelay));

                                }
                                break;
                        }
                    }
                    doc = null;
                }
                try { }
                catch (Exception e)
                {
                    ErrorReporter.LogException(new string[] { "Failed to load entities", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                    throw e;
                }
            }
        }

        public static void LoadLevelSettings(XmlDocument doc)
        {
            try
            {
                foreach (XmlNode node in doc.FirstChild.NextSibling.FirstChild)
                {
                    string name = node.Name;
                    if (name != null)
                    {
                        if (name != "BackgroundTexture")
                        {
                            if (name == "Gravity")
                            {
                                goto Label_0068;
                            }
                            if (name == "AirResistance")
                            {
                                goto Label_0084;
                            }
                        }
                        else
                        {
                            Level._backgroundTexture = node.InnerText;
                        }
                    }
                    goto Label_0096;
                Label_0068:
                    Level.Gravity = new Vector2(0f, float.Parse(node.InnerText));
                    goto Label_0096;
                Label_0084:
                    Level.AirResistance = float.Parse(node.InnerText);
                Label_0096:;
                }
                doc = null;
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed to load level settings", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        public static void LoadNewLevel()
        {
            Sounds.PlayBGM(MainMethod.rand.Next(0, Sounds.numberOfSongs - 1));

            PPlayer.Player = new PPlayer("A", "Aw", "Aj", 1f);
            PPlayer.HadBlueKey = PPlayer.HadRedKey = PPlayer.HadYellowKey = PPlayer.HadGreenKey = false;
            
            XmlDocument doc = SaveFile.LoadDocument("Content/Levels/Main/" + Level.Current.ToString() + "/LevelData.xml");

            Entity.EntityList = new List<Entity>();


            LoadStaticObjects(doc);

            if (doc.HasChildNodes)
            {
                LoadLevelSettings(doc);
                LoadEntities(doc);
                if (!ScreenManager.Editing)
                    EntityCheckpointChecker();
            }
            else
            {
                Level.AirResistance = 0.1f;
                Level.Gravity = new Vector2(0f, 1f);
                Level._backgroundTexture = "SkyA";
            }
        }

        public static void EntityCheckpointChecker()
        {
            for (int i = 0; i < Entity.EntityList.Count; i++)
            {
                if (Entity.EntityList[i].StartCheckpoint != 0)
                {
                    Entity.EntityList[i].Active = false;
                    Entity.EntityList[i].WasActive = false;
                }
            }
        }

        public static void LoadStaticObjects(XmlDocument doc)
        {
            XmlNode node = doc.DocumentElement;
            Objectdata[] datalist = new Objectdata[0];
            if (node != null)
            {
                datalist = GetStaticObjectData(node.FirstChild.NextSibling.NextSibling);
            }
            if (datalist != null)
            {
                try
                {
                    SquareObject[,] sq = new SquareObject[((int)datalist[0].Position.X) + 1, ((int)datalist[0].Position.Y) + 1];
                    if (ScreenManager.Editing)
                    {
                        sq = new SquareObject[0x7d0, 0x7d0];
                    }
                    Collectable.collectableList = new List<Collectable>();
                    Checkpoint.checkpointList = new List<Checkpoint>();
                    Level.ChaliceList = new List<Collectable>();

                    for (int i = 1; i < (datalist.Length - 1); i++)
                    {
                        Objectdata od = datalist[i];
                        int x = (int)od.Position.X;
                        int y = (int)od.Position.Y;
                        byte q = 0;
                        if (od.Texturename != null)
                        {
                            if (od.Texturename.StartsWith("Checkpoint") && byte.TryParse(od.Texturename.Substring(10), out q))
                            {
                                Collectable coll = new Collectable("Checkpoint", od.Position * 25f, 0.5f, 3, 0.5f);
                                Checkpoint.checkpointList.Add(new Checkpoint(coll, q));
                            }
                            else
                                switch (od.Texturename)
                                {
                                    case "a": //Grass
                                        sq[x, y] = new SquareObject("a", (Vector2)(od.Position * 25f), 0.04f, 0.25f, 85);
                                        break;

                                    case "b": //Soil
                                        sq[x, y] = new SquareObject("b", (Vector2)(od.Position * 25f), 0.04f, 0.25f, 60);
                                        break;

                                    case "Mag":
                                        sq[x, y] = new SquareObject("Mag", (Vector2)(od.Position * 25f), 0.04f, 0.25f, 150);
                                        break;

                                    case "UMag":
                                        sq[x, y] = new SquareObject("UMag", (Vector2)(od.Position * 25f), 0.04f, 0.25f, 150);
                                        break;

                                    case "c": //IceGrass
                                        sq[x, y] = new SquareObject("c", (Vector2)(od.Position * 25f), 0.04f, 0.25f, 10);
                                        break;

                                    case "d": //Ice
                                        sq[x, y] = new SquareObject("d", (Vector2)(od.Position * 25f), 0.04f, 0.25f, 1);
                                        break;

                                    case "e": //Lava
                                        sq[x, y] = new SquareObject("e", ((Vector2)(od.Position * 25f)) + new Vector2(0f, 5f), new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(0, 0, 0, 0), 0.039f, 0.25f, 150, Color.White);
                                        break;

                                    case "f": //Magma
                                        sq[x, y] = new SquareObject("f", (Vector2)(od.Position * 25f), new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(50, 30, 30, 30), 0.039f, 0.25f, 150, Color.White);
                                        break;

                                    case "g": //Sand
                                        sq[x, y] = new SquareObject("g", (Vector2)(od.Position * 25f), new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(0, 0, 0, 0), 0.04f, 0.25f, 50, Color.White);
                                        break;

                                    case "h": //Metal
                                        sq[x, y] = new SquareObject("h", (Vector2)(od.Position * 25f), 0.04f, 0.25f, 60);
                                        break;

                                    case "i": //Gravel
                                        sq[x, y] = new SquareObject("i", (Vector2)(od.Position * 25f), 0.04f, 0.25f, 100);
                                        break;

                                    case "j": //SpikesUP
                                        sq[x, y] = new SquareObject("j", (od.Position * 25f) + new Vector2(0f, 10f), new SquareObject.Damage(1, 0, 0, 0), new SquareObject.Bounce(0, 0, 0, 0), 0.039f, 0.25f, 200, Color.White);
                                        break;

                                    case "k": //SpikesDOWN
                                        sq[x, y] = new SquareObject("k", od.Position * 25f, new SquareObject.Damage(0, 1, 0, 0), new SquareObject.Bounce(0, 0, 0, 0), 0.039f, 0.25f, 200, Color.White);
                                        break;

                                    case "l": //SpikesLEFT
                                        sq[x, y] = new SquareObject("l", od.Position * 25f + new Vector2(10f, 0f), new SquareObject.Damage(0, 0, 1, 0), new SquareObject.Bounce(0, 0, 0, 0), 0.039f, 0.25f, 200, Color.White);
                                        break;

                                    case "m": //SpikesRIGHT
                                        sq[x, y] = new SquareObject("m", od.Position * 25f, new SquareObject.Damage(0, 0, 0, 1), new SquareObject.Bounce(0, 0, 0, 0), 0.039f, 0.25f, 200, Color.White);
                                        break;

                                    case "n": //Bouncer
                                        sq[x, y] = new SquareObject("n", od.Position * 25f + new Vector2(0f, 7.5f), new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(100, 0, 0, 0), 0.039f, 0.25f, 80, Color.White);
                                        break;


                                    case "A":
                                        PPlayer.Player.sqobject.Position = od.Position * 25f;
                                        break;

                                    case "Coaster":
                                        {
                                            Collectable.collectableList.Add(new Collectable("Coaster", od.Position * 25f, 1, 4, 0.5f));
                                        }
                                        break;
                                    case "Chalice":
                                        {
                                            Level.ChaliceList.Add(new Collectable("Chalice", od.Position * 25f, 0.8f, 1, 0.65f));
                                            break;
                                        }
                                    case "RedKey":
                                        {
                                            Collectable.collectableList.Add(new Collectable("RedKey", od.Position * 25f, 1, 5, 0.5f));
                                        }
                                        break;
                                    case "BlueKey":
                                        {
                                            Collectable.collectableList.Add(new Collectable("BlueKey", od.Position * 25f, 1, 6, 0.5f));
                                        }
                                        break;
                                    case "GreenKey":
                                        {
                                            Collectable.collectableList.Add(new Collectable("GreenKey", od.Position * 25f, 1, 7, 0.5f));
                                        }
                                        break;
                                    case "YellowKey":
                                        {
                                            Collectable.collectableList.Add(new Collectable("YellowKey", od.Position * 25f, 1, 8, 0.5f));
                                        }
                                        break;

                                    default:
                                        Collectable.collectableList.Add(new Collectable("Sign", od.Position * 25f, 1f, 2, 0.5f, od.Texturename.TrimStart('{')));
                                        break;
                                }
                        }
                    }

                    Checkpoint.checkpointList.Add(new Checkpoint(new Collectable("", new Vector2(PPlayer.Player.sqobject.Position.X, PPlayer.Player.sqobject.Position.Y), 0f, 3, 0.5f), 0));
                    PPlayer.CurrentCheckpoint = 0;


                    PPlayer.Player.Setup();
                    SquareObject.sqObjectArray = sq;

                }

                catch (Exception e)
                {
                    ErrorReporter.LogException(new string[] { "Failed to load player and/or blocks", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                    throw e;
                }
            }
        }

        public static void NextLevel()
        {
            Level.Current++;
            Checkpoint.checkpointList = new List<Checkpoint>();
            PPlayer.CurrentCheckpoint = 0;
            LoadNewLevel();
            LevelComplete = false;
        }

        public static void RestartLevel()
        {
            try
            {
                PPlayer.Player = new PPlayer("A", "Aw", "Aj", 1f);
                //LoadEntities(SaveFile.LoadDocument("Content/Levels/Main/" + Level.Current.ToString() + "/LevelData.xml"));
                PPlayer.Player.Setup();
                for (int i = 0; i < Entity.EntityList.Count; i++)
                {
                    if (Entity.EntityList[i].sqobject.texturename == "LazerBullet")
                    { 
                        Entity.EntityList.RemoveAt(i);
                        i--;    //This line combined with the else statement prevents it from trying to access a part of the 
                                //entity list that doesnt exist.
                    }
                    else
                        Entity.EntityList[i].ResetStatus();
                }

                for (int i = 0; i < Collectable.collectableList.Count; i++)
                {
                    if (Collectable.collectableList[i].RecentlyCollected)
                        Collectable.collectableList[i].Position = Collectable.collectableList[i].StartPosition;
                }
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed to restart the level", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        public static void StartEditorLevel(int Lvl)
        {
            Level.Current = Lvl;
            ScreenManager.NoMode();
            ScreenManager.Editing = true;
            Button.ButtonList = null;
            LevelButton.lvButtonList = null;
            LoadEditorLevel();
        }

        public static void StartSelectedLevel(int Lvl)
        {
            Level.Current = Lvl;
            ScreenManager.NoMode();
            ScreenManager.Ingame = true;
            Button.ButtonList = null;
            LevelButton.lvButtonList = null;
            TextSprite.TextList = null;
            LoadNewLevel();
        }
    }
}

