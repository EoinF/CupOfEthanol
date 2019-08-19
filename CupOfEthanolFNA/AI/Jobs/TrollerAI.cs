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
        private void Troller1()
        {

			this.OnGround = false;
			this.CheckColissions_Precise(true, false);

			if (sqobject.HitLeft)
				this.sqobject.Flipeffect = SpriteEffects.None;
			if (sqobject.HitRight)
				this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;

			this.Walking = false;

			float rnd = rand.Next(1, 3);
            this.VariableC -= rnd;
            rnd = rand.Next(0, 40);
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                if (this.OnGround && (this.VariableC < 0f))
                {
                    this.sqobject.Velocity = new Vector2(1f, -(this.Speed / 15f) - (rnd / 15f));
                    this.VariableC = 130f;
                }
            }
            else if (this.OnGround && (this.VariableC < 0f))
            {
                this.sqobject.Velocity = new Vector2(-1f, -(this.Speed / 15f) - (rnd / 15f));
                this.VariableC = 130f;
            }

			if (!this.OnGround)
			{
				this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, this.sqobject.Velocity.Y / (1.01f));
			}
        }

        private void Troller2()
        {
            float rnd = rand.Next(1, 6);
            this.VariableC -= rnd;
            rnd = rand.Next(0, 40);
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                if (this.OnGround && (this.VariableC < 0f))
                {
                    this.sqobject.Velocity = new Vector2(2f, -(this.Speed / 13f) - (rnd / 16f));
                    this.VariableC = 250f;
                }
            }
            else
            {
                if (this.OnGround && (this.VariableC < 0f))
                {
                    this.sqobject.Velocity = new Vector2(-1f, -(this.Speed / 13f) - (rnd / 16f));
                    this.VariableC = 250f;
                }
            }
            this.OnGround = false;

            this.CheckColissions_Precise(true, false);
            if (sqobject.HitLeft)
                this.sqobject.Flipeffect = SpriteEffects.None;
            if (sqobject.HitRight)
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;

            this.Walking = false;
        }

        private void Troller3()
        {
            float rnd = rand.Next(1, 5);
            this.VariableC -= rnd;
            rnd = rand.Next(0, 40);
            FacePlayer(200);
            if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                if (this.OnGround && (this.VariableC < 0f))
                {
                    this.sqobject.Velocity = new Vector2(1f, -(this.Speed / 15f) - (rnd / 20f));
                    this.VariableC = 160f;
                }
            }
            else if (this.OnGround && (this.VariableC < 0f))
            {
                this.sqobject.Velocity = new Vector2(-1f, -(this.Speed / 15f) - (rnd / 20f));
                this.VariableC = 160f;
            }

            this.OnGround = false;
            this.CheckColissions_Precise(true, false);
            if (sqobject.HitLeft)
                this.sqobject.Flipeffect = SpriteEffects.None;
            if (sqobject.HitRight)
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;

            this.Walking = false;
        }

        private void Troller4()
        {
            float rnd = rand.Next(1, 5);
            this.VariableC -= rnd;
            rnd = rand.Next(0, 40);
            if (Vector2.Distance(PPlayer.Player.sqobject.Position, this.sqobject.Position) < 200f)
            {
                FacePlayer(200);
                if (this.sqobject.Flipeffect == SpriteEffects.None)
                {
                    if (this.OnGround && (this.VariableC < 0f))
                    {
                        this.sqobject.Velocity = this.PlayerLockon(1f * (this.Speed / 20f));
                        this.VariableC = 160f;
                    }
                }
                else if (this.OnGround && (this.VariableC < 0f))
                {
                    this.sqobject.Velocity = this.PlayerLockon(-1f * (this.Speed / 20f));
                    this.VariableC = 160f;
                }
            }
            else if (this.sqobject.Flipeffect == SpriteEffects.None)
            {
                if (this.OnGround && (this.VariableC < 0f))
                {
                    this.sqobject.Velocity = new Vector2(1f, -(this.Speed / 15f) - (rnd / 20f));
                    this.VariableC = 160f;
                }
            }
            else if (this.OnGround && (this.VariableC < 0f))
            {
                this.sqobject.Velocity = new Vector2(-1f, -(this.Speed / 15f) - (rnd / 20f));
                this.VariableC = 160f;
            }
            if (this.sqobject.Velocity.Y < -10f)
            {
                this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, -10f);
            }
            if (this.sqobject.Velocity.Y > 7f)
            {
                this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, 7f);
            }

            this.OnGround = false;
            this.CheckColissions_Precise(true, false);
            if (sqobject.HitLeft)
                this.sqobject.Flipeffect = SpriteEffects.None;
            if (sqobject.HitRight)
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;

            this.Walking = false;
        }
    }
}