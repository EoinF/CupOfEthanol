namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Runtime.InteropServices;

    public class SquareObject : RigidObject
    {
        public Bounce bounce;
        public Damage damage;
        public bool HitHead = false;
        public bool HitFeet = false;
        public bool HitLeft = false;
        public bool HitRight = false;
        public static SquareObject[,] sqObjectArray;

        #region Constructors

        public SquareObject(string texture, Vector2 position, float layer) : base(texture, position, layer)
        {
            this.damage = new Damage(0, 0, 0, 0);
            this.bounce = new Bounce(0, 0, 0, 0);
        }

        public SquareObject(string texture, Vector2 position, float layer, float size) : base(texture, position, layer, size)
        {
            this.damage = new Damage(0, 0, 0, 0);
            this.bounce = new Bounce(0, 0, 0, 0);
        }

        public SquareObject(string texture, Vector2 position, Damage dmg, Bounce bnce, float layer) : base(texture, position, layer)
        {
            this.damage = dmg;
            this.bounce = bnce;
        }

        public SquareObject(string texture, Vector2 position, float layer, float size, int friction) : base(texture, position, layer, size, friction)
        {
            this.damage = new Damage(0, 0, 0, 0);
            this.bounce = new Bounce(0, 0, 0, 0);
        }

        public SquareObject(string texture, Vector2 position, Damage dmg, Bounce bnce, float layer, float size) : base(texture, position, layer, size)
        {
            this.damage = dmg;
            this.bounce = bnce;
        }

        public SquareObject(string texture, Vector2 position, float layer, float size, int friction, Color colour) : base(texture, position, layer, size, friction, colour)
        {
            this.damage = new Damage(0, 0, 0, 0);
            this.bounce = new Bounce(0, 0, 0, 0);
        }

        public SquareObject(string texture, Vector2 position, Damage dmg, Bounce bnce, float layer, float size, int friction) : base(texture, position, layer, size, friction)
        {
            this.damage = dmg;
            this.bounce = bnce;
        }

        public SquareObject(string texture, Vector2 position, Damage dmg, Bounce bnce, float layer, float size, int friction, Color colour) : base(texture, position, layer, size, friction, colour)
        {
            this.damage = dmg;
            this.bounce = bnce;
        }

        #endregion

        public bool CollideBottom(SquareObject sq, bool isLazer = false)
        {
            int counter = 0;
			if (isLazer)
			{
				return false;
			}
			HitFeet = true;
			

            if (this.Velocity.Y >= sq.Velocity.Y || sq.bounce.Top > 0)
                Velocity = new Vector2(Velocity.X, Math.Abs(Velocity.Y / 8f) - (0.1f * sq.bounce.Top));

            if (this.Velocity.Y < sq.Velocity.Y && sq.bounce.Top <= 0)
                Velocity = new Vector2(Velocity.X, sq.Velocity.Y);


            if ((Velocity.X > (0.001f * sq.frictionforce)) || (Velocity.X < (-0.001f * sq.frictionforce)))
            {
                Velocity -= new Vector2((Math.Sign(Velocity.X) * 0.001f) * sq.frictionforce, 0);
            }
            else
            {
                Velocity = new Vector2(0, Velocity.Y);
            }


            while (this.rect.Intersects(sq.TopEdge))
            {
                Position -= new Vector2(0, 0.01f);
                counter++;
                if (counter > 0x1388)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CollideLeft(SquareObject sq, bool isLazer = false)
		{
			if (isLazer)
			{
				return false;
			}
			HitLeft = true;
			
            Velocity = new Vector2(Math.Abs(Velocity.X / 5f) + (0.1f * sq.bounce.Right), Velocity.Y / (1.03f + (0.0015f * sq.frictionforce)));
            
            
            /*while (rect.Intersects(sq.RightEdge))
            {
                Position += new Vector2(0.01f, 0f);
                counter++;
                if (counter > 0x1388)
                {
                    return true;
                }
            }*/

            Position += new Vector2( sq.rect.Right - this.Position.X, 0);
            return false;
        }

        public bool CollideRight(SquareObject sq, bool isLazer = false)
        {
            int counter = 0;
			if (isLazer)
			{
				return false;
			}
			HitRight = true;
			
            Velocity = new Vector2(-Math.Abs(Velocity.X / 5f) - (0.1f * sq.bounce.Left), Velocity.Y / (1.03f + (0.0015f * sq.frictionforce)));
            
            
            while (rect.Intersects(sq.LeftEdge))
            {
                counter++;
                if (counter > 0x1388)
                {
                    return true;
                }
                Position -= new Vector2(0.01f, 0f);
            }
            
            //if (Velocity.X > 0)
            //    Velocity = new Vector2(sq.Position.X - this.rect.Right, Velocity.Y);
            //Position -= new Vector2(0.01f, 0f);
            Position += new Vector2(sq.Position.X - this.rect.Right, 0);

            return false;
        }

        public bool CollideTop(SquareObject sq, bool isLazer = false)
        {
            int counter = 0;
			if (isLazer)
			{
				return false;
			}
			HitHead = true;
			

            Velocity = new Vector2(Velocity.X, Math.Abs(Velocity.Y / 8f) + (0.1f * sq.bounce.Bottom));
            

            if ((Velocity.X > (0.002f * sq.frictionforce)) || (Velocity.X < (-0.002f * sq.frictionforce)))
            {
                Velocity -= new Vector2((Math.Sign(Velocity.X) * 0.002f) * sq.frictionforce, 0f);
            }
            else
            {
                Velocity = new Vector2(0f, Velocity.Y);
            }

            //if (Velocity.Y < 0)
            //    Velocity = new Vector2(Velocity.X, this.Position.Y - sq.rect.Bottom);
            //Position += new Vector2(0, Math.Abs(Velocity.Y));
            this.Position += new Vector2(0, sq.rect.Bottom - this.Position.Y + 0.01f);

            
            while (rect.Intersects(sq.BottomEdge))
            {
                Position += new Vector2(0f, 0.1f);
                counter++;
                if (counter > 0x1388)
                {
                    return true;
                }
            }
            return false;
        }

        public static void DrawSquares(SquareObject[,] sqObjectArray, SpriteBatch spriteBatch, int marginX, int marginY)
        {
            for (int x = (int)((-Level.Offset.X + 400) / 25f) - marginX; x < ((int)((-Level.Offset.X+ 400) / 25f)) + marginX; x++)
            {
                for (int y = (int)((-Level.Offset.Y + 380) / 25f) - marginY; y < ((int)((-Level.Offset.Y + 380) / 25f) + marginY); y++)
                {
                    if (((((x > -1) && (y > -1)) && (x < SquareObject.sqObjectArray.GetLength(0))) && (y < SquareObject.sqObjectArray.GetLength(1))) && (sqObjectArray[x, y] != null))
                    {
                        sqObjectArray[x, y].Draw(spriteBatch);
                    }
                }
            }
        }



        public Rectangle BottomEdge
        {
            get
            {
                return new Rectangle((int)Position.X, (int)(Position.Y + (Texture.Height * Size)) - 3, (int)(Texture.Width * Size), 3);
            }
        }

        public Rectangle LeftEdge
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y + 1, 3, (int)(Texture.Height * Size) - 2);
            }
        }

        public Rectangle RightEdge
        {
            get
            {
                return new Rectangle((int)(Position.X + (Texture.Width * Size)) - 3, (int)Position.Y + 1, 3, (int)(Texture.Height * Size) - 2);
            }
        }

        public Rectangle TopEdge
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * Size), 3);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Bounce
        {
            public float Top;
            public float Bottom;
            public float Left;
            public float Right;
            public Bounce(int top, int left, int right, int bottom)
            {
                this.Top = top;
                this.Bottom = bottom;
                this.Left = left;
                this.Right = right;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Damage
        {
            public bool Top;
            public bool Left;
            public bool Right;
            public bool Bottom;
            
            public Damage(byte top, byte bottom, byte left, byte right)
            {
                this.Top = top != 0;
                this.Bottom = bottom != 0;
                this.Left = left != 0;
                this.Right = right != 0;
            }
            
        }
    }
}

