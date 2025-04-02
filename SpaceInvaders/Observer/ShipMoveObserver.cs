//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipMoveObserver : ColObserver
    {
        public override void Notify()
        {
            Ship pShip = ShipMan.GetShip();

            BumperCategory pBumper = (BumperCategory)this.pSubject.pObjA;
            if(pBumper.GetCategoryType() == BumperCategory.Type.Left)
            {
                pShip.SetState(ShipMan.MoveState.MoveRight);
            }
            else if(pBumper.GetCategoryType() == BumperCategory.Type.Right)
            {
                pShip.SetState(ShipMan.MoveState.MoveLeft);
            }
        }
        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShipMoveObserver;
        }
    }

    // data
}

// --- End of File ---
