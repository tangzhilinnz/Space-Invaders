using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveBombByShipObserver : ColObserver
    {
        public RemoveBombByShipObserver()
        {
            this.pBomb = null;
        }
        public RemoveBombByShipObserver(RemoveBombByShipObserver b)
        {
            this.pBomb = b.pBomb;
        }
        public override void Notify()
        {

            this.pBomb = (Bomb)this.pSubject.pObjA;

            Debug.Assert(this.pBomb != null);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;
                //   Delay
                RemoveBombByShipObserver pObserver = new RemoveBombByShipObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pBomb.Remove();
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveBombByShipObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pBomb;
    }
}

// --- End of File ---
