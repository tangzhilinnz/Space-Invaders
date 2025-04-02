//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class ColObserver : SLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            SoundObserver,
            GridObserver,
            ShipMoveObserver,
			BombObserver,
            RemoveBrickObserver,
            ShipReadyObserver,
            RemoveMissileObserver,
            RemoveAlienObserver,
            RemoveBombObserver,
            RemoveUFOByWallObserver,
            RemoveUFOByMissileObserver,
            ShipMissileBrickObserver,
            RemoveBombByShipObserver,
            FreezeSceneObserver,
            RemoveShipObserver,
            AddScoreObserver,
            Uninitialized
        }
        public abstract void Notify();
		
        public virtual void Execute()
        {
            // default implementation
        }

        override public void Wash()
        {
            Debug.Assert(false);
        }

        public ColSubject pSubject;
    }
}

// --- End of File ---
