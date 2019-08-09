namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public static class Editor
    {
        public static int BrushSize = 1;
        public static SquareObject CurrentBlock;
        public static Collectable CurrentCollectable;
        public static Entity CurrentEntity;
        public static int EntityCycleCounter = -1;
        private static bool mouse_Entity = false;
        private static bool mouse_Items = false;
        private static bool mouse_Move = false;
        private static bool mouse_Static = false;
        public static SquareObject SelectedBlock;
        public static int SelectedCheckpoint = -1;
        public static int SelectedCollectable = -1;

        public static List<TextSprite> LabelList;
        public static int SelectedEntity = -1;
        public static bool MovingEntity = false;


        public static bool Collectable_ClickCheck(int x, int y)
        {
            if (SelectedCollectable == -1)
            {
                for (int i = 0; i < Collectable.collectableList.Count; i++)
                {
                    if ((Collectable.collectableList[i].Position / 25f) == new Vector2((float) x, (float) y))
                    {
                        if (Collectable.collectableList[i].Type == 2)
                        {
                            InputBox.Active = true;
                            InputBox.text = Collectable.collectableList[i].Text + "|";
                            MessageBox.GameMessage = new MessageBox(Collectable.collectableList[i].Text + "|");
                        }
                        SelectedCollectable = i;
                        return true;
                    }
                }
                return false;
            }
            Collectable.collectableList[SelectedCollectable].Position = (Vector2) (new Vector2((float) x, (float) y) * 25f);
            if (InputBox.text != "")
            {
                Collectable.collectableList[SelectedCollectable].Text = InputBox.text.Remove(InputBox.text.Length - 1, 1);
            }
            SelectedCollectable = -1;
            InputBox.Active = false;
            InputBox.text = "|";
            return true;
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ScreenManager.Editing && !ScreenManager.Levelselect)
            {
                DrawBackGrounds(spriteBatch);
                if (PPlayer.DeathCountdown < 0)
                {
                    SquareObject.DrawSquares(SquareObject.sqObjectArray, spriteBatch, 17, 16);
                    DrawEntities(spriteBatch, gameTime);
                    DrawMisc(spriteBatch);
                    DrawCollectables(spriteBatch);
                    DrawAttachedItems(spriteBatch);
                }
            }
        }

        private static void DrawAttachedItems(SpriteBatch spriteBatch)
        {
            int x = (int) ((InputManager.Mousestate[0].X - Level.Offset.X) / 25f);
            int y = (int) ((InputManager.Mousestate[0].Y - Level.Offset.Y) / 25f);
            if ((((x > -1) && (x < SquareObject.sqObjectArray.GetLength(0))) && (y > -1)) && (y < SquareObject.sqObjectArray.GetLength(1)))
            {
                if (SelectedBlock != null && !ScreenManager.Paused)
                {
                    spriteBatch.Draw(SelectedBlock.Texture, new Vector2((float)(x * 0x19), (float)(y * 0x19)) + Level.Offset, null, Color.White, 0f, Vector2.Zero, SelectedBlock.Size, 0, 0.999f);
                }
                if ((Mouse_Items && !ScreenManager.Paused) && (CurrentCollectable != null))
                {
                    spriteBatch.Draw(CurrentCollectable.Texture, new Vector2((float) (x * 0x19), (float) (y * 0x19)) + Level.Offset, null, Color.White, 0f, Vector2.Zero, CurrentCollectable.Size, 0, 0.999f);
                }
                if (((Mouse_Static || Mouse_Items) && !ScreenManager.Paused) && ((CurrentBlock != null) && (CurrentBlock.texturename != "")))
                {
                    for (int i = 0; i < BrushSize; i++)
                    {
                        for (int j = 0; j < BrushSize; j++)
                        {
                            spriteBatch.Draw(CurrentBlock.Texture, new Vector2((float) ((x + i) * 0x19), (float) ((y + j) * 0x19)) + Level.Offset, null, Color.White, 0f, Vector2.Zero, CurrentBlock.Size, 0, 0.999f);
                        }
                    }
                }
                if ((Mouse_Static || Mouse_Move) && (SelectedCollectable != -1))
                {
                    spriteBatch.Draw(Collectable.collectableList[SelectedCollectable].Texture, new Vector2((float) (x * 0x19), (float) (y * 0x19)) + Level.Offset, null, Color.White, 0f, Vector2.Zero, 1f, 0, 0.999f);
                }
            }
        }

        private static void DrawBackGrounds(SpriteBatch spriteBatch)
        {
            if (ScreenManager.Paused)
            {
                EditorPauseMenu.Draw(spriteBatch);
            }
            Level.DrawBackground(spriteBatch);
        }

        private static void DrawCollectables(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Collectable.collectableList.Count; i++)
            {
                if (((Collectable.collectableList[i] != null)
                    && (Vector2.Distance(Collectable.collectableList[i].Position + Level.Offset, new Vector2(400f, 300f)) < 550f))
                    && (i != SelectedCollectable))
                {
                    Collectable.collectableList[i].Draw(spriteBatch);
                }
            }
        }

        private static void DrawEntities(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < Entity.EntityList.Count; i++)
            {
                if (i == SelectedEntity)
                {
                    Entity.EntityList[i].sqobject.Colour = Color.YellowGreen;
                    //Rectangle? CS$0$0001 = null;
                    if (MovingEntity)
                        spriteBatch.Draw(Entity.EntityList[i].sqobject.Texture, new Vector2(InputManager.Mousestate[0].X - (Entity.EntityList[i].sqobject.Texture.Width * Entity.EntityList[i].sqobject.Size), InputManager.Mousestate[0].Y - (Entity.EntityList[i].sqobject.Texture.Height * Entity.EntityList[i].sqobject.Size)), null, Entity.EntityList[i].sqobject.Colour, 0f, Vector2.Zero, Entity.EntityList[i].sqobject.Size, Entity.EntityList[i].sqobject.Flipeffect, 0.1f);
                    else
                        Entity.EntityList[i].Draw(spriteBatch, gameTime);

                }
                else
                {
                    if (Entity.EntityList[i] != null)
                    {
						Color colour = Color.White;
						if (Entity.EntityList[i].Job == "X1")
							colour = Color.Red;
						else if (Entity.EntityList[i].Job == "X2")
							colour = Color.Blue;
						else if (Entity.EntityList[i].Job == "X3")
							colour = Color.Green;
						else if (Entity.EntityList[i].Job == "X4")
							colour = Color.Yellow;
                        Entity.EntityList[i].sqobject.Colour = colour;
                        Entity.EntityList[i].Draw(spriteBatch, gameTime);
                    }
                }

            }
            if (Mouse_Entity && (CurrentEntity.Active))
            {
                if (ScreenManager.Paused)
                {
                    spriteBatch.Draw(CurrentEntity.sqobject.Texture, CurrentEntity.sqobject.Position, null, Color.White, 0f, Vector2.Zero, CurrentEntity.sqobject.Size, CurrentEntity.sqobject.Flipeffect, 0.999f);
                }
                else
                {
                    spriteBatch.Draw(CurrentEntity.sqobject.Texture, CurrentEntity.sqobject.Position + Level.Offset, null, Color.White, 0f, Vector2.Zero, CurrentEntity.sqobject.Size, CurrentEntity.sqobject.Flipeffect, 0.999f);
                }
            }
        }

        private static void DrawMisc(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textures.GetTexture("Cursor"), new Vector2((float) InputManager.Mousestate[0].X, (float) InputManager.Mousestate[0].Y), null, Color.White, 0f, Vector2.Zero, 1f, 0, 1f);
            if (InputBox.Active)
            {
                InputBox.Draw(spriteBatch);
            }
            if (MessageBox.GameMessage != null)
            {
                MessageBox.GameMessage.Draw(spriteBatch);
            }
            if (MessageBox.StatusMessage != null)
            {
                MessageBox.StatusMessage.Draw(spriteBatch);
            }
            if (PConsole.Active)
            {
                PConsole.Draw(spriteBatch);
            }

            if (LabelList != null)
            {
                if (SelectedEntity != -1 && !ScreenManager.Paused)
                {
                    foreach (TextSprite ts in LabelList)
                    {
                        spriteBatch.DrawString(ts.Spritefont, ts.Text, ts.Position, ts.Colour, 0f, Vector2.Zero, 1f, 0, 0.999f);
                    }

                    Button.DrawAll(spriteBatch);
                    TextSprite.DrawAll(spriteBatch);
                }
            }

        }

        public static void Entity_ClickCheck()
        {
            for (int i = 0; i < Entity.EntityList.Count; i++)
            {
                if (Entity.EntityList[i] != null)
                {
                    if (MouseClick.Rect.Intersects(new Rectangle((int)(Entity.EntityList[i].sqobject.Position.X + Level.Offset.X), (int)(Entity.EntityList[i].sqobject.Position.Y + Level.Offset.Y), (int)(Entity.EntityList[i].sqobject.Texture.Width * Entity.EntityList[i].sqobject.Size), (int)(Entity.EntityList[i].sqobject.Texture.Height * Entity.EntityList[i].sqobject.Size))))
                    {
                        SelectedEntity = i;
                        MovingEntity = false;
                        Editor.LabelList = new List<TextSprite>();
                        TextSprite.TextList = new List<TextSprite>();
                        Button.ButtonList = new List<Button>();

                        TextSprite.TextList.Add(new TextSprite("Job:", "Tiny", new Vector2(550, 20), Color.White));
                        TextSprite.TextList.Add(new TextSprite("Start Direction:", "Tiny", new Vector2(550, 60), Color.White));
                        TextSprite.TextList.Add(new TextSprite("Checkpoint born:", "Tiny", new Vector2(550, 100), Color.White));
                        TextSprite.TextList.Add(new TextSprite("Checkpoint die:", "Tiny", new Vector2(550, 140), Color.White));
                        TextSprite.TextList.Add(new TextSprite("Start Delay:", "Tiny", new Vector2(550, 180), Color.White));

                        LabelList.Add(new TextSprite(Entity.EntityList[i].Job.Substring(1), "Tiny", new Vector2(650, 20), Color.White));
                        LabelList.Add(new TextSprite(GetDirection(Entity.EntityList[i]), "Tiny", new Vector2(650, 60), Color.White));
                        LabelList.Add(new TextSprite(Entity.EntityList[i].StartCheckpoint.ToString(), "Tiny", new Vector2(650, 100), Color.White));
                        LabelList.Add(new TextSprite(Entity.EntityList[i].EndCheckpoint.ToString(), "Tiny", new Vector2(650, 140), Color.White));
                        LabelList.Add(new TextSprite(Entity.EntityList[i].StartDelay.ToString(), "Tiny", new Vector2(650, 180), Color.White));

                        TextSprite ts = new TextSprite("+", "Small", new Vector2(740, 20), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(740, 20), 5));
                        ts = new TextSprite("-", "Small", new Vector2(760, 20), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(760, 20), 5));

                        ts = new TextSprite("+", "Small", new Vector2(740, 60), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(740, 60), 5));
                        ts = new TextSprite("-", "Small", new Vector2(760, 60), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(760, 60), 5));

                        ts = new TextSprite("+", "Small", new Vector2(740, 100), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(740, 100), 5));
                        ts = new TextSprite("-", "Small", new Vector2(760, 100), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(760, 100), 5));

                        ts = new TextSprite("+", "Small", new Vector2(740, 140), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(740, 140), 5));
                        ts = new TextSprite("-", "Small", new Vector2(760, 140), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(760, 140), 5));

                        ts = new TextSprite("+", "Small", new Vector2(740, 180), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(740, 180), 5));
                        ts = new TextSprite("-", "Small", new Vector2(760, 180), Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(760, 180), 5));

                        ts = new TextSprite("Move", "Medium", Vector2.Zero, Color.Red);
                        Button.ButtonList.Add(new Button(ts, new Vector2(650, 220), 4));

                        return;
                    }
                }

            }
            if (SelectedEntity > -1 && MovingEntity)
            {
                SquareObject sq = Entity.EntityList[SelectedEntity].sqobject;
                Entity.EntityList[SelectedEntity].sqobject.Position = new Vector2(InputManager.Mousestate[0].X - (sq.Texture.Width * sq.Size), InputManager.Mousestate[0].Y - (sq.Texture.Height * sq.Size)) - Level.Offset;
                MovingEntity = false;
                return;
            }

            if (LabelList != null)
            {
                bool ButtonClicked = false;

                for (int i = 0; i < Button.ButtonList.Count; i++)
                {
                    if (Button.ButtonList[i].Rect.Intersects(MouseClick.Rect))
                        ButtonClicked = true;
                }

                if (!ButtonClicked)
                {
                    SelectedEntity = -1;
                    MovingEntity = false;
                    LabelList = null;
                    Button.ButtonList = new List<Button>();
                    TextSprite.TextList = new List<TextSprite>();
                }
            }
        }

        private static string GetDirection(Entity e)
        {
            if (e.sqobject.Flipeffect == 0)
            {
                return "Down/Right";
            }
            return "Up/Left";
        }

        public static void PlaceBlock(SquareObject sq, int x, int y)
        {
            if ((((((x >= 0) && (x <= (SquareObject.sqObjectArray.GetLength(0) - 1))) && (y >= 0)) && (y <= (SquareObject.sqObjectArray.GetLength(1) - 1))) && (!mouse_Static || (CurrentBlock != null))) && ((SquareObject.sqObjectArray[x, y] == null) || (SquareObject.sqObjectArray[x, y].texturename != "A")))
            {
                if (sq.texturename == "Checkpoint" &&
					(SquareObject.sqObjectArray[x, y] == null ||
					SquareObject.sqObjectArray[x, y].texturename != "Checkpoint"))
                {
                    if (Checkpoint.checkpointList.Count == 0)
                    {
                        Checkpoint.checkpointList.Add(new Checkpoint(new Collectable("Checkpoint", new Vector2(x * 0x19, y * 0x19), 0, 1, 0), 1));
                        SquareObject.sqObjectArray[x, y] = new SquareObject("Checkpoint", new Vector2(x * 0x19, y * 0x19), 0.039f, 0.5f, 1);
                        return;
                    }
                    else
                    {
                        for (byte id = 1; id != 255; id++)
                        {
                            bool flag = false;
                            foreach (Checkpoint check in Checkpoint.checkpointList)
                            {
                                if (id == check.ID)
                                {
                                    flag = true;
                                }
                            }
                            if (!flag)
                            {
                                Checkpoint.checkpointList.Add(new Checkpoint(new Collectable("Checkpoint", new Vector2(x * 0x19, y * 0x19), 0, 1, 0), id));
                                SquareObject.sqObjectArray[x, y] = new SquareObject("Checkpoint", new Vector2(x * 0x19, y * 0x19), 0.039f, 0.5f, id);
                                return;
                            }
                        }
                    }
                }
                SquareObject.sqObjectArray[x, y] = null;
                if (sq.texturename == "e") //Lava
                {
                    SquareObject.sqObjectArray[x, y] = new SquareObject(sq.texturename, new Vector2((float) (x * 0x19), (float) (y * 0x19)) + new Vector2(0f, 5f), 0.039f, 0.25f);
                }
                if (sq.texturename == "j") //SpikesUP
                {
                    SquareObject.sqObjectArray[x, y] = new SquareObject(sq.texturename, new Vector2((float) (x * 0x19), (float) (y * 0x19)) + new Vector2(0f, 10f), 0.039f, 0.25f);
                }
                if (sq.texturename == "l") //SpikesLEFT
                {
                    SquareObject.sqObjectArray[x, y] = new SquareObject(sq.texturename, new Vector2((float) (x * 0x19), (float) (y * 0x19)) + new Vector2(10f, 0f), 0.039f, 0.25f);
                }
                if (sq.texturename == "n") //Bouncer
                {
                    SquareObject.sqObjectArray[x, y] = new SquareObject(sq.texturename, new Vector2((float) (x * 0x19), (float) (y * 0x19)) + new Vector2(0f, 7.5f), 0.039f, 0.25f);
                }
                if (sq.texturename == "Coaster")
                {
                    int coastersPlaced = 0;
                    foreach (SquareObject square in SquareObject.sqObjectArray)
                        if (square != null)
                            if (square.texturename == "Coaster")
                                coastersPlaced++;
                    if (coastersPlaced == 3)
                    {
                        MessageBox.StatusMessage = new MessageBox("3 coasters have been placed already. You can't place more than 3 in one level.", new Vector2(50, 50), 150);
                        return;
                    }
                    else
                    {
                        coastersPlaced++;
                        MessageBox.StatusMessage = new MessageBox(coastersPlaced.ToString() + " Coasters Placed Now!", new Vector2(50, 50), 150);
                    }
                }
                if (SquareObject.sqObjectArray[x, y] == null)
                {
                    SquareObject.sqObjectArray[x, y] = new SquareObject(sq.texturename, new Vector2((float) (x * 0x19), (float) (y * 0x19)), 0.4f, sq.Size);
                }

            }
        }

        public static bool Static_ClickCheck(int x, int y)
        {
            if (((((x >= 0) && (x <= SquareObject.sqObjectArray.GetLength(0))) && (y >= 0)) && (y <= SquareObject.sqObjectArray.GetLength(1))) && ((SelectedCollectable == -1) && (SelectedEntity == -1)))
            {
                if ((SquareObject.sqObjectArray[x, y] != null) && (SelectedBlock == null))
                {
                    if (SquareObject.sqObjectArray[x, y].texturename == "Checkpoint")
                    {
                        for (int i = 0; i < Checkpoint.checkpointList.Count; i++)
                        {
                            if (Checkpoint.checkpointList[i].ID == SquareObject.sqObjectArray[x, y].frictionforce)
                            {
                                Checkpoint.checkpointList.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    SelectedBlock = SquareObject.sqObjectArray[x, y];
                    SquareObject.sqObjectArray[x, y] = null;

                    return true;
                }
                if (SelectedBlock != null)
                {
                    PlaceBlock(SelectedBlock, x, y);
                    SelectedBlock = null;
                    return true;
                }
            }
            return false;
        }

        public static void Update()
        {
            if (ScreenManager.Editing && !ScreenManager.Levelselect)
            {
                if (PPlayer.DeathCountdown == 0)
                {
                    LevelLoader.LoadEditorLevel();
                }
                if (InputBox.Active)
                {
                    InputBox.Update();
                }
                Level.Offset = new Vector2(400f - PPlayer.Player.sqobject.Position.X, 380f - PPlayer.Player.sqobject.Position.Y);
                if (!ScreenManager.Paused && (CurrentEntity != null))
                {
                    CurrentEntity.sqobject.Position = new Vector2(InputManager.Mousestate[0].X - (CurrentEntity.sqobject.Texture.Width * CurrentEntity.sqobject.Size), InputManager.Mousestate[0].Y - (CurrentEntity.sqobject.Texture.Height * CurrentEntity.sqobject.Size)) - Level.Offset;
                }
                if (MessageBox.GameMessage != null)
                {
                    MessageBox.GameMessage.Update();
                }

                if ((MessageBox.StatusMessage != null))
                {
                    MessageBox.StatusMessage.Update();
                }

            }
        }

        public static void Update_Entity()
        {
            string name = Textures.LabelTextToEntityTexture(EditorPauseMenu.LabelList[0].Text);
            CurrentEntity = new Entity(name, "", "", new Vector2(80f, 420f), 1, 1f, name + EditorPauseMenu.LabelList[1].Text, new SquareObject.Damage(0, 0, 0, 0), new SquareObject.Bounce(0, 0, 0, 0), 1f, 1, EditorPauseMenu.LabelList[2].Text, false, 0, 0, 0);
            CurrentEntity.Active = true;
        }

        public static void Update_Items()
        {
            string item = EditorPauseMenu.LabelList[0].Text;
			BrushSize = int.Parse(EditorPauseMenu.LabelList[3].Text);
			if (item != null)
            {
                if (item != "Player")
                {
                    if (item == "Chalice")
                    {
                        CurrentBlock = new SquareObject("Chalice", new Vector2(80f, 420f), 0.98f, 0.8f);
                        CurrentCollectable = null;
                    }
                    else if (item == "Checkpoint")
                    {
                        CurrentBlock = new SquareObject("Checkpoint", new Vector2(80f, 420f), 0.98f, 0.5f);
                        CurrentCollectable = null;
                    }
                    else if (item == "Coaster")
                    {
                        CurrentBlock = new SquareObject("Coaster", new Vector2(80f, 420f), 0.98f,1);
                        CurrentCollectable = null;
                    }
                    else if (item == "Sign")
                    {
                        CurrentCollectable = new Collectable("Sign", new Vector2(80f, 420f), 1f, 2, 0.91f);
                        CurrentBlock = null;
                    }
                    else if (item == "RedKey")
                    {
                        CurrentBlock = new SquareObject("RedKey", new Vector2(80f, 420f), 0.98f, 1);
                        CurrentCollectable = null;
                    }
                    else if (item == "BlueKey")
                    {
                        CurrentBlock = new SquareObject("BlueKey", new Vector2(80f, 420f), 0.98f, 1);
                        CurrentCollectable = null;
                    }
                    else if (item == "GreenKey")
                    {
                        CurrentBlock = new SquareObject("GreenKey", new Vector2(80f, 420f), 0.98f, 1);
                        CurrentCollectable = null;
                    }
                    else if (item == "YellowKey")
                    {
                        CurrentBlock = new SquareObject("YellowKey", new Vector2(80f, 420f), 0.98f, 1);
                        CurrentCollectable = null;
                    }
                }
                else
                {
                    CurrentBlock = new SquareObject("A", new Vector2(80f, 420f), 0.98f, 1f);
                    CurrentCollectable = null;
                }
            }
        }

        public static void Update_Static()
        {
            string texture = Textures.LabelTextToStaticTexture(EditorPauseMenu.LabelList[0].Text);
            BrushSize = int.Parse(EditorPauseMenu.LabelList[3].Text);
            CurrentBlock = new SquareObject(texture, new Vector2(80f, 420f), 0.98f, 0.25f);
        }

        public static void Reset()
        {
            CurrentBlock = null;
            CurrentCollectable = null;
            CurrentEntity = null;
            MovingEntity = false;
            SelectedBlock = null;
            SelectedCheckpoint = -1;
            SelectedEntity = -1;
            SelectedCollectable = -1;
            Mouse_Move = true;
            BrushSize = 1;
        }
        public static bool Mouse_Entity
        {
            get
            {
                return mouse_Entity;
            }
            set
            {
                mouse_Static = false;
                mouse_Entity = true;
                mouse_Move = false;
                mouse_Items = false;
            }
        }

        public static bool Mouse_Items
        {
            get
            {
                return mouse_Items;
            }
            set
            {
                mouse_Static = false;
                mouse_Entity = false;
                mouse_Move = false;
                mouse_Items = true;
            }
        }

        public static bool Mouse_Move
        {
            get
            {
                return mouse_Move;
            }
            set
            {
                mouse_Static = false;
                mouse_Entity = false;
                mouse_Move = true;
                mouse_Items = false;
            }
        }

        public static bool Mouse_Static
        {
            get
            {
                return mouse_Static;
            }
            set
            {
                mouse_Static = true;
                mouse_Entity = false;
                mouse_Move = false;
                mouse_Items = false;
            }
        }
    }
}

