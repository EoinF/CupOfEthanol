namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public static class EditorPauseMenu
    {
        private static List<TextSprite> _labelList;

        public static List<TextSprite> LabelList
        {
            get
            {
                return _labelList;
            }
            set
            {
                _labelList = value;
            }
        }

        public static void Load()
        {
            Load_Labels();
            Load_Buttons();
            Load_TextSprites();
			JobDescription.Init();

			if (Editor.CurrentEntity != null)
            {
                Editor.CurrentEntity.sqobject.Position = new Vector2(80f, 420f);
            }
        }

        private static void Load_Buttons()
        {
            Button.ButtonList = new List<Button>();
			TextInput.TextInputList = new List<TextInput>();
            Button.ButtonList.Add(new Button(new TextSprite("+", "Small", new Vector2(50f, 560f), Color.Red), new Vector2(50f, 560f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("-", "Small", new Vector2(70f, 560f), Color.Red), new Vector2(70f, 560f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("+", "Small", new Vector2(200f, 500f), Color.Red), new Vector2(200f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("-", "Small", new Vector2(220f, 500f), Color.Red), new Vector2(220f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("+", "Small", new Vector2(260f, 500f), Color.Red), new Vector2(260f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("-", "Small", new Vector2(280f, 500f), Color.Red), new Vector2(280f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("+", "Small", new Vector2(320f, 500f), Color.Red), new Vector2(320f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("-", "Small", new Vector2(340f, 500f), Color.Red), new Vector2(340f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("+", "Small", new Vector2(440f, 500f), Color.Red), new Vector2(440f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("-", "Small", new Vector2(460f, 500f), Color.Red), new Vector2(460f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("+", "Small", new Vector2(530f, 500f), Color.Red), new Vector2(530f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("-", "Small", new Vector2(550f, 500f), Color.Red), new Vector2(550f, 500f), 5));
            Button.ButtonList.Add(new Button(new TextSprite("+", "Small", new Vector2(530f, 500f), Color.Red), new Vector2(620f, 500f), 5));
			Button.ButtonList.Add(new Button(new TextSprite("-", "Small", new Vector2(550f, 500f), Color.Red), new Vector2(640f, 500f), 5));

			Button.ButtonList.Add(new Button(new TextSprite("Move", "Small", new Vector2(60f, 50f), Color.Red), new Vector2(60f, 50f), 4));
            Button.ButtonList.Add(new Button(new TextSprite("Entity", "Small", new Vector2(60f, 120f), Color.Red), new Vector2(60f, 120f), 4));
            Button.ButtonList.Add(new Button(new TextSprite("Static", "Small", new Vector2(60f, 190f), Color.Red), new Vector2(60f, 190f), 4));
            Button.ButtonList.Add(new Button(new TextSprite("Items", "Small", new Vector2(60f, 260f), Color.Red), new Vector2(60f, 260f), 4));

            Button.ButtonList.Add(new Button(new TextSprite("Main Menu", "Small", new Vector2(180f, 75f), Color.Red), new Vector2(180f, 100f), 3));
			Button.ButtonList.Add(new Button(new TextSprite("Save", "Small", new Vector2(225f, 150f), Color.Red), new Vector2(225f, 160f), 4));
			Button.ButtonList.Add(new Button(new TextSprite("Save & Test", "Small", new Vector2(180f, 225f), Color.Red), new Vector2(180f, 220f), 3));

			Button.ButtonList.Add(new Button(new TextSprite("Publish to Workshop", "Small", new Vector2(420f, 75f), Color.Red), new Vector2(420f, 100f), 3));
            Button.ButtonList.Add(new Button(new TextSprite("Delete All Objects", "Small", new Vector2(420f, 150f), Color.Red), new Vector2(420f, 160f), 3));
            Button.ButtonList.Add(new Button(new TextSprite("Cycle Entities", "Small", new Vector2(430f, 240f), Color.Red), new Vector2(420f, 220f), 3));
			Button.ButtonList.Add(new Button(new TextSprite("Delete Map", "Small", new Vector2(420f, 20f), Color.Red), new Vector2(420f, 45f), 3));

			TextInput.TextInputList.Add(new TextInput("Level Name",
				new Vector2(180f, 30f),
				new TextSprite(Level.CurrentLevelButton.Name + "|", "Small", new Vector2(195f, 55f), Color.White),
				new Vector2(180f, 45f)));
		}

        private static void Load_Labels()
        {
            LabelList = new List<TextSprite>();
            string tex = "";
            if (Editor.Mouse_Static && (Editor.CurrentBlock != null))
            {
                tex = Textures.StaticTextureToLabelText(Editor.CurrentBlock.texturename);
            }
            if (Editor.Mouse_Entity)
            {
                tex = "Dog";
                if (Editor.CurrentEntity != null)
                {
                    tex = Textures.EntityTextureToLabelText(Editor.CurrentEntity.sqobject.texturename);
                }
            }
            if (Editor.Mouse_Items)
            {
                tex = "Player";
                if (Editor.CurrentCollectable != null)
                {
                    tex = Editor.CurrentCollectable.texturename;
                }
                else if ((Editor.CurrentBlock != null) && (Editor.CurrentBlock.texturename != "A"))
                {
                    tex = Editor.CurrentBlock.texturename;
                }
            }
            LabelList.Add(new TextSprite(tex, "Medium", new Vector2(90f, 525f), Color.Yellow)); //0
            tex = "";
            if (Editor.Mouse_Entity)
            {
                tex = "1";
                if (Editor.CurrentEntity != null)
                {
                    tex = Editor.CurrentEntity.Job.Substring(Editor.CurrentEntity.Job.Length - 1);
                }
            }
            LabelList.Add(new TextSprite(tex, "Medium", new Vector2(215f, 450f), Color.Yellow)); //1
            if (Editor.Mouse_Entity)
            {
                tex = "t";
                if (Editor.CurrentEntity.sqobject.Flipeffect == 0)
                {
                    tex = "f";
                }
            }
            LabelList.Add(new TextSprite(tex, "Medium", new Vector2(275f, 450f), Color.Yellow)); //2
            tex = "";
            if (Editor.Mouse_Static || Editor.Mouse_Items)
            {
                tex = Editor.BrushSize.ToString();
            }
            LabelList.Add(new TextSprite(tex, "Medium", new Vector2(335f, 450f), Color.Yellow)); //3
            tex = Level._backgroundTexture;
            LabelList.Add(new TextSprite(tex, "Medium", new Vector2(440f, 450f), Color.Yellow)); //4
            tex = "";
            if (Editor.Mouse_Entity)
            {
                tex = Editor.CurrentEntity.StartCheckpoint.ToString();
            }
            LabelList.Add(new TextSprite(tex, "Medium", new Vector2(540f, 450f), Color.Yellow)); //5
            tex = "";
            if (Editor.Mouse_Entity)
            {
                tex = Editor.CurrentEntity.StartCheckpoint.ToString();
            }
            LabelList.Add(new TextSprite(tex, "Medium", new Vector2(640f, 450f), Color.Yellow)); //6
		}

        private static void Load_TextSprites()
        {
            TextSprite.TextList = new List<TextSprite>();
            TextSprite.TextList.Add(new TextSprite("SelectedObject:", "Tiny", new Vector2(95f, 400f), Color.White));
            TextSprite.TextList.Add(new TextSprite("JobType:", "Tiny", new Vector2(200f, 420f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Direction:", "Tiny", new Vector2(260f, 420f), Color.White));
            TextSprite.TextList.Add(new TextSprite("BlockPlacement:", "Tiny", new Vector2(320f, 420f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Background:", "Tiny", new Vector2(440f, 420f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Checkpoint Born:", "Tiny", new Vector2(530f, 420f), Color.White));
            TextSprite.TextList.Add(new TextSprite("Checkpoint Die:", "Tiny", new Vector2(620f, 420f), Color.White));
        }

        public static void Pause()
        {
            if (ScreenManager.Editing)
            {
                Load();
            }
            ScreenManager.NoMode();
            ScreenManager.Paused = true;
        }

        public static void Unpause()
        {
			TextSprite.TextList = new List<TextSprite>();
			TextInput.TextInputList = new List<TextInput>();

			Button.ButtonList = new List<Button>(); ;
            if (Editor.Mouse_Move && Editor.SelectedEntity != -1)
                Reload_MoveButtonsandLabels();

            ScreenManager.NoMode();
        }

        public static void Reload_MoveButtonsandLabels()
        {
            TextSprite.TextList.Add(new TextSprite("Job: ", "Tiny", new Vector2(550, 20), Color.White));
            TextSprite.TextList.Add(new TextSprite("Start Direction: ", "Tiny", new Vector2(550, 60), Color.White));
            TextSprite.TextList.Add(new TextSprite("Checkpoint born: ", "Tiny", new Vector2(550, 100), Color.White));
            TextSprite.TextList.Add(new TextSprite("Checkpoint die:", "Tiny", new Vector2(550, 140), Color.White));
            TextSprite.TextList.Add(new TextSprite("Start Delay:", "Tiny", new Vector2(550, 180), Color.White));


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
        }



        public static void Draw(SpriteBatch spriteBatch)
        {
            DrawButtonsAndLabels(spriteBatch);
            if (Editor.CurrentBlock != null)
            {
                for (int i = 0; i < Editor.BrushSize; i++)
                {
                    for (int j = 0; j < Editor.BrushSize; j++)
                    {
                        Editor.CurrentBlock.DrawFixed(spriteBatch, new Vector2((float)(i * 0x19), (float)(j * 0x19)));
                    }
                }
            }
            if (Editor.CurrentCollectable != null)
            {
                Editor.CurrentCollectable.DrawFixed(spriteBatch);
            }
			
			JobDescription.Draw(spriteBatch, LabelList[0].Text, LabelList[1].Text);
			
			
            spriteBatch.Draw(Textures.GetTexture("EPause_Menu"), new Vector2(20f, 15f), null, new Color(1f, 1f, 1f, 0.7f), 0f, Vector2.Zero, 1f, 0, 0.9f);
        }

        private static void DrawButtonsAndLabels(SpriteBatch spriteBatch)
        {
			if (TextInput.TextInputList != null)
			{
				foreach (TextInput input in TextInput.TextInputList)
				{
					input.Draw(spriteBatch);
				}
			}
			TextSprite.DrawAll(spriteBatch);
            foreach (TextSprite ts in LabelList)
            {
                spriteBatch.DrawString(ts.Spritefont, ts.Text, ts.Position, ts.Colour, 0f, Vector2.Zero, 1f, 0, 0.999f);
            }
            Button.DrawAll(spriteBatch);
        }

    }
}

