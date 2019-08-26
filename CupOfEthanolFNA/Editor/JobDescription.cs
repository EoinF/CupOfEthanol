using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LackingPlatforms
{
	static class JobDescription
	{
		static Vector2 BackgroundPosition = new Vector2(190f, 290f);

		static TextSprite titleLabel;
		static TextSprite descriptionLabel;


		public static void Init()
		{
			titleLabel = new TextSprite("", "Medium", BackgroundPosition + new Vector2(15, 5), Color.White);
			descriptionLabel = new TextSprite("", "Small", BackgroundPosition + new Vector2(15, 35), Color.White);
		}

		public static void Draw(SpriteBatch spriteBatch, string nameLabel, string jobLabel)
		{
			spriteBatch.Draw(Textures.GetTexture("JobDescriptionBackground"), BackgroundPosition, null, new Color(1f, 1f, 1f, 0.5f), 0f, Vector2.Zero, 1f, 0, 0.9992f);
			string title = "";
			string description = "";

			if (nameLabel != "" && jobLabel != "")
			{
				title = nameLabel + "(" + jobLabel + ")";
				description = getEntityDescription(nameLabel, jobLabel);
			}
			else if (nameLabel != "")
			{
				title = nameLabel;
			}

			spriteBatch.DrawString(titleLabel.Spritefont, title, titleLabel.Position, titleLabel.Colour, 0, Vector2.Zero, 1, 0, 0.9995f);
			spriteBatch.DrawString(descriptionLabel.Spritefont, description, descriptionLabel.Position, descriptionLabel.Colour, 0f, Vector2.Zero, 1f, 0, 0.9995f);

		}

		private static string getEntityDescription(string name, string job)
		{
			switch (name)
			{
				case "Dog":
					if (job == "1")
					{
						return @"Walks horizontally and turns when it reaches an edge or
bumps into a block";
					}
					else if (job == "2")
					{
						return @"Walks horizontally and turns when it reaches an edge or
bumps into a block or other entity";
					}
					else if (job == "3")
					{
						return @"Walks horizontally and turns when it bumps into a block";
					}
					else if (job == "4")
					{
						return @"Walks horizontally and turns when it bumps into a block or other entity";
					}
					break;
				case "Bird":
					if (job == "1")
					{
						return @"Moves horizontally, unaffected by gravity
Turns around when it bumps into a block";
					}
					else if (job == "2")
					{
						return @"Moves horizontally, unaffected by gravity
Turns around when it bumps into a block
Also turns around after a few seconds if it hasn't bumped into anything";
					}
					else if (job == "3")
					{
						return @"Moves vertically, unaffected by gravity
Turns around when it bumps into a block";
					}
					else if (job == "4")
					{
						return @"Moves vertically, unaffected by gravity
Turns around when it bumps into a block
Also turns around after a few seconds if it hasn't bumped into anything";
					}
					break;
				case "Flamer":
					if (job == "1")
					{
						return @"Walks horizontally and turns when it reaches an edge or
bumps into a block";
					}
					else if (job == "2")
					{
						return @"Walks horizontally and turns when it reaches an edge or
bumps into a block or other entity";
					}
					else if (job == "3")
					{
						return @"Walks horizontally and turns when it bumps into a block";
					}
					else if (job == "4")
					{
						return @"Walks horizontally and turns when it bumps into a block or other entity";
					}
					break;
				case "Troller":
					if (job == "1")
					{
						return @"Jumps with slightly random velocities.
There's also random time intervals between jumps.";
					}
					else if (job == "2")
					{
						return @"Time intervals between jumps are more random than 'Troller1'. 
Jumps Higher than 'Troller1'.";
					}
					else if (job == "3")
					{
						return @"Time intervals between jumps are more random than 'Troller1'. 
Also changes direction towards the player
when the player gets close to it.";
					}
					else if (job == "4")
					{
						return "[Not working]";
					}
					break;
				case "Wolf":
					if (job == "1")
					{
						return @"Walks horizontally and turns when it reaches an edge or
bumps into a block";
					}
					else if (job == "2")
					{
						return @"Walks horizontally and turns when it reaches an edge or
bumps into a block
Lunges at the player if it gets close";
					}
					else if (job == "3")
					{
						return @"Walks horizontally and turns when it bumps into a block
Lunges at the player if it gets close";
					}
					else if (job == "4")
					{
						return @"Walks horizontally and turns when it bumps into a block or other entity
Lunges at the player if it gets close";
					}
					break;
				case "Stealth":
					if (job == "1")
					{
						return @"Walks while invisible over and back in short bursts. 
When it stops it reveals its location.";
					}
					else if (job == "2")
					{
						return "Same as 'Stealth1' but collides with other enemies.";
					}
					else if (job == "3")
					{
						return @"Walks while invisible over and back continuously, 
revealing its location every so often.";
					}
					else if (job == "4")
					{
						return "Same as 'Stealth3' but collides with other enemies.";
					}
					break;
				case "Tentacle":
					if (job == "1")
					{
						return @"Floats in the air spewing tentacle particles about the place. 
Chases the player when it gets close.";
					}
					else if (job == "2")
					{
						return @"Floats in the air spewing tentacle particles about the place. 
Chases the player when it gets close.
The tentacle particles move along the ground and turn around
when bumping into a block";
					}
					else if (job == "3")
					{
						return @"Same as 'Tentacle2'
Shoots out tentacles at a higher rate";
					}
					else if (job == "4")
					{
						return "[Not working]";
					}
					break;
				case "Platform":
				case "ConcretePlatform":
					if (job == "1")
					{
						return "Moves horizontally and turns when it hits an object";
					}
					else if (job == "2")
					{
						return "Moves vertically and turns when it hits an object";
					}
					else if (job == "3")
					{
						return "Moves horizontally and turns when it hits an object";
					}
					else if (job == "4")
					{
						return "Moves vertically and turns when it hits an object";
					}
					break;
				case "Cannon":
					return "Doesn't move. Shoots a lazer at a rate of " + job;
				case "Wheelbot":
					return "Similar to 'dog1'. Also shoots a lazer at a rate of " + job;
				case "Robot":
					return "Flying robot. Shoots a lazer at a rate of " + job;
				case "Shadow":
					if (job == "1")
					{
						return @"Becomes more and more transparent, the further it gets from the 
player. Walks horizontally and turns when it hits an object.
Makes small jumps over small gaps or up 1 tile hills.
Makes large jumps over big gaps";
					}
					else if (job == "2")
					{
						return @"Same as 'Shadow1'
Moves twice as fast as 'Shadow1'";
					}
					else if (job == "3")
					{
						return "[Not working]";
					}
					else if (job == "4")
					{
						return "[Not working]";
					}
					break;
				case "Icicle":
					int jobInt = int.Parse(job);
					int rate = 1 + (jobInt - 1) % 3;
					int distance = (jobInt - 1) / 3;
					string description = "Drops down vertically at a rate of " + rate + @".
Starts falling only when the player is ";
					if (distance == 0)
					{
						description += "within a VERY SHORT DISTANCE";
					}
					else if (distance == 1)
					{
						description += "within a MODERATE DISTANCE";
					}
					else
					{
						description += "within a VERY FAR DISTANCE";
					}
					return description + @"
Respawns at its initial position when it collides with something";

				case "Computer":
					return "Disappears when a Button pedastal with job '" + job + "' is activated";

				case "ButtonPedastal":
					return "Destroys all computers with job '" + job + "' when the player jumps on it";

				case "Door":
					string colour = "";
					if (job == "1")
					{
						colour = "RED";
					}
					else if (job == "2")
					{
						colour = "BLUE";
					}
					else if (job == "3")
					{
						colour = "GREEN";
					}
					else if (job == "4")
					{
						colour = "YELLOW";
					}
					return @"Disappears when the player presses the 'spacebar' while next to it
Only unlocks when the player possesses the " + colour + " key";
			}

			return "No description found...";
		}
	}
}
