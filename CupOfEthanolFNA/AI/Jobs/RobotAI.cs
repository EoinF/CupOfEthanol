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
        private void Robot(int FireRate)
        {
            sqobject.Velocity -= (Vector2)(Level.Gravity * 0.22f);
            VariableC++;
            if (VariableC > 1440f)
            {
                VariableC = 1f;
            }
            int rnd = rand.Next(-251, 250);
            sqobject.Velocity = new Vector2(((float)Math.Cos((double)(this.VariableC / 16f))) * 0.15f, ((float)Math.Sin((double)(this.VariableC / 16f))) * 0.6f);
            sqobject.Velocity += new Vector2((float)(rnd / 0x3e8), 0f);
            rnd = rand.Next(-251, 250);
            sqobject.Velocity += new Vector2(0f, (float)(rnd / 0x3e8));
            if (Vector2.Distance(PPlayer.Player.sqobject.Position, this.sqobject.Position) < 350f)
            {
                Vector2 Direction = PPlayer.Player.sqobject.Position - this.sqobject.Position;

                if (Direction.X < 0f)
                    this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                else
                    this.sqobject.Flipeffect = SpriteEffects.None;
                Direction.Normalize();
                this.sqobject.Velocity += (Direction * 0.4f) + new Vector2(0f, 0.01f);
            }

            rnd = rand.Next(20, 50);
            this.Cannon(FireRate);
            if ((this.VariableE > -1f) && (this.Lives != this.VariableE))
            {
                bool CanMove = true;
                if (Math.Sign(sqobject.Velocity.X) > 0)
                {
                    if (this.BlockExists_Left(0, 24, 1)
                       || this.BlockExists_Left(1, 24, 1)
                       || this.BlockExists_Left(-1, 24, 1)
                       || this.BlockExists_Left(0, 49, 1)
                       || this.BlockExists_Left(1, 49, 1)
                       || this.BlockExists_Left(-1, 49, 1))
                        CanMove = false;
                }
                else
                {
                    if (this.BlockExists_Right(0, 24, 1)
                       || this.BlockExists_Right(1, 24, 1)
                       || this.BlockExists_Right(-1, 24, 1)
                       || this.BlockExists_Right(0, 49, 1)
                       || this.BlockExists_Right(1, 49, 1)
                       || this.BlockExists_Right(-1, 49, 1))
                        CanMove = false;
                }
                if (CanMove)
                    this.sqobject.Velocity += new Vector2(-Math.Sign(sqobject.Velocity.X) * rnd, 5f);
            }
            this.VariableE = this.Lives;

            this.CheckColissions_Precise(true, true);
            this.Walking = false;
        }
    }
}