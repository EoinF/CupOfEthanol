namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Objectdata
    {
        public Vector2 Position;
        public string Texturename;
        public string Direction;
        public byte StartCheckpoint;
        public byte EndCheckpoint;
        public int StartDelay;

        public Objectdata(Vector2 pos, string tex, byte startCheckpoint, byte endCheckpoint)
        {
            this.Position = pos;
            this.Texturename = tex;
            this.Direction = "f";
            StartCheckpoint = startCheckpoint;
            EndCheckpoint = endCheckpoint;
            StartDelay = 0;
        }

        public Objectdata(Vector2 pos, string tex, byte startCheckpoint, byte endCheckpoint, string dir)
        {
            this.Position = pos;
            this.Texturename = tex;
            this.Direction = dir;
            StartCheckpoint = startCheckpoint;
            EndCheckpoint = endCheckpoint;
            StartDelay = 0;
        }

        public Objectdata(Vector2 pos, string tex, byte startCheckpoint, byte endCheckpoint, string dir, int startDelay)
        {
            this.Position = pos;
            this.Texturename = tex;
            this.Direction = dir;
            StartCheckpoint = startCheckpoint;
            EndCheckpoint = endCheckpoint;
            StartDelay = startDelay;
        }
    }
}

