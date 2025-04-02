//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class TimedCharacterCmd : Command
    {
        public TimedCharacterCmd( TimedCharacterCmd _pCmd_old, string _pLetter,
            float xPos, float yPos, float _red, float _green, float _blue)
        {
            this.pLetter = _pLetter;
            this.x = xPos;
            this.y = yPos;
            this.red = _red;
            this.green = _green;
            this.blue = _blue;
            this.poFont = null;
            this.pCmd_old = _pCmd_old;

            if (this.pCmd_old != null)
            {
                Debug.WriteLine(" {0}  old:{1}", this.GetHashCode(), this.pCmd_old.GetHashCode());
            }
            else
            {
               Debug.WriteLine(" {0}  old:{1}", this.GetHashCode(), "null");

            }
        }
        override  public void Execute(float deltaTime)
        {
            //Debug.WriteLine("\nexec start: {0} ", this.GetHashCode());

            // Get rid of the old one 
            if (this.pCmd_old != null)
            {
                //Debug.WriteLine("{0} remove this one", this.pCmd_old.GetHashCode());
                FontMan.Remove(this.pCmd_old.poFont);
            }

            // New one
            Font pFont = FontMan.Add(Font.Name.TimedCharacter, 
                                     SpriteBatch.Name.Texts, 
                                     this.pLetter, 
                                     Glyph.Name.SpaceInvaders, 
                                     this.x, 
                                     this.y,
                                     3.0f, 3.0f);

            pFont.SetColor(red, green, blue);
           
            this.poFont = pFont;

            //Debug.WriteLine("exec exit: {0} this.poFont: {1}", this.GetHashCode(), this.poFont.GetHashCode());
        }

        // --------------------------------------
        // Data: 
        // --------------------------------------
        private string pLetter;
        private float x;
        private float y;
        private float red;
        private float green;
        private float blue;
        private Font poFont;
        private TimedCharacterCmd pCmd_old;
    }
}

// --- End of File ---
