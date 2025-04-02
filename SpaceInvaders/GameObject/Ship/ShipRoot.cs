//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipRoot : Composite
    {
        public ShipRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColBoxProxy.SetColor(0, 0, 1);
        }

        ~ShipRoot()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShipRoot(this);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs ShipRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(b);

            if (pGameObj != null)
                ColPair.Collide(pGameObj, this);
        }

        public override void VisitBomb(Bomb b)
        {
            // Bomb vs ShieldRoot
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            if (pGameObj != null)
                ColPair.Collide(b, pGameObj);
        }


        public override void VisitBumperRoot(BumperRoot b)
        {
            //Debug.WriteLine("collide: {0} with {1}", this, b);

            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            if (pGameObj != null) ColPair.Collide(b, pGameObj);
        }

        public override void VisitGrid(AlienGrid a)
        {
            // Alien Grid vs Ship Root 
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            if (pGameObj != null) ColPair.Collide(a, pGameObj);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }


        // Data: ---------------


    }
}

// --- End of File ---

