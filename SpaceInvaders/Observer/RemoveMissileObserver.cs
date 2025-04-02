//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveMissileObserver : ColObserver
    {
        public RemoveMissileObserver(bool _isExplosed = true)
        {
            this.pMissile = null;
            this.isExplosed= _isExplosed;
        }
        public RemoveMissileObserver(RemoveMissileObserver m)
        {
            this.pMissile = m.pMissile;
            this.isExplosed = m.isExplosed;
        }
        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveMissileObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pMissile = (Missile)this.pSubject.pObjA;
            Debug.Assert(this.pMissile != null);

            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;
                //   Delay
                RemoveMissileObserver pObserver = new RemoveMissileObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            if (this.isExplosed)
            {
                TimerEventMan.Add(TimerEvent.Name.ExplosionSpawn,
                    new ExplosionSpawnEvent(ExplosionCategory.Type.ShipShot, this.pMissile.x, this.pMissile.y), 0.2f, 1);
            }

            // Let the gameObject deal with this... 
            this.pMissile.Remove();
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveMissileObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pMissile;
        private bool isExplosed;
    }
}

// --- End of File ---
