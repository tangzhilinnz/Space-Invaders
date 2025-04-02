//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class WallTop : WallCategory
    {
        public WallTop(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, posX, posY, WallCategory.Type.Top)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.poColObj.pColBoxProxy.SetColor(1, 1, 0);
        }

        ~WallTop()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitWallTop(this);
        }
        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        public override void VisitGrid(AlienGrid a)
        {
            // No AlienGrid vs WallTop
            Debug.WriteLine("\ncollide: {0} with {1}", this, a);
            Debug.WriteLine("               --->DONE<----");
        }

        public override void VisitBomb(Bomb b)
        {
            // No Bomb vs WallTop
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs WallTop
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }


        // Data: ---------------


    }
}

// --- End of File ---
