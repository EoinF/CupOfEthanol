namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class RigidObject : GameObject
    {
        private Vector2 _velocity;
        public byte frictionforce;

        public RigidObject(string texture, Vector2 position, float layer) : base(texture, position, layer)
        {
            this._velocity = new Vector2();
        }

        public RigidObject(string texture, Vector2 position, float layer, float size) : base(texture, position, layer, size)
        {
            this._velocity = new Vector2();
        }

        public RigidObject(string texture, Vector2 position, float layer, float size, byte frictionForce) : base(texture, position, layer, size)
        {
            this._velocity = new Vector2();
            this.frictionforce = frictionForce;
        }

        public RigidObject(string texture, Vector2 position, float layer, float size, byte frictionForce, Color colour) : base(texture, position, layer, size, colour)
        {
            this._velocity = new Vector2();
            this.frictionforce = frictionForce;
        }

        public void Update(float gravity)
        {
            this.Velocity += new Vector2(0f, gravity * 0.22f);
            if (this.Velocity.Y > 10f)
            {
                this.Velocity = new Vector2(this.Velocity.X, 10f);
            }
            base.Position += this.Velocity;
        }

        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * Size), (int)(Texture.Height * Size));
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return this._velocity;
            }
            set
            {
                this._velocity = value;
            }
        }
    }
}

