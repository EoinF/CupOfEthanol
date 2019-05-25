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
        private void Kennel1()
        {
            if (this.CheckChildQuantity(2))
            {
                EntityList.Add(new Entity("Dog", "", "Dog_Walk", this.sqobject.Position, 1, 10f, "Dog1", new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(40, 5, 5, 10), 40f, 5, "f", false, this.ID, 0, 0));
            }
        }

        private void Kennel2()
        {
            if (this.CheckChildQuantity(4))
            {
                EntityList.Add(new Entity("Dog", "", "Dog_Walk", this.sqobject.Position, 1, 10f, "Dog4", new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(40, 5, 5, 10), 40f, 5, "f", false, this.ID, 0, 0));
            }
        }

        private void Kennel3()
        {
            if (this.CheckChildQuantity(2))
            {
                EntityList.Add(new Entity("Wolf", "", "Wolf_Walk", this.sqobject.Position, 1, 10f, "Wolf1", new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(40, 5, 5, 10), 60f, 5, "f", false, this.ID, 0, 0));
            }
        }

        private void Kennel4()
        {
            if (this.CheckChildQuantity(4))
            {
                EntityList.Add(new Entity("Wolf", "", "Wolf_Walk", this.sqobject.Position, 1, 10f, "Wolf2", new SquareObject.Damage(0, 1, 1, 1), new SquareObject.Bounce(40, 5, 5, 10), 60f, 5, "f", false, this.ID, 0, 0));
            }
        }
    }
}
