using System;
using System.Diagnostics;

namespace SE456
{
    class RemoveUFOByMissileObserver : ColObserver
    {
        public RemoveUFOByMissileObserver()
        {
            this.pUFO = null;
        }
        public RemoveUFOByMissileObserver(RemoveUFOByMissileObserver m)
        {
            this.pUFO = m.pUFO;
        }
        public override void Notify()
        {
            this.pUFO = (UFO)this.pSubject.pObjB;
            Debug.Assert(this.pUFO != null);

            if (this.pUFO.bMarkForDeath == false)
            {
                this.pUFO.bMarkForDeath = true;
                //   Delay
                RemoveUFOByMissileObserver pObserver = new RemoveUFOByMissileObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            TimerEventMan.Add(TimerEvent.Name.ExplosionSpawn, new ExplosionSpawnEvent(ExplosionCategory.Type.UFO, this.pUFO.x, this.pUFO.y), 0.4f, 1);
            TimerEventMan.Add(TimerEvent.Name.UFOScoreSpawn, new UFOScoreSpawnEvent(this.pUFO.x, this.pUFO.y, 1.2f, ((UFO)this.pUFO).score), 0.44f, 1);

            this.pUFO.Remove();

            TimerEvent pUFOMoveSnd = TimerEventMan.Find(TimerEvent.Name.UFOMoveSnd, 1);
            Debug.Assert(pUFOMoveSnd != null);
            TimerEventMan.Remove(pUFOMoveSnd, 1);
        }

        override public void Dump()
        {

        }
        override public System.Enum GetName()
        {
            return ColObserver.Name.RemoveUFOByWallObserver;
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pUFO;
    }
}

// --- End of File ---
