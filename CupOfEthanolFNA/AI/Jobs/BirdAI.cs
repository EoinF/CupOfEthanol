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
        private void Bird1()
        {
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                this.sqobject.Velocity = new Vector2(1f, 0f);
            }
            else
            {
                this.sqobject.Velocity = new Vector2(-1f, 0f);
            }

            //this.sqobject.Position += new Vector2(this.sqobject.Velocity.X * (this.Speed / 20f), 0f);
            this.CheckColissions_Precise(true, false);
            this.Walking = false;
            this.OnGround = false;

            if (this.sqobject.HitRight)
            {
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                VariableA = 180;
            }
            if (this.sqobject.HitLeft)
            {
                this.sqobject.Flipeffect = SpriteEffects.None;
                VariableA = 180;
            }

        }

        private void Bird2()
        {
            this.VariableA++;
            if (this.VariableA > 300f)
            {
                if (this.sqobject.Flipeffect == SpriteEffects.FlipHorizontally)
                {
                    this.sqobject.Flipeffect = SpriteEffects.None;
                }
                else
                {
                    this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                }
                this.VariableA = 180;
            }
            Bird1();

        }

        private void Bird3()
        {
            FacePlayer(200);
            this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, Math.Sign(sqobject.Velocity.Y) * 1.5f);

            this.CheckColissions_Precise(true, false);
            this.Walking = false;
            this.OnGround = false;

            if (this.sqobject.HitFeet)
            {
                this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, -Math.Abs(sqobject.Velocity.Y * 22)); //Multiplied by 22 to ensure it overcomes gravity when going back up
                VariableA = 180;
            }
            if (this.sqobject.HitHead)
            {
                this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, Math.Abs(sqobject.Velocity.Y * 22));
                VariableA = 180;
            }
        }
        private void Bird4()
        {
            this.VariableA++;
            if (VariableA > 300)
            {
                this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, -Math.Sign(sqobject.Velocity.Y));
                this.VariableA = 180f;
            }
            Bird3();

        }

    }
}
