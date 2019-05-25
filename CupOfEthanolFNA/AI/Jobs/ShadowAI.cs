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
        private void Shadow1()
        {
            this.Speed = 50f;
            if (!this.OnGround)
            {
                this.Speed = 20f;
            }
            this.Dog3(false);
            this.sqobject.Colour = Color.White;
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 500f)
            {
                float d = Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position);
                float alpha = 1f - (25f * ((float)Math.Sqrt(((double)d) / ((double)(250000f - (d * d))))));
                this.sqobject.Colour = new Color(1, 1, 1, alpha);
            }

            if (this.OnGround)
            {
                if (this.sqobject.Flipeffect == SpriteEffects.None)
                {
                    //if ((BlockExists_Right(0, 30f, -1) && !BlockExists_Right(-1, 30f, -1)))
                    if ((BlockExists_Right(0, 30, 1) && !BlockExists_Right(-1, 30, 1)))
                    {
                        this.JumpTimeout = 11;
                    }
                    if ((!this.BlockExists_Right(1, 1, 1) && !this.BlockExists_Right(2, 1, 1)))
                    {
                        if (this.BlockExists_Right(1, 26, 1) || this.BlockExists_Right(2, 26, 1))
                            this.JumpTimeout = 12;
                        else if (this.BlockExists_Right(1, 51, 1))
                            this.JumpTimeout = 17;
                        else
                        {
                            if (!this.BlockExists_Below(1, 1))
                                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                        }
                    }
                }
                else
                {
                    if ((this.BlockExists_Left(0, 30, 1) && !this.BlockExists_Left(-1, 30, 1)))
                    {
                        this.JumpTimeout = 11;
                    }
                    if ((!this.BlockExists_Left(1, 1, 1) && !this.BlockExists_Left(2, 1, 1)))
                    {
                        if (this.BlockExists_Left(1, 26, 1) || this.BlockExists_Left(2, 26, 1))
                            this.JumpTimeout = 12;
                        else if (this.BlockExists_Left(1, 51, 1))
                            this.JumpTimeout = 17;
                        else
                        {
                            if (!this.BlockExists_Below(-1, 1))
                                this.sqobject.Flipeffect = SpriteEffects.None;
                        }
                    }

                }
            }

            if (this.JumpTimeout > 0)
            {
                this.sqobject.Velocity += new Vector2(0f, -0.65f + (0.04f / (((float)this.JumpTimeout) / 20f)));
                this.JumpTimeout--;
            }
        }

        private void Shadow2()
        {
            OnGround = false;
            Walking = false;
            Dog1();
            Speed = 70f;
            if (!OnGround)
            {
                Speed = 20f;
            }
            sqobject.Colour = Color.White;
            float d = Math.Abs(Vector2.Distance(sqobject.Position, PPlayer.Player.sqobject.Position));
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 500f)
            {
                float alpha = 1f - (25f * ((float)Math.Sqrt(((double)d) / ((double)(250000f - (d * d))))));
                sqobject.Colour = new Color(1, 1, 1, alpha);
            }
            if (sqobject.Flipeffect == SpriteEffects.None)
            {
                if ((BlockExists_Right(0, 30f, 0.01f) && !BlockExists_Right(-1, 30f, 0.01f)) && OnGround)
                {
                    this.JumpTimeout = 11;
                }
                if ((!this.BlockExists_Right(1, 0, 0.01f) && !this.BlockExists_Right(2, 0, 0.01f)) && this.OnGround)
                {
                    this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                }
            }
            else
            {
                if ((this.BlockExists_Left(0, 30f, 0.01f) && !this.BlockExists_Left(-1, 30f, 0.01f)) && this.OnGround)
                {
                    this.JumpTimeout = 11;
                }
                if ((!this.BlockExists_Left(1, 0, 0.01f) && !this.BlockExists_Left(2, 0, 0.01f)) && this.OnGround)
                {
                    this.sqobject.Flipeffect = SpriteEffects.None;
                }
            }
            if (this.JumpTimeout > 0)
            {
                this.sqobject.Velocity += new Vector2(0f, -0.65f + (0.04f / (((float)this.JumpTimeout) / 20f)));
                this.JumpTimeout--;
            }
        }

        private void Shadow3()
        {
            this.OnGround = false;
            this.Walking = false;
            this.Dog1();
            this.Speed = 50f;
            if (!this.OnGround)
            {
                this.Speed = 20f;
            }
            this.sqobject.Colour = Color.White;
            float d = Math.Abs(Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position));
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 500f)
            {
                float alpha = 1f - (25f * ((float)Math.Sqrt(((double)d) / ((double)(250000f - (d * d))))));
                this.sqobject.Colour = new Color(1,1,1, alpha);
            }
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 350f)
            {
                if (PPlayer.Player.sqobject.Position.X > this.sqobject.Position.X)
                {
                    this.sqobject.Flipeffect = SpriteEffects.None;
                }
                if (PPlayer.Player.sqobject.Position.X < this.sqobject.Position.X)
                {
                    this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                }
                if ((((PPlayer.Player.sqobject.Position.Y + PPlayer.Player.sqobject.Texture.Height) + 2f) < (this.sqobject.Position.Y + this.sqobject.Texture.Height)) && this.OnGround)
                {
                    if ((this.BlockExists_Right(-1, 2f, 0.01f) && !this.BlockExists_Right(-2, 2f, 0.01f)) && this.OnGround)
                    {
                        this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                    }
                    if ((this.BlockExists_Left(-1, 2f, 0.01f) && !this.BlockExists_Left(-2, 2f, 0.01f)) && this.OnGround)
                    {
                        this.sqobject.Flipeffect = SpriteEffects.None;
                    }
                    this.JumpTimeout = 0x10;
                }
            }
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                if ((this.BlockExists_Right(0, 30f, 0.01f) && !this.BlockExists_Right(-1, 30f, 0.01f)) && this.OnGround)
                {
                    this.JumpTimeout = 11;
                }
                if ((!this.BlockExists_Right(1, 0, 0.01f) && !this.BlockExists_Right(2, 0, 0.01f)) && this.OnGround)
                {
                    this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                }
            }
            else
            {
                if ((this.BlockExists_Left(0, 30f, 0.01f) && !this.BlockExists_Left(-1, 30f, 0.01f)) && this.OnGround)
                {
                    this.JumpTimeout = 11;
                }
                if ((!this.BlockExists_Left(1, 0, 0.01f) && !this.BlockExists_Left(2, 0, 0.01f)) && this.OnGround)
                {
                    this.sqobject.Flipeffect = SpriteEffects.None;
                }
            }
            if (this.JumpTimeout > 0)
            {
                this.sqobject.Velocity += new Vector2(0f, -0.65f + (0.04f / (((float)this.JumpTimeout) / 20f)));
                this.JumpTimeout--;
            }
        }

        private void Shadow4()
        {
            this.OnGround = false;
            this.Walking = false;
            this.Dog1();
            this.Speed = 70f;
            if (!this.OnGround)
            {
                this.Speed = 20f;
            }
            this.sqobject.Colour = Color.White;
            float d = Math.Abs(Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position));
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 500f)
            {
                float alpha = 1f - (25f * ((float)Math.Sqrt(((double)d) / ((double)(250000f - (d * d))))));
                this.sqobject.Colour = new Color(1, 1, 1, alpha);
            }
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) > 350f)
            {
                if (PPlayer.Player.sqobject.Position.X > this.sqobject.Position.X)
                {
                    this.sqobject.Flipeffect = SpriteEffects.None;
                }
                if (PPlayer.Player.sqobject.Position.X < this.sqobject.Position.X)
                {
                    this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                }
                if (((PPlayer.Player.sqobject.Position.Y + PPlayer.Player.sqobject.Texture.Height) < (this.sqobject.Position.Y + this.sqobject.Texture.Height)) && this.OnGround)
                {
                    this.JumpTimeout = 0x10;
                }
            }
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                if ((this.BlockExists_Right(0, 35f, 0.01f) && !this.BlockExists_Right(-1, 35f, 0.01f)) && this.OnGround)
                {
                    this.JumpTimeout = 11;
                }
                if ((!this.BlockExists_Right(1, 0, 0.01f) && !this.BlockExists_Right(2, 0, 0.01f)) && this.OnGround)
                {
                    this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                }
            }
            else
            {
                if ((this.BlockExists_Left(0, 35f, 0.01f) && !this.BlockExists_Left(-1, 35f, 0.01f)) && this.OnGround)
                {
                    this.JumpTimeout = 11;
                }
                if ((!this.BlockExists_Right(1, 0, 0.01f) && !this.BlockExists_Right(2, 0, 0.01f)) && this.OnGround)
                {
                    this.sqobject.Flipeffect = SpriteEffects.None;
                }
            }
            if (this.JumpTimeout > 0)
            {
                this.sqobject.Velocity += new Vector2(0f, -0.65f + (0.04f / (((float)this.JumpTimeout) / 20f)));
                this.JumpTimeout--;
            }
        }
    }
}