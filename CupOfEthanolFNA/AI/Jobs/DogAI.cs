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
        private void Dog1()
        {
            if (OnGround)
            {
                if (this.sqobject.Flipeffect == SpriteEffects.None)
                {
                    if (!this.BlockExists_Right(1, 0, 1) && !this.BlockExists_Right(1, 25, 1))
                        this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    if (!this.BlockExists_Left(1, 0, 1) && !this.BlockExists_Left(1, 25, 1))
                        this.sqobject.Flipeffect = SpriteEffects.None;
                }
                
            }
            this.Dog3(false);
        }

        private void Dog2()
        {
            if (OnGround)
            {
                if (this.sqobject.Flipeffect == SpriteEffects.None)
                {
                    if (!this.BlockExists_Right(1, 0, 1) && !this.BlockExists_Right(1, 25, 1))
                        this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    if (!this.BlockExists_Left(1, 0, 1) && !this.BlockExists_Left(1, 25, 1))
                        this.sqobject.Flipeffect = SpriteEffects.None;
                }

            }
            this.Dog3(true);
        }

        private void Dog3(bool CollideWithEntities)
        {
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                this.sqobject.Velocity = new Vector2(1f, this.sqobject.Velocity.Y);
            }
            else
            {
                this.sqobject.Velocity = new Vector2(-1f, this.sqobject.Velocity.Y);
            }

            this.Walking = false;
            this.OnGround = false;
            this.CheckColissions_Precise(true, CollideWithEntities);
            if (this.sqobject.HitRight)
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
            if (this.sqobject.HitLeft)
                this.sqobject.Flipeffect = SpriteEffects.None;
        }

        private void Dog4()
        {
            Dog3(true);
        }


    }
}
