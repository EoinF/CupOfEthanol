namespace LackingPlatforms
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class Textures
    {
        private static Texture2D[] mainThumnails = new Texture2D[Level.maxLevels]; //i.e. thumnails for the main levels

        private static List<Texture2D> customThumnails; //i.e. thumnails for user created levels
        private static string[] subThumnailsNames;

        #region MenuTextures
        private static Texture2D Button1;
        private static Texture2D Button2;
        private static Texture2D Button3;
        private static Texture2D Button4;
        private static Texture2D Button5;
        private static Texture2D Cursor;
        private static Texture2D EPause_Menu;
        private static Texture2D Pause_Menu;
        #endregion

        #region Items
        private static Texture2D Chalice;
        private static Texture2D Checkpoint;
        private static Texture2D Coaster;
        private static Texture2D Sign;
        private static Texture2D RedKey;
        private static Texture2D BlueKey;
        private static Texture2D GreenKey;
        private static Texture2D YellowKey;
        #endregion

        #region BlockTextures
        private static Texture2D Bnc;
        private static Texture2D DSpk;
        private static Texture2D Grs;
        private static Texture2D Gvl;
        private static Texture2D Ice;
        private static Texture2D LSpk;
        private static Texture2D Lva;
        private static Texture2D Mag;
        private static Texture2D Mtl;
        private static Texture2D RSpk;
        private static Texture2D Snd;
        private static Texture2D UGrs;
        private static Texture2D UIce;
        private static Texture2D ULva;
        private static Texture2D UMag;
        private static Texture2D USpk;
        #endregion

        private static Texture2D SkyA;
        private static Texture2D SkyB;

        #region FontTextures
        private static SpriteFont LargeF;
        private static SpriteFont MediumF;
        private static SpriteFont SmallF;
        private static SpriteFont TinyF;
        #endregion

        #region EntityTextures
        private static Texture2D Bird;
        private static Texture2D Blaster;
        private static Texture2D Cannon;
        private static Texture2D ConcretePlatform;
        private static Texture2D Clancy;
        private static Texture2D Dog;
        private static Texture2D Dog_Walk;
        private static Texture2D Door;
        private static Texture2D Flamer;
        private static Texture2D Flamer_Walk;
        private static Texture2D GuinnessVan;
        private static Texture2D Icicle;
        private static Texture2D LazerBullet;
        private static Texture2D Maureen;
        private static Texture2D Platform;
        private static Texture2D Player;
        private static Texture2D Player_Jump;
        private static Texture2D Player_Walk;
        private static Texture2D RainingEmber;
        private static Texture2D Robot;
        private static Texture2D Shadow;
        private static Texture2D Shadow_Walk;
        private static Texture2D Shadowplayer;
        private static Texture2D Shadowplayer_Jump;
        private static Texture2D Shadowplayer_Walk;
        private static Texture2D Stealth;
        private static Texture2D Stealth_Walk;
        private static Texture2D Tentacle;
        private static Texture2D TentacleSpawn;
        private static Texture2D Troller;
        private static Texture2D Wheelbot;
        private static Texture2D Wheelbot_Walk;
        private static Texture2D Wolf;
        private static Texture2D Wolf_Walk;
        private static Texture2D VanishBlock;
#endregion

        public static string EntityTextureToLabelText(string text)
        {
            switch (text)
            {
                case "A":
                    return "Player";

                case "B":
                    return "Shadowplayer";

                case "C":
                    return "Bird";

                case "D":
                    return "Platform";

                case "E":
                    return "ConcretePlatform";

                case "F":
                    return "Dog";

                case "G":
                    return "Wolf";

                case "H":
                    return "Flamer";

                case "I":
                    return "Stealth";

                case "J":
                    return "Troller";

                case "K":
                    return "Tentacle";

                case "L":
                    return "Tentactlespawn";

                case "M":
                    return "Cannon";

                case "LazerBullet":
                    return "LazerBullet";

                case "N":
                    return "Wheelbot";

                case "O":
                    return "Robot";

                case "P":
                    return "Shadow";

                case "Q":
                    return "Clancy";

                case "R":
                    return "VanishBlock";

                case "S":
                    return "RainingEmber";

                case "T":
                    return "Icicle";

                case "U":
                    return "Maureen";

                case "V":
                    return "GuinnessVan";

                case "W":
                    return "Blaster";

                case "X":
                    return "Door";

                case "Y":
                    return "Key";
            }
            return "default";
        }

        public static SpriteFont GetFont(string fontname)
        {
            switch (fontname)
            {
                case "Tiny":
                    return TinyF;

                case "Small":
                    return SmallF;

                case "Medium":
                    return MediumF;

                case "Large":
                    return LargeF;
            }
            return MediumF;
        }

        public static Texture2D GetTexture(string texturename)
        {
            switch (texturename)
            {
                case "A":
                    return Player;

                case "Aw":
                    return Player_Walk;

                case "Aj":
                    return Player_Jump;

                case "B":
                    return Shadowplayer;

                case "Bw":
                    return Shadowplayer_Walk;

                case "Bj":
                    return Shadowplayer_Jump;

                case "C":
                    return Bird;

                case "D":
                    return Platform;

                case "E":
                    return ConcretePlatform;

                case "F":
                    return Dog;

                case "Fw":
                    return Dog_Walk;

                case "G":
                    return Wolf;

                case "Gw":
                    return Wolf_Walk;

                case "H":
                    return Flamer;

                case "Hw":
                    return Flamer_Walk;

                case "I":
                    return Stealth;

                case "Iw":
                    return Stealth_Walk;

                case "J":
                    return Troller;

                case "K":
                    return Tentacle;

                case "L":
                    return TentacleSpawn;

                case "M":
                    return Cannon;

                case "LazerBullet":
                    return LazerBullet;

                case "N":
                    return Wheelbot;

                case "Nw":
                    return Wheelbot_Walk;

                case "O":
                    return Robot;

                case "P":
                    return Shadow;

                case "Pw":
                    return Shadow_Walk;

                case "Q":
                    return Clancy;

                case "R":
                    return VanishBlock;

                case "S":
                    return RainingEmber;

                case "T":
                    return Icicle;

                case "U":
                    return Maureen;

                case "V":
                    return GuinnessVan;
                    
                case "W":
                    return Blaster;

                case "X":
                    return Door;


                case "Chalice":
                    return Chalice;

                case "Coaster":
                    return Coaster;

                case "Checkpoint":
                    return Checkpoint;

                case "Sign":
                    return Sign;

                case "RedKey":
                    return RedKey;

                case "BlueKey":
                    return BlueKey;

                case "GreenKey":
                    return GreenKey;

                case "YellowKey":
                    return YellowKey;

                case "1":
                    return Button1;

                case "2":
                    return Button2;

                case "3":
                    return Button3;

                case "4":
                    return Button4;

                case "5":
                    return Button5;

                case "Cursor":
                    return Cursor;

                case "EPause_Menu":
                    return EPause_Menu;

                case "Pause_Menu":
                    return Pause_Menu;

                case "a":
                    return Grs;

                case "b":
                    return UGrs;

                case "c":
                    return Ice;

                case "d":
                    return UIce;

                case "e":
                    return Lva;

                case "f":
                    return ULva;

                case "Mag":
                    return Mag;

                case "UMag":
                    return UMag;

                case "g":
                    return Snd;

                case "h":
                    return Mtl;

                case "i":
                    return Gvl;

                case "j":
                    return USpk;

                case "k":
                    return DSpk;

                case "l":
                    return LSpk;

                case "m":
                    return RSpk;

                case "n":
                    return Bnc;

                case "SkyA":
                    return SkyA;

                case "SkyB":
                    return SkyB;

                case null:
                    return Button2;
            }
            return Button2;
        }

        public static Texture2D GetThumbnail(int i)
        {
            return mainThumnails[i];
        }

        public static Texture2D GetCustomThumbnail(int i)
        {
            return customThumnails[i];
        }

        public static string LabelTextToEntityTexture(string text)
        {
            switch (EditorPauseMenu.LabelList[0].Text)
            {
                case "Player":
                    return "A";

                case "Shadowplayer":
                    return "B";

                case "Bird":
                    return "C";

                case "Platform":
                    return "D";

                case "ConcretePlatform":
                    return "E";

                case "Dog":
                    return "F";

                case "Wolf":
                    return "G";

                case "Flamer":
                    return "H";

                case "Stealth":
                    return "I";

                case "Troller":
                    return "J";

                case "Tentacle":
                    return "K";

                case "Tentactlespawn":
                    return "L";

                case "Cannon":
                    return "M";

                case "LazerBullet":
                    return "LazerBullet";

                case "Wheelbot":
                    return "N";

                case "Robot":
                    return "O";

                case "Shadow":
                    return "P";

                case "Clancy":
                    return "Q";

                case "VanishBlock":
                    return "R";

                case "RainingEmber":
                    return "S";

                case "Icicle":
                    return "T";

                case "Maureen":
                    return "U";

                case "GuinnessVan":
                    return "V";

                case "Blaster":
                    return "W";

                case "Door":
                    return "X";
            }
            return "default";
        }

        public static string LabelTextToStaticTexture(string text)
        {
            switch (EditorPauseMenu.LabelList[0].Text)
            {
                case "Grass":
                    return "a";

                case "Soil":
                    return "b";

                case "IceGrass":
                    return "c";

                case "Ice":
                    return "d";

                case "Lava":
                    return "e";

                case "Magma":
                    return "f";

                case "Sand":
                    return "g";

                case "Metal":
                    return "h";

                case "Gravel":
                    return "i";

                case "SpikesUP":
                    return "j";

                case "SpikesDOWN":
                    return "k";

                case "SpikesLEFT":
                    return "l";

                case "SpikesRIGHT":
                    return "m";

                case "Bouncer":
                    return "n";
            }
            return "default";
        }

        private static void LoadBackgrounds(ContentManager Content)
        {
            try
            {
                SkyA = Content.Load<Texture2D>("Textures/Backgrounds/SkyA");
                SkyB = Content.Load<Texture2D>("Textures/Backgrounds/SkyB");
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Level Background textures are missing or named incorrectly", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        private static void LoadFonts(ContentManager Content)
        {
            try
            {
                TinyF = Content.Load<SpriteFont>("SpriteFonts/TinyFont");
                SmallF = Content.Load<SpriteFont>("SpriteFonts/SmallFont");
                MediumF = Content.Load<SpriteFont>("SpriteFonts/MediumFont");
                LargeF = Content.Load<SpriteFont>("SpriteFonts/LargeFont");
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "The spritefont files are missing or damaged", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        private static void LoadMiscellaneous(ContentManager Content)
        {
            try
            {
                Chalice = Content.Load<Texture2D>("Textures/Objects/chalice");
                Coaster = Content.Load<Texture2D>("Textures/Objects/Coaster");
                Checkpoint = Content.Load<Texture2D>("Textures/Objects/checkpoint");
                Sign = Content.Load<Texture2D>("Textures/Objects/sign");
                RedKey = Content.Load<Texture2D>("Textures/Objects/RedKey");
                BlueKey = Content.Load<Texture2D>("Textures/Objects/BlueKey");
                GreenKey = Content.Load<Texture2D>("Textures/Objects/GreenKey");
                YellowKey = Content.Load<Texture2D>("Textures/Objects/YellowKey");
                Cursor = Content.Load<Texture2D>("Textures/Menu/Cursor");
                Button1 = Content.Load<Texture2D>("Textures/Menu/Button1");
                Button2 = Content.Load<Texture2D>("Textures/Menu/Button2");
                Button3 = Content.Load<Texture2D>("Textures/Menu/Button3");
                Button4 = Content.Load<Texture2D>("Textures/Menu/Button4");
                Button5 = Content.Load<Texture2D>("Textures/Menu/Button5");
                EPause_Menu = Content.Load<Texture2D>("Textures/Menu/EPauseMenuBackground");
                Pause_Menu = Content.Load<Texture2D>("Textures/Menu/PauseMenuBackground");
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed to load Menu backgrounds and buttons and special game item textures. The texture files may be missing or named incorrectly", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        private static void LoadSprites(ContentManager Content)
        {
            try
            {
                Player = Content.Load<Texture2D>("Textures/Entities/player.standing");
                Player_Jump = Content.Load<Texture2D>("Textures/Entities/player.jumping");
                Player_Walk = Content.Load<Texture2D>("Textures/Entities/player.walking");
                Shadowplayer = Content.Load<Texture2D>("Textures/Entities/shadowplayer.standing");
                Shadowplayer_Jump = Content.Load<Texture2D>("Textures/Entities/shadowplayer.jumping");
                Shadowplayer_Walk = Content.Load<Texture2D>("Textures/Entities/shadowplayer.walking");
                Bird = Content.Load<Texture2D>("Textures/Entities/deathbird");
                Platform = Content.Load<Texture2D>("Textures/Entities/Platform");
                ConcretePlatform = Content.Load<Texture2D>("Textures/Entities/ConcretePlatform");
                Dog = Content.Load<Texture2D>("Textures/Entities/dog.standing");
                Dog_Walk = Content.Load<Texture2D>("Textures/Entities/dog.walking");
                Troller = Content.Load<Texture2D>("Textures/Entities/troller.standing");
                Flamer = Content.Load<Texture2D>("Textures/Entities/flamer.standing");
                Flamer_Walk = Content.Load<Texture2D>("Textures/Entities/flamer.walking");
                Wolf = Content.Load<Texture2D>("Textures/Entities/wolf.standing");
                Wolf_Walk = Content.Load<Texture2D>("Textures/Entities/wolf.walking");
                Stealth = Content.Load<Texture2D>("Textures/Entities/stealth");
                Stealth_Walk = Content.Load<Texture2D>("Textures/Entities/stealth.walking");
                Tentacle = Content.Load<Texture2D>("Textures/Entities/tentacle.standing");
                TentacleSpawn = Content.Load<Texture2D>("Textures/Entities/tentaclespawn");
                Cannon = Content.Load<Texture2D>("Textures/Entities/cannon");
                Wheelbot = Content.Load<Texture2D>("Textures/Entities/wheelbot.standing");
                Wheelbot_Walk = Content.Load<Texture2D>("Textures/Entities/wheelbot.walking");
                Robot = Content.Load<Texture2D>("Textures/Entities/robot");
                Shadow = Content.Load<Texture2D>("Textures/Entities/shadow.standing");
                Shadow_Walk = Content.Load<Texture2D>("Textures/Entities/shadow.walking");
                LazerBullet = Content.Load<Texture2D>("Textures/Entities/LazerBullet");
                Clancy = Content.Load<Texture2D>("Textures/Entities/Vanish");
                VanishBlock = Content.Load<Texture2D>("Textures/Entities/Vanish");
                RainingEmber = Content.Load<Texture2D>("Textures/Entities/Ember");
                Icicle = Content.Load<Texture2D>("Textures/Entities/Icicle");
                Maureen = Content.Load<Texture2D>("Textures/Entities/Vanish");
                GuinnessVan = Content.Load<Texture2D>("Textures/Entities/Vanish");
                Blaster = Content.Load<Texture2D>("Textures/Entities/Vanish");
                Door = Content.Load<Texture2D>("Textures/Entities/Door");
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Failed to load Player/Ai textures. The texture files may be missing or named incorrectly", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        public static void LoadTextures(ContentManager Content)
        {
            LoadBackgrounds(Content);
            LoadFonts(Content);
            LoadMiscellaneous(Content);
            LoadSprites(Content);
            LoadTiles(Content);
            LoadThumbnails(Content);
        }

        private static void LoadTiles(ContentManager Content)
        {
            try
            {
                Grs = Content.Load<Texture2D>("Textures/Tiles/grass");
                UGrs = Content.Load<Texture2D>("Textures/Tiles/Ugrass");
                Mtl = Content.Load<Texture2D>("Textures/Tiles/metal");
                Gvl = Content.Load<Texture2D>("Textures/Tiles/gravel");
                Ice = Content.Load<Texture2D>("Textures/Tiles/ice");
                UIce = Content.Load<Texture2D>("Textures/Tiles/Uice");
                Lva = Content.Load<Texture2D>("Textures/Tiles/lava");
                ULva = Content.Load<Texture2D>("Textures/Tiles/Ulava");
                Mag = Content.Load<Texture2D>("Textures/Tiles/vgrass");
                UMag = Content.Load<Texture2D>("Textures/Tiles/Uvgrass");
                Snd = Content.Load<Texture2D>("Textures/Tiles/sand");
                USpk = Content.Load<Texture2D>("Textures/Tiles/spikesUP");
                DSpk = Content.Load<Texture2D>("Textures/Tiles/spikesDOWN");
                LSpk = Content.Load<Texture2D>("Textures/Tiles/spikesLEFT");
                RSpk = Content.Load<Texture2D>("Textures/Tiles/spikesRIGHT");
                Bnc = Content.Load<Texture2D>("Textures/Tiles/bouncer");
            }
            catch (Exception e)
            {
                ErrorReporter.LogException(new string[] { "Error loading block textures. The texture files may be missing or named incorrectly", e.Message, "MethodName = " + e.TargetSite.Name, e.StackTrace });
                throw e;
            }
        }

        private static void LoadThumbnails(ContentManager Content)
        {
            //Loading the main level thumnails
            for (int i = 0; i < Level.maxLevels; i++)
            {
                try
                {
                    mainThumnails[i] = Content.Load<Texture2D>("Levels/Main/" + (i + 1) + "/Thumbnail");
                }
                catch (Exception ex)
                {
                    mainThumnails[i] = Player;
                }
            }

            //TODO:Loading the custom level thumnails
        }

        public static string StaticTextureToLabelText(string text)
        {
            switch (text)
            {
                case "a":
                    return "Grass";

                case "b":
                    return "Soil";

                case "c":
                    return "IceGrass";

                case "d":
                    return "Ice";

                case "e":
                    return "Lava";

                case "f":
                    return "Magma";

                case "g":
                    return "Sand";

                case "h":
                    return "Metal";

                case "i":
                    return "Gravel";

                case "j":
                    return "SpikesUP";

                case "k":
                    return "SpikesDOWN";

                case "l":
                    return "SpikesLEFT";

                case "m":
                    return "SpikesRIGHT";

                case "n":
                    return "Bouncer";
            }
            return "default";
        }
    }
}

