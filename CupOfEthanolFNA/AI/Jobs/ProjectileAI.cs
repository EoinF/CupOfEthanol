using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LackingPlatforms
{
    public partial class Entity
    {
        private void Particle()
        {
            this.Lives--;
            //if (this.sqobject.Velocity.Y > 8f)
            //{
            //    this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, 8f);
            //}
            this.CheckColissions_Precise(true, this.Lives < 260);
        }

        private void Lazer()
        {
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                this.sqobject.Velocity = new Vector2(1, 0f);
            }
            else
            {
                this.sqobject.Velocity = new Vector2(-1, 0f);
            }
            this.Lives--;
            this.CheckColissions(this.sqobject.Velocity);
            this.CheckEntityCollisions();
            sqobject.Position += new Vector2(sqobject.Velocity.X * (this.Speed / 20f), sqobject.Velocity.Y);

            if (sqobject.HitLeft || sqobject.HitRight || sqobject.HitFeet || sqobject.HitHead)
            {
                this.DeathTimer = 0f;
            }
        }

        private void ExplLazer1()
        {
            this.sqobject.Position += new Vector2(this.Speed * 0.2f, 0f);
            this.Lives--;
            Vector2 OriginalVelocity = this.sqobject.Velocity;
            this.CheckColissions_Precise(true, this.Lives < 960);
            
            if (this.sqobject.Velocity != OriginalVelocity)
            {
                this.DeathTimer = 2f;
            }
            if (this.DeathTimer > 0f)
            {
                int random1 = rand.Next(0, 2);
                int random2 = rand.Next(0, 0x3e8) - 500;
                this.DeathTimer = -1f;
                string k = "t";
                if (random1 > 1)
                {
                    k = "f";
                }
                for (int i = 1; i < 3; i++)
                {
                    EntityList.Add(new Entity("", "", "", this.sqobject.Position, 1, 1f, "FallingEmber", new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(0x21, 0x21, 0x21, 0x21), 7f, 40, k, false, 0, 0, 0));
                    EntityList[EntityList.Count - 1].sqobject.Velocity = new Vector2((float)(random2 / 300), (float)((random2 - 500) / 0x3e8));
                }
            }
        }

        private void ExplLazer2()
        {
            this.sqobject.Position += new Vector2(this.Speed * 0.2f, 0f);
            this.Lives--;
            Vector2 OriginalVelocity = this.sqobject.Velocity;
            this.CheckColissions_Precise(true, this.Lives < 960);
            
            if (this.sqobject.Velocity != OriginalVelocity)
            {
                this.DeathTimer = 2f;
            }
            if (this.DeathTimer > 0f)
            {
                int random1 = rand.Next(0, 2);
                int random2 = rand.Next(0, 0x3e8) - 500;
                this.DeathTimer = -1f;
                string k = "t";
                if (random1 > 1)
                {
                    k = "f";
                }
                for (int i = 1; i < 5; i++)
                {
                    EntityList.Add(new Entity("", "", "", this.sqobject.Position, 1, 1f, "FallingEmber", new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(0x21, 0x21, 0x21, 0x21), 7f, 40, k, false, 0, 0, 0));
                    EntityList[EntityList.Count - 1].sqobject.Velocity = new Vector2((float)(random2 / 300), (float)((random2 - 500) / 0x3e8));
                }
            }
        }
    }
}