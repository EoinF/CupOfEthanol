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
        private void VanishBlock1()
        {
            this.sqobject.Velocity = Vector2.Zero;
            VariableB--;

            if (VariableE != this.Lives
                && VariableB < 0)
                VariableB = 360;
            VariableE = this.Lives;


            if (VariableB > 300)
            {
                sqobject.Colour = new Color(1f, 1f, 1f, (VariableB - 300) / 100f);
                VariableC = 300;
            }
            else if (VariableB > 0)
            {
                sqobject.Position = new Vector2(-999999, -999999);
            }

            if (VariableC > 1)
                VariableC--;
            
            if (VariableC == 1)
            { 
                sqobject.Position = StartPoint;
                this.sqobject.Colour = Color.White;
                if (PPlayer.Player.sqobject.rect.Intersects(sqobject.rect))
                    PPlayer.Player.Die();
            }




            


               
        }

        private void VanishBlock2()
        {
        }

        private void VanishBlock3()
        {
        }

        private void VanishBlock4()
        {
        }
    }
}