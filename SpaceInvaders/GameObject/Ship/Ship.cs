//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;
using System.Runtime;

namespace SE456
{
    public class Ship : ShipCategory
    {

        public Ship(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName, posX, posY, ShipCategory.Type.Ship)
        {
            this.x = posX;
            this.y = posY;

            this.shipSpeed = 2.0f;
            this.MoveState = null;
            this.MissileState = null;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Bomb
            // Call the appropriate collision reaction
            other.VisitShip(this);
        }

        public override void VisitBumperRoot(BumperRoot b)
        {
            //Debug.WriteLine("collide: {0} with {1}", this, b);

            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(b);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitBumperRight(BumperRight b)
        {
            //Debug.WriteLine("collide: {0} with {1}", this, b);

            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }
        public override void VisitBumperLeft(BumperLeft b)
        {
            //Debug.WriteLine("collide: {0} with {1}", this, b);

            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void VisitBomb(Bomb b)
        {
            // Bomb vs ship
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void VisitGrid(AlienGrid a)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            Debug.Assert(pColPair != null);

            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public void MoveRight()
        {
            this.MoveState.MoveRight(this);
        }

        public void MoveLeft()
        {
            this.MoveState.MoveLeft(this);
        }

        public void ShootMissile()
        {
            this.MissileState.ShootMissile(this);
        }


        public void SetState(ShipMan.MissileState inState)
        {
            this.MissileState = ShipMan.GetState(inState);
        }
        public void SetState(ShipMan.MoveState inState)
        {
            this.MoveState = ShipMan.GetState(inState);
        }

        // Data: --------------------
        public float shipSpeed;
        private ShipMoveState MoveState;
        private ShipMissileState MissileState;
    }
}

// --- End of File ---
