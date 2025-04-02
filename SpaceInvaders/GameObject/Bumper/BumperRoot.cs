//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class BumperRoot : Composite
    {
        public BumperRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColBoxProxy.SetColor(1, 1, 1);

            this.name = name;
        }
        ~BumperRoot()
        {

        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitBumperRoot(this);
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }


        // Data: ---------------


    }
}// --- End of File ---
