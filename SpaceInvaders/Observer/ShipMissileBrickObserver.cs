using System;
using System.Diagnostics;

namespace SE456
{
    class ShipMissileBrickObserver : ColObserver
    {
        public ShipMissileBrickObserver()
        {
            this.pMissile = null;
        }
        public ShipMissileBrickObserver(ShipMissileBrickObserver b)
        {
            this.pMissile = b.pMissile;
        }
        public override void Notify()
        {
            this.pMissile = (Missile)this.pSubject.pObjA;
            Debug.Assert(this.pMissile != null);
            ShieldBrick pBrick = (ShieldBrick)this.pSubject.pObjB;
            Debug.Assert(pBrick != null);

            if ((pBrick.pNext == null && pBrick.pPrev == null) || ((Missile)pMissile).penetration == 0)
            {
                if (pMissile.bMarkForDeath == false)
                {
                    pMissile.bMarkForDeath = true;
                    ShipMissileBrickObserver pObserver = new ShipMissileBrickObserver(this);
                    DelayedObjectMan.Attach(pObserver);
                }
            }
            else
            {
                ((Missile)pMissile).penetration--;
            }
        }
        public override void Execute()
        {
            float offset = 0.0f;

            if (((Missile)pMissile).penetration == 1 || ((Missile)pMissile).penetration == 2)
            {
                offset = 5.0f;
            }
            if (((Missile)pMissile).penetration == 0)
            {
                offset = 10.0f;
            }

            TimerEventMan.Add(TimerEvent.Name.ExplosionSpawn,
                new ExplosionSpawnEvent(ExplosionCategory.Type.ShipShot, this.pMissile.x, this.pMissile.y - offset), 0.2f, 1);
            this.pMissile.Remove();

            Ship pShip = ShipMan.GetShip();
            pShip.SetState(ShipMan.MissileState.Ready);
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.ShipMissileBrickObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pMissile;
    }
}
