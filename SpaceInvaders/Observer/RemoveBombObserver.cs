//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveBombObserver : ColObserver
    {
        public RemoveBombObserver(bool _rightObj = false)
        {
            this.pBomb = null;
            this.rightObj = _rightObj;
        }
        public RemoveBombObserver(RemoveBombObserver b)
        {
            this.pBomb = b.pBomb;
        }
        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBombObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            if (this.rightObj)
            {
                this.pBomb = (Bomb)this.pSubject.pObjB;
            }
            else
            {
                this.pBomb = (Bomb)this.pSubject.pObjA;
            }

            Debug.Assert(this.pBomb != null);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;
                //   Delay
                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            TimerEventMan.Add(TimerEvent.Name.ExplosionSpawn,
                    new ExplosionSpawnEvent(ExplosionCategory.Type.AlienShot, this.pBomb.x, this.pBomb.y), 0.2f, 1);

            // Let the gameObject deal with this... 
            this.pBomb.Remove();
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveBombObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pBomb;
        private bool rightObj;
    }
}

// --- End of File ---
