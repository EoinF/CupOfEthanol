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
        private void Platform1()
        {
            //this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, 0);
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                this.sqobject.Velocity = new Vector2(1f, 0f);
            }
            else
            {
                this.sqobject.Velocity = new Vector2(-1f, 0f);
            }
            this.CheckColissions_Precise(true, true);

            if (this.sqobject.HitLeft)
                this.sqobject.Flipeffect = SpriteEffects.None;
            if (this.sqobject.HitRight)
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
        }

        private void Platform2()
        {
			//this.sqobject.Velocity = new Vector2(0, Math.Sign(sqobject.Velocity.Y));
			if (this.sqobject.Flipeffect == SpriteEffects.FlipHorizontally)
			{
				this.sqobject.Flipeffect = SpriteEffects.FlipVertically;
			}

            if (this.sqobject.Flipeffect == SpriteEffects.FlipVertically)
            {
                this.sqobject.Velocity = new Vector2(0f, 1f);
            }
            else
            {
                this.sqobject.Velocity = new Vector2(0f, -1f);
            }

            this.CheckColissions_Precise(true, true);

            if (this.sqobject.HitHead)
                this.sqobject.Flipeffect = SpriteEffects.FlipVertically;
            if (this.sqobject.HitFeet)
                this.sqobject.Flipeffect = SpriteEffects.None;
        }

        private void Platform3()
        {
            this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, 0);
            this.CheckColissions_Precise(true, true);
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                this.sqobject.Velocity = new Vector2(1f, 0f);
            }
            else
            {
                this.sqobject.Velocity = new Vector2(-1f, 0f);
            }
            if (this.sqobject.HitRight)
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
            if (this.sqobject.HitLeft)
                this.sqobject.Flipeffect = SpriteEffects.None;
        }

        private void Platform4()
        {
            this.sqobject.Velocity = new Vector2(0, Math.Sign(sqobject.Velocity.Y));
            this.CheckColissions_Precise(true, true);
            if (this.VariableA == 180)
            {
                this.VariableA = 0;
                if (this.sqobject.Flipeffect == SpriteEffects.None)
                {
                    this.sqobject.Flipeffect = SpriteEffects.FlipVertically;
                }
                else
                {
                    this.sqobject.Flipeffect = SpriteEffects.None;
                }
            }

            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                this.sqobject.Velocity = new Vector2(0f, 1f);
            }
            else
            {
                this.sqobject.Velocity = new Vector2(0f, -1f);
            }
        }
    }
}