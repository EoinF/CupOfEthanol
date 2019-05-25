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
            if (((this.VariableC / 53f) == ((int)(this.VariableC / 53f))) && this.CheckChildQuantity(4))
            {
                EntityList.Add(new Entity("L", "", "", this.sqobject.Position - new Vector2(0f, 8f), 300, 1, "Particle", new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(40, 10, 10, 20), 10f, 100, "f", true, this.ID, 0, 0));
                if (rnd == 0)
                {
                    rnd = 1;
                }
                EntityList[EntityList.Count - 1].sqobject.Velocity = new Vector2((float)((rnd / 0xaf) + (3 * Math.Sign(rnd))), -2f + (((float)(rnd - 300)) / 160f));
                this.CheckColissions_Precise(true, true);
                this.Walking = false;
            }
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