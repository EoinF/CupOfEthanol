using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LackingPlatforms
{

	public class ButtonPedestalEntity : Entity
	{
		public bool isButtonPressed;
		public bool wasButtonPressed;

		public ButtonPedestalEntity(string texture, string jumpAnimation, string walkAnimation, Vector2 position, int lives, float size, string job,
			SquareObject.Damage dmg, SquareObject.Bounce bnce, float speed, byte friction, string OppositeDir, bool staticTexture, int id,
			byte startCheckpoint, byte endCheckpoint, Color? colour = null, int startDelay = 0)
		: base(texture, jumpAnimation, walkAnimation, position, lives, size, job, dmg, bnce, speed, friction, OppositeDir, staticTexture, id,
			  startCheckpoint, endCheckpoint, colour, startDelay)
		{
			isButtonPressed = false;
			wasButtonPressed = false;
		}

		public override void ResetStatus()
		{
			base.ResetStatus();
			isButtonPressed = wasButtonPressed;
		}

		public override void SaveStatus()
		{
			base.SaveStatus();
			wasButtonPressed = isButtonPressed;
		}


		public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
		{
			if (isButtonPressed)
			{
				spriteBatch.Draw(this.JumpAnim, this.sqobject.Position + Level.Offset, null, this.sqobject.Colour, 0f, Vector2.Zero, this.sqobject.Size, this.sqobject.Flipeffect, 0.1f);
			}
			else
			{
				base.Draw(spriteBatch, gametime);
			}
		}
	}
}
