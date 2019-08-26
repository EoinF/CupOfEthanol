namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public class Collectable : RigidObject
    {
        public static List<Collectable> collectableList;
        public string Text;
        public int Type;
        public bool RecentlyCollected = false;
        public Vector2 StartPosition;

        public Collectable(string texture, Vector2 position, float size, int type, float layer) : base(texture, position, layer, size)
        {
            this.Type = type;
            StartPosition = position;
        }

        public Collectable(string texture, Vector2 position, float size, int type, float layer, string text) : base(texture, position, layer, size)
        {
            this.Type = type;
            this.Text = text;
            StartPosition = position;
        }

        public static void CheckCollisions()
        {
            int q = -1;
            foreach (Collectable collect in collectableList)
            {
                if(collect.Type == 4)
                    q++;

                if ((collect.rect.Intersects(PPlayer.Player.MiddleRectangle) || collect.rect.Intersects(PPlayer.Player.ArmsRectangle)))
                {
                    if (collect.Type == 2)
                        MessageBox.GameMessage = new MessageBox(collect.Text);
                    if (collect.Type == 4)
                    {
						Sounds.Play("collect_coaster");
                        collect.Position = new Vector2(-990999, -9999999);
                        int collected = 0;
                        for (int i = 0; i < SaveFile.SaveData.MainCoastersCollected[Level.Current - 1].Length; i++)
                        {
                            if (!SaveFile.SaveData.MainCoastersCollected[Level.Current - 1][i])
                                collected++;
                        }

                        if (!SaveFile.SaveData.MainCoastersCollected[Level.Current - 1][q])
                        {
                            collected--;
                            SaveFile.SaveData.MainCoastersCollected[Level.Current - 1][q] = true;

                            MessageBox.GameMessage = new MessageBox("New Coaster Collected!££" + collected.ToString() + " remaining...", new Vector2(50,50), 180);
                            SaveFile.SaveGame();
                        }
                        else
                            MessageBox.GameMessage = new MessageBox("Old Coaster Re-Collected!££" + collected.ToString() + " remaining...", new Vector2(50, 50), 180);
                    }
                    if (collect.Type == 5)
                    {
                        if (!PPlayer.Player.HasRedKey)
                        {
                            collect.Position = new Vector2(-990999, -9999999);
                            PPlayer.Player.HasRedKey = true;
                            collect.RecentlyCollected = true;
							Sounds.Play("collect_key");
						}
                    }
                    if (collect.Type == 6)
                    {
                        if (!PPlayer.Player.HasBlueKey)
                        {
                            collect.Position = new Vector2(-990999, -9999999);
                            PPlayer.Player.HasBlueKey = true;
                            collect.RecentlyCollected = true;
							Sounds.Play("collect_key");
						}
                    }
                    if (collect.Type == 7)
                    {
                        if (!PPlayer.Player.HasGreenKey)
                        {
                            collect.Position = new Vector2(-990999, -9999999);
                            PPlayer.Player.HasGreenKey = true;
                            collect.RecentlyCollected = true;
							Sounds.Play("collect_key");
						}
                    }
                    if (collect.Type == 8)
                    {
                        if (!PPlayer.Player.HasYellowKey)
                        {
                            collect.Position = new Vector2(-990999, -9999999);
                            PPlayer.Player.HasYellowKey = true;
                            collect.RecentlyCollected = true;
							Sounds.Play("collect_key");
						}
                    }

                }
                
            }
            for (int i = 0; i < Level.ChaliceList.Count; i++)
            {
                if (Level.ChaliceList[i].rect.Intersects(PPlayer.Player.MiddleRectangle) || Level.ChaliceList[i].rect.Intersects(PPlayer.Player.ArmsRectangle))
                {
                    LevelLoader.LevelComplete = true;
                }
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            if (this.Type == 2)
            {
                spriteBatch.Draw(Textures.GetTexture("Sign"), base.Position + Level.Offset, null, Color.White, 0f, Vector2.Zero, 1f, 0, base.Layer);
            }
            else
            {
                base.Draw(spriteBatch);
            }
        }
    }
}

