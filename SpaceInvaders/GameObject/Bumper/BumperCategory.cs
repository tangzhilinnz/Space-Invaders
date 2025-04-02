//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class BumperCategory : Leaf
    {
        public enum Type
        {
            Right,
            Left,

            Unitialized
        }

        protected BumperCategory(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y, BumperCategory.Type _type)
        : base(gameName, spriteName, _x, _y)
        {
            BumperType = _type;
        }

        ~BumperCategory()
        {
        }

        public BumperCategory.Type GetCategoryType()
        {
            return this.BumperType;
        }

        // Data ----------
        protected BumperCategory.Type BumperType;

    }
}// --- End of File ---
