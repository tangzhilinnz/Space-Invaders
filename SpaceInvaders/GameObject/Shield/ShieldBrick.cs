//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShieldBrick : ShieldCategory
    {
        public ShieldBrick(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ShieldCategory.Type.Brick)
        {
            this.x = posX;
            this.y = posY;

            this.SetCollisionColor(1.0f, 1.0f, 1.0f);
        }

        public void Resurrect(float posX, float posY)
        {
            this.x = posX;
            this.y = posY;

            this.SetCollisionColor(1.0f, 1.0f, 1.0f);
            base.Resurrect();
            this.SetCollisionColor(1.0f, 1.0f, 1.0f);
        }
        ~ShieldBrick()
        {

        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShieldBrick(this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }
        public override void VisitBomb(Bomb b)
        {
            // Bomb vs ShieldBrick
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void VisitColumn(AlienColumn a)
        {
            // Shield Brick vs Alien Column
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            base.Update();
        }

        // ---------------------------------
        // Data: 
        // ---------------------------------


    }
}

// --- End of File ---
