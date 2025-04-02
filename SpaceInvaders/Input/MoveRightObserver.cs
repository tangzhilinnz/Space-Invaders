//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class MoveRightObserver : InputObserver
    {
        public override void Notify()
        {
            //Debug.WriteLine("Move Right");
			Ship pShip = ShipMan.GetShip();
            pShip.MoveRight();
        }
        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public Enum GetName()
        {
            return Name.MoveRightObserver;
        }
    }
}

// --- End of File ---
