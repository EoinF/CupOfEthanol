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
        private void Computer(int Job)
		{
			this.sqobject.Velocity = new Vector2(0f, 0f);
			Walking = true;
        }
    }
}