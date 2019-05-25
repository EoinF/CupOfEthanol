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
        private void RainingEmber1()
        {
            if (this.sqobject.Flipeffect == SpriteEffects.None)
                this.sqobject.Velocity = new Vector2(-this.Speed / 20, this.Speed / 20);
            else
                this.sqobject.Velocity = new Vector2(this.Speed / 20, this.Speed / 20);
            CheckColissions_Precise(true, false);

            if (sqobject.HitFeet || sqobject.HitHead || sqobject.HitLeft || sqobject.HitRight)
                this.VariableC = 60;
            if (VariableC > 0)
            {
                VariableC--;
                this.sqobject.Position = new Vector2(-999999, -999);
                if (VariableC == 0)
                    this.sqobject.Position = StartPoint;
            }
            this.Walking = false;
        }

        private void RainingEmber2()
        {
        }

        private void RainingEmber3()
        {
        }

        private void RainingEmber4()
        {
        }
    }
}