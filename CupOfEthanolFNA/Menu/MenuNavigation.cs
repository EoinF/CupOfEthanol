namespace LackingPlatforms
{
    using Microsoft.Xna.Framework.Input;
    using System;
	using System.Windows.Forms;

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
                MainMethod.popupBox = new PopupBox(new string[] { "Are you sure?", "You will lose all unsaved progress!" }, PopupType.QUIT);
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
                if (MainMethod.popupBox.type == PopupType.QUIT)
                {
                    MainMethod.popupBox.QuitGame(choice);
                }
				if (MainMethod.popupBox.type == PopupType.DELETE_LEVEL)
				{
					MainMethod.popupBox.DeleteLevel(choice);
				}
                if (MainMethod.popupBox.type == PopupType.ERASE_BLOCKS)
                {
                    MainMethod.popupBox.EraseBlocks(choice);
				}
				if (MainMethod.popupBox.type == PopupType.ERASE_ENTITIES)
				{
					MainMethod.popupBox.EraseEntities(choice);
				}
				if (MainMethod.popupBox.type == PopupType.START_NEW_GAME)
				{
					MainMethod.popupBox.NewGame(choice);
				}
				if (MainMethod.popupBox.type == PopupType.OVERWRITE_LEVEL)
				{
					MainMethod.popupBox.OverwriteLevel(choice);
				}
				return true;
            }
            return false;
        }

        private static bool LevelSelect_Nav()
        {
            if (ScreenManager.Levelselect && (LevelButton.lvButtonList != null))
            {
                for (int i = (LevelButton.CurrentGroup * 6); i < 6 + (LevelButton.CurrentGroup * 6) && i < LevelButton.lvButtonList.Count; i++)
                {
                    if (MouseClick.Rect.Intersects(LevelButton.lvButtonList[i].Rect))
					{
						if (ScreenManager.Custom)
						{
							LevelLoader.StartCustomLevel(i, LevelButton.lvButtonList[i] as CustomLevelButton);
							return true;
						}
						if (ScreenManager.Editing)
                        {
							LevelLoader.StartEditorLevel(i, LevelButton.lvButtonList[i] as CustomLevelButton);
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

            if (InputManager.Mousestate[1].LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                int i;
                if (!LevelSelect_Nav())
                {
                    for (i = 0; i < Button.ButtonList.Count; i++)
                    {
                        if (Button.ButtonList[i].Active && MouseClick.Rect.Intersects(Button.ButtonList[i].Rect))
                        {
                            switch (Button.ButtonList[i].Text.Text)
                            {
                                case "New Game":
									ScreenManager.NewGame();
									return;

                                case "Continue":
									ScreenManager.NoMode();
									ScreenManager.Mainmenu = true;
									ScreenManager.Levelselect = true;
									MainMenu.LevelSelectOn();
									return;

								case "Stats":
									ScreenManager.StatsOn();
									return;

								case "Erase Save Data":
									MainMethod.popupBox = new PopupBox(new string[] {
										"Really delete all saved data?",
										"This will bring you back to level 1",
										"All of your custom levels will NOT be deleted"
									}, PopupType.START_NEW_GAME);
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
									foreach (CustomLevelButton customLevelButton in LevelButton.lvButtonList)
									{
										if (customLevelButton != Level.CurrentLevelButton // Don't compare the button with itself
											&& customLevelButton.Name == TextInput.TextInputList[0].Text) // If the level name already exists
										{
											MainMethod.popupBox = new PopupBox(new string[] {
												"This filename already exists",
												"This will overwrite the existing level",
												"Really overwrite the old data?"
											}, PopupType.OVERWRITE_LEVEL);
											return;
										}
									}
									LevelSaver.SaveMap();
									return;

								case "Save & Test":
									if (TextInput.TextInputList[0].Text.Length == 0)
									{
										MessageBox.StatusMessage = new MessageBox("Level must have a name!", new Microsoft.Xna.Framework.Vector2(217, 190), 120);
										return;
									}
									foreach (CustomLevelButton customLevelButton in LevelButton.lvButtonList)
									{
										if (customLevelButton != Level.CurrentLevelButton // Don't compare the button with itself
											&& customLevelButton.Name == TextInput.TextInputList[0].Text) // If the level name already exists
										{
											MainMethod.popupBox = new PopupBox(new string[] {
												"This filename already exists",
												"This will overwrite the existing level",
												"Really overwrite the old data?"
											}, PopupType.OVERWRITE_LEVEL);
											return;
										}
									}
									LevelSaver.SaveMap();
									SaveFile.LoadSaveFiles();
									LevelLoader.StartCustomLevel(Level.Current - 1, Level.CurrentLevelButton);
									ScreenManager.Testing = true;
									return;

								case "Back to Editor":
									LevelLoader.StartEditorLevel(Level.Current - 1, Level.CurrentLevelButton);
									return;

								case "+":
									Plus_Minus.Plus(i);
									return;

								case "-":
                                    Plus_Minus.Minus(i);
                                    return;

								case "Delete Map":
									MainMethod.popupBox = new PopupBox(new string[] { "Are you sure?", "This map can not be restored", "once deleted." }, PopupType.DELETE_LEVEL);
									return;

								case "DelAllBlocks":
                                    MainMethod.popupBox = new PopupBox(new string[] { "Are you sure?", "This will delete all the blocks", "in this level permanently." }, PopupType.ERASE_BLOCKS);
                                    return;

                                case "DelAllEntities":
                                    MainMethod.popupBox = new PopupBox(new string[] { "Are you sure?", "This will delete all the entities", "in this level permanently." }, PopupType.ERASE_ENTITIES);
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
            }
        }
    }
}

