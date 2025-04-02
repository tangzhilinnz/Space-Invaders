//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class Font : DLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            TestMessage,
            TestOneOff,

            StaticText,
            WaveName,
            AnimationCount,
            Lives,
            Credit,

            HI_Score,
            Player1_Score,
            Player2_Score,
            TimedCharacter,

            UFOScore,

            NullObject,
            Uninitialized
        }

        //------------------------------------
        // Constructor
        //------------------------------------

        public Font()
        {
            this.mName = Name.Uninitialized;
            this.poSpriteFont = new SpriteFont();
			Debug.Assert(this.poSpriteFont != null);
        }

        //------------------------------------
        // Methods
        //------------------------------------

        public void Set(Font.Name _name, 
						string _pMessage, 
						Glyph.Name _glyphName, 
						float _xStart, float _yStart,
                        float _xScale, float _yScale)
        {
            Debug.Assert(_pMessage != null);

            this.mName = _name;
            this.poSpriteFont.Set(_name, _pMessage, _glyphName, _xStart, _yStart, _xScale, _yScale);
        }


        public void UpdateMessage(string _pMessage)
        {
            Debug.Assert(_pMessage != null);
            Debug.Assert(this.poSpriteFont != null);
            this.poSpriteFont.UpdateMessage(_pMessage);
        }

        public void SetColor(float red, float green, float blue)
        {
            this.poSpriteFont.SetColor(red, green, blue);
        }

        //------------------------------------
        // Override
        //------------------------------------

        public override Enum GetName()
        {
            return this.mName;
        }

        override public void Wash()
        {
           	this.mName = Name.Uninitialized;
            this.poSpriteFont.Set(Font.Name.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f, 1.0f, 1.0f);
 
			base.baseWash();
        }

        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.GetName(), this.GetHashCode());

            base.baseDump();
        }

        //------------------------------------
        // Data
        //------------------------------------
        public Name mName;
        public SpriteFont poSpriteFont;
        static private string pNullString = "null";
    }
}

// --- End of File ---
