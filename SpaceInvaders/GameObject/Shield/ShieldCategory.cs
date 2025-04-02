//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class ShieldCategory : Leaf
    {
        public enum Type
        {
            Root,
            Column,
            Brick,
            Grid,

            LeftTop0,
            LeftTop1,
            LeftBottom,
            RightTop0,
            RightTop1,
            RightBottom,

            Unitialized
        }

        protected ShieldCategory(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, ShieldCategory.Type shieldType)
            : base(name, spriteName, posX, posY)
        {
            this.ShieldType = shieldType;
        }
        override public void Resurrect()
        {
            base.Resurrect();
        }
        // Data: ---------------
        ~ShieldCategory()
        {
        }
        public ShieldCategory.Type GetCategoryType()
        {
            return this.ShieldType;
        }

        // --------------------------------------------------------------------
        // Data:
        // --------------------------------------------------------------------

        // this is just a placeholder, who knows what data will be stored here
        protected ShieldCategory.Type ShieldType;

    }
}

// --- End of File ---
