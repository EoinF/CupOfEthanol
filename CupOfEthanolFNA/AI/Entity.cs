namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public partial class Entity
    {
        public string _jumpAnim;
        public string _walkAnim;

        public bool Active;

        private int currentFrame;
        public float DeathTimer;
        public static List<Entity> EntityList;
        //public float FreezeTimer;
        public int StartDelay;

        private int ID;
        public string Job;
        public int JumpTimeout;
        private int _lives;
        public static int MaxID;
        public bool OnGround;
        public static Random rand = new Random();
        public float Speed;
        public SquareObject sqobject;

        public byte StartCheckpoint;
        public byte EndCheckpoint;

        private Vector2 StartPoint;
        public bool StaticTexture;
        private float timer;
        private float VariableA;
        private float VariableB;
        private float VariableC;
        public float VariableD;
        private int VariableE;
        public float VariableF;
        public bool Walking;

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }


        public Entity(string texture, string jumpAnimation, string walkAnimation, Vector2 position, int lives, float size, string job, SquareObject.Damage dmg, SquareObject.Bounce bnce, float speed, byte friction, string OppositeDir, bool staticTexture, int id, byte startCheckpoint, byte endCheckpoint)
        {
            StartPoint = position;
            _lives = lives;
            Job = job;
            sqobject = new SquareObject(texture, position, dmg, bnce, 0.9f, size, friction, Color.White);
            Speed = speed;
            _walkAnim = walkAnimation;
            _jumpAnim = jumpAnimation;
            if (OppositeDir == "t")
            {
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
            }
            this.StaticTexture = staticTexture;
            this.ID = id;
            Active = true;

            DeathTimer = -1;
            StartDelay = 0;
            JumpTimeout = -1;
            MaxID = 1;
            VariableA = 0f;
            VariableB = 0f;
            VariableC = 0f;
            VariableD = 0f;
            VariableE = -1;
            VariableF = 0;
            OnGround = false;
            Walking = false;
            timer = -1;
            currentFrame = 0;

            SaveStatus();
            OriginalColour = sqobject.Colour;
            OriginalFlipEffect = this.sqobject.Flipeffect;

            StartCheckpoint = startCheckpoint;
            EndCheckpoint = endCheckpoint;
        }

        public Entity(string texture, string jumpAnimation, string walkAnimation, Vector2 position, int lives, float size, string job, SquareObject.Damage dmg, SquareObject.Bounce bnce, float speed, byte friction, string OppositeDir, bool staticTexture, int id, byte startCheckpoint, byte endCheckpoint, Color colour)
        {
            StartPoint = position;
            _lives = lives;
            Job = job;
            sqobject = new SquareObject(texture, position, dmg, bnce, 0.9f, size, friction, colour);
            Speed = speed;
            _walkAnim = walkAnimation;
            _jumpAnim = jumpAnimation;
            if (OppositeDir == "t")
            {
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
            }
            StaticTexture = staticTexture;
            ID = id;
            Active = true;

            DeathTimer = -1;
            StartDelay = 0;
            JumpTimeout = -1;
            MaxID = 1;
            VariableA = 0f;
            VariableB = 0f;
            VariableC = 0f;
            VariableD = 0f;
            VariableE = -1;
            VariableF = 0;
            OnGround = false;
            Walking = false;
            timer = -1;
            currentFrame = 0;

            SaveStatus();
            OriginalColour = sqobject.Colour;
            OriginalFlipEffect = this.sqobject.Flipeffect;

            StartCheckpoint = startCheckpoint;
            EndCheckpoint = endCheckpoint;
        }


        public Entity(string texture, string jumpAnimation, string walkAnimation, Vector2 position, int lives, float size, string job, SquareObject.Damage dmg, SquareObject.Bounce bnce, float speed, byte friction, string OppositeDir, bool staticTexture, int id, byte startCheckpoint, byte endCheckpoint, Color colour, int startDelay)
        {
            StartPoint = position;
            _lives = lives;
            Job = job;
            sqobject = new SquareObject(texture, position, dmg, bnce, 0.9f, size, friction, colour);
            Speed = speed;
            _walkAnim = walkAnimation;
            _jumpAnim = jumpAnimation;
            if (OppositeDir == "t")
            {
                this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
            }
            StaticTexture = staticTexture;
            ID = id;
            Active = true;

            DeathTimer = -1;
            StartDelay = startDelay;
            JumpTimeout = -1;
            MaxID = 1;
            VariableA = 0f;
            VariableB = 0f;
            VariableC = 0f;
            VariableD = 0f;
            VariableE = -1;
            VariableF = 0;
            OnGround = false;
            Walking = false;
            timer = -1;
            currentFrame = 0;

            SaveStatus();
            OriginalColour = sqobject.Colour;
            OriginalFlipEffect = this.sqobject.Flipeffect;

            StartCheckpoint = startCheckpoint;
            EndCheckpoint = endCheckpoint;
        }


        private bool BlockExists_Below(int HorizontalBlocks, float Verticalmargin)
        {
            int[] coord = new int[] { (int) (this.sqobject.Position.X / 25f), (int) ((((sqobject.Position.Y + sqobject.Texture.Height) + 25f) - Verticalmargin + 0.001f) / 25f) };
            coord[0] += HorizontalBlocks;
            coord[1]--;
            if (coord[0] > -1
                && coord[0] < SquareObject.sqObjectArray.GetLength(0)
                && coord[1] > -1
                && coord[1] < SquareObject.sqObjectArray.GetLength(1))
                return SquareObject.sqObjectArray[coord[0], coord[1]] != null;
            else
                return false;
        }

        private bool BlockExists_Left(int VerticalBlocks, float Horizontalmargin, float Verticalmargin)
        {
            //int[] coord = new int[] { (int)(((this.sqobject.Position.X + 25f) - Horizontalmargin - 0.001f) / 25f), (int)((sqobject.Position.Y + sqobject.Texture.Height + Verticalmargin) / 25f) };
            int[] coord = new int[] { (int)((this.sqobject.Position.X - Horizontalmargin) / 25f), (int)((sqobject.Position.Y + sqobject.Texture.Height - Verticalmargin) / 25f) };
            
            coord[1] += VerticalBlocks;

            if (coord[0] >= 0
                && coord[0] < SquareObject.sqObjectArray.GetLength(0)
                && coord[1] >= 0
                && coord[1] < SquareObject.sqObjectArray.GetLength(1))
            {
                if (MainMethod.DebugMode)
                {
                    if (SquareObject.sqObjectArray[coord[0], coord[1]] != null)
                    {
                        if (SquareObject.sqObjectArray[coord[0], coord[1]].Colour != Color.Black)
                            SquareObject.sqObjectArray[coord[0], coord[1]].Colour = Color.Black;
                        else
                            SquareObject.sqObjectArray[coord[0], coord[1]].Colour = Color.White;
                    }
                }

                return (SquareObject.sqObjectArray[coord[0], coord[1]] != null
                    && SquareObject.sqObjectArray[coord[0], coord[1]].texturename != "e"
                    && SquareObject.sqObjectArray[coord[0], coord[1]].texturename != "j"
                    && SquareObject.sqObjectArray[coord[0], coord[1]].texturename != "n");
            }
            else return false;
        }

        private bool BlockExists_Right(int VerticalBlocks, float Horizontalmargin, float Verticalmargin)
        {
            //int[] coord = new int[] { (int)((((this.sqobject.Position.X + sqobject.Texture.Width) - 25f) - Horizontalmargin + 0.001f) / 25f), (int)((sqobject.Position.Y + sqobject.Texture.Height + Verticalmargin) / 25f) };
            int[] coord = new int[] { (int)((this.sqobject.Position.X + sqobject.Texture.Width + Horizontalmargin)/ 25f), (int)((sqobject.Position.Y + sqobject.Texture.Height - Verticalmargin) / 25f)};
            
            coord[1] += VerticalBlocks;
            if (coord[0] >= 0
                && coord[0] < SquareObject.sqObjectArray.GetLength(0)
                && coord[1] >= 0
                && coord[1] < SquareObject.sqObjectArray.GetLength(1))
            {
                if (MainMethod.DebugMode)
                {
                    if (SquareObject.sqObjectArray[coord[0], coord[1]] != null)
                    {
                        if (SquareObject.sqObjectArray[coord[0], coord[1]].Colour != Color.Black)
                            SquareObject.sqObjectArray[coord[0], coord[1]].Colour = Color.Black;
                        else
                            SquareObject.sqObjectArray[coord[0], coord[1]].Colour = Color.White;

                    }
                }
                return (SquareObject.sqObjectArray[coord[0], coord[1]] != null
                    && SquareObject.sqObjectArray[coord[0], coord[1]].texturename != "e"
                    && SquareObject.sqObjectArray[coord[0], coord[1]].texturename != "j"
                    && SquareObject.sqObjectArray[coord[0], coord[1]].texturename != "n");
            }
            else return false;
        }

        /*
        private bool BlockExists_Left(int VerticalBlocks, float Horizontalmargin, float Verticalmargin)
        {
            int[] coord = new int[] { (int)(((this.sqobject.Position.X + 25f + sqobject.Velocity.X) - Horizontalmargin - 0.001f) / 25f), (int)((sqobject.Position.Y + sqobject.Texture.Height + sqobject.Velocity.Y + Verticalmargin) / 25f) };
            coord[0]--;
            coord[1] += VerticalBlocks;

            if (coord[0] >= 0
                && coord[0] < SquareObject.sqObjectArray.GetLength(0)
                && coord[1] >= 0
                && coord[1] < SquareObject.sqObjectArray.GetLength(1))
                return (SquareObject.sqObjectArray[coord[0], coord[1]] != null);
            return true;
        }

        private bool BlockExists_Right(int VerticalBlocks, float Horizontalmargin, float Verticalmargin)
        {
            int[] coord = new int[] { (int) ((((this.sqobject.Position.X + sqobject.Texture.Width + sqobject.Velocity.X) - 25f) + Horizontalmargin + 0.001f) / 25f), (int) ((sqobject.Position.Y + sqobject.Texture.Height + sqobject.Velocity.Y + Verticalmargin) / 25f) };
            coord[0]++;
            coord[1] += VerticalBlocks;
            if (coord[0] >= 0
                && coord[0] < SquareObject.sqObjectArray.GetLength(0)
                && coord[1] >= 0
                && coord[1] < SquareObject.sqObjectArray.GetLength(1))
                return SquareObject.sqObjectArray[coord[0], coord[1]] != null;
            else return false;
        }
        */
        private bool CheckChildQuantity(int q)
        {
            int i = 0;
            for (int k = 0; k < EntityList.Count; k++)
            {
                if (EntityList[k].ID == this.ID)
                {
                    i++;
                    if (Vector2.Distance(this.sqobject.Position, EntityList[k].sqobject.Position) > 2000f)
                    {
                        EntityList[k].DeathTimer = 0f;
                    }
                }
            }
            return (i < (q + 1));
        }

        /// <summary>
        /// Divides up the velocity into smaller segments and adds velocity to position bit by bit
        /// </summary>
        private void CheckColissions_Precise(bool CheckTiles, bool CheckEntitys)
        {
            if (sqobject.Velocity == Vector2.Zero) //You cannot normalize a zero vector so it breaks
                return;

            Vector2 vector = new Vector2(sqobject.Velocity.X * (this.Speed / 20f), sqobject.Velocity.Y);

            //Vector2 vector = this.sqobject.Velocity;
            //vector = this.sqobject.Velocity;
            vector.Normalize();
            vector *= 5;

            float checks = new Vector2(sqobject.Velocity.X * (this.Speed / 20f), sqobject.Velocity.Y).Length() / 5; //This varies how accurately it checks. A lower number means more accuracy but less performance


            //float checks = vector1.Length() / unitvector.Length();
            for (int c = 0; c < (int)checks; c++)
            {
                if (CheckTiles)
                    this.CheckColissions(vector);
                if (CheckEntitys)
                    this.CheckEntityCollisions();

                this.sqobject.Position += vector;

                if (sqobject.HitFeet || sqobject.HitHead)
                {
                    if (sqobject.HitLeft || sqobject.HitRight)
                    {
                        vector = Vector2.Zero;
                        break;
                    }
                    vector = new Vector2(vector.X, 0);
                }
            }
            checks -= (int)checks;

            if (CheckTiles)
                this.CheckColissions(vector * checks);
            if (CheckEntitys)
                this.CheckEntityCollisions();

            if (sqobject.HitFeet || sqobject.HitHead)
            {
                vector = new Vector2(vector.X, 0);
            }
            if (sqobject.HitLeft || sqobject.HitRight)
            {
                vector = new Vector2(0, vector.Y);
            }

            this.sqobject.Position += vector * checks;

            if (CheckTiles)
                this.CheckColissions(Vector2.Zero);
            if (CheckEntitys)
                this.CheckEntityCollisions();
            
        }


        private void CheckColissions(Vector2 vector)
        {
             
            /*if (PlusorNothing.X < 0)
                PlusorNothing = new Vector2(0, PlusorNothing.Y);
            if (PlusorNothing.Y < 0)
                PlusorNothing = new Vector2(PlusorNothing.X, 0);

            Vector2 MinusorNothing = vector;
            if (MinusorNothing.X > 0)
                MinusorNothing = new Vector2(0, MinusorNothing.Y);
            if (MinusorNothing.Y > 0)
                MinusorNothing = new Vector2(MinusorNothing.X, 0);
            */

            for (int x = (int)((sqobject.Position.X - 15) / 25f); x < (int)((sqobject.Position.X + 5 + (sqobject.Texture.Width * sqobject.Size)) / 25f) + 1; x++)
            {
                for (int y = (int)((sqobject.Position.Y - 15) / 25f); y < (int)((sqobject.Position.Y + 5 + (sqobject.Texture.Height * sqobject.Size)) / 25f) + 1; y++)
                {
                    if (((((x > -1) && (y > -1)) && (x < SquareObject.sqObjectArray.GetLength(0))) && (y < SquareObject.sqObjectArray.GetLength(1))) && (SquareObject.sqObjectArray[x, y] != null))
                    {
                        this.CheckSquareCollisions(x, y);
                    }
                }
            }
        }

        private void CheckEntityCollisions()
        {
            for (int i = 0; i < EntityList.Count; i++)
            {
                if (EntityList[i].Active)
                {
                    if (Vector2.Distance(EntityList[i].sqobject.Position, this.sqobject.Position) < 300)
                    if (!this.Equals(EntityList[i]))
                    {
                        if (EntityList[i].DeathTimer < 0)
                        {
                            if (this.TopSquareCollision(EntityList[i].sqobject))
                                EntityList[i].sqobject.CollideBottom(this.sqobject);
                            if (this.BottomSquareCollision(EntityList[i].sqobject))
                                EntityList[i].sqobject.CollideTop(this.sqobject);

                            if (this.LeftSquareCollision(EntityList[i].sqobject))
                                EntityList[i].sqobject.CollideRight(this.sqobject);
                            if (this.RightSquareCollision(EntityList[i].sqobject))
                                EntityList[i].sqobject.CollideLeft(this.sqobject);
                        }
                    }
                }
            }
        }

        private void CheckSquareCollisions(int x, int y)
        {
            try
            {
                if (this.sqobject.rect.Intersects(SquareObject.sqObjectArray[x, y].rect))
                {
                    SquareObject sq = SquareObject.sqObjectArray[x, y];
                    if (this.sqobject.Velocity.Y >= 0 && !sqobject.HitFeet)
                    {
                        if (y == 0)
                        {
                            this.BottomSquareCollision(sq);
                        }
                        else if (SquareObject.sqObjectArray[x, y - 1] == null)
                        {
                            this.BottomSquareCollision(sq);
                        }
                    }

                    if (this.sqobject.Velocity.Y <= Level.Gravity.Y / 2 && !sqobject.HitHead)
                    {
                        if (y > (SquareObject.sqObjectArray.GetLength(1) - 1))
                        {
                            this.TopSquareCollision(sq);
                        }
                        else if (SquareObject.sqObjectArray[x, y + 1] == null)
                        {
                            this.TopSquareCollision(sq);
                        }
                    }

                    if (x == 0 && !sqobject.HitLeft)
                    {
                        this.RightSquareCollision(sq);
                    }
                    else if (SquareObject.sqObjectArray[x - 1, y] == null)
                    {
                        this.RightSquareCollision(sq);
                    }
                    else if (SquareObject.sqObjectArray[x - 1, y].texturename.Contains("Spk"))
                    {
                        this.RightSquareCollision(sq);
                    }

                    if (x == (SquareObject.sqObjectArray.GetLength(0)) && !sqobject.HitRight)
                    {
                        this.LeftSquareCollision(sq);
                    }
                    else if (SquareObject.sqObjectArray[x + 1, y] == null)
                    {
                        this.LeftSquareCollision(sq);
                    }
                    else if (SquareObject.sqObjectArray[x + 1, y].texturename.Contains("Spk"))
                    {
                        this.LeftSquareCollision(sq);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed to check static collisions for an entity", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            if (this.StaticTexture)
            {
                spriteBatch.Draw(this.sqobject.Texture, this.sqobject.Position + Level.Offset, null, this.sqobject.Colour, 0f, Vector2.Zero, this.sqobject.Size, 0, 0.1f);
            }
            else if (!(this.Walking || (this.JumpTimeout >= 10)))
            {
                spriteBatch.Draw(this.sqobject.Texture, this.sqobject.Position + Level.Offset, null, this.sqobject.Colour, 0f, Vector2.Zero, this.sqobject.Size, this.sqobject.Flipeffect, 0.1f);
            }
            else if (this.DeathTimer > 0f)
            {
                spriteBatch.Draw(this.sqobject.Texture, (this.sqobject.Position + Level.Offset) + new Vector2(5f, 5f), null, this.sqobject.Colour, 0f, Vector2.Zero, this.sqobject.Size, this.sqobject.Flipeffect, 0.1f);
            }
            else
            {
                int spriteWidth;
                int spriteHeight;
                float interval;
                Texture2D Animation;
                int Totalframes = 0;
                if (this.JumpTimeout > 10)
                {
                    Animation = this.JumpAnim;
                    interval = 100f;
                    spriteWidth = this.sqobject.Texture.Width;
                    spriteHeight = this.JumpAnim.Height;
                    Totalframes = (int) ((this.JumpAnim.Width + 0.011f) / ((float) this.sqobject.Texture.Width));
                }
                else
                {
                    Animation = WalkAnim;
                    interval = 100f;
                    spriteWidth = sqobject.Texture.Width;
                    spriteHeight = WalkAnim.Height;
                    Totalframes = (int) ((this.WalkAnim.Width + 0.011f) / ((float) this.sqobject.Texture.Width));
                }
                this.timer += (float) gametime.ElapsedGameTime.TotalMilliseconds;
                if (this.timer > interval)
                {
                    this.currentFrame++;
                    if (this.currentFrame > (Totalframes - 1))
                    {
                        this.currentFrame = 0;
                    }
                    this.timer = 0f;
                }
                Rectangle sourceRect = new Rectangle(this.currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
                spriteBatch.Draw(Animation, this.sqobject.Position + Level.Offset, new Rectangle?(sourceRect), this.sqobject.Colour, 0f, Vector2.Zero, this.sqobject.Size, this.sqobject.Flipeffect, 0.1f);
            }
        }

        private void JobChecker()
        {
            //if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 1300f)
            {
                if (this.Active)
                {
                    //if (this.sqobject.Velocity.Y < 12f)
                    //{
                        this.sqobject.Velocity += Level.Gravity * 0.22f;
                    //}

                    if (this.Job.Contains("M"))
                        this.Cannon(int.Parse(this.Job.Substring(1)));

                    if (this.Job.Contains("N"))
                        this.Wheelbot(int.Parse(this.Job.Substring(1)));

                    if (this.Job.Contains("O"))
                        this.Robot(int.Parse(this.Job.Substring(1)));

                    switch (this.Job)
                    {
                        case "C1":
                            this.Bird1();
                            return;

                        case "C2":
                            this.Bird2();
                            return;

                        case "C3":
                            this.Bird3();
                            return;

                        case "C4":
                            this.Bird4();
                            return;

                        case "F1":
                        case "H1":
                            this.Dog1();
                            return;

                        case "F2":
                        case "H2":
                            this.Dog2();
                            return;

                        case "F3":
                        case "H3":
                            this.Dog3(false);
                            return;

                        case "F4":
                        case "H4":
                            this.Dog4();
                            return;

                        case "J1":
                            this.Troller1();
                            return;

                        case "J2":
                            this.Troller2();
                            return;

                        case "J3":
                            this.Troller3();
                            return;

                        case "J4":
                            this.Troller4();
                            return;

                        case "G1":
                            this.Dog1();
                            this.Wolf1();
                            return;

                        case "G2":
                            this.Dog1();
                            this.Wolf2();
                            return;

                        case "G3":
                            this.Wolf3();
                            return;

                        case "G4":
                            this.Wolf4();
                            return;

                        case "I1":
                            this.Stealth1(false);
                            return;

                        case "I2":
                            this.Stealth1(true);
                            return;

                        case "I3":
                            this.Stealth2(false);
                            return;

                        case "I4":
                            this.Stealth2(true);
                            return;

                        case "K1":
                            this.Tentacle1();
                            return;

                        case "K2":
                            this.Tentacle2();
                            return;

                        case "K3":
                            this.Tentacle3();
                            return;

                        case "K4":
                            this.Tentacle4();
                            return;

                        case "L":
                            this.TentacleSpawn();
                            return;

                        case "D1":
                        case "E1":
                            this.Platform1();
                            return;

                        case "D2":
                        case "E2":
                            this.Platform2();
                            return;

                        case "D3":
                        case "E3":
                            this.Platform3();
                            return;

                        case "D4":
                        case "E4":
                            this.Platform4();
                            return;

                        case "P1":
                            this.Shadow1();
                            return;

                        case "P2":
                            this.Shadow2();
                            return;

                        case "P3":
                            this.Shadow3();
                            return;

                        case "P4":
                            this.Shadow4();
                            return;

                        case "B1":
                            this.Shadowplayer1();
                            return;

                        case "B2":
                            this.Shadowplayer2();
                            return;

                        case "B3":
                            this.Shadowplayer3();
                            return;

                        case "B4":
                            this.Shadowplayer4();
                            return;

                        case "Q1":
                            this.Clancy1();
                            return;

                        case "Q2":
                            this.Clancy2();
                            return;

                        case "Q3":
                            this.Clancy3();
                            return;

                        case "Q4":
                            this.Clancy4();
                            return;

                        case "R1":
                            this.VanishBlock1();
                            return;

                        case "R2":
                            this.VanishBlock2();
                            return;

                        case "R3":
                            this.VanishBlock3();
                            return;

                        case "R4":
                            this.VanishBlock4();
                            return;

                        case "S1":
                            this.RainingEmber1();
                            return;

                        case "S2":
                            this.RainingEmber2();
                            return;

                        case "S3":
                            this.RainingEmber3();
                            return;

                        case "S4":
                            this.RainingEmber4();
                            return;

                        case "T1":
                            this.Icicle(1,1);
                            return;
                        case "T2":
                            this.Icicle(2,1);
                            return;
                        case "T3":
                            this.Icicle(3,1);
                            return;
                        case "T4":
                            this.Icicle(1, 2);
                            return;
                        case "T5":
                            this.Icicle(2, 2);
                            return;
                        case "T6":
                            this.Icicle(3, 2);
                            return;
                        case "T7":
                            this.Icicle(1, 4);
                            return;
                        case "T8":
                            this.Icicle(2, 4);
                            return;
                        case "T9":
                            this.Icicle(3, 4);
                            return;

                        case "U":
                            this.Maureen();
                            return;

                        case "V":
                            this.GuinnessVan();
                            return;

                        case "W1":
                            this.Blaster1();
                            return;

                        case "W2":
                            this.Blaster2();
                            return;

                        case "W3":
                            this.Blaster3();
                            return;

                        case "W4":
                            this.Blaster4();
                            return;

                        case "X1":
                            this.Door1();
                            return;

                        case "X2":
                            this.Door2();
                            return;

                        case "X3":
                            this.Door3();
                            return;

                        case "X4":
                            this.Door4();
                            return;

                        case "Particle":
                            this.Particle();
                            return;

                        case "Lazer":
                            this.Lazer();
                            return;
                    }
                    this.sqobject.Velocity = (Vector2) (this.sqobject.Velocity * 0f);
                }
            }
            //else
            {
            //    this.FreezeTimer = 150f;
            }
        }


        private void OneMove()
        {
        }

        private Vector2 PlayerLockon(float Ux)
        {
            float x = (sqobject.Position.X + (sqobject.Texture.Width / 2)) - (PPlayer.Player.sqobject.Position.X + (PPlayer.Player.sqobject.Texture.Width / 2));
            float y = (sqobject.Position.Y + (sqobject.Texture.Height / 2)) - (PPlayer.Player.sqobject.Position.Y + (PPlayer.Player.sqobject.Texture.Height / 2));
            Ux /= this.Speed / 20f;
            float g = Level.Gravity.Y * 0.22f;
            return new Vector2(Ux, -(float)(Math.Sqrt((double)(((8f * g * y) / Ux) / 2.0f))));
        }

        private bool LeftSquareCollision(SquareObject squareobject)
        {
            if (this.sqobject.LeftEdge.Intersects(squareobject.RightEdge) && !sqobject.HitLeft)
            {
                this.sqobject.CollideLeft(squareobject);
                this.VariableB = 180f;
                return true;
            }
            return false;
        }

        private bool RightSquareCollision(SquareObject squareobject)
        {
            if (sqobject.rect.Intersects(squareobject.LeftEdge) && !sqobject.HitRight)
            {
                sqobject.CollideRight(squareobject);
                VariableB = 180f;
                return true;
            }
            return false;
        }

        private bool TopSquareCollision(SquareObject squareobject)
        {
                if (this.sqobject.TopEdge.Intersects(squareobject.BottomEdge))
                {
                    this.VariableA = 180f;
                    this.sqobject.CollideTop(squareobject);
                    return true;
                }
            
                return false;
            
        }

        private bool BottomSquareCollision(SquareObject squareobject)
        {

            if (sqobject.BottomEdge.Intersects(squareobject.TopEdge))
            {
                //if ((this.sqobject.rect.Bottom) - squareobject.Position.Y <= 5)
                {
                    this.Walking = true;
                    this.OnGround = true;
                    this.sqobject.CollideBottom(squareobject);
                    this.VariableA = 180f;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The entity turns to face the player
        /// </summary>
        /// <param name="distance">The maximum distance the entity and player can be apart for turning to occur</param>
        /// <returns>Whether the entity turned or not</returns>
        private bool FacePlayer(int distance)
        {
            if (Vector2.Distance(PPlayer.Player.sqobject.Position, this.sqobject.Position) < distance)
            {
                if (PPlayer.Player.sqobject.Position.X > this.sqobject.Position.X)
                {
                    this.sqobject.Flipeffect = SpriteEffects.None;
                    return true;
                }
                if (PPlayer.Player.sqobject.Position.X < this.sqobject.Position.X)
                {
                    this.sqobject.Flipeffect = SpriteEffects.FlipHorizontally;
                    return true;
                }
            }
            return false;
        }


        public void Update(SquareObject[,] sqObjectArray)
        {
            if (!ScreenManager.Editing)
            {
                if (StartDelay <= 0)
                {
                    if (this.Lives == 0 && this.DeathTimer < 0f)
                    {
                        this.sqobject.Velocity += new Vector2(0f, -1f);
                        this.DeathTimer = 80f;
                    }
                    if (this.DeathTimer < 0f)
                    {
                        this.JobChecker();
                        this.sqobject.HitFeet = false;
                        this.sqobject.HitLeft = false;
                        this.sqobject.HitRight = false;
                        this.sqobject.HitHead = false;
                        //this.FreezeTimer--;
                    }
                    if (this.DeathTimer > 0f)
                    {
                        this.DeathTimer--;
                        this.sqobject.Velocity += (Vector2)(Level.Gravity * 0.05f);
                        this.sqobject.Position += this.sqobject.Velocity;
                        this.sqobject.Colour = new Color(1f, (this.DeathTimer / 160f), (this.DeathTimer / 80f), 0.015f * this.DeathTimer);
                    }
                }
                if (this.DeathTimer == 0f)
                {
                    this.Active = false;
                    if (this.sqobject.texturename == "LazerBullet")
                        EntityList.Remove(this);
                    this.DeathTimer--;
                }
                else
                    StartDelay--;
            }
        }

        public Texture2D JumpAnim
        {
            get
            {
                return Textures.GetTexture(this._jumpAnim);
            }
        }

        private Vector2 SpawnDisplacement
        {
            get
            {
                return new Vector2(this.StartPoint.X - this.sqobject.Position.X, this.StartPoint.Y - this.sqobject.Position.Y);
            }
        }

        public Texture2D WalkAnim
        {
            get
            {
                return Textures.GetTexture(this._walkAnim);
            }
        }

        public void ResetStatus()
        {
            Active = WasActive;
            Lives = PreviousLives;
            StartDelay = PreviousStartDelay;
            sqobject.Position = StartPoint;
            sqobject.Colour = OriginalColour;
            sqobject.Velocity = Vector2.Zero;
            sqobject.Flipeffect = OriginalFlipEffect;
            VariableA = 0;
            VariableB = 0;
            VariableC = 0;
            VariableD = 0;
            if (this.Job.StartsWith("M") || this.Job.StartsWith("N") || this.Job.StartsWith("O"))
                VariableD = 300;
            VariableE = -1;
        }

        public bool WasActive;
        public int PreviousLives;
        public Color OriginalColour;
        public SpriteEffects OriginalFlipEffect;
        public int PreviousStartDelay;

        public void SaveStatus()
        {
            WasActive = Active && DeathTimer <= 0f;
            PreviousLives = Lives;
            PreviousStartDelay = StartDelay;
        }

        public void CheckpointReached()
        {
            if (StartCheckpoint == PPlayer.CurrentCheckpoint)
            {
                this.WasActive = true;
                this.ResetStatus();
            }

            if (EndCheckpoint == PPlayer.CurrentCheckpoint)
            {
                this.Active = false;
                this.WasActive = false;
            }


        }
    }
}

