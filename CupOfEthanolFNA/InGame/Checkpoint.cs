namespace LackingPlatforms
{
    using System;
    using System.Collections.Generic;

    public class Checkpoint
    {
        public static List<Checkpoint> checkpointList;
        public Collectable collectable;
        public int ID;

        public Checkpoint(Collectable collect, int id)
        {
            this.collectable = collect;
            ID = id;
        }

        public static void CheckCollisions()
        {
            for (int k = 0; k < checkpointList.Count; k++)
            {
                if (checkpointList[k].collectable != null
                    && checkpointList[k].ID != PPlayer.CurrentCheckpoint
                    && checkpointList[k].collectable.Size != 0f)
                {
                    if (checkpointList[k].collectable.rect.Intersects(PPlayer.Player.MiddleRectangle)
                        || checkpointList[k].collectable.rect.Intersects(PPlayer.Player.ArmsRectangle))
                {
                    PPlayer.CurrentCheckpoint = checkpointList[k].ID;
                    PPlayer.HadRedKey = PPlayer.Player.HasRedKey;
                    PPlayer.HadBlueKey = PPlayer.Player.HasBlueKey;
                    PPlayer.HadGreenKey = PPlayer.Player.HasGreenKey;
                    PPlayer.HadYellowKey = PPlayer.Player.HasYellowKey;

                    for (int i = 0; i < Entity.EntityList.Count; i++)
                    {
                        Entity.EntityList[i].SaveStatus();
                        Entity.EntityList[i].CheckpointReached();
                    }

                    for (int i = 0; i < Collectable.collectableList.Count; i++)
                    {
                        Collectable.collectableList[i].RecentlyCollected = false;
                    }
                }
                }
            }
        }
    }
}

