using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LackingPlatforms
{
    public partial class Entity
    {
        private void Door1()
        {
            this.sqobject.Velocity = Vector2.Zero;
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 20)
                if (PPlayer.Player.HasRedKey)
                {
                    if (InputManager.JustPressed(Keys.Space))
                    {
                        PPlayer.Player.HasRedKey = false;
                        Lives = 0;
                    }
                }
        }
        private void Door2()
        {
            this.sqobject.Velocity = Vector2.Zero;
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 20)
                if (PPlayer.Player.HasBlueKey)
                {
                    if (InputManager.JustPressed(Keys.Space))
                    {
                        PPlayer.Player.HasBlueKey = false;
                        Lives = 0;
                    }
                }
        }
        private void Door3()
        {
            this.sqobject.Velocity = Vector2.Zero;
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 20)
                if (PPlayer.Player.HasGreenKey)
                {
                    if (InputManager.JustPressed(Keys.Space))
                    {
                        PPlayer.Player.HasGreenKey = false;
                        Lives = 0;
                    }
                }
        }
        private void Door4()
        {
            this.sqobject.Velocity = Vector2.Zero;
            if (Vector2.Distance(this.sqobject.Position, PPlayer.Player.sqobject.Position) < 20)
                if (PPlayer.Player.HasYellowKey)
                {
                    if (InputManager.JustPressed(Keys.Space))
                    {
                        PPlayer.Player.HasYellowKey = false;
                        Lives = 0;
                    }
                }
        }
    
    }
}