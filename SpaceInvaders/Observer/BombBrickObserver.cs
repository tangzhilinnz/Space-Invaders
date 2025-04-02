using System;
using System.Diagnostics;

namespace SE456
{
    class BombBrickObserver : ColObserver
    {
        public BombBrickObserver()
        {
            this.pBomb = null;
        }
        public BombBrickObserver(BombBrickObserver b)
        {
            this.pBomb = b.pBomb;
        }
        public override void Notify()
        {

            this.pBomb = (Bomb)this.pSubject.pObjA;
            Debug.Assert(this.pBomb != null);
            ShieldBrick pBrick = (ShieldBrick)this.pSubject.pObjB;
            Debug.Assert(pBrick != null);

            if ((pBrick.pNext == null && pBrick.pPrev == null) || ((Bomb)pBomb).penetration == 0)
            {
                if (pBomb.bMarkForDeath == false)
                {
                    pBomb.bMarkForDeath = true;
                    BombBrickObserver pObserver = new BombBrickObserver(this);
                    DelayedObjectMan.Attach(pObserver);
                }
            }
            else
            {
                ((Bomb)pBomb).penetration--;
            }
        }
        public override void Execute()
        {
            TimerEventMan.Add(TimerEvent.Name.ExplosionSpawn,
                    new ExplosionSpawnEvent(ExplosionCategory.Type.AlienShot, this.pBomb.x, this.pBomb.y), 0.2f, 1);
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
    }
}
