//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class ColObject
    {
        public ColObject(SpriteGameProxy pSpriteGameProxy)
        {
            Debug.Assert(pSpriteGameProxy != null);

            // Create Collision Rect
            // Use the reference sprite to set size and shape
            // need to refactor if you want it different
            SpriteGame pSprite = pSpriteGameProxy.pRealSprite;
            Debug.Assert(pSprite != null);

            // Origin is in the UPPER RIGHT 
            this.poColRect = new ColRect(pSprite.GetRect());
            Debug.Assert(this.poColRect != null);

            // Create the sprite
            this.pColBoxProxy = SpriteBoxProxyMan.Add(SpriteBox.Name.CollisionBox);
            Debug.Assert(this.pColBoxProxy != null);
            this.pColBoxProxy.SetColor(1.0f, 0.0f, 0.0f);
        }
		
		public void Resurrect(SpriteGameProxy pSpriteProxy)
        {
            Debug.Assert(pSpriteProxy != null);

            // Create Collision Rect
            // Use the reference sprite to set size and shape
            // need to refactor if you want it different
            SpriteGame pSprite = pSpriteProxy.pRealSprite;
            Debug.Assert(pSprite != null);

            Debug.Assert(this.poColRect != null);
            this.poColRect.Set(pSprite.GetRect());

            Debug.Assert(this.pColBoxProxy != null);
            this.pColBoxProxy.SetRect(this.poColRect);
            this.pColBoxProxy.SetColor(1.0f, 1.0f, 1.0f);
        }
        public void UpdatePos(float x, float y)
        {
            // Note we are not considering angle or scale at this time

            this.poColRect.x = x;
            this.poColRect.y = y;

            this.pColBoxProxy.SetRect(this.poColRect);
            this.pColBoxProxy.x = this.poColRect.x;
            this.pColBoxProxy.y = this.poColRect.y;

        }

        // -------------------------------------------------------
        // Data:
        // -------------------------------------------------------
        public SpriteBoxProxy pColBoxProxy;
        public ColRect poColRect;
    }
}

// --- End of File
