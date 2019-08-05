namespace LackingPlatforms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Graphics;

    public static class Plus_Minus
    {
        private static string[] Blocks = new string[] { "Grass", "Soil", "IceGrass", "Ice", "Lava", "Magma", "Sand", 
            "Metal", "Gravel", "SpikesUP", "SpikesDOWN", "SpikesLEFT", "SpikesRIGHT", "Bouncer" };

        private static string[] Entities = new string[] { 
            "Dog", "Bird", "Flamer", "Troller", "Wolf", "Stealth", "Tentacle", "Platform", "ConcretePlatform", "Cannon", 
            "Wheelbot", "Robot", "Shadow", "Shadowplayer", "Clancy", "VanishBlock", "RainingEmber", "Icicle", "Maureen",
            "GuinnessVan", "Blaster", "Door"
         };
        private static string[] Items = new string[] { "Player", "Coaster", "Checkpoint", "Sign", "Chalice", "RedKey", "BlueKey", "GreenKey", "YellowKey" };


        public static void Minus(int i)
        {
            int k;
            if (i == 1)
            {
                if (ScreenManager.Paused)
                {
                    if (Editor.Mouse_Entity)
                    {
                        PreviousTexture();
                    }
                    if (Editor.Mouse_Static)
                    {
                        PreviousStaticTexture();
                    }
                    if (Editor.Mouse_Items)
                    {
                        PreviousItem();
                    }
                }
                else
                {
                    k = Convert.ToInt16(Editor.LabelList[0].Text);
                    if (k > 1)
                        k--;
                    Editor.LabelList[0].Text = k.ToString();
                    Entity.EntityList[Editor.SelectedEntity].Job = Entity.EntityList[Editor.SelectedEntity].Job.Substring(0, 1) + k.ToString();
                }
            }
            if (i == 3)
            {
                if (ScreenManager.Paused)
                {
                    if (EditorPauseMenu.LabelList[1].Text != "")
                    {
                        k = Convert.ToInt16(EditorPauseMenu.LabelList[1].Text);
                        if (k > 1)
                        {
                            k--;
                        }
                        EditorPauseMenu.LabelList[1].Text = k.ToString();
                    }
                }
                else
                {
                    Editor.LabelList[1].Text = "Down/Right";
                    Entity.EntityList[Editor.SelectedEntity].sqobject.Flipeffect = SpriteEffects.None;
                }
            }
            if (i == 5)
            {
                if (ScreenManager.Paused)
                {
                    if (Editor.Mouse_Entity)
                    {
                        EditorPauseMenu.LabelList[2].Text = "f";
                    }
                }
                else
                {
                    k = Convert.ToByte(Editor.LabelList[2].Text);

                    for (bool flag = false; !flag; k--)
                    {
                        if (k < 1)
                            k = 255;
                        for (int c = 0; c < Checkpoint.checkpointList.Count; c++)
                        {
                            if (Checkpoint.checkpointList[c].ID == k - 1)
                            {
                                PPlayer.Player.sqobject.Position = Checkpoint.checkpointList[c].collectable.Position;
                                flag = true;
                            }
                        }
                    }

                    Editor.LabelList[2].Text = k.ToString();
                    Entity.EntityList[Editor.SelectedEntity].StartCheckpoint = (byte)k;

                }
            }
            if (i == 7)
            {
                if (ScreenManager.Paused)
                {
                    if (Editor.Mouse_Static || Editor.Mouse_Items)
                    {
                        k = Convert.ToInt16(EditorPauseMenu.LabelList[3].Text) - 1;
                        if (k < 1)
                        {
                            k = 1;
                        }
                        EditorPauseMenu.LabelList[3].Text = k.ToString();
                    }
                }
                else
                {
                    k = Convert.ToByte(Editor.LabelList[3].Text);

                    for (bool flag = false; !flag; k--)
                    {
                        if (k < 1)
                            k = 255;
                        for (int c = 0; c < Checkpoint.checkpointList.Count; c++)
                        {
                            if (Checkpoint.checkpointList[c].ID == k - 1)
                            {
                                PPlayer.Player.sqobject.Position = Checkpoint.checkpointList[c].collectable.Position;
                                flag = true;
                            }
                        }
                    }
                    Editor.LabelList[3].Text = k.ToString();
                    Entity.EntityList[Editor.SelectedEntity].EndCheckpoint = (byte)k;
                }
            }
            if (i == 9)
            {
                if (ScreenManager.Paused)
                {
                    try
                    {
                        string text = EditorPauseMenu.LabelList[4].Text.Substring(3);
                        //string text = text;
                        if (text != null)
                        {
                            if (text == "B")
                                text = "A";

                            if (text == "C")
                                text = "B";

                            if (text == "D")
                                text = "C";
                        }
                        EditorPauseMenu.LabelList[4].Text = "Sky" + text;
                        Level._backgroundTexture = "Sky" + text;
                    }
                    catch
                    {
                        ErrorReporter.LogException(new string[] { "Error with background texture variable name" });
                        throw new Exception("Error with background texture variable name");
                    }
                }
                else
                {
                    k = int.Parse(Editor.LabelList[4].Text);
                    k -= 10;
                    if (k < 0)
                        k = 0;
                    Editor.LabelList[4].Text = k.ToString();
                    Entity.EntityList[Editor.SelectedEntity].StartDelay = k;
                }
            }

            if (i == 11)
            {
                k = int.Parse(EditorPauseMenu.LabelList[5].Text);

                for (bool flag = false; !flag; k--)
                {
                    if (k < 1)
                        k = 255;
                    for (int c = 0; c < Checkpoint.checkpointList.Count; c++)
                    {
                        if (Checkpoint.checkpointList[c].ID == k - 1)
                        {
                            PPlayer.Player.sqobject.Position = Checkpoint.checkpointList[c].collectable.Position;
                            flag = true;
                        }
                    }
                }

                EditorPauseMenu.LabelList[5].Text = k.ToString();

            }
            if (Editor.Mouse_Entity)
                Editor.Update_Entity();

            if (Editor.Mouse_Static)
                Editor.Update_Static();
            
            if (Editor.Mouse_Items)
                Editor.Update_Items();
        }

        private static void NextItem()
        {
            for (int i = 0; i < (Items.Length - 1); i++)
            {
                if (EditorPauseMenu.LabelList[0].Text == Items[i])
                {
                    EditorPauseMenu.LabelList[0].Text = Items[i + 1];
                    break;
                }
            }
        }

        private static void NextStaticTexture()
        {
            for (int i = 0; i < (Blocks.Length - 1); i++)
            {
                if (EditorPauseMenu.LabelList[0].Text == Blocks[i])
                {
                    EditorPauseMenu.LabelList[0].Text = Blocks[i + 1];
                    break;
                }
            }
        }

        private static void NextTexture()
        {
            for (int i = 0; i < (Entities.Length - 1); i++)
            {
                if (EditorPauseMenu.LabelList[0].Text == Entities[i])
                {
                    EditorPauseMenu.LabelList[0].Text = Entities[i + 1];
                    break;
                }
            }
        }

        public static void Plus(int i)
        {
            int k;
            if (i == 0)
            {
                if (ScreenManager.Paused)
                {
                    if (Editor.Mouse_Entity)
                    {
                        NextTexture();
                    }
                    if (Editor.Mouse_Static)
                    {
                        NextStaticTexture();
                    }
                    if (Editor.Mouse_Items)
                    {
                        NextItem();
                    }
                }
                else
                {
                    k = Convert.ToInt16(Editor.LabelList[0].Text);

                    if (Editor.LabelList[1].Text != "")
                    {
                        k = Convert.ToInt16(Editor.LabelList[0].Text);
                        if (Entity.EntityList[Editor.SelectedEntity].Job.StartsWith("M")
                            || Entity.EntityList[Editor.SelectedEntity].Job.StartsWith("N")
                            || Entity.EntityList[Editor.SelectedEntity].Job.StartsWith("O")
                            || Entity.EntityList[Editor.SelectedEntity].Job.StartsWith("W")
                            || Entity.EntityList[Editor.SelectedEntity].Job.StartsWith("T"))
                        {
                            if (k < 9)
                            {
                                k++;
                            }
                        }
                        else
                            if (k < 4)
                            {
                                k++;
                            }
                        Editor.LabelList[0].Text = k.ToString();
                    }

                    Editor.LabelList[0].Text = k.ToString();
                    Entity.EntityList[Editor.SelectedEntity].Job = Entity.EntityList[Editor.SelectedEntity].Job.Substring(0, 1) + k.ToString();
                }
            }
            if (i == 2)
            {
                if (ScreenManager.Paused)
                {
                    if (EditorPauseMenu.LabelList[1].Text != "")
                    {
                        k = Convert.ToInt16(EditorPauseMenu.LabelList[1].Text);
                        if (EditorPauseMenu.LabelList[0].Text == "Cannon"
                            || EditorPauseMenu.LabelList[0].Text == "Robot"
                            || EditorPauseMenu.LabelList[0].Text == "Wheelbot"
                            || EditorPauseMenu.LabelList[0].Text == "Blaster"
                            || EditorPauseMenu.LabelList[0].Text == "Icicle")
                        {
                            if (k < 9)
                            {
                                k++;
                            }
                        }
                        else
                            if (k < 4)
                            {
                                k++;
                            }
                        EditorPauseMenu.LabelList[1].Text = k.ToString();
                    }
                }
                else
                {
                    Editor.LabelList[1].Text = "Up/Left";
                    Entity.EntityList[Editor.SelectedEntity].sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                }
            }
            if (i == 4)
            {
                if (ScreenManager.Paused)
                {
                    if (Editor.Mouse_Entity)
                    {
                        EditorPauseMenu.LabelList[2].Text = "t";
                    }
                }
                else
                {
                    k = int.Parse(Editor.LabelList[2].Text);

                    if (k < 0)
                        k = 0;

                    for (bool flag = false; !flag; k++)
                    {
                        if (k > 254)
                            k = -1;
                        for (int c = 0; c < Checkpoint.checkpointList.Count; c++)
                        {
                            if (Checkpoint.checkpointList[c].ID == k + 1)
                            {
                                PPlayer.Player.sqobject.Position = Checkpoint.checkpointList[c].collectable.Position;
                                flag = true;
                            }
                        }
                    }

                    Editor.LabelList[2].Text = k.ToString();
                    Entity.EntityList[Editor.SelectedEntity].StartCheckpoint = (byte)k;
                }
            }
            if (i == 6)
            {
                if (ScreenManager.Paused)
                {
                    if (Editor.Mouse_Static || Editor.Mouse_Items)
                    {
                        k = Convert.ToInt16(EditorPauseMenu.LabelList[3].Text) + 1;
                        if (k > 10)
                        {
                            k = 10;
                        }
                        EditorPauseMenu.LabelList[3].Text = k.ToString();
                    }
                }
                else
                {
                    k = int.Parse(Editor.LabelList[3].Text);

                    if (k < 0)
                        k = 0;

                    for (bool flag = false; !flag; k++)
                    {
                        if (k > 254)
                            k = -1;
                        for (int c = 0; c < Checkpoint.checkpointList.Count; c++)
                        {
                            if (Checkpoint.checkpointList[c].ID == k + 1)
                            {
                                PPlayer.Player.sqobject.Position = Checkpoint.checkpointList[c].collectable.Position;
                                flag = true;
                            }
                        }
                    }

                    Editor.LabelList[3].Text = k.ToString();
                    Entity.EntityList[Editor.SelectedEntity].EndCheckpoint = (byte)k;
                }
            }
            if (i == 8)
            {
                if (ScreenManager.Paused)
                {
                    try
                    {
                        string text = EditorPauseMenu.LabelList[4].Text.Substring(3);
                        //string CS$4$0001 = k;
                        if (text != null)
                        {
                            if (text == "C")
                                text = "D";

                            if (text == "B")
                                text = "C";

                            if (text == "A")
                                text = "B";
                        }

                        EditorPauseMenu.LabelList[4].Text = "Sky" + text;
                        Level._backgroundTexture = "Sky" + text;
                    }
                    catch
                    {
                        ErrorReporter.LogException(new string[] { "Error with background texture variable name" });
                        throw new Exception("Error with background texture variable name");
                    }
                }
                else
                {
                    k = int.Parse(Editor.LabelList[4].Text);
                    k += 10;
                    Editor.LabelList[4].Text = k.ToString();
                    Entity.EntityList[Editor.SelectedEntity].StartDelay = k;
                }
            }

            if (i == 10)
            {
                if (Editor.Mouse_Entity)
                {
                    try
                    {
                        k = int.Parse(EditorPauseMenu.LabelList[5].Text);


                        if (k < 0)
                            k = 0;

                        for (bool flag = false; !flag; k++)
                        {
                            if (k > 254)
                                k = -1;
                            for (int c = 0; c < Checkpoint.checkpointList.Count; c++)
                            {
                                if (Checkpoint.checkpointList[c].ID == k + 1)
                                {
                                    PPlayer.Player.sqobject.Position = Checkpoint.checkpointList[c].collectable.Position;
                                    flag = true;
                                }
                            }
                        }


                        EditorPauseMenu.LabelList[5].Text = k.ToString();
                    }
                    catch { }
                }
            }

            if (Editor.Mouse_Entity)
                Editor.Update_Entity();

            if (Editor.Mouse_Static)
                Editor.Update_Static();

            if (Editor.Mouse_Items)
                Editor.Update_Items();
        }

        private static void PreviousItem()
        {
            for (int i = 1; i < Items.Length; i++)
            {
                if (EditorPauseMenu.LabelList[0].Text == Items[i])
                {
                    EditorPauseMenu.LabelList[0].Text = Items[i - 1];
                    break;
                }
            }
        }

        private static void PreviousStaticTexture()
        {
            for (int i = 1; i < Blocks.Length; i++)
            {
                if (EditorPauseMenu.LabelList[0].Text == Blocks[i])
                {
                    EditorPauseMenu.LabelList[0].Text = Blocks[i - 1];
                    break;
                }
            }
        }

        private static void PreviousTexture()
        {
            for (int i = 1; i < Entities.Length; i++)
            {
                if (EditorPauseMenu.LabelList[0].Text == Entities[i])
                {
                    EditorPauseMenu.LabelList[0].Text = Entities[i - 1];
                    break;
                }
            }
        }
    }
}

