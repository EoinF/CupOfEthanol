namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;

    public class PPlayer
    {
        private static int soundwave;
        public static int walkingSound_wave
        {
            get { return soundwave; }
            set 
            {
                if (MainMethod.rand.Next(0, 4) != 1 || soundwave == 0 || value == -1)
                    soundwave = value % 15;
            }
        }


        public bool HasRedKey;
        public bool HasBlueKey;
        public bool HasGreenKey;
        public bool HasYellowKey;

        public static bool HadRedKey = false;
        public static bool HadBlueKey = false;
        public static bool HadGreenKey = false;
        public static bool HadYellowKey = false;

        public string _JumpAnim = "";
        public string _WalkAnim = "";
        public static float MoveSpeed = 10;
        public static float JumpHeight = 10;
        public static int CurrentCheckpoint = -1;
        private int currentFrame = 0;
        public static int DeathCountdown = 0;
        private int frameCount = 3;
        public int JumpTimeout = 0;
        public bool OnGround = false;
        public Vector2 PlatformVelocity = Vector2.Zero;
        public static PPlayer Player;
        public Vector2 PreviousPlatVelocity = Vector2.Zero;
        public bool PushedHor = false;
        public bool PushedVer = false;
        private Rectangle sourceRect;
        private int spriteHeight;
        private int spriteWidth = 13;
        public SquareObject sqobject;
        private float timer = 0f;
        public bool Walking = false;

        public static bool _colission = false;
        public static bool Colission
        {
            get { return _colission; }
            set
            {
                if (!_colission)
                    _colission = value;
            }
        }
        public static void ResetColission()
        {
            _colission = false;
        }


        public PPlayer(string texture, string walktexture, string jumptexture, float size)
        {
            this._JumpAnim = jumptexture;
            this.sqobject = new SquareObject(texture, new Vector2(400f, 300f), 0.85f, size);
            this._WalkAnim = walktexture;
            DeathCountdown = -1;
        }

        public void ChangeStats(int mapbottom)
        {
            this.OnGround = false;
            this.JumpTimeout--;
            if ((this.sqobject.Position.Y / 25f) > (mapbottom + 10))
            {
                this.Die(30);
            }
        }

        public void ChangeVelocity()
        {
            this.sqobject.Velocity = new Vector2(sqobject.Velocity.X / (1f + (Level.AirResistance * 0.03f)), sqobject.Velocity.Y / (1f + (Level.AirResistance * 0.03f)));
            this.sqobject.Velocity += Level.Gravity * 0.22f;

            if (PlatformVelocity.Y < 0)
                PlatformVelocity.Y = 0;

            this.sqobject.Position += this.PlatformVelocity;
            if (!OnGround)
            {
                this.sqobject.Velocity += this.PlatformVelocity;
                this.PlatformVelocity *= 0;
            }
            //if (this.sqobject.Velocity.Y < -16f)
            {
            //    this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, -16f);
            }
        }

        public void CheckColissions(Vector2 vector)
        {
            Vector2 PlusorNothing = vector;
            if (PlusorNothing.X < 0)
                PlusorNothing = new Vector2(0, PlusorNothing.Y);
            if (PlusorNothing.Y < 0)
                PlusorNothing = new Vector2(PlusorNothing.X, 0);

            Vector2 MinusorNothing = vector;
            if (MinusorNothing.X > 0)
                MinusorNothing = new Vector2(0, MinusorNothing.Y);
            if (MinusorNothing.Y > 0)
                MinusorNothing = new Vector2(MinusorNothing.X, 0);
            try
            {
                int checks = 0;
                for (int i = 0; i < Entity.EntityList.Count; i++)
                {
                    if (Entity.EntityList[i].Active)
                        if (Vector2.Distance(Entity.EntityList[i].sqobject.Position, this.sqobject.Position) < 300)
                        {
                            if (Entity.EntityList[i].DeathTimer < 0f)
                            {
                                Player.CheckEnemyCollisions(i);
                                checks++;
                            }
                        }
                }

                checks++;
                for (int x = (int)((this.sqobject.Position.X + MinusorNothing.X - 10) / 25f); x < (int)((this.sqobject.Position.X + PlusorNothing.X + (this.sqobject.Texture.Width * sqobject.Size) + 10) / 25f) + 1; x++)
                {
                    for (int y = (int) ((this.sqobject.Position.Y + MinusorNothing.Y - 10) / 25f); y < (int)((this.sqobject.Position.Y + PlusorNothing.Y + (this.sqobject.Texture.Height * sqobject.Size) + 10) / 25f) + 1; y++)
                    {
                        if (((((x > -1) && (y > -1)) && (x < SquareObject.sqObjectArray.GetLength(0))) && (y < SquareObject.sqObjectArray.GetLength(1))) && (SquareObject.sqObjectArray[x, y] != null))
                        {
                            Player.CheckStaticSquareCollisions(x, y);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed to check collisions for player", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        public void CheckEnemyCollisions(int num)
        {
            try
            {
                Entity ent = Entity.EntityList[num];
                if (this.MiddleRectangle.Intersects(ent.sqobject.BottomEdge) && !this.MiddleRectangle.Intersects(ent.sqobject.TopEdge)
                && this.sqobject.Velocity.Y < 1)
                {
                    JumpTimeout = 0;

                    if (this.sqobject.CollideTop(ent.sqobject))
                    {
                        this.Die();
                    }
                    if (ent.sqobject.damage.Bottom)
                    {
                        this.Die();
                    }
                    else
                    {
                        if (Entity.EntityList[num].Lives != 0)
                            Entity.EntityList[num].Lives--;
                    }
                    if (Entity.EntityList[num].sqobject.Velocity.Y >= 0f)
                    {
                        sqobject.HitHead = true;
                    }
                    if (Entity.EntityList[num].sqobject.Velocity.Y > 0f)
                    {
                        this.PushedVer = true;
                    }
                }
                if ((this.MiddleRectangle.Intersects(ent.sqobject.TopEdge) && !this.MiddleRectangle.Intersects(ent.sqobject.BottomEdge))
                    && (Player.sqobject.Velocity.Y > ent.sqobject.Velocity.Y))
                {
                    if ((this.sqobject.rect.Bottom) - ent.sqobject.Position.Y <= 4)
                    {
                        this.sqobject.frictionforce = ent.sqobject.frictionforce;
                        if (!sqobject.HitFeet)
                        {
                            if (ent.sqobject.Velocity.X != 0f)
                            {
                                if (ent.sqobject.Velocity.X > 0f)
                                {
                                    if (this.sqobject.Velocity.X < (ent.sqobject.Velocity.X * (ent.Speed / 20f)))
                                    {
                                        this.PlatformVelocity.X += (ent.sqobject.Velocity.X * (ent.Speed / 20f)) / 10f;
                                    }
                                }
                                else
                                {
                                    if (this.sqobject.Velocity.X > (ent.sqobject.Velocity.X * (ent.Speed / 20f)))
                                    {
                                        this.PlatformVelocity.X += (ent.sqobject.Velocity.X * (ent.Speed / 20f)) / 10f;
                                    }
                                }
                                if ((ent.sqobject.Velocity.X > 0f) && (this.PlatformVelocity.X > (ent.sqobject.Velocity.X * (ent.Speed / 20f))))
                                {
                                    this.PlatformVelocity.X = ent.sqobject.Velocity.X * (ent.Speed / 20f);
                                }
                                else if ((ent.sqobject.Velocity.X < 0f) && (this.PlatformVelocity.X < (ent.sqobject.Velocity.X * (ent.Speed / 20f))))
                                {
                                    this.PlatformVelocity.X = ent.sqobject.Velocity.X * (ent.Speed / 20f);
                                }
                            }
                            else
                            {
                                if (this.PlatformVelocity.X != 0)
                                    this.PlatformVelocity.X /= (sqobject.frictionforce / 50);
                            }

                            if (ent.sqobject.Velocity.Y != 0f)
                            {
                                if (ent.sqobject.Velocity.Y < 0f)
                                {
                                    if (this.sqobject.Velocity.Y > ent.sqobject.Velocity.Y)
                                    {
                                        this.PlatformVelocity.Y += ent.sqobject.Velocity.Y / 20f;
                                    }
                                }
                                else if ((ent.sqobject.Velocity.Y < 0f) && (this.PlatformVelocity.Y < ent.sqobject.Velocity.Y))
                                {
                                    this.PlatformVelocity.Y = ent.sqobject.Velocity.Y;
                                }
                            }
                            else
                            {
                                if (this.PlatformVelocity.Y != 0)
                                    this.PlatformVelocity.Y /= (sqobject.frictionforce / 50);
                            }
                        }

                        if (ent.sqobject.bounce.Top > 0f)
                        {
                            this.sqobject.Velocity = new Vector2(this.sqobject.Velocity.X, 0f);
                        }
                        if (this.sqobject.CollideBottom(ent.sqobject))
                        {
                            this.Die();
                        }
                        if (ent.sqobject.damage.Top)
                        {
                            this.Die();
                        }
                        else
                        {
                            if (Entity.EntityList[num].Lives != 0)
                                Entity.EntityList[num].Lives--;
                        }
                        this.OnGround = true;
                        if (Entity.EntityList[num].sqobject.Velocity.Y <= 0f)
                        {
                            this.sqobject.HitFeet = true;
                        }
                        if (Entity.EntityList[num].sqobject.Velocity.Y < 0f)
                        {
                            this.PushedVer = true;
                        }
                    }
                }
                if (this.MiddleRectangle.Intersects(Entity.EntityList[num].sqobject.LeftEdge) || this.ArmsRectangle.Intersects(Entity.EntityList[num].sqobject.LeftEdge))
                {
                    if (this.sqobject.CollideRight(Entity.EntityList[num].sqobject) || Entity.EntityList[num].sqobject.damage.Left)
                    {
                        this.Die();
                    }
                    else
                    {
                        if (Entity.EntityList[num].Lives != 0)
                            Entity.EntityList[num].Lives--;
                    }
                    this.sqobject.Velocity += new Vector2(ent.sqobject.Velocity.X * (ent.Speed / 20f), 0f);
                    if (Entity.EntityList[num].sqobject.Velocity.X <= 0f)
                    {
                        this.sqobject.HitRight = true;
                    }
                    if (Entity.EntityList[num].sqobject.Velocity.X < 0f)
                    {
                        this.PushedHor = true;
                    }
                }
                if (this.MiddleRectangle.Intersects(Entity.EntityList[num].sqobject.RightEdge) || this.ArmsRectangle.Intersects(Entity.EntityList[num].sqobject.RightEdge))
                {
                    if (this.sqobject.CollideLeft(Entity.EntityList[num].sqobject) || Entity.EntityList[num].sqobject.damage.Right)
                    {
                        this.Die();
                    }
                    else
                    {
                        if (Entity.EntityList[num].Lives != 0)
                            Entity.EntityList[num].Lives--;
                    }
                    this.sqobject.Velocity += new Vector2(ent.sqobject.Velocity.X * (ent.Speed / 20f), 0f);
                    //if (Entity.EntityList[num].sqobject.Velocity.X >= 0f)
                    {
                        this.sqobject.HitLeft = true;
                    }
                    if (Entity.EntityList[num].sqobject.Velocity.X > 0f)
                    {
                        this.PushedHor = true;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed to check entity collisions for player", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        public void CheckStaticSquareCollisions(int x, int y)
        {
            bool col = false;
            try
            {
                if (this.sqobject.rect.Intersects(SquareObject.sqObjectArray[x, y].rect))
                {
                    if (this.sqobject.Velocity.Y >= 0) //&& !sqobject.HitFeet)
                    {
                        if (y == 0)
                        {
                            if (this.BottomSquareCollision(SquareObject.sqObjectArray[x, y]))
                                col = true;
                        }
                        else if (SquareObject.sqObjectArray[x, y - 1] == null)
                        {
                            if (this.BottomSquareCollision(SquareObject.sqObjectArray[x, y]))
                                col = true;
                        }
                    }
                    if (this.sqobject.Velocity.Y <= Level.Gravity.Y / 2)// && !sqobject.HitHead)
                    {
                        if (y == (SquareObject.sqObjectArray.GetLength(1)))
                        {
                            this.TopSquareCollision(SquareObject.sqObjectArray[x, y]);
                        }
                        else if (SquareObject.sqObjectArray[x, y + 1] == null)
                        {
                            this.TopSquareCollision(SquareObject.sqObjectArray[x, y]);
                        }
                    }

                    if (this.sqobject.Velocity.X <= 0 && !col && !sqobject.HitLeft)
                    {
                        if (x == (SquareObject.sqObjectArray.GetLength(0)))
                        {
                            this.LeftSquareCollision(SquareObject.sqObjectArray[x, y]);
                        }
                        else if (SquareObject.sqObjectArray[x + 1, y] == null)
                        {
                            this.LeftSquareCollision(SquareObject.sqObjectArray[x, y]);
                        }
                        else if (SquareObject.sqObjectArray[x + 1, y].texturename.Contains("Spk"))
                        {
                            this.LeftSquareCollision(SquareObject.sqObjectArray[x, y]);
                        }
                    }
                    if (this.sqobject.Velocity.X >= 0 && !col && !sqobject.HitRight)
                    {
                        if (x == 0)
                        {
                            this.RightSquareCollision(SquareObject.sqObjectArray[x, y]);
                        }
                        else if (SquareObject.sqObjectArray[x - 1, y] == null)
                        {
                            this.RightSquareCollision(SquareObject.sqObjectArray[x, y]);
                        }
                        else if (SquareObject.sqObjectArray[x - 1, y].texturename.Contains("Spk"))
                        {
                            this.RightSquareCollision(SquareObject.sqObjectArray[x, y]);
                        }
                    }


                    if (!Colission)
                    ResetColission();
                }
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed to check static collisions for player", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        public void Die()
        {
            DeathCountdown = 60;
            this.Walking = false;
        }

        public void Die(int x)
        {
            DeathCountdown = x;
            this.Walking = false;
        }


        public void Dying()
        {
            this.sqobject.Colour = new Color(1f, 1f, 1f, 0.02f * DeathCountdown);
            DeathCountdown--;
        }

        public bool IsDying()
        {
            return (DeathCountdown > 0);
        }

        public bool LeftSquareCollision(SquareObject sq)
        {
            if (Player.MiddleRectangle.Intersects(sq.RightEdge) || this.ArmsRectangle.Intersects(sq.RightEdge))
            {
                if (this.sqobject.CollideLeft(sq) || sq.damage.Right)
                {
                    this.Die();
                }
                this.sqobject.HitLeft = true;
                return true;
            }
            return false;
        }

        public bool BottomSquareCollision(SquareObject sq)
        {
            if (this.MiddleRectangle.Intersects(sq.TopEdge) && !this.MiddleRectangle.Intersects(sq.BottomEdge))
            {
                if ((this.sqobject.rect.Bottom) - sq.Position.Y <= 10)
                {
                    this.OnGround = true;
                    if (Player.sqobject.CollideBottom(sq) || sq.damage.Top)
                    {
                        this.Die();
                    }
                    if ((InputManager.Keystate[0].IsKeyDown(Keys.W) || InputManager.Keystate[0].IsKeyDown(Keys.Up)) && (sq.bounce.Top > 0f))
                    {
                        this.sqobject.Velocity += new Vector2(0f, -(2f + (0.02f * sq.bounce.Top)));
                        this.OnGround = false;
                    }
                    Player.sqobject.frictionforce = sq.frictionforce;
                    this.sqobject.HitFeet = true;

                    return true;
                }
            }
            /*
            if (!sqobject.HitFeet && !Walking)
            {
                if (sqobject.BottomEdge.Intersects(sq.rect))
                {
                    while (sqobject.BottomEdge.Intersects(sq.rect))
                        sqobject.Position -= new Vector2(0, 0.01f);
                    sqobject.HitFeet = true;
                }
            }
            */
            return false;
        }

        public bool RightSquareCollision(SquareObject sq)
        {
            if (Player.MiddleRectangle.Intersects(sq.LeftEdge) || this.ArmsRectangle.Intersects(sq.LeftEdge))
            {
                if (this.sqobject.CollideRight(sq) || sq.damage.Left)
                {
                    this.Die();
                }
                this.sqobject.HitRight = true;
                return true;
            }
            return false;
        }

        public bool TopSquareCollision(SquareObject sq)
        {
            if (this.MiddleRectangle.Intersects(sq.BottomEdge) && !this.MiddleRectangle.Intersects(sq.TopEdge))
            {
                if (Player.sqobject.CollideTop(sq) || sq.damage.Bottom)
                {
                    this.Die();
                }
                return true;
            }

            /*if (!sqobject.HitHead && !Walking)
            {
                if (sqobject.TopEdge.Intersects(sq.rect))
                {
                    while (sqobject.TopEdge.Intersects(sq.rect))
                        sqobject.Position += new Vector2(0, 0.01f);
                    sqobject.HitHead = true;
                }
            }
            */
            return false;
        }

        public void Setup()
        {
            spriteHeight = this.sqobject.Texture.Height;
            for (int c = 0; c < Checkpoint.checkpointList.Count; c++)
            {
                if (Checkpoint.checkpointList[c].ID == CurrentCheckpoint)
                    sqobject.Position = Checkpoint.checkpointList[c].collectable.Position - new Vector2(0, 25);

            }

            HasBlueKey = HadBlueKey;
            HasGreenKey = HadGreenKey;
            HasRedKey = HadRedKey;
            HasYellowKey = HadYellowKey;
        }



        public void Update()
        {
            if (this.IsDying())
            {
                this.Dying();
                this.sqobject.Velocity += Level.Gravity * 0.05f;
                this.sqobject.Position += this.sqobject.Velocity;
                return;
            }
            this.ChangeVelocity();
            this.ChangeStats(SquareObject.sqObjectArray.GetLength(1));

            if (this.sqobject.Velocity != Vector2.Zero)
            {
                Vector2 vector = this.sqobject.Velocity;
                vector = this.sqobject.Velocity;
                vector.Normalize();
                vector *= 1;

                float checks = sqobject.Velocity.Length() / 1;

                while (checks > 1)
                {
                    this.sqobject.Position += vector;
                    this.CheckColissions(vector);
                    checks--;
                    if (sqobject.HitHead || sqobject.HitFeet)
                    {
                        vector = Vector2.UnitX * vector.X;
                        if (sqobject.HitLeft || sqobject.HitRight)
                        {
                            vector = Vector2.Zero;
                            checks -= (int)checks;
                            break;
                        }
                    }
                    if (sqobject.HitLeft || sqobject.HitRight)
                        vector = Vector2.UnitY * vector.Y;
                }

                this.sqobject.Position += vector * checks;
                this.CheckColissions(vector);
            }
            //this.CheckColissions(Vector2.Zero);
            if (this.sqobject.HitFeet || this.sqobject.HitHead)
                this.JumpTimeout = 0;

            if (this.sqobject.HitHead && this.sqobject.HitFeet && this.PushedVer)
                this.Die();
            
            if (this.sqobject.HitLeft && this.sqobject.HitRight && this.PushedHor)
                this.Die();
            
            this.sqobject.HitHead = false;
            this.sqobject.HitFeet = false;
            this.sqobject.HitLeft = false;
            this.sqobject.HitRight = false;
            this.PushedVer = false;
            this.PushedHor = false;
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            if (!(this.Walking || (this.JumpTimeout >= 10)))
            {
                this.sqobject.Draw(spriteBatch);
            }
            else
            {
                float interval;
                Texture2D Animation;
                if (!((this.JumpTimeout <= 10) || ScreenManager.Paused))
                {
                    Animation = this.JumpAnim;
                    interval = 200f;
                    this.spriteWidth = this.JumpAnim.Width / 3;
                    this.spriteHeight = this.JumpAnim.Height;
                }
                else
                {
                    Animation = this.WalkAnim;
                    interval = 100f;
                    this.spriteWidth = this.WalkAnim.Width / 3;
                    this.spriteHeight = this.WalkAnim.Height;
                }
                this.timer += (float) gametime.ElapsedGameTime.TotalMilliseconds;
                if (this.timer > interval)
                {
                    this.currentFrame++;
                    if (this.currentFrame > (this.frameCount - 1))
                    {
                        this.currentFrame = 0;
                    }
                    this.timer = 0f;
                }
                this.sourceRect = new Rectangle(this.currentFrame * this.spriteWidth, 0, this.spriteWidth, this.spriteHeight);
                spriteBatch.Draw(Animation, this.sqobject.Position + Level.Offset, new Rectangle?(this.sourceRect), this.sqobject.Colour, 0f, Vector2.Zero, this.sqobject.Size, this.sqobject.Flipeffect, 0.8f);
            }

            DrawHUD(spriteBatch);
        }

        public void DrawHUD(SpriteBatch spriteBatch)
        {
            Vector2 DrawPos = new Vector2(600, 20);
            if (Player.HasRedKey)
            {
                DrawKey(spriteBatch, DrawPos, "RedKey");
                DrawPos += new Vector2(20, 0);
            }
            if (Player.HasBlueKey)
            {
                DrawKey(spriteBatch, DrawPos, "BlueKey");
                DrawPos += new Vector2(20, 0);
            }
            if (Player.HasGreenKey)
            {
                DrawKey(spriteBatch, DrawPos, "GreenKey");
                DrawPos += new Vector2(20, 0);
            }
            if (Player.HasYellowKey)
            {
                DrawKey(spriteBatch, DrawPos, "YellowKey");
            }
        }

        public static void DrawKey(SpriteBatch spriteBatch, Vector2 Position, string Texture)
        {
            spriteBatch.Draw(Textures.GetTexture(Texture), Position, null, Color.WhiteSmoke, 0, Vector2.Zero, 0.3f, SpriteEffects.None, 0.95f);

        }

        public Rectangle ArmsRectangle
        {
            get
            {
                return new Rectangle((int)sqobject.Position.X, (int)sqobject.Position.Y + (int)(11 * sqobject.Size), (int)((sqobject.Texture.Width * sqobject.Size) + 0.5f), (int)(19f * sqobject.Size));
            }
        }

        public Texture2D JumpAnim
        {
            get
            {
                return Textures.GetTexture(_JumpAnim);
            }
        }

        public Rectangle MiddleRectangle
        {
            get
            {
                return new Rectangle((int)sqobject.Position.X + 3, (int)sqobject.Position.Y, (int)((sqobject.Texture.Width * sqobject.Size) + 0.5f) - 6, (int)(sqobject.Texture.Height * sqobject.Size));
            }
        }

        public Texture2D WalkAnim
        {
            get
            {
                return Textures.GetTexture(this._WalkAnim);
            }
        }
    }
}

