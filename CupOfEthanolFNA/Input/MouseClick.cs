namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public static class MouseClick
    {
        public static void Erase_Block(int x, int y)
        {
            if ((((((x >= 0) && (x <= (SquareObject.sqObjectArray.GetLength(0) - 1))) && (y >= 0)) && (y <= (SquareObject.sqObjectArray.GetLength(1) - 1))) && (SquareObject.sqObjectArray[x, y] != null)) && (!(SquareObject.sqObjectArray[x, y].texturename == "A")))
            {
                if (SquareObject.sqObjectArray[x, y].texturename == "Checkpoint")
                {
                   for (int i = 0; i < Checkpoint.checkpointList.Count; i++)
                    {
                        if (Checkpoint.checkpointList[i].ID == SquareObject.sqObjectArray[x,y].frictionforce)
                        {
                            Checkpoint.checkpointList.RemoveAt(i);
                            break;
                        }
                    }
                }
                SquareObject.sqObjectArray[x, y] = null;
            }
        }

        public static void Left()
        {
            if (MainMethod.popupBox != null)
            {
                if (InputManager.Mousestate[1].LeftButton == ButtonState.Released)
                {
                    LeftClick_Paused();
                }
            }
            else if (ScreenManager.Paused)
            {
                if (InputManager.Mousestate[1].LeftButton == ButtonState.Released)
                {
                    MenuNavigation.NextScreen();
                }
            }
            else
            {
                if ((ScreenManager.Editing && !ScreenManager.Levelselect) && !ScreenManager.Paused)
                {
                    bool CheckedInput = false;
                    if (Editor.MovingEntity)
                    {
                        if (Editor.LabelList != null)
                            if (InputManager.Mousestate[1].LeftButton == ButtonState.Released)
                            {
                                MenuNavigation.NextScreen();
                                CheckedInput = true;
                            }
                    }

                    if (Editor.Mouse_Static)
                    {
                        LeftClick_Static();
                    }
                    if (Editor.Mouse_Entity && (InputManager.Mousestate[1].LeftButton == ButtonState.Released))
                    {
                        LeftClick_Entity(Editor.CurrentEntity);
                    }
                    if (Editor.Mouse_Move)
                    {
                        LeftClick_Move();
                    }
                    if (Editor.Mouse_Items)
                    {
                        LeftClick_Items();
                        if ((Editor.CurrentBlock != null) && (InputManager.Mousestate[1].LeftButton == ButtonState.Released))
                        {
                            LeftClick_Static();
                        }
                    }
                    if (!Editor.MovingEntity && !CheckedInput)
                    {
                        if (Editor.LabelList != null)
                            if (InputManager.Mousestate[1].LeftButton == ButtonState.Released)
                            {
                                MenuNavigation.NextScreen();
                            }
                    }
                }
                if (ScreenManager.Mainmenu)
                {
                    MenuNavigation.NextScreen();
                }
            }
        }

        private static void LeftClick_Entity(Entity e)
        {
            if (((e.Active) && ((InputManager.Mousestate[0].X <= 800) && (InputManager.Mousestate[0].X >= 0))) && ((InputManager.Mousestate[0].Y <= 600) && (InputManager.Mousestate[0].Y >= 0)))
            {
                string direction = "f";
                if (e.sqobject.Flipeffect == SpriteEffects.FlipHorizontally)
                {
                    direction = "t";
                }
                Entity.EntityList.Add(new Entity(e.sqobject.texturename, e._jumpAnim, e._walkAnim, e.sqobject.Position, e.Lives, e.sqobject.Size, e.Job, e.sqobject.damage, e.sqobject.bounce, e.Speed, e.sqobject.frictionforce, direction, false, 0, byte.Parse(EditorPauseMenu.LabelList[5].Text), byte.Parse(EditorPauseMenu.LabelList[6].Text)));
            }
        }

        private static void LeftClick_Items()
        {
            if (InputManager.Mousestate[1].LeftButton == ButtonState.Released)
            {
                int x = (int) ((InputManager.Mousestate[0].X - Level.Offset.X) / 25f);
                int y = (int) ((InputManager.Mousestate[0].Y - Level.Offset.Y) / 25f);
                if ((((x >= 0) && (x <= (SquareObject.sqObjectArray.GetLength(0) - 1))) && (y >= 0)) && (y <= (SquareObject.sqObjectArray.GetLength(1) - 1)))
                {
                    if (Editor.CurrentCollectable != null)
                    {
                        Collectable.collectableList.Add(new Collectable(Editor.CurrentCollectable.texturename, new Vector2((float) (x * 0x19), (float) (y * 0x19)), Editor.CurrentCollectable.Size, Editor.CurrentCollectable.Type, 0.6f));
                    }
                    if (Editor.CurrentBlock != null)
                    {
                        if (Editor.CurrentBlock.texturename == "A")
                        {
                            for (int i = 0; i < SquareObject.sqObjectArray.GetLength(0); i++)
                                for (int j = 0; j < SquareObject.sqObjectArray.GetLength(1); j++)
                                {
                                    if ((SquareObject.sqObjectArray[i, j] != null) && (SquareObject.sqObjectArray[i, j].texturename == "A"))
                                    {
                                        SquareObject.sqObjectArray[i, j] = null;
                                        return;
                                    }
                                }
                        }
                        //if (Editor.CurrentBlock.texturename == "Chalice")
                        //{
                        //    for (i = 0; i < SquareObject.sqObjectArray.GetLength(0); i++)
                        //    {
                        //        for (j = 0; j < SquareObject.sqObjectArray.GetLength(1); j++)
                        //        {
                        //            if ((SquareObject.sqObjectArray[i, j] != null) && (SquareObject.sqObjectArray[i, j].texturename == "Chalice"))
                        //            {
                        //                SquareObject.sqObjectArray[i, j] = null;
                        //                return;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                }
            }
        }

        private static void LeftClick_Move()
        {
            if (InputManager.Mousestate[1].LeftButton == ButtonState.Released)
            {
                int x = (int) ((InputManager.Mousestate[0].X - Level.Offset.X) / 25f);
                int y = (int) ((InputManager.Mousestate[0].Y - Level.Offset.Y) / 25f);
                if (!Editor.Static_ClickCheck(x, y)
                    && !Editor.Collectable_ClickCheck(x, y))
                {
                    Editor.Entity_ClickCheck();
                }
            }
        }

        private static bool LeftClick_Paused()
        {
            if (MainMethod.popupBox != null)
            {
                for (int i = 0; i < MainMethod.popupBox.ButtonList.Count; i++)
                {
                    if (Rect.Intersects(MainMethod.popupBox.ButtonList[i].Rect))
                    {
                        MenuNavigation.CheckPopupBox(i);
                    }
                }
                return true;
            }
            MenuNavigation.NextScreen();
            return false;
        }

        private static void LeftClick_Static()
        {
            int x = (int) ((InputManager.Mousestate[0].X - Level.Offset.X) / 25f);
            int y = (int) ((InputManager.Mousestate[0].Y - Level.Offset.Y) / 25f);
            if (Editor.CurrentBlock.texturename != "")
            {
                for (int i = 0; i < Editor.BrushSize; i++)
                {
                    for (int j = 0; j < Editor.BrushSize; j++)
                    {
                        Editor.PlaceBlock(Editor.CurrentBlock, x + i, y + j);
                    }
                }
            }
        }

        public static void Right()
        {
            if (!ScreenManager.Paused)
            {
                if (Editor.Mouse_Static)
                {
                    RightClick_Static();
                }
                if (Editor.Mouse_Move)
                {
                    RightClick_Move();
                }
                if (Editor.Mouse_Entity)
                {
                    RightClick_Entity();
                }
                if (Editor.Mouse_Items)
                {
                    RightClick_Items();
                    RightClick_Static();
                }
            }
        }

        private static void RightClick_Entity()
        {
            for (int i = 0; i < Entity.EntityList.Count; i++)
            {
                if (Entity.EntityList[i] != null)
                    if (Rect.Intersects(new Rectangle((int)(Entity.EntityList[i].sqobject.Position.X + Level.Offset.X), (int)(Entity.EntityList[i].sqobject.Position.Y + Level.Offset.Y), (int)(Entity.EntityList[i].sqobject.Texture.Width * Entity.EntityList[i].sqobject.Size), (int)(Entity.EntityList[i].sqobject.Texture.Height * Entity.EntityList[i].sqobject.Size))))
                    {
                        Entity.EntityList[i] = null;
                        if (i == Editor.SelectedEntity)
                            Editor.SelectedEntity = -1;
                    }
            }
        }

        private static void RightClick_Items()
        {
            int x = (int) ((InputManager.Mousestate[0].X - Level.Offset.X) / 25f);
            int y = (int) ((InputManager.Mousestate[0].Y - Level.Offset.Y) / 25f);
            for (int i = 0; i < Collectable.collectableList.Count; i++)
            {
                if (Collectable.collectableList[i].rect.Intersects(new Rectangle((int)(InputManager.Mousestate[0].X - Level.Offset.X - 0.001f), (int)((InputManager.Mousestate[0].Y - Level.Offset.Y) - 0.001f), 1, 1)))
                {
                    if (i != Editor.SelectedCollectable)
                        Collectable.collectableList.RemoveAt(i);
                    break;
                }
            }
        }

        private static void RightClick_Move()
        {
            RightClick_Static();
            RightClick_Entity();
            RightClick_Items();
        }

        private static void RightClick_Static()
        {
            int x = (int) ((InputManager.Mousestate[0].X - Level.Offset.X) / 25f);
            int y = (int) ((InputManager.Mousestate[0].Y - Level.Offset.Y) / 25f);
            for (int i = 0; i < Editor.BrushSize; i++)
            {
                for (int j = 0; j < Editor.BrushSize; j++)
                {
                    Erase_Block(x + i, y + j);
                }
            }
        }

        public static Rectangle Rect
        {
            get
            {
                return new Rectangle((int)InputManager.Mousestate[0].X, (int)InputManager.Mousestate[0].Y, 1, 1);
            }
        }
    }
}

