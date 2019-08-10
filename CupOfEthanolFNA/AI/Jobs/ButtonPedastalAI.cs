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
        private void ButtonPedastal(int Job)
		{
			this.sqobject.Velocity = new Vector2(0f, 0f);

			if (!(this as ButtonPedestalEntity).isButtonPressed)
			{
				this.sqobject.bounce.Top = 30;
				if (!this.Walking && this.TopEdgePlus.Intersects(PPlayer.Player.sqobject.BottomEdge) && PPlayer.Player.sqobject.Velocity.Y > 0)
				{
					this.VariableE = 15;
					this.Walking = true;
				}
				else if (this.VariableE > 0)
				{
					if (this.VariableE == 1)
					{
						this.Walking = false;
						this.sqobject.bounce.Top = 0;
						this.sqobject.Position = new Vector2(this.sqobject.Position.X, this.sqobject.Position.Y + 5);
						(this as ButtonPedestalEntity).isButtonPressed = true;

						foreach (Entity entity in Entity.EntityList) {
							if (entity.Job.StartsWith("Q"))
							{
								if (entity.Job.Substring(1) == Job.ToString())
								{
									entity.Lives = 0;
								}
							}
						}
					}
					this.VariableE--;
				}
			}
		}
    }
}