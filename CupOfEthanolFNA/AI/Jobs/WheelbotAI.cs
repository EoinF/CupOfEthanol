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
        private void Wheelbot(int FireRate)
        {
            this.Dog1();
            this.Cannon(FireRate);
        }
    }
}