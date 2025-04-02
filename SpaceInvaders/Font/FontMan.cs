//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class FontMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public FontMan(int reserveNum = 0, int reserveGrow = 1)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            // initialize derived data here
            FontMan.psActiveFontMan = null;

            // initialize derived data here
            FontMan.psNodeCompare = new Font();
            Debug.Assert(FontMan.psNodeCompare != null);
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(psInstance == null);

            // Do the initialization
            if (psInstance == null)
            {
                psInstance = new FontMan();
            }
        }
        public static void Destroy()
        {
        }

        public static Font Add(Font.Name _name, SpriteBatch.Name SB_Name,
            string pMessage, Glyph.Name glyphName, float xStart, float yStart,
            float xScale = 1.0f, float yScale = 1.0f)
        {
            FontMan pMan = FontMan.psActiveFontMan;
            //FontMan pMan = FontMan.privGetInstance();

            Font pNode = (Font)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(_name, pMessage, glyphName, xStart, yStart, xScale, yScale);

            // Add to sprite batch
            SpriteBatch pSB = SpriteBatchMan.Find(SB_Name);
            Debug.Assert(pSB != null);
            Debug.Assert(pNode.poSpriteFont != null);
            pSB.Attach(pNode.poSpriteFont);

            return pNode;
        }

        public static void SetActive(FontMan pFontMan)
        {
            FontMan pMan = FontMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pFontMan != null);
            FontMan.psActiveFontMan = pFontMan;
        }

        public static void AddXml(Glyph.Name _glyphName, 
									string _assetName, 
									Texture.Name _textName)
        {
            GlyphMan.AddXml(_assetName, _glyphName, _textName);
        }

        public static Font Find(Font.Name _name)
        {
            FontMan pMan = FontMan.psActiveFontMan;
            //FontMan pMan = FontMan.privGetInstance();

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            FontMan.psNodeCompare.mName = _name;

            Font pData = (Font)pMan.baseFind(FontMan.psNodeCompare);
            return pData;
        }

        public static void Remove(Font _pNode)
        {
            Debug.Assert(_pNode != null);

            //FontMan pMan = FontMan.psActiveFontMan;
            FontMan pMan = FontMan.psActiveFontMan;

            // Remove it from the manager
            SpriteNode pSpriteNode = _pNode.poSpriteFont.GetSpriteNode();
            Debug.Assert(pSpriteNode != null);
            SpriteBatchMan.Remove(pSpriteNode);

            pMan.baseRemove(_pNode);
        }
        public static void Dump()
        {
            //FontMan pMan = FontMan.privGetInstance();
            FontMan pMan = FontMan.psActiveFontMan;
            pMan.baseDump();
        }


        //------------------------------------
        // Override Abstract methods
        //------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new Font();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //------------------------------------
        // Private methods
        //------------------------------------
        private static FontMan privGetInstance()
        {
            // Safety - this forces users to call Create() first 
            // before using class
            Debug.Assert(psInstance != null);

            return psInstance;
        }


        //------------------------------------
        // Data: unique data for this manager 
        //------------------------------------
        private static FontMan psActiveFontMan = null;
        private static Font psNodeCompare;
        private static FontMan psInstance = null;

    }
}

// --- End of File ---
