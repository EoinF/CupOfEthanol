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
        private void Cannon(int FireRate)
        {
            if (this.Job.Contains("M"))
            {
                this.sqobject.Velocity = Vector2.Zero;
            }
            if (this.VariableD > 300 - (30 * FireRate))
            {
                string d = "t";
                Vector2 AddedDisplacement = new Vector2(-(Textures.GetTexture("LazerBullet").Width * sqobject.Size) - 1, (this.sqobject.Texture.Height / 2f) - 2);

                if (this.sqobject.Flipeffect == SpriteEffects.None)
                {
                    d = "f";
                    AddedDisplacement += new Vector2((sqobject.Texture.Width * sqobject.Size) + Textures.GetTexture("LazerBullet").Width + 2, 0f);
                }
                EntityList.Add(new Entity("LazerBullet", "LazerBullet", "LazerBullet", this.sqobject.Position + AddedDisplacement, 1000, 1f, "Lazer", new SquareObject.Damage(1, 1, 1, 1), new SquareObject.Bounce(2, 2, 2, 2), 50f, 50, d, false, 0, 0, 0));

                this.VariableD = 0f;
                this.sqobject.Velocity -= Math.Sign(AddedDisplacement.X) * new Vector2(6, 0f);
            }
            this.VariableD++;
        }
    }
}