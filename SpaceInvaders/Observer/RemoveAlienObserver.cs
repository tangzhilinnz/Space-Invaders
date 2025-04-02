//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveAlienObserver : ColObserver
    {
        public RemoveAlienObserver()
        {
            this.pAlien = null;
        }
        public RemoveAlienObserver(RemoveAlienObserver b)
        {
            Debug.Assert(b != null);
            this.pAlien = b.pAlien;
        }

        public override void Notify()
        {
            // Delete alien
            this.pAlien = this.pSubject.pObjB;
            Debug.Assert(this.pAlien != null);

            if (pAlien.bMarkForDeath == false)
            {
                pAlien.bMarkForDeath = true;
                //   Delay
                RemoveAlienObserver pObserver = new RemoveAlienObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
            else
            {
                pAlien.bMarkForDeath = true;
            }
        }
        public override void Execute()
        {
            //  if this alien is the last child in the column, then remove column
            // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pAlien;
            GameObject pB = (GameObject)IteratorForwardComposite.GetParent(pA);
            GameObject pC = (GameObject)IteratorForwardComposite.GetParent(pB);

            TimerEventMan.Add(TimerEvent.Name.ExplosionSpawn,
                new ExplosionSpawnEvent(ExplosionCategory.Type.Alien, pA.x, pA.y), 0.2f, 1);

            // Alien
            if (pA.GetNumChildren() == 0)
            {
                pA.Remove();
            }

            // Column 
            if (pB.GetNumChildren() == 0)
            {
                pB.Remove();
            }

            // Grid
            if (pC.GetNumChildren() == 0)
            {
                pC.poColObj.poColRect.Set(0, 0, 0, 0);
                TimerEventMan.Add(TimerEvent.Name.NextLevel, new NextLevelCmd(), 0.25f, 1);
            }
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveAlienObserver;
        }


        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pAlien;
    }
}

// --- End of File ---
