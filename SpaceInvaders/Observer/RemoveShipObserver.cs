using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveShipObserver : ColObserver
    {
        public RemoveShipObserver()
        {
            this.pShip = null;
        }
        public RemoveShipObserver(RemoveShipObserver b)
        {
            this.pShip = b.pShip;
        }
        public override void Notify()
        {
            this.pShip = (Ship)this.pSubject.pObjB;
            Debug.Assert(this.pShip != null);

            GameObject pAlienGrid = GameObjectNodeMan.Find(GameObject.Name.AlienGrid);
            Debug.Assert(pAlienGrid != null);

            if (pShip.bMarkForDeath == false && pAlienGrid.GetNumChildren() != 0)
            {
                pShip.bMarkForDeath = true;
                //   Delay
                RemoveShipObserver pObserver = new RemoveShipObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            TimerEventMan.Add(TimerEvent.Name.ExplosionSpawn,
                    new ExplosionSpawnEvent(ExplosionCategory.Type.Ship, this.pShip.x, this.pShip.y, 4), 0.2f, 1);

            // Let the gameObject deal with this... 
            this.pShip.Remove();
            InputMan.LockInput();
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveShipObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pShip;
    }
}

// --- End of File ---
