//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class ExplosionCategory : Leaf
    {
        public enum Type
        {
            Alien,
            AlienShot,
            Ship,
            ShipShot,
            UFO,
            Unitialized
        }

        protected ExplosionCategory(GameObject.Name gameName, SpriteGame.Name spriteName, float _x, float _y, ExplosionCategory.Type _type)
        : base(gameName, spriteName, _x, _y)
        {
            ExplosionType = _type;
        }

        ~ExplosionCategory()
        {
        }

        public ExplosionCategory.Type GetCategoryType()
        {
            return this.ExplosionType;
        }

        public override void Update()
        {
            if (this.y < 220.0f + 5.0f)
            {
                this.pSpriteProxy.SetColor(0.0f, 0.7f, 0.0f);
            }

            if (this.y > 640.0f)
            {
                this.pSpriteProxy.SetColor(1.0f, 0.0f, 0.0f);
            }

            // Go to first child
            base.Update();
        }

        // Data ----------
        protected ExplosionCategory.Type ExplosionType;

    }
}// --- End of File ---
