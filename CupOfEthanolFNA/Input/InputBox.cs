namespace LackingPlatforms
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;

    public static class InputBox
    {
        public static bool Active = false;
        public static string text = "|";
        public static int InputTimeout = 0;


        private static bool CheckSpCharacters(Keys key, ref string CurrentText)
        {
            switch (key)
            {
                case Keys.D0:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "0";
                    }
                    else
                    {
                        CurrentText += ")";
                    }
                    return true;

                case Keys.D1:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "1";
                    }
                    else
                    {
                        CurrentText += "!";
                    }
                    return true;

                case Keys.D2:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "2";
                    }
                    else
                    {
                        CurrentText += "\"";
                    }
                    return true;

                case Keys.D3:
                    CurrentText += "3";
                    return true;

                case Keys.D4:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "4";
                    }
                    else
                    {
                        CurrentText += "$";
                    }
                    return true;

                case Keys.D5:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "5";
                    }
                    else
                    {
                        CurrentText += "%";
                    }
                    return true;

                case Keys.D6:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "6";
                    }
                    else
                    {
                        CurrentText += "^";
                    }
                    return true;

                case Keys.D7:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "7";
                    }
                    else
                    {
                        CurrentText += "&";
                    }
                    return true;

                case Keys.D8:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "8";
                    }
                    else
                    {
                        CurrentText += "*";
                    }
                    return true;

                case Keys.D9:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "9";
                    }
                    else
                    {
                        CurrentText += "(";
                    }
                    return true;

                case Keys.OemSemicolon:
                    if (!IsHoldingShift())
                    {
                        CurrentText += ";";
                    }
                    else
                    {
                        CurrentText += ":";
                    }
                    return true;

                case Keys.OemComma:
                    CurrentText += ",";
                    return true;

                case Keys.OemPeriod:
                    CurrentText += ".";
                    return true;

                case Keys.OemQuestion:
                    if (!IsHoldingShift())
                        CurrentText += "/";
                    else
                        CurrentText += "?";
                    return true;

                case Keys.OemTilde:
                    if (!IsHoldingShift())
                    {
                        CurrentText += "'";
                        break;
                    }
                    CurrentText += "@";
                    break;

                case Keys.Enter:
                    CurrentText += "£";
                    break;

                default:
                    return false;
            }
            return true;
        }

        public static void CheckSpKeys(ref string CurrentText)
        {
            if (IsHeld(Keys.Back) || InputManager.JustPressed(Keys.Back))
            {
                if (CurrentText.Length > 1)
                {
                    CurrentText = CurrentText.Remove(CurrentText.Length - 2, 2);
                    CurrentText += "|";
                }
            }
            else if (IsHeld(Keys.Space) || InputManager.JustPressed(Keys.Space))
            {
                CurrentText = CurrentText.Remove(CurrentText.Length - 1, 1);
                CurrentText += " |";
            }
        }

        public static void Deactivate()
        {
            Active = false;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            MessageBox.GameMessage.Draw(spriteBatch);
        }

        public static void InputToText(ref string CurrentText)
        {
            Keys[] keyarray = InputManager.Keystate[0].GetPressedKeys();
            for (int i = 0; i < keyarray.Length; i++)
            {
                if (IsHeld(keyarray[i]) || InputManager.JustPressed(keyarray[i]))
                {
                    CurrentText = CurrentText.Remove(CurrentText.Length - 1, 1);
                    if (!CheckSpCharacters(keyarray[i], ref CurrentText))
                    {
                        string t = keyarray[i].ToString();
                        if (t.Length < 2)
                        {
                            if (IsHoldingShift())
                            {
                                CurrentText += t.ToUpper();
                            }
                            else
                            {
                                CurrentText += t.ToLower();
                            }
                        }
                    }
                    CurrentText += "|";
                }
            }
        }

        private static bool IsHeld(Keys key)
        {
            int p = 0;
            for (int q = 1; q < 20; q++)
            {
                if (InputManager.Keystate[q].IsKeyDown(key))
                {
                    p++;
                }
            }
            if (p < 19)
            {
                return false;
            }
            return true;
        }

        private static bool IsHoldingShift()
        {
            return (InputManager.Keystate[0].IsKeyDown(Keys.RightShift) || InputManager.Keystate[0].IsKeyDown(Keys.LeftShift));
        }

        private static bool IsHoldingCtrl()
        {
            return (InputManager.Keystate[0].IsKeyDown(Keys.RightControl) || InputManager.Keystate[0].IsKeyDown(Keys.LeftControl));
        }

        public static void Update()
        {
            InputToText(ref text);
            CheckSpKeys(ref text);
            MessageBox.GameMessage = new MessageBox(text);
        }
    }
}

