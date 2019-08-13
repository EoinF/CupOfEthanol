namespace LackingPlatforms
{
    using Microsoft.Xna.Framework.Input;
    using System;

    public static class MenuNavigation
    {
        private static void BClick_CycleEntities()
        {
            if (Entity.EntityList.Count > 0)
            {
                Editor.EntityCycleCounter++;
                if (Editor.EntityCycleCounter > (Entity.EntityList.Count - 1))
                {
                    Editor.EntityCycleCounter = 0;
                }
                PPlayer.Player.sqobject.Position = Entity.EntityList[Editor.EntityCycleCounter].sqobject.Position;
            }
        }

        private static void BClick_Entity()
        {
            Editor.Mouse_Entity = true;
            Editor.CurrentBlock = null;
            Editor.CurrentCollectable = null;
            Editor.SelectedBlock = null;
            Editor.SelectedEntity = -1;
            Editor.SelectedCollectable = -1;
            EditorPauseMenu.LabelList[0].Text = "Dog";
            EditorPauseMenu.LabelList[1].Text = "1";
            EditorPauseMenu.LabelList[2].Text = "f";
            EditorPauseMenu.LabelList[3].Text = "";
            EditorPauseMenu.LabelList[5].Text = "0";
            EditorPauseMenu.LabelList[6].Text = "0";
            Editor.Update_Entity();
        }

        private static void BClick_Items()
        {
            Editor.Mouse_Items = true;
            Editor.CurrentBlock = null;
            Editor.CurrentEntity = null;
            Editor.SelectedBlock = null;
            Editor.SelectedEntity = -1;
            Editor.SelectedCollectable = -1;
            EditorPauseMenu.LabelList[0].Text = "Player";
            EditorPauseMenu.LabelList[1].Text = "";
            EditorPauseMenu.LabelList[2].Text = "";
            EditorPauseMenu.LabelList[3].Text = "1";
            EditorPauseMenu.LabelList[5].Text = "";
            EditorPauseMenu.LabelList[6].Text = "";
            Editor.Update_Items();
        }

        private static void BClick_MainMenu()
        {
            if ((ScreenManager.Ingame || ScreenManager.Editing) && !ScreenManager.Levelselect)
            {
                MainMethod.popupBox = new PopupBox(new string[] { "Are you sure?", "You will lose all unsaved progress!" }, true, false, false);
            }
            else
            {
                MainMenu.Activate();
            }
        }

        private static void BClick_Move()
        {
            if (ScreenManager.Paused)
            {
                Editor.Mouse_Move = true;
                Editor.CurrentBlock = null;
                Editor.CurrentCollectable = null;
                Editor.CurrentEntity = null;
                EditorPauseMenu.LabelList[0].Text = "";
                EditorPauseMenu.LabelList[1].Text = "";
                EditorPauseMenu.LabelList[2].Text = "";
                EditorPauseMenu.LabelList[3].Text = "";
                EditorPauseMenu.LabelList[5].Text = "";
                EditorPauseMenu.LabelList[6].Text = "";

            }
            else
            {
                if (!Editor.MovingEntity)
                    Editor.MovingEntity = true;
                else
                    Editor.MovingEntity = false;
            }

        }

        private static void BClick_Static()
        {
            Editor.Mouse_Static = true;
            Editor.CurrentCollectable = null;
            Editor.CurrentEntity = null;
            Editor.SelectedBlock = null;
            Editor.SelectedEntity = -1;
            Editor.SelectedCollectable = -1;
            EditorPauseMenu.LabelList[0].Text = "Grass";
            EditorPauseMenu.LabelList[1].Text = "";
            EditorPauseMenu.LabelList[2].Text = "";
            EditorPauseMenu.LabelList[3].Text = "1";
            EditorPauseMenu.LabelList[5].Text = "";
            EditorPauseMenu.LabelList[6].Text = "";
            Editor.BrushSize = 1;
            Editor.Update_Static();
        }

        public static bool CheckPopupBox(int i)
        {
            if (MainMethod.popupBox != null)
            {
                bool choice = false;
                if (MainMethod.popupBox.ButtonList[i].Text.Text == "Yes")
                {
                    choice = true;
                }
                if (MainMethod.popupBox.QuittingEditor)
                {
                    MainMethod.popupBox.QuitGame(choice);
                }
                if (MainMethod.popupBox.ErasingBlocks)
                {
                    MainMethod.popupBox.EraseBlocks(choice);
                }
                if (MainMethod.popupBox.ErasingEntities)
                {
                    MainMethod.popupBox.EraseEntities(choice);
                }
                return true;
            }
            return false;
        }

        private static bool LevelSelect_Nav()
        {
            if (ScreenManager.Levelselect && (LevelButton.lvButtonList != null))
            {
                for (int i = (LevelButton.CurrentGroup * 6); i < 6 + (LevelButton.CurrentGroup * 6); i++)
                {
                    if (MouseClick.Rect.Intersects(LevelButton.lvButtonList[i].Rect))
					{
						if (ScreenManager.Custom)
						{
							LevelLoader.StartCustomLevel(i + 1);
							return true;
						}
						if (ScreenManager.Editing)
                        {
                            LevelLoader.StartEditorLevel(i + 1);
                            return true;
                        }
                        if (LevelButton.lvButtonList[i].Status != "Locked")
                        {
                            LevelLoader.StartSelectedLevel(i + 1);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void NextScreen()
        {

            if (InputManager.Mousestate[1].LeftButton != ButtonState.Pressed)
            {
                int i;
                if (!LevelSelect_Nav())
                {
                    for (i = 0; i < Button.ButtonList.Count; i++)
                    {
                        if (MouseClick.Rect.Intersects(Button.ButtonList[i].Rect))
                        {
                            switch (Button.ButtonList[i].Text.Text)
                            {
                                case "New Game":
                                    ScreenManager.CreatingOn();
                                    return;

                                case "Continue":
                                    ScreenManager.LoadingOn();
                                    return;

                                case "Play Custom":
                                    ScreenManager.LoadingCustomOn();
                                    return;

								case "Editor":
									ScreenManager.EditingOn();
									return;

								case "Credits":
									ScreenManager.CreditsOn();
									return;

								case "Exit":
                                    ScreenManager.ExitGame();
                                    return;

                                case "Main Menu":
                                    BClick_MainMenu();
                                    return;

                                case "Resume":
                                    PauseMenu.Unpause();
                                    return;

                                case "Move":
                                    BClick_Move();
                                    return;

                                case "Entity":
                                    BClick_Entity();
                                    return;

                                case "Static":
                                    BClick_Static();
                                    return;

                                case "Items":
                                    BClick_Items();
                                    return;

								case "Save":
									LevelSaver.SaveMap();
									return;

								case "Save & Test":
									LevelSaver.SaveMap();
									LevelLoader.StartCustomLevel(Level.Current);
									ScreenManager.Testing = true;
									return;

								case "Back to Editor":
									LevelLoader.StartEditorLevel(Level.Current);
									return;

								case "+":
									Plus_Minus.Plus(i);
									return;

								case "-":
                                    Plus_Minus.Minus(i);
                                    return;

                                case "DelAllBlocks":
                                    MainMethod.popupBox = new PopupBox(new string[] { "Are you sure?", "This will delete all the blocks", "in this level permanently." }, false, true, false);
                                    return;

                                case "DelAllEntities":
                                    MainMethod.popupBox = new PopupBox(new string[] { "Are you sure?", "This will delete all the entities", "in this level permanently." }, false, false, true);
                                    return;

                                case "Cycle Entities":
                                    BClick_CycleEntities();
                                    return;

                                case "Next":
                                    LevelButton.NextGroup();
                                    return;

                                case "Previous":
                                    LevelButton.PreviousGroup();
                                    return;
                            }
                        }
                    }
                }
                if ((Button.ButtonList != null) && (Button.ButtonList.Count > 0))
                {
                    for (i = 0; (i < 3) && (i < Button.ButtonList.Count); i++)
                    {
                        if (MouseClick.Rect.Intersects(Button.ButtonList[i].Rect))
                        {
                            if (ScreenManager.Creating)
                            {
                                //TODO: Name selection for save files
                                SaveFile.Selectedfile = i;
                                SaveFile.SaveList[i] = new SaveFile(0, true, SaveFile.NewCoasterList);
                                SaveFile.SaveGame();
                                ScreenManager.NoMode();
                                ScreenManager.Mainmenu = true;
                                ScreenManager.Levelselect = true;
                                MainMenu.LevelSelectOn();
                                return;
                            }
                            if (ScreenManager.Loading)
                            {
                                SaveFile.Selectedfile = i;
                                ScreenManager.NoMode();
                                ScreenManager.Mainmenu = true;
                                ScreenManager.Levelselect = true;
                                MainMenu.LevelSelectOn();
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}

