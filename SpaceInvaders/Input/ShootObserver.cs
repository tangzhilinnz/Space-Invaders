//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShootObserver : InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Shoot Observer");
			Ship pShip = ShipMan.GetShip();
            pShip.ShootMissile();
        }
        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShootObserver;
        }
    }
}

// --- End of File ---
