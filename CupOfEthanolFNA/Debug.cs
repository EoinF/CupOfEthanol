using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace LackingPlatforms
{

    public class Debug
    {
        private int _currentFrames = 0;
        public int[] _totalFrameList = new int[99999];
        private int SecondsTicked = 0;
        private int Milliseconds = 0;
        private Color colour = Color.DarkViolet;
        private int AvgFPS = -1;
        private int PerformanceFrames = 0;
        private int CurrentFPS = 0;
        

        
        public int CurrentFrames
        {
            get { return _currentFrames; }
            set { _currentFrames = value; }
        }

        public int[] TotalFrameList
        {
            get { return _totalFrameList; }
            set { _totalFrameList = value; }
        }



        public void Update(GameTime currentTime)
        {
            Milliseconds += (int)currentTime.ElapsedGameTime.TotalMilliseconds;
            PerformanceFrames++;
            if (Milliseconds > 1000)
                Tick();
            if (InputManager.Keystate[0].IsKeyDown(Keys.O) && SecondsTicked > 0)
                AvgFPS = _totalFrameList.Sum() / SecondsTicked;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Textures.GetFont("Medium"), "GPU = " + _totalFrameList[SecondsTicked].ToString(), new Vector2(20, 50), this.colour, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            CurrentFrames++;

            spriteBatch.DrawString(Textures.GetFont("Medium"), "CPU = " + CurrentFPS.ToString(), new Vector2(20, 90), this.colour, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            

            if (AvgFPS != -1)
                spriteBatch.DrawString(Textures.GetFont("Medium"), "Average:" + AvgFPS, new Vector2(120, 50), this.colour, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            
        }

        private void Tick()
        {
            SecondsTicked++;
            _totalFrameList[SecondsTicked] = CurrentFrames;
            CurrentFrames = 0;
            CurrentFPS = PerformanceFrames;
            PerformanceFrames = 0;

            Milliseconds -= 1000;

            if (this.colour == Color.DarkViolet)
                this.colour = Color.DarkOliveGreen;
            else
                this.colour = Color.DarkViolet;
        }
    }
}
