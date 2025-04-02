//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShieldRoot : Composite
    {
        public ShieldRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

        }
        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;
            base.Resurrect();
        }
        ~ShieldRoot()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldRoot(this);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs ShieldRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            if (pGameObj != null)
            {
                ColPair.Collide(pGameObj, this);
            }
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            if (pGameObj != null)
            {
                ColPair.Collide(m, pGameObj);
            }
        }
        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs ShieldRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(b);

            if (pGameObj != null)
                ColPair.Collide(pGameObj, this);
        }
        public override void VisitBomb(Bomb b)
        {
            // Missile vs ShieldRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            if (pGameObj != null)
                ColPair.Collide(b, pGameObj);
        }


        public override void VisitGrid(AlienGrid a)
        {
            // Shield Root vs Alien Grid
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(a);
            if (pGameObj != null) ColPair.Collide(pGameObj, this);
        }

        public override void VisitColumn(AlienColumn a)
        {
            // Shield Root vs Alien Column
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            if (pGameObj != null)  ColPair.Collide(a, pGameObj);
        }

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        // ------------------------------------------
        // Data:
        // ------------------------------------------


    }
}

// --- End of File ---
