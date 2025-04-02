//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class ColVisitor : DLink
    {
        public virtual void VisitBumperRoot(BumperRoot b)
        {
            Debug.WriteLine("Visit by BumperRoot not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitBumperRight(BumperRight b)
        {
            Debug.WriteLine("Visit by BumperRight not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitBumperLeft(BumperLeft b)
        {
            Debug.WriteLine("Visit by BumperLeft not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitGrid(AlienGrid a)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by BirdGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitColumn(AlienColumn b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by BirdColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitAlienCrab(AlienCrab b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Alien Crab not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitAlienOctopus(AlienOctopus b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Alien Octopus not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitAlienSquid(AlienSquid b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Alien Squid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissile(Missile m)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Missile not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissileGroup(MissileGroup m)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by MissileGroup not implemented");
            Debug.Assert(false);
        }
       
        public virtual void VisitNullGameObject(GameObjectNull n)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by NullGameObject not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallGroup(WallGroup w)
        {
            Debug.WriteLine("Visit by WallGroup not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitWallBottom(WallBottom w)
        {
            Debug.WriteLine("Visit by WallBottom not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitWallRight(WallRight w)
        {
            Debug.WriteLine("Visit by WallRight not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitWallLeft(WallLeft w)
        {
            Debug.WriteLine("Visit by WallLeft not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallTop(WallTop w)
        {
            Debug.WriteLine("Visit by WallTop not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShip(Ship s)
        {
            Debug.WriteLine("Visit by Ship not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShipRoot(ShipRoot s)
        {
            Debug.WriteLine("Visit by ShipRoot not implemented");
            Debug.Assert(false);
        }
		public virtual void VisitBomb(Bomb b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Bomb not implemented");
             Debug.Assert(false);
        }
        public virtual void VisitBombRoot(BombRoot b)
        {
            Debug.WriteLine("Visit by BombRoot not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShieldGrid(ShieldGrid s)
        {
            Debug.WriteLine("Visit by ShieldGrid not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShieldRoot(ShieldRoot s)
        {
            Debug.WriteLine("Visit by ShieldRoot not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShieldColumn(ShieldColumn s)
        {
            Debug.WriteLine("Visit by ShieldColumn not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShieldBrick(ShieldBrick s)
        {
            Debug.WriteLine("Visit by ShieldBrick not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUFO(UFO u)
        {
            Debug.WriteLine("Visit by UFO not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUFORoot(UFORoot u)
        {
            Debug.WriteLine("Visit by UFORoot not implemented");
            Debug.Assert(false);
        }

        abstract public void Accept(ColVisitor other);
    }

}

// --- End of File ---
