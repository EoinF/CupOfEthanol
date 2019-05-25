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
        private void Wolf1()
        {
            if (this.Lives == 1)
            {
                if (this.VariableC == 0f)
                {
                    this.VariableC = -1f;
                }
                if (this.VariableC > -40f)
                {
                    this.sqobject.Colour = new Color(1f, -this.VariableC / 40f, -this.VariableC / 40f);
                }
                else
                {
                    this.sqobject.Colour = Color.White;
                }
                this.VariableC--;
            }
        }

        private void Wolf2()
        {
            if (Vector2.Distance(PPlayer.Player.sqobject.Position, this.sqobject.Position) < 200f)
            {
                if (VariableD <= 0)
                {
                    this.Speed = 140f;
                    if (this.OnGround)
                    {
                        if (PPlayer.Player.sqobject.Position.X > this.sqobject.Position.X)
                        {
                            this.sqobject.Flipeffect = SpriteEffects.None;
                        }
                        if (PPlayer.Player.sqobject.Position.X < this.sqobject.Position.X)
                        {
                            this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                        }
                        this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, -4f);
                    }
                    VariableD = 50;
                }
            }
            else
            {
                this.Speed = 60f;
            }
            VariableD--;
            this.Wolf1();
        }

        private void Wolf3()
        {
            this.Dog3(false);
            this.Wolf2();
        }

        private void Wolf4()
        {
            this.Dog4();
            this.Wolf2();
        }
    }
}