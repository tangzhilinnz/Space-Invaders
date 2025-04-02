//-----------------------------------------------------------------------------
// Copyright 2025, Ed Keenan, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveBrickObserver : ColObserver
    {
        public RemoveBrickObserver()
        {
            this.pBrick = null;
        }
        public RemoveBrickObserver(RemoveBrickObserver b)
        {
            Debug.Assert(b != null);
            this.pBrick = b.pBrick;
        }

        public override void Notify()
        {
            // Delete missile
            this.pBrick = (ShieldBrick)this.pSubject.pObjB;
            Debug.Assert(this.pBrick != null);

            if (pBrick.bMarkForDeath == false)
            {
                pBrick.bMarkForDeath = true;
                //   Delay
                RemoveBrickObserver pObserver = new RemoveBrickObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
            else
            {
                pBrick.bMarkForDeath = true;
            }
        }
        public override void Execute()
        {
            //  if this brick removed the last child in the column, then remove column
           // Debug.WriteLine(" brick {0}  parent {1}", this.pBrick, this.pBrick.pParent);
            GameObject pA = (GameObject)this.pBrick;
            GameObject pB = (GameObject)IteratorForwardComposite.GetParent(pA);
            GameObject pC = (GameObject)IteratorForwardComposite.GetParent(pB);

            // Root - do not delete
            GameObject pD = (GameObject)IteratorForwardComposite.GetParent(pC);

            // Brick
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
                  pC.Remove();
            }

            if (pD.GetNumChildren() == 0)
            {
                pD.poColObj.poColRect.Set(0, 0, 0, 0);
                //ColPairMan.Remove(ColPairMan.Find(ColPair.Name.Misslie_Shield));
            }
        }
        private bool privCheckParent(GameObject pObj)
        {
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(pObj);
            if (pGameObj == null)
            {
                return true;
            }

            return false;
        }
        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveBrickObserver;
        }



        // -------------------------------------------
        // data:
        // -------------------------------------------

        private GameObject pBrick;
    }
}

// --- End of File ---
