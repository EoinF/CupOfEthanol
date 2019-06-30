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
        private void Tentacle1()
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
				float Distance = Direction.Length();

				if (Direction.X < 0f)
					this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
				else
					this.sqobject.Flipeffect = SpriteEffects.None;
				Direction.Normalize();
				this.sqobject.Velocity += (Direction * 0.1f * (float)Math.Sqrt(Distance)) + new Vector2(0f, 0.01f);
			}

			rnd = rand.Next(20, 50);
			this.SpawnTentacle(2);
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
        

		private void SpawnTentacle(int FireRate)
		{
			rand = new Random();
			int rnd = rand.Next(-251, 250);
			if (this.VariableD > 200 - (60 * FireRate))
			{
				string d = "t";
				Vector2 AddedDisplacement = new Vector2(-(Textures.GetTexture("L").Width * sqobject.Size) - 1, -Textures.GetTexture("L").Height - 2);

				if (this.sqobject.Flipeffect == SpriteEffects.None)
				{
					d = "f";
					AddedDisplacement += new Vector2((sqobject.Texture.Width * sqobject.Size) + Textures.GetTexture("L").Width + 2, 0f);
				}
				//EntityList.Add(new Entity("L", "", "", this.sqobject.Position - new Vector2(0f, 8f), 300, 1, "Particle", new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(40, 10, 10, 20), 10f, 100, "f", false, 0, 0, 0));

				EntityList.Add(new Entity("L", "L", "L", this.sqobject.Position + AddedDisplacement, 300, 1f, "Particle", new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(40, 10, 10, 20), 10f, 100, d, false, 0, 0, 0));
				EntityList[EntityList.Count - 1].sqobject.Velocity = new Vector2((float)((rnd + 700f) / 80f) * (d == "t" ? -1: 1), -2f + (((float)(rnd - 400)) / 130f));

				this.VariableD = 0f;
				//this.sqobject.Velocity -= Math.Sign(AddedDisplacement.X) * new Vector2(6, 0f);
			}
			this.VariableD++;
		}

        private void Tentacle2()
        {
            rand = new Random();
            this.VariableC++;
            if (this.VariableC > 1440f)
            {
                this.VariableC = 1f;
            }
            int rnd = rand.Next(-251, 250);
            this.sqobject.Velocity = new Vector2(((float)Math.Cos((double)(this.VariableC / 16f))) * 0.15f, ((float)Math.Sin((double)(this.VariableC / 16f))) * 0.6f);
            this.sqobject.Velocity += new Vector2((float)(rnd / 0x3e8), 0f);
            rnd = rand.Next(-251, 250);
            this.sqobject.Velocity += new Vector2(0f, (float)(rnd / 0x3e8));
            if (Vector2.Distance(PPlayer.Player.sqobject.Position, this.sqobject.Position) < 150f)
            {
                Vector2 Direction = PPlayer.Player.sqobject.Position - this.sqobject.Position;
                Direction = (Vector2)(Direction / Direction.Length());
                this.sqobject.Velocity += ((Vector2)(Direction * 0.4f)) - new Vector2(0f, 0.08f);
            }
            if (((this.VariableC / 53f) == ((int)(this.VariableC / 53f))) && this.CheckChildQuantity(3))
            {
                EntityList.Add(new Entity("L", "", "", this.sqobject.Position - new Vector2(0f, 8f), 300, 1, "L", new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(40, 10, 10, 20), 10f, 100, "f", true, this.ID, 0, 0));

                if (rnd == 0)
                {
                    rnd = 1;
                }
                EntityList[EntityList.Count - 1].sqobject.Velocity = new Vector2((float)((rnd / 0xaf) + (3 * Math.Sign(rnd))), -2f + (((float)(rnd - 300)) / 160f));

                this.CheckColissions_Precise(true, true);
                this.Walking = false;
            }
        }

        private void Tentacle3()
        {
            rand = new Random();
            this.VariableC++;
            if (this.VariableC > 1440f)
            {
                this.VariableC = 1f;
            }
            int rnd = rand.Next(-251, 250);
            this.sqobject.Velocity = new Vector2(((float)Math.Cos((double)(this.VariableC / 16f))) * 0.15f, ((float)Math.Sin((double)(this.VariableC / 16f))) * 0.6f);
            this.sqobject.Velocity += new Vector2((float)(rnd / 0x3e8), 0f);
            rnd = rand.Next(-251, 250);
            this.sqobject.Velocity += new Vector2(0f, (float)(rnd / 0x3e8));
            if (Vector2.Distance(PPlayer.Player.sqobject.Position, this.sqobject.Position) < 350f)
            {
                Vector2 Direction = PPlayer.Player.sqobject.Position - this.sqobject.Position;
                Direction = (Vector2)(Direction / Direction.Length());
                this.sqobject.Velocity += Direction - new Vector2(0f, 0.08f);
            }
            if (((this.VariableC / 53f) == ((int)(this.VariableC / 53f))) && this.CheckChildQuantity(4))
            {
                EntityList.Add(new Entity("L", "", "", this.sqobject.Position - new Vector2(0f, 8f), 300, 1, "Particle", new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(40, 10, 10, 20), 10f, 100, "f", true, this.ID, 0, 0));
                if (rnd == 0)
                {
                    rnd = 1;
                }
                EntityList[EntityList.Count - 1].sqobject.Velocity = new Vector2((float)((rnd / 0xaf) + Math.Sign(rnd)), -2f + (((float)(rnd - 300)) / 160f));
                if (Vector2.Distance(PPlayer.Player.sqobject.Position, this.sqobject.Position) < 450f)
                {
                    EntityList[EntityList.Count - 1].sqobject.Velocity = this.PlayerLockon(-3f) + new Vector2((float)(rnd / 700), (float)(rnd / 700));
                }

            }
            this.CheckColissions_Precise(true, true);
            this.Walking = false;
        }

        private void Tentacle4()
        {
            rand = new Random();
            this.VariableC++;
            if (this.VariableC > 1440f)
            {
                this.VariableC = 1f;
            }
            int rnd = rand.Next(-251, 250);
            this.sqobject.Velocity = new Vector2(((float)Math.Cos((double)(this.VariableC / 16f))) * 0.15f, ((float)Math.Sin((double)(this.VariableC / 16f))) * 0.6f);
            this.sqobject.Velocity += new Vector2((float)(rnd / 0x3e8), 0f);
            rnd = rand.Next(-251, 250);
            this.sqobject.Velocity += new Vector2(0f, (float)(rnd / 0x3e8));
            if (Vector2.Distance(PPlayer.Player.sqobject.Position, this.sqobject.Position) < 150f)
            {
                Vector2 Direction = PPlayer.Player.sqobject.Position - this.sqobject.Position;
                Direction = (Vector2)(Direction / Direction.Length());
                this.sqobject.Velocity += Direction - new Vector2(0f, 0.08f);
            }
            if (((this.VariableC / 53f) == ((int)(this.VariableC / 53f))) && this.CheckChildQuantity(3))
            {
                EntityList.Add(new Entity("L", "", "", this.sqobject.Position - new Vector2(0f, 8f), 300, 1, "L", new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(40, 10, 10, 20), 10f, 100, "f", true, this.ID, 0, 0));

                    if (rnd == 0)
                    {
                        rnd = 1;
                    }
                    EntityList[EntityList.Count - 1].sqobject.Velocity = new Vector2((float)((rnd / 0xaf) + Math.Sign(rnd)), -2f + (((float)(rnd - 300)) / 160f));
                    if (Vector2.Distance(PPlayer.Player.sqobject.Position, this.sqobject.Position) < 450f)
                    {
                        EntityList[EntityList.Count - 1].sqobject.Velocity = this.PlayerLockon(-3f) + new Vector2((float)(rnd / 700), (float)(rnd / 700));
                    }
                
            
            this.CheckColissions_Precise(true, true);
            this.Walking = false;
            }
        }

        public void TentacleSpawn()
        {
            Dog1();
            CheckColissions_Precise(false, true);
            Lives--;
        }

    }
}