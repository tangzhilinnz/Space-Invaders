//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class BombCategory : Leaf
    {
        public enum Type
        {
            Bomb,
            BombRoot,
            Unitialized
        }

        protected BombCategory(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, BombCategory.Type bombType)
            : base(name, spriteName, posX, posY)
        {
            this.BombType = bombType;
        }

        // Data: ---------------
        ~BombCategory()
        {
        }

        public override void Update()
        {
            if (this.y < 220.0f + 5.0f)
            {
                this.pSpriteProxy.SetColor(0.0f, 0.7f, 0.0f);
            }
            else if (this.y < 600.0f)
            {
                this.pSpriteProxy.SetColor(0.9f, 0.9f, 0.9f);
            }
            else
            {
                this.pSpriteProxy.SetColor(1.0f, 0.0f, 0.0f);
            }

            // Go to first child
            base.Update();
        }

        // this is just a placeholder, who knows what data will be stored here
        protected BombCategory.Type BombType;

    }
}

// --- End of File ---
