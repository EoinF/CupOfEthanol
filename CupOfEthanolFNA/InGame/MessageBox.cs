namespace LackingPlatforms
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class MessageBox
    {
        public int LifeTime;
        public static MessageBox GameMessage;
        public static MessageBox StatusMessage;
        public string msg;
        public string Row;
        private static Vector2 startLocation;

        public MessageBox(string text)
        {
            text = text + " ";

            startLocation = new Vector2(50, 50);
            this.LifeTime = 180;
            this.Row = "";
            int h = 0;
            int k = 0;
            text = CheckForSpSymbols(text);
            while (h != text.Length)
            {
                for (int i = 50; i > 0; i--)
                {
                    if (text.Length < (i + h))
                    {
                        i = text.Length - h;
                    }
                    if (Textures.GetFont("Medium").MeasureString(text.Substring(h, i)).X < 800f)
                    {
                        string tx = text.Substring(h, i);
                        string[] split = tx.Split(new char[] { ' ' });
                        if (split[split.Length - 1].Length != 0)
                        {
                            if (text.Length < (split[split.Length - 1].Length + h))
                            {
                                i = text.Length - h;
                            }
                            else
                            {
                                if (i != split[split.Length - 1].Length)
                                {
                                    i -= split[split.Length - 1].Length;
                                }
                                tx = text.Substring(h, i);
                            }
                        }
                        this.Row = this.Row + "\n" + tx;
                        if (k > 2)
                        {
                            //used to be k = k.  /Eoin
                            k = h;
                        }
                        h += i;
                        break;
                    }
                }
                k++;
            }
        }

        public MessageBox(string text, Vector2 StartLocation, int lifetime)
        {
            text = text + " ";

            startLocation = StartLocation;
            this.LifeTime = lifetime;
            this.Row = "";
            int h = 0;
            int k = 0;
            text = CheckForSpSymbols(text);
            while (h != text.Length)
            {
                for (int i = 50; i > 0; i--)
                {
                    if (text.Length < (i + h))
                    {
                        i = text.Length - h;
                    }
                    if (Textures.GetFont("Medium").MeasureString(text.Substring(h, i)).X < 800f)
                    {
                        string tx = text.Substring(h, i);
                        string[] split = tx.Split(new char[] { ' ' });
                        if (split[split.Length - 1].Length != 0)
                        {
                            if (text.Length < (split[split.Length - 1].Length + h))
                            {
                                i = text.Length - h;
                            }
                            else
                            {
                                if (i != split[split.Length - 1].Length)
                                {
                                    i -= split[split.Length - 1].Length;
                                }
                                tx = text.Substring(h, i);
                            }
                        }
                        this.Row = this.Row + "\n" + tx;
                        if (k > 2)
                        {
                            //used to be k = k.  /Eoin
                            k = h;
                        }
                        h += i;
                        break;
                    }
                }
                k++;
            }
        }


        public static string CheckForSpSymbols(string text)
        {
            string[] temp = text.Split('£');
            text = "";
            foreach (string s in temp)
            {
                text = text + s + "\n";
            }
            return text;
        }

        public void Draw(SpriteBatch spriteBatch, bool isStatus = false)
		{
			Color colour = new Color(isStatus ? 0.9f: 0.75f, isStatus? 0.6f : 0.1f, isStatus ? 0.9f : 0.75f, 1);
			float alpha = 0.035f * this.LifeTime;
			float brightness = alpha > 0.4f ? 0.2f : 0;
			Color backgroundColour = new Color(brightness, brightness, 0.5f + brightness, alpha > 0.4f ? alpha : 0f);
			if (LifeTime < 61)
			{
				colour = new Color(this.LifeTime / 80f, this.LifeTime / 100f, this.LifeTime / 80f, 0.035f * this.LifeTime);
			}

			if (isStatus)
			{
				spriteBatch.Draw(Textures.GetTexture("StatusBackground"), startLocation + new Vector2(-10, 17), null, backgroundColour, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9999f); 
			}
            spriteBatch.DrawString(Textures.GetFont("Medium"), this.Row, startLocation, colour, 0f, Vector2.Zero, 1f, 0, 1f);
        }

        public void Update()
        {
            this.LifeTime--;
            if (this.LifeTime < 0)
            {
                GameMessage = null;
                StatusMessage = null;
            }
        }
    }
}

