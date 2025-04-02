//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShieldGrid : Composite
    {
        public ShieldGrid(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            //Debug.WriteLine("Grid proxy sprite: {0}", this.pSpriteProxy.GetHashCode());
            //Debug.WriteLine("Grid col   sprite: {0}",this.poColObj.pColSprite.GetHashCode());
            this.x = posX;
            this.y = posY;

            this.SetCollisionColor(0.0f, 1.0f, 0.0f);
        }

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;
            
            base.Resurrect();

            this.SetCollisionColor(0.0f, 1.0f, 0.0f);
        }
        ~ShieldGrid()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldGrid(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldGrid
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            if (pGameObj != null)
                ColPair.Collide(m, pGameObj);
        }

        public override void VisitBomb(Bomb b)
        {
            // Missile vs ShieldGrid
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);

            if (pGameObj != null)
                ColPair.Collide(b, pGameObj);
        }

        public override void VisitColumn(AlienColumn a)
        {
            // Shield Grid vs Alien Column
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            if (pGameObj != null) ColPair.Collide(a, pGameObj);
        }

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        // -------------------------------------------
        // Data: 
        // -------------------------------------------


    }
}

// --- End of File ---
