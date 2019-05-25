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
        private void Icicle(int period, int distance)
        {
            if (Math.Abs(PPlayer.Player.sqobject.Position.X - this.sqobject.Position.X) < (120 * period)
                && VariableB < 1
                && VariableD < 1)
                VariableB = 50;

            if (this.VariableB > 0)
            {
                VariableB--;

                if (VariableB < 10)
                {
                    VariableB = 0;
                    VariableD = 500;
                }
            }

            if (VariableD > 0)
            {
                this.sqobject.Velocity = new Vector2(0, this.Speed / 16);
                CheckColissions_Precise(true, false);

                if (sqobject.HitFeet || sqobject.HitHead || sqobject.HitLeft || sqobject.HitRight)
                    this.VariableC = 60 * period;
                if (VariableC > 0)
                {
                    VariableC--;
                    this.sqobject.Position = new Vector2(-999999, -999);
                    if (VariableC == 0)
                    {
                        this.sqobject.Position = StartPoint;
                        VariableD = 0;
                    }
                }
                this.Walking = false;
                VariableD--;
                if (VariableD == 0)
                {
                    this.sqobject.Position = StartPoint;
                    VariableC = 0;
                }

            }
            else
            {
                this.sqobject.Velocity = Vector2.Zero;
            }

            if (VariableE != this.Lives)
            {
                VariableC = 150;
                VariableD = 150;
            }
            VariableE = this.Lives;

        }
    }
}