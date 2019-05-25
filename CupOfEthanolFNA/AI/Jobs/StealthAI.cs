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
        private void Stealth1(bool CollideWithEntities)
        {
            this.Walking = false;
            if (this.VariableD < 250)
            {
                if (this.sqobject.Flipeffect == SpriteEffects.None)
                {
                    if (this.sqobject.Velocity.X < 1f)
                    {
                        this.sqobject.Velocity = new Vector2(1f, this.sqobject.Velocity.Y);
                    }
                }
                else if (this.sqobject.Velocity.X > -1f)
                {
                    this.sqobject.Velocity = new Vector2(-1f, this.sqobject.Velocity.Y);
                }
                this.CheckColissions_Precise(true, CollideWithEntities);

                if (this.sqobject.HitRight)
                    this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                if (this.sqobject.HitLeft)
                    this.sqobject.Flipeffect = SpriteEffects.None;
                //this.sqobject.Position += new Vector2(this.sqobject.Velocity.X * (this.Speed / 20f), this.sqobject.Velocity.Y);
            }
            else if (this.VariableC < -1f)
            {
                this.VariableC = 150;
            }
            if (this.VariableC == 0)
            {
                this.VariableD = 0;
                this.sqobject.Velocity = Vector2.Zero;
            }
            if (this.VariableC > 0f)
            {
                this.sqobject.Colour = new Color(1f, 1f, 1f, this.VariableC / 150f);
            }
            else
            {
                this.sqobject.Colour = new Color(1, 1, 1, 1);
            }
            this.VariableC--;
            this.VariableD++;
        }

        private void Stealth2(bool CollideWithEntities)
        {
            this.Walking = false;
            this.Dog3(CollideWithEntities);
            if ((this.VariableD > 400f) && (this.VariableC < -1f))
            {
                this.VariableC = 200f;
            }
            else
                this.sqobject.Velocity = Vector2.Zero;

            if (this.VariableC == 0f)
            {
                this.VariableD = 180f;
            }
            if (this.VariableC > 0f)
            {
                this.sqobject.Colour = new Color(1f, 1f, 1f, this.VariableC / 200f);
            }
            else
            {
                this.sqobject.Colour = new Color(1, 1, 1, 1);
            }
            this.VariableC--;
            this.VariableD++;
        }
    }
}