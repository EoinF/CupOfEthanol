namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public static class InGame
    {
        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ScreenManager.Ingame || (ScreenManager.Paused && !ScreenManager.Editing))
            {
                if (ScreenManager.Paused)
                {
                    PauseMenu.Draw(spriteBatch);
                }
                Level.DrawBackground(spriteBatch);
                if (PPlayer.DeathCountdown != 0)
                {
                    PPlayer.Player.Draw(spriteBatch, gameTime);
                    if (PPlayer.Player.IsDying())
                        SquareObject.DrawSquares(SquareObject.sqObjectArray, spriteBatch, 17, 16);
                    else
                        SquareObject.DrawSquares(SquareObject.sqObjectArray, spriteBatch, 17, 16);
                    
                    foreach (Entity entity in Entity.EntityList)
                    {
                        if (entity.Active)
                            entity.Draw(spriteBatch, gameTime);
                    }
                    if (MessageBox.GameMessage != null)
                    {
                        MessageBox.GameMessage.Draw(spriteBatch);
                    }
                    for (int w = 0; w < Checkpoint.checkpointList.Count; w++)
                    {
                        if (((Checkpoint.checkpointList[w] != null) && (Vector2.Distance(Checkpoint.checkpointList[w].collectable.Position + Level.Offset, new Vector2(400f, 300f)) < 550f)) && (Checkpoint.checkpointList[w].collectable != null))
                        {
                            if (PPlayer.CurrentCheckpoint == Checkpoint.checkpointList[w].ID)
                            {
                                Checkpoint.checkpointList[w].collectable.Draw(spriteBatch, Color.Red);
                            }
                            else
                            {
                                Checkpoint.checkpointList[w].collectable.Draw(spriteBatch);
                            }
                        }
                    }
                    foreach (Collectable Cobject in Collectable.collectableList)
                    {
                        if ((Cobject != null) && (Vector2.Distance(Cobject.Position + Level.Offset, new Vector2(400f, 300f)) < 550f))
                        {
                            Cobject.Draw(spriteBatch);
                        }
                    }
                    for (int i = 0; i < Level.ChaliceList.Count; i++)
                        Level.ChaliceList[i].Draw(spriteBatch);
                }
            }
        }

        public static void Update()
        {
            if (ScreenManager.Ingame && !ScreenManager.Paused)
            {
                if (PPlayer.DeathCountdown == 0)
                {
                    LevelLoader.RestartLevel();
                }
                for (int i = 0; i < Entity.EntityList.Count; i++)
                {
                    if (Entity.EntityList[i].Active)
                    {
                        Entity.EntityList[i].Update(SquareObject.sqObjectArray);
                    }
                }

                PPlayer.Player.Update();
                if (PPlayer.DeathCountdown < 0)
                {
                    Checkpoint.CheckCollisions();
                    Collectable.CheckCollisions();
                    Level.Offset = new Vector2(400f - PPlayer.Player.sqobject.Position.X, 380f - PPlayer.Player.sqobject.Position.Y);
                }

                if (MessageBox.GameMessage != null)
                {
                    MessageBox.GameMessage.Update();
                }
            }
        }
    }
}

