//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class SpriteFont : SpriteBase
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            HotPink,

            RedBird,
            YellowBird,
            GreenBird,
            WhiteBird,
            BlueBird,

            RedGhost,
            PinkGhost,
            BlueGhost,
            OrangeGhost,
            MsPacMan,
            PowerUpGhost,
            Prezel,

            Null_Object,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------

        public SpriteFont()
            : base()
        {
            // Create a dummy sprite, it will get correctly linked in Set()

            this.poAzulSprite = new Azul.Sprite();
            this.poScreenRect = new Azul.Rect();
            this.poColor = new Azul.Color(1.0f, 1.0f, 1.0f);

            this.pMessage = null;
            this.glyphName = Glyph.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sy = 1.0f;
            this.sx = 1.0f;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------

        public void Set(Font.Name name, String pMessage, Glyph.Name glyphName, float xStart, float yStart, float _xScale, float _yScale)
        {
            Debug.Assert(pMessage != null);
            this.pMessage = pMessage;

            this.x = xStart;
            this.y = yStart;

            this.sx = _xScale;
            this.sy = _yScale;

            this.name = name;

            // TODO: for wash... this should be a nullGlyph
            this.glyphName = glyphName;

            // Force color to white
            Debug.Assert(this.poColor != null);
            this.poColor.Set(1.0f, 1.0f, 1.0f);
        }

        public void SetColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.poColor != null);
            this.poColor.Set(red, green, blue, alpha);
        }

        public void UpdateMessage(String pMessage)
        {
            Debug.Assert(pMessage != null);
            this.pMessage = pMessage;
        }

        override public void Update()
        {
            Debug.Assert(this.poAzulSprite != null);
        }

        override public void Render()
        {
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(this.poColor != null);
            Debug.Assert(this.poScreenRect != null);
            Debug.Assert(this.pMessage != null);
            //Debug.Assert(this.pMessage.Length > 0);
            if (this.pMessage.Length <= 0)
            {
                return;
            }

            float xTmp = this.x;
            float yTmp = this.y;

            float xEnd = this.x;

            for (int i = 0; i < this.pMessage.Length; i++)
            {
                int key = Convert.ToByte(pMessage[i]);

                Glyph pGlyph = GlyphMan.Find(this.glyphName, key);
                Debug.Assert(pGlyph != null);

                xTmp = xEnd + pGlyph.GetAzulRect().width * this.sx / 2;
                this.poScreenRect.Set(xTmp, yTmp, pGlyph.GetAzulRect().width, pGlyph.GetAzulRect().height);

                poAzulSprite.Swap(pGlyph.GetAzulTexture(), pGlyph.GetAzulRect(), this.poScreenRect, this.poColor);
                poAzulSprite.sx = this.sx;
                poAzulSprite.sy = this.sy;

                poAzulSprite.Update();
                poAzulSprite.Render();

                // move the starting to the next character
                xEnd = pGlyph.GetAzulRect().width * this.sx / 2 + xTmp;

            }
        }

        private void privClear()
        {
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(this.poColor != null);
            Debug.Assert(this.poScreenRect != null);

            this.poScreenRect.Set(0, 0, 0, 0);
            this.poColor.Set(1.0f, 1.0f, 1.0f);

            this.pMessage = null;
            this.glyphName = Glyph.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
        }

        //---------------------------------------------------------------------------------------------------------
        // Override
        //---------------------------------------------------------------------------------------------------------

        public override System.Enum GetName()
        {
            return this.name;
        }

        override public void Wash()
        {
            this.privClear();
        }

        override public bool Compare(NodeBase pTarget)
        {
            // This is used in ManBase.Find() 
            Debug.Assert(pTarget != null);

            SpriteFont pDataB = (SpriteFont)pTarget;

            bool status = false;

            if (this.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }

        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            base.baseDump();
        }

        //----------------------------------------------
        // Data
        //----------------------------------------------
        public Font.Name name;
        private Azul.Sprite poAzulSprite;
        private Azul.Rect poScreenRect;
        private Azul.Color poColor;   // this color is multplied by the texture

        private string pMessage;
        public Glyph.Name glyphName;

        public float x;
        public float y;

        public float sx;
        public float sy;
    }
}

// --- End of File ---
